using AffaliteBL.DTOs.WithdrawalDto;
using AffaliteBL.IServices;
using AffaliteBL.Services;
using AffaliteDAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WithdrawalController : ControllerBase
    {
        private readonly IWithdrawalService _service;

        public WithdrawalController(IWithdrawalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data =  _service.GetAll();
            return Ok(data);
        }
        //  Get by AffiliateId
        [HttpGet("affiliate/{affiliateId}")]
        public async Task<IActionResult> GetByAffiliateId(int affiliateId)
        {
            //var affiliateId = User.FindFirst("uid")?.Value;
            var result = await _service.GetByAffiliateId(affiliateId);
            return Ok(result);
        }

        //  Get by MerchantId
        [HttpGet("merchant/{merchantId}")]
        public async Task<IActionResult> GetByMerchantId(int merchantId)
        {
            //var merchantId = User.FindFirst("uid")?.Value;

            var result = await _service.GetByMerchantId(merchantId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateWithdrawalDto dto)
        {
            // ال userId ييجي من التوكن مش من الفرونت
            var userId = User.FindFirst("uid")?.Value;

            var result =  _service.Add(userId, dto);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateWithdrawalDto dto)
        {
            var result =  _service.Update(dto);
            return Ok(result);
        }
        //  Get by AffiliateId
        [HttpGet("affiliate/")]
        public async Task<IActionResult> GetByAffiliateId()
        {
            var affiliateId = User.FindFirst("uid")?.Value;
            var result = await _service.GetByAffiliateId(affiliateId);
            return Ok(result);
        }

        //  Get by MerchantId
        [HttpGet("merchant")]
        public async Task<IActionResult> GetByMerchantId()
        {
            var merchantId = User.FindFirst("uid")?.Value;

            var result = await _service.GetByMerchantId(merchantId);
            return Ok(result);
        }
    }
}
