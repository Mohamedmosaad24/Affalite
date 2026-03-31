using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionsController : ControllerBase
    {

        private readonly IGenericRepository<Commission> _commRepo;
        public CommissionsController(IGenericRepository<Commission> commRepo) => _commRepo = commRepo;

        [HttpGet("order/{orderId}")]
        public IActionResult GetByOrder(int orderId) => Ok(_commRepo.GetAll().FirstOrDefault(c => c.OrderId == orderId));
    
}
}
