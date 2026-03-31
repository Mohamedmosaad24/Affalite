using AffaliteBL.DTOs.OrderDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.Entities.Enums;
using AffaliteDAL.IRepo;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<Commission> _commissionRepo;
        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<Commission> commissionRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _commissionRepo = commissionRepo;
            _mapper = mapper;
        }

        public OrderReadDTO CreateOrder(OrderCreateDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _orderRepo.Add(order);
            _orderRepo.SaveChanges();

            var commission = new Commission
            {
                OrderId = order.Id,
                AffiliateAmount = order.TotalPrice * (order.AffiliateCommissionPct / 100),
                PlatformAmount = order.TotalPrice * 0.05m,
                Status = CommissionStatus.Pending
            };
            commission.MerchantAmount = order.TotalPrice - (commission.AffiliateAmount + commission.PlatformAmount);

            _commissionRepo.Add(commission);
            _commissionRepo.SaveChanges();

            return _mapper.Map<OrderReadDTO>(order);
        }

        public OrderReadDTO GetOrderById(int id)
        {
            var order = _orderRepo.GetById(id);
            return _mapper.Map<OrderReadDTO>(order);

        }
    }
}
