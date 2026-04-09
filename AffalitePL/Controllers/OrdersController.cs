using AffaliteBL.DTOs.OrderDTOs;
using AffaliteBL.IServices;
using AffaliteBL.Services;
using AffaliteDAL.Entities;
using AffaliteDAL.Entities.Enums;
using AffaliteDAL.IRepo;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAffiliateService _affiliateService;
        private readonly IMerchantService _merchantService;
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IMapper _mapper;

   
        public OrdersController(IOrderService orderService, IGenericRepository<Order> orderRepo, IMapper mapper, IAffiliateService affiliateService, IMerchantService merchantService)
        {
            _orderService = orderService;
            _orderRepo = orderRepo;
            _mapper = mapper;
            _affiliateService = affiliateService;
            _merchantService = merchantService;
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
            var order = _orderRepo.GetById(id);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderReadDTO>(order));
        }

     
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderRepo.GetAll();
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
            var orders = _orderRepo.GetAll().Where(o => o.AffiliateId == affiliateId);
            return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orders));
        }


        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] OrderStatus status)
        {

            var order = _orderRepo.GetById(id);
            if (order == null) return NotFound();
            Affiliate affilaite = _affiliateService.GetAffiliateById((int)order.AffiliateId);
            //Merchant merchant = _merchantService.GetMerchantById((int)order.MerchantId);
            if(status == OrderStatus.Paid)
            {
                affilaite.Balance += order.Commission.AffiliateAmount;
                //merchant.Balance += order.Commission.MerchantAmount ;
            }
            order.Status = status;
            _orderRepo.Update(order);
            _orderRepo.SaveChanges();
            return NoContent();
        }
    }
}
