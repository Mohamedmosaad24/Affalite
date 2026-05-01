using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AffaliteBL.DTOs.MerchantDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AutoMapper;

namespace AffalitePL.Controllers
{
    [Route("api/merchants")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService _merchantService;
        private readonly IMapper _mapper;

        public MerchantController(IMerchantService merchantService, IMapper mapper)
        {
            _merchantService = merchantService;
            _mapper = mapper;
        }

        // GET: /api/merchants
        [HttpGet]
        public IActionResult GetAllMerchants()
        {
            var merchants = _merchantService.GetAllMerchants();
            var result = _mapper.Map<IEnumerable<GetMerchantDTO>>(merchants);
            return Ok(result);
        }

        // GET: /api/merchants/{id}
        [HttpGet("{id}")]
        public IActionResult GetMerchantById(int id)
        {
            var merchant = _merchantService.GetMerchantById(id);

            if (merchant == null)
                return NotFound($"Merchant with id: {id} not found");

            var result = _mapper.Map<GetMerchantDTO>(merchant);
            return Ok(result);
        }

        // POST: /api/merchants
        [HttpPost]
        public IActionResult CreateMerchant(CreateMerchantDTO createMerchantDTO)
        {
            var merchant = _mapper.Map<Merchant>(createMerchantDTO);
            _merchantService.CreateMerchant(merchant);
            return Ok("Create merchant");
        }

        // PUT: /api/merchants/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateMerchant(int id, UpdateMerchantDTO updateMerchantDTO)
        {
            var merchant = _mapper.Map<Merchant>(updateMerchantDTO);
            merchant.Id = id;

            _merchantService.UpdateMerchant(merchant);
            return Ok(merchant);
        }

        // DELETE: /api/merchants/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteMerchant(int id)
        {
            var merchant = _merchantService.GetMerchantById(id);

            if (merchant == null)
                return NotFound($"Merchant with id: {id} not found");

            _merchantService.DeleteMerchant(id);
            return Ok($"Delete merchant with id: {id}");
        }

        // GET: /api/merchants/{id}/orders
        [HttpGet("{id}/orders")]
        public IActionResult GetMerchantOrders(int id)
        {
            var result = _merchantService.GetMerchantOrders(id);
            return Ok(result);
        }

        // GET: /api/merchants/{id}/balance
        //[HttpGet("balance")]
        //public IActionResult GetMerchantBalance()
        //{
        //    var merchantId = User.FindFirst("uid")?.Value;
        //    var merchant = _merchantService.GetMerchantByUserId(merchantId);
        //    var result = _merchantService.GetMerchantBalance(merchant.Id);

        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}
        [HttpGet("balance")]
        public IActionResult GetMerchantBalance()
        {
            var userId = User.FindFirst("uid")?.Value
                      ?? User.FindFirst("sub")?.Value
                      ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return Unauthorized();

            var merchant = _merchantService.GetMerchantByUserId(userId);
            if (merchant == null) return NotFound("Merchant not found");

            var result = _merchantService.GetMerchantBalance(merchant.Id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet("${userId}/merchant")]
        public  IActionResult GetMerchantByUserId(string userId)
        {
            var res= _merchantService.GetMerchantByUserId(userId);
           var merchant= _mapper.Map<GetMerchantDTO>(res);
            return Ok(merchant);
        }
        [HttpGet("merchantDetials")]
        public IActionResult GetAllMerchantsDetails()
        {
            var result = _merchantService.GetAllMerchantsWithDetails();
            return Ok(result);
        }
    }
}