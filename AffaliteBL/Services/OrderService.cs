using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly ICartRepo _cartRepo;
        private readonly IGenericRepository<Commission> _commissionRepo;
        private readonly IGenericRepository<MerchantCommissions> _merchantCommissions; 
        private readonly IGenericRepository<MerchantOrder> _merchantOrder; 
        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<Commission> commissionRepo, IMapper mapper, ICartRepo cartRepo
            , IGenericRepository<MerchantCommissions> merchantCommissions, IGenericRepository<MerchantOrder> merchantOrder)
        {
            _orderRepo = orderRepo;
            _commissionRepo = commissionRepo;
            _mapper = mapper;
            _cartRepo = cartRepo;
            _merchantCommissions = merchantCommissions;
            _merchantOrder = merchantOrder;
        }

        public OrderReadDTO CreateOrder(OrderCreateDTO orderDto)
        {
            var neworder = _mapper.Map<Order>(orderDto);
            decimal platformAmount = 0;

            var cart = _cartRepo
               .GetCartWithAffilaiteId((int)orderDto.AffiliateId);

            if (cart == null || cart.Items.Count == 0)
                throw new Exception("Cart is empty");
            decimal totalPrice = neworder.AffiliateCommissionPct;

            foreach (var item in cart.Items)
            {


                item.Product.SaleCount += item.Quantity;

                var itemTotal = item.Product.Price * item.Quantity;

                platformAmount += (item.Product.PlatformCommissionPct / 100) * itemTotal;
                totalPrice += itemTotal;



            }

            Order order = new Order
            {
                AffiliateId = cart.AffiliateId,
                CustomerName = neworder.CustomerName,
                CustomerPhone = neworder.CustomerPhone,
                CustomerAddress = neworder.CustomerAddress,
                AffiliateCommissionPct = neworder.AffiliateCommissionPct,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Pending,
                TotalPrice = totalPrice,
                Items = new List<OrderItem>(),

            };
            _orderRepo.Add(order);
            _orderRepo.SaveChanges();
            foreach (var item in cart.Items)
            {
                item.Product.SaleCount += item.Quantity;
                item.Product.Stock -=1;
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                order.Items.Add(orderItem);
            }

            _orderRepo.SaveChanges();
            var commission = new Commission
            {
                OrderId = order.Id,
                AffiliateAmount = order.AffiliateCommissionPct,
                PlatformAmount = platformAmount,
                Status = CommissionStatus.Pending,
                MerchantAmount = totalPrice - (platformAmount + order.AffiliateCommissionPct)
            };
            _commissionRepo.Add(commission);
            _commissionRepo.SaveChanges();
            commission.MerchantAmount = order.TotalPrice - (commission.AffiliateAmount + commission.PlatformAmount);

            foreach (var item in cart.Items)
            {



                var itemTotal = item.Product.Price * item.Quantity;

                platformAmount += (item.Product.PlatformCommissionPct / 100) * itemTotal;

                MerchantCommissions merchantCommissions = new MerchantCommissions
                {
                    CommissionId = commission.Id,
                    MerchantId = item.Product.MerchantId,
                    value = itemTotal - (item.Product.PlatformCommissionPct / 100) * itemTotal
                };
                _merchantCommissions.Add(merchantCommissions);
                _merchantCommissions.SaveChanges();
                MerchantOrder merchantOrder = new MerchantOrder
                {
                    MerchantId = item.Product.MerchantId,
                    OrderId = order.Id
                };
                _merchantOrder.Add(merchantOrder);
                _merchantOrder.SaveChanges();



            }

            _cartRepo.Delete(cart);
            _cartRepo.SaveChanges();



            return _mapper.Map<OrderReadDTO>(order);
        }


        public OrderReadDTO GetOrderById(int id)
        {
            var order = _orderRepo.GetById(id);
            return _mapper.Map<OrderReadDTO>(order);

        }
    }
}
