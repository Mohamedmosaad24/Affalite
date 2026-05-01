using AffaliteBL.DTOs.CommissionDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.Entities.Enums;
using AffaliteDAL.IRepo;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionsController : ControllerBase
    {
        private readonly ICommissionService _commissionService;
        private readonly IGenericRepository<Commission> _commissionRepo;
        private readonly IMapper _mapper;


        public CommissionsController(
            ICommissionService commissionService,
            IGenericRepository<Commission> commissionRepo,
            IMapper mapper)
        {
            _commissionService = commissionService;
            _commissionRepo = commissionRepo;
            _mapper = mapper;
        }

   
        [HttpGet]
        public IActionResult GetAll()
        {
            var commissions = _commissionRepo.GetAll();
            var result = _mapper.Map<IEnumerable<CommissionReadDTO>>(commissions);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var commission = _commissionRepo.GetById(id);
            if (commission == null) return NotFound();

            return Ok(_mapper.Map<CommissionReadDTO>(commission));
        }


        [HttpGet("order/{orderId}")]
        public IActionResult GetByOrder(int orderId)
        {
            var result = _commissionService.GetCommissionByOrderId(orderId);
            if (result == null) return NotFound();

            return Ok(result);
        }


        [HttpGet("affiliate/{affiliateId}")]
        public IActionResult GetByAffiliate(int affiliateId)
        {
            var result = _commissionService.GetCommissionsByAffiliate(affiliateId);
            return Ok(result);
        }

        [HttpGet("merchant/{merchantid}")]
        public IActionResult GetByMerchant(int merchantid)
        {
            //var merchantId = User.FindFirst("uid")?.Value;
            //var merchant = merchantService.GetMerchantByUserId(merchantId);
            var result = _commissionService.GetCommissionsByMerchant(merchantid);
            return Ok(result);
        }


        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] CommissionStatus status)
        {
            _commissionService.UpdateCommissionStatus(id, status);
            return NoContent();
        }

        [HttpGet("merchant")]
        public IActionResult GetByMerchant()
        {
            var merchantId = User.FindFirst("uid")?.Value;
            var merchant = merchantService.GetMerchantByUserId(merchantId);
            var result = _commissionService.GetCommissionsByMerchant(merchant.Id);
            return Ok(result);
        }
    }
}