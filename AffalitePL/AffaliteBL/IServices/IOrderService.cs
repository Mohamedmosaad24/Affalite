using System;
using AffaliteBL.DTOs.AffiliateDTOs;
using AffaliteBL.DTOs.CommissionDTOs;
using AffaliteBL.DTOs.OrderDTOs;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices
{

    public interface IOrderService
    {
        OrderReadDTO CreateOrder(OrderCreateDTO orderDto);
        OrderReadDTO GetOrderById(int id);
    }
}
