using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteBL.DTOs.OrderDTOs;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices
{
    public interface IOrderService
    {
        OrderReadDTO CreateOrder(OrderCreateDTO orderDto);
        OrderReadDTO GetOrderById(int id);
         List<Order> getOrdersByAff(int affId);



    }
}
