using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteBL.DTOs.NotificationDTOs;
using AffaliteBL.DTOs.OrderDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.Entities.Enums;
using AffaliteDAL.IRepo;
using AffaliteDAL.Repo;
using AutoMapper;

namespace AffaliteBL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IOrderRepo _orderRepoo;
        private readonly ICartRepo _cartRepo;
        private readonly IGenericRepository<Commission> _commissionRepo;
        private readonly IGenericRepository<MerchantCommissions> _merchantCommissions;
        private readonly IGenericRepository<MerchantOrder> _merchantOrder;
        private readonly IGenericRepository<Affiliate> _affiliateRepo;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IMerchantRepo _merchantRepo;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<Commission> commissionRepo, IMapper mapper, ICartRepo cartRepo
            , IGenericRepository<MerchantCommissions> merchantCommissions, IGenericRepository<MerchantOrder> merchantOrder, IOrderRepo orderRepoo)
        public OrderService(
            IGenericRepository<Order> orderRepo,
            IGenericRepository<Commission> commissionRepo,
            IMapper mapper,
            ICartRepo cartRepo,
            IGenericRepository<MerchantCommissions> merchantCommissions,
            IGenericRepository<MerchantOrder> merchantOrder,
            IGenericRepository<Affiliate> affiliateRepo,
            INotificationService notificationService,
            IEmailService emailService)
        {
            _orderRepo = orderRepo;
            _commissionRepo = commissionRepo;
            _mapper = mapper;
            _cartRepo = cartRepo;
            _merchantCommissions = merchantCommissions;
            _merchantOrder = merchantOrder;
            _orderRepoo = orderRepoo;
            _affiliateRepo = affiliateRepo;
            _notificationService = notificationService;
            _emailService = emailService;
        }

        public async Task<OrderReadDTO> CreateOrder(OrderCreateDTO orderDto)
        {
            var neworder = _mapper.Map<Order>(orderDto);
            decimal platformAmount = 0;

            var cart = _cartRepo.GetCartWithAffilaiteId((int)orderDto.AffiliateId);

            if (cart == null || cart.Items.Count == 0)
                throw new Exception("Cart is empty");

            decimal totalPrice = 0;

            // ✅ Loop واحد بس للحساب
            foreach (var item in cart.Items)
            {
                var itemTotal = item.Product.Price * item.Quantity;
                platformAmount += (item.Product.PlatformCommissionPct / 100) * itemTotal;
                totalPrice += itemTotal;
            }

            // ✅ إنشاء الـ Order
            Order order = new Order
            {
                AffiliateId = cart.AffiliateId,
                CustomerName = neworder.CustomerName,
                CustomerPhone = neworder.CustomerPhone,
                CustomerAddress = neworder.CustomerAddress,
                AffiliateCommissionPct = neworder.AffiliateCommissionPct,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Pending,
                TotalPrice = totalPrice + neworder.AffiliateCommissionPct + 10 ,
                Items = new List<OrderItem>()
            };
            _orderRepo.Add(order);
            _orderRepo.SaveChanges();

            // ✅ إضافة الـ OrderItems وتحديث الـ Stock
            foreach (var item in cart.Items)
            {
                item.Product.SaleCount += item.Quantity; // ← مرة واحدة بس
                item.Product.Stock -= item.Quantity;      // ← كان بيطرح 1 بس، صح يطرح الـ quantity

                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }
            _orderRepo.SaveChanges();

            // ✅ إنشاء الـ Commission
            
            var commission = new Commission
            {
                OrderId = order.Id,
                AffiliateAmount =cart.AffilaiteCommission,
                PlatformAmount = platformAmount,
                MerchantAmount = cart.Total - (platformAmount + cart.AffilaiteCommission +10),
                Status = CommissionStatus.Pending
            };
            _commissionRepo.Add(commission);
            _commissionRepo.SaveChanges();

            // ✅ MerchantCommissions
            foreach (var item in cart.Items)
            {
                var itemTotal = item.Product.Price * item.Quantity;
                _merchantCommissions.Add(new MerchantCommissions
                {
                    CommissionId = commission.Id,
                    MerchantId = item.Product.MerchantId,
                    value = itemTotal - ((item.Product.PlatformCommissionPct / 100) * itemTotal)
                });
            }
            _merchantCommissions.SaveChanges();

            // ✅ MerchantOrder - Distinct عشان منكررش
            var merchantIds = cart.Items
                .Select(i => i.Product.MerchantId)
                .Distinct();

            foreach (var merchantId in merchantIds)
            {
                _merchantOrder.Add(new MerchantOrder
                {
                    MerchantId = merchantId,
                    OrderId = order.Id
                });
            }
            _merchantOrder.SaveChanges();

            // ✅ امسح الـ Cart
            _cartRepo.Delete(cart);
            _cartRepo.SaveChanges();

            var affiliate = _affiliateRepo.GetById((int)orderDto.AffiliateId);

            _notificationService.CreateNotification(new CreateNotificationDTO
            {
                UserId = affiliate!.AppUserId,
                Title = "Order Placed Successfully!",
                Message = $"Your order #{order.Id} for {neworder.CustomerName} totaling ${order.TotalPrice:F2} has been placed.",
                Type = NotificationType.Order,
                RelatedEntityId = order.Id.ToString()
            });

            await _emailService.SendOrderConfirmationEmailAsync(
                affiliate.AppUser?.Email ?? "",
                neworder.CustomerName,
                order.Id,
                order.TotalPrice);

            foreach (var merchantId in merchantIds)
            {
                var merchant = _merchantRepo.GetById(merchantId);
                if (merchant != null)
                {
                    _notificationService.CreateNotification(new AffaliteBL.DTOs.NotificationDTOs.CreateNotificationDTO
                    {
                        UserId = merchant.AppUserId,
                        Title = "New Order Received!",
                        Message = $"You have a new order #{order.Id} totaling ${order.TotalPrice:F2}.",
                        Type = NotificationType.Merchant,
                        RelatedEntityId = order.Id.ToString()
                    });
                }
            }

            return _mapper.Map<OrderReadDTO>(order);
        }


        public OrderReadDTO GetOrderById(int id)
        {
            var order = _orderRepo.GetById(id);
            return _mapper.Map<OrderReadDTO>(order);

        }
        public List<Order> getOrdersByAff(int affId)
        {
            return _orderRepoo.GetByAffId(affId);
        }
    }
}
