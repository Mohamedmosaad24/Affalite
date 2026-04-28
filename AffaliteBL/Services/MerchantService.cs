using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteBL.DTOs.MerchantDTOs;
using AffaliteBL.DTOs.OrderDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;

namespace AffaliteBL.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepo _repo;

        public MerchantService(IMerchantRepo repo)
        {
            _repo = repo;
        }
      

        public IEnumerable<Merchant> GetAllMerchants()
        {
            return _repo.GetAllMerchants();
        }

        public Merchant? GetMerchantById(int id)
        {
            return _repo.GetById(id);
        }

        public void CreateMerchant(Merchant merchant)
        {
            if (merchant == null)
                throw new ArgumentNullException(nameof(merchant));

            _repo.Add(merchant);
            _repo.SaveChanges();
        }

        public void UpdateMerchant(Merchant merchant)
        {
            if (merchant == null)
                throw new ArgumentNullException(nameof(merchant));

            var existingMerchant = _repo.GetById(merchant.Id);

            if (existingMerchant != null)
            {
                //existingMerchant.Balance = merchant.Balance;

                _repo.Update(existingMerchant);
                _repo.SaveChanges();
            }
        }

        public void DeleteMerchant(int id)
        {
            var merchant = _repo.GetById(id);

            if (merchant != null)
            {
                _repo.Delete(merchant);
                _repo.SaveChanges();
            }
        }

        public IEnumerable<OrderReadDTO> GetMerchantOrders(int merchantId)
        {
            var orders = _repo.GetMerchantOrders(merchantId);

            return orders.Select(o => new OrderReadDTO
            {
                Id = o.Id,
                TotalPrice = o.TotalPrice,
                AffiliateCommissionPct = o.AffiliateCommissionPct,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt
            });
        }

        public MerchantBalanceDTO? GetMerchantBalance(int merchantId)
        {
            var balance = _repo.GetMerchantBalance(merchantId);

            if (balance == null)
                return null;

            return new MerchantBalanceDTO
            {
                MerchantId = merchantId,
                Balance = balance.Value
            };
        }
        public Merchant? GetMerchantByUserId(string userId)
        {
            return _repo.GetMerchantByUserId(userId);
        }

    }
}