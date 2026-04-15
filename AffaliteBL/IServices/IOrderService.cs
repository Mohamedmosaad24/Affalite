using AffaliteBL.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.IServices
{
    public interface IOrderService
    {
        Task<OrderReadDTO> CreateOrder(OrderCreateDTO orderDto);
        OrderReadDTO GetOrderById(int id);
    }
}
