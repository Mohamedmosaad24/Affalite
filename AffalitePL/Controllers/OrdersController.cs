using AffaliteBL.DTOs.OrderDTOs;
using AffaliteBL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
