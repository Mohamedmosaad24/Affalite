using System;
using System;
using AffaliteBL.DTOs.AffiliateDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class CommissionsController : ControllerBase
    {
        private readonly IGenericRepository<Commission> _commRepo;
        public CommissionsController(IGenericRepository<Commission> commRepo) => _commRepo = commRepo;

        [HttpGet("order/{orderId}")]
        public IActionResult GetByOrder(int orderId) => Ok(_commRepo.GetAll().FirstOrDefault(c => c.OrderId == orderId));
    }


}
