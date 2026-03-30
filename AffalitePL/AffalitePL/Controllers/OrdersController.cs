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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService) => _orderService = orderService;

        [HttpPost]
        public IActionResult Create(OrderCreateDTO dto) => Ok(_orderService.CreateOrder(dto));

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_orderService.GetOrderById(id));
    }

}
