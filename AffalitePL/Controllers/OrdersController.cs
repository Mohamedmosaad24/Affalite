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
        private readonly IOrderRepo IOrderRepo1;
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IMapper _mapper;

   
        public OrdersController(IOrderService orderService, IGenericRepository<Order> orderRepo, IMapper mapper, IAffiliateService affiliateService, IMerchantService merchantService,IOrderRepo orderRepo1)
        {
            _orderService = orderService;
            _orderRepo = orderRepo;
            _mapper = mapper;
            _affiliateService = affiliateService;
            _merchantService = merchantService;
            IOrderRepo1 = orderRepo1;
        }


        [HttpPost]
        public IActionResult Create(OrderCreateDTO dto)
        {
            var result = _orderService.CreateOrder(dto);
            return Ok(result);
        }



        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderRepo.GetAllQueryable()
             .AsNoTracking()
             .Include(o => o.Items)
             .ThenInclude(i => i.Product)
             .ThenInclude(i=>i.Images)
             .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderReadDTO>(order));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderRepo.GetAllQueryable()
            .AsNoTracking()
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ThenInclude(i=>i.Images)
            .ToList();
            return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orders));
        }


        //[HttpGet("merchant/{merchantId}")]
        //public IActionResult GetByMerchant(int merchantId)
        //{
        //    var orders = _orderRepo.GetAll().Where(o => o.MerchantId == merchantId);
        //    return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orders));
        //}

        [HttpGet("affiliate/{affiliateId}")]
        public IActionResult GetByAffiliate(int affiliateId)
        {
            var orders = _orderService.getOrdersByAff(affiliateId);
            return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orders));
        }


        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] OrderStatus status)
        {

            var order = IOrderRepo1.GetById(id);
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

            order.Status = status;
            _orderRepo.Update(order);
            _orderRepo.SaveChanges();
            return Ok(order);
        }
    }
}
