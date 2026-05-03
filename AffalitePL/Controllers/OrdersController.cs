using AffaliteBL.DTOs.NotificationDTOs;
using AffaliteBL.DTOs.OrderDTOs;
using AffaliteBL.IServices;
using AffaliteBL.Services;
using AffaliteDAL.Entities;
using AffaliteDAL.Entities.Enums;
using AffaliteDAL.IRepo;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AffalitePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAffiliateService _affiliateService;
        private readonly IMerchantService _merchantService;
        private readonly IOrderRepo _orderRepo1;
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<Affiliate> _affiliateRepo;
        private readonly IGenericRepository<Merchant> _merchantRepo;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;


        public OrdersController(
            IOrderService orderService,
            IGenericRepository<Order> orderRepo,
            IMapper mapper,
            IAffiliateService affiliateService,
            IMerchantService merchantService,
            IOrderRepo orderRepo1,
            IGenericRepository<Affiliate> affiliateRepo,
            IGenericRepository<Merchant> merchantRepo,
            INotificationService notificationService,
            IEmailService emailService)
        {
            _orderService = orderService;
            _orderRepo = orderRepo;
            _mapper = mapper;
            _affiliateService = affiliateService;
            _merchantService = merchantService;
            _orderRepo1 = orderRepo1;
            _affiliateRepo = affiliateRepo;
            _merchantRepo = merchantRepo;
            _notificationService = notificationService;
            _emailService = emailService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateDTO dto)
        {
            var result = await _orderService.CreateOrder(dto);
            return Ok(result);
        }



        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderRepo.GetAllQueryable()
                .AsNoTracking()

                .Include(o => o.Affiliate)
                    .ThenInclude(a => a.AppUser)

                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Images)

                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Reviews)

                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Merchant)
                            .ThenInclude(m => m.AppUser)

                .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            return Ok(_mapper.Map<OrderReadDTO>(order));
        }

        //     .ThenInclude(i => i.Product)
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderRepo.GetAllQueryable()
                .AsNoTracking()

                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Merchant)
                            .ThenInclude(m => m.AppUser)

                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Images)

                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Reviews)

                .Include(o => o.Affiliate)
                    .ThenInclude(a => a.AppUser)

                .ToList();

            return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orders));
        }

     

        [HttpGet("merchant/{merchantId}")]
        public IActionResult GetByMerchant(int merchantId)
        {
            var orders = _orderService.getOrdersByMer(merchantId);

            if (orders == null || !orders.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orders));
        }

     

        [HttpGet("affiliate/{affiliateId}")]
        public IActionResult GetByAffiliate(int affiliateId)
        {
            var orders = _orderRepo1.GetByAffId(affiliateId);

            if (orders == null || !orders.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orders));
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] OrderStatus status)
        {
            var order = _orderRepo1.GetById(id);
            if (order == null) return NotFound();


            Affiliate affilaite = _affiliateService.GetAffiliateById((int)order.AffiliateId);
            List<Merchant> MerchantIds = order.MerchantOrder.Select(m => m.Merchant).ToList();

            if (status == OrderStatus.Paid && order.Commission.Status!=CommissionStatus.Paid)
            {
                affilaite.Balance += order.Commission.AffiliateAmount;
                foreach (var merchant in MerchantIds)
                {
                    var merchantRe = order.Commission.MerchantCommissions.Where(m => m.MerchantId == merchant.Id ).ToList();
                    merchant.Balance += merchantRe.Sum(c=>c.value);
                }
                order.Commission.Status = CommissionStatus.Paid;
            }

            if(status == OrderStatus.Cancelled)
            {
                order.Commission.Status = CommissionStatus.Failed;
            }




            var affiliate = _affiliateService.GetAffiliateById((int)order.AffiliateId);

            var oldStatus = order.Status;

            order.Status = status;
            _orderRepo.Update(order);
            _orderRepo.SaveChanges();

            if (oldStatus != status)
            {
                _notificationService.CreateNotification(new AffaliteBL.DTOs.NotificationDTOs.CreateNotificationDTO
                {
                    UserId = affiliate.AppUserId,
                    Title = "Order Status Updated",
                    Message = $"Order #{order.Id} status changed to {status}.",
                    Type = NotificationType.Order,
                    RelatedEntityId = order.Id.ToString()
                });

                _emailService.SendOrderConfirmationEmailAsync(
                    affiliate.AppUser?.Email ?? "",
                    order.CustomerName,
                    order.Id,
                    order.TotalPrice);
            }

            return Ok(order);
        }
    }
}
