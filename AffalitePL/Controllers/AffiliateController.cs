using AffaliteBL.DTOs.AffiliateDTOs;
using AffaliteBL.IServices;
using AffaliteBL.Services;
using AffaliteDAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffiliateController : ControllerBase
    {
        private readonly IAffiliateService _affiliateService;
        private readonly IMapper _mapper;
        public AffiliateController(IAffiliateService affiliateService , IMapper mapper) {
            _affiliateService = affiliateService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllAffiliates()
        {
           var affiliate = _affiliateService.GetAllAffiliates();
            var result = _mapper.Map<IEnumerable<GetAffiliateDTO>>(affiliate);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetAffiliateById(int id)
        {
            var affiliate = _affiliateService.GetAffiliateById(id);
                if (affiliate == null)
                {
                    return NotFound($"Affiliate with id: {id} not found");
                }
                var result = _mapper.Map<GetAffiliateDTO>(affiliate);
            return Ok(result);
        }
        //[HttpPost]
        //public IActionResult CreateAffiliate(CreateAffiliateDTO createAffiliateDTO)
        //{
        //    var affiliate = _mapper.Map<Affiliate>(createAffiliateDTO);
        //    _affiliateService.CreateAffiliate(affiliate);
        //    return Ok("Create affiliate");
        //}
        [HttpPut("{id}")]
        public IActionResult UpdateAffiliate(int id, UpdateAffiliateDTO updateAffiliateDTO)
        {
            var affiliate = _mapper.Map<Affiliate>(updateAffiliateDTO);
            affiliate.Id = id;
            _affiliateService.UpdateAffiliate(affiliate);
            return Ok(affiliate);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAffiliate(int id)
        {
            var affiliate = _affiliateService.GetAffiliateById(id);
            if (affiliate == null)
            {
                return NotFound($"Affiliate with id: {id} not found");
            }
            _affiliateService.DeleteAffiliate(id);
            return Ok($"Delete affiliate with id: {id}");
        }
        [HttpGet("orders")]
        public IActionResult GetAffiliateOrders()
        {
            var userId = User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var aff = _affiliateService.GetAffiliateUserId(userId);
            var result = _affiliateService.GetAffiliateOrders(aff.Id);
            return Ok(result);
        }

        [HttpGet("{id}/commissions")]
        public IActionResult GetAffiliateCommissions(int id)
        {
            //var userId = User.FindFirst("uid")?.Value;
            //if (string.IsNullOrEmpty(userId)) return Unauthorized();

            //var aff = _affiliateService.GetAffiliateUserId(userId);

            var result = _affiliateService.GetAffiliateCommissions(id);
            return Ok(result);
        }
        [HttpGet("commissions")]
        public IActionResult GetAffiliateCommissions()
        {
            var userId = User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var aff = _affiliateService.GetAffiliateUserId(userId);

            var result = _affiliateService.GetAffiliateCommissions(aff.Id);
            return Ok(result);
        }
        [HttpGet("balance")]
        public IActionResult GetAffiliateBalance()
        {
            var userId = User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var aff = _affiliateService.GetAffiliateUserId(userId);
            var result = _affiliateService.GetAffiliateBalance(aff.Id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpGet("user/{userId}")]
        public IActionResult GetAffiliateByUserId(string userId)
        {
            var result = _affiliateService.GetAffiliateUserId(userId);

            if (result == null)
                return NotFound();

            var affiliate = _mapper.Map<GetAffiliateDTO>(result);
            return Ok(affiliate);  // ✅ رجع affiliate مش result
        }
    }
}
