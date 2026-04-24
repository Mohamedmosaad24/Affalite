using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteBL.DTOs.WithdrawalDto;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using AffaliteDAL.Repo;

namespace AffaliteBL.Services
{
    public class WithdrawalService : IWithdrawalService
    {
        private readonly IGenericRepository<WithdrawRequest> _repo;
        private readonly IWithdrawalRepo withdrawalRepo;
        private readonly IAffiliateRepo _affiliateRepo;
        private readonly IMerchantRepo _merchantRepo;

        public WithdrawalService(
            IAffiliateRepo affiliateRepo,
            IMerchantRepo merchantRepo,
            IGenericRepository<WithdrawRequest> repo
            , IWithdrawalRepo withdrawalRepo)
        {
            _repo = repo;
            this.withdrawalRepo = withdrawalRepo;
            _affiliateRepo = affiliateRepo;
            _merchantRepo = merchantRepo;
        }

        public IEnumerable<WithdrawRequest> GetAll()
        {
            return  _repo.GetAll();
        }

        // ✅ ADD REQUEST
        public WithdrawRequest Add(string userId, CreateWithdrawalDto dto)
        {
            // نحدد هل هو affiliate ولا merchant
            var affiliate = _affiliateRepo.GetAffiliateUserId(userId);
            var merchant = _merchantRepo.GetMerchantByUserId(userId);

            if (affiliate == null && merchant == null)
                throw new Exception("User not found");

            decimal balance = 0;
            int entityId = 0;
            UserType userType;

            if (affiliate != null)
            {
                balance = affiliate.Balance;
                entityId = affiliate.Id;
                userType = UserType.Affiliate;
            }
            else
            {
                balance = merchant.Balance;
                entityId = merchant.Id;
                userType = UserType.Merchant;
            }

            if (dto.Amount > balance)
                throw new Exception("Insufficient balance");

            // ✅ خصم الرصيد أول ما يعمل طلب
            if (userType == UserType.Affiliate)
                affiliate.Balance -= dto.Amount;
            else
                merchant.Balance -= dto.Amount;

            var request = new WithdrawRequest
            {
                Amount = dto.Amount,
                PaymentMethod = dto.Method,
                Number = dto.Number,
                UserRefId = entityId,
                UserType = userType,
                Status = WithdrawalStatus.Pending
            };

             _repo.Add(request);
             _repo.SaveChanges();

            return request;
        }

        // ✅ UPDATE STATUS
        public WithdrawRequest Update(UpdateWithdrawalDto dto)
        {
            var request =  _repo.GetById(dto.Id);

            if (request == null)
                throw new Exception("Request not found");

            request.Status = dto.Status;

            // ❌ لو اترفض → رجع الفلوس
            if (dto.Status == WithdrawalStatus.Rejected)
            {
                if (request.UserType == UserType.Affiliate)
                {
                    var aff =  _affiliateRepo.GetById(request.UserRefId);
                    aff.Balance += request.Amount;
                }
                else
                {
                    var mer =  _merchantRepo.GetById(request.UserRefId);
                    mer.Balance += request.Amount;
                }
            }

            // ✅ لو Approved → خلاص مفيش حاجة
             _repo.Update(request);
             _repo.SaveChanges();

            return request;
        }

        public async Task<IEnumerable<WithdrawRequest>> GetByAffiliateId(string affiliateId)
        {
            var affiliate = _affiliateRepo.GetAffiliateUserId(affiliateId);

            return await withdrawalRepo.GetByUserAsync(affiliate.Id, UserType.Affiliate);
        }

        public async Task<IEnumerable<WithdrawRequest>> GetByMerchantId(string merchantId)
        {
            var merchant = _merchantRepo.GetMerchantByUserId(merchantId);

            return await withdrawalRepo.GetByUserAsync(merchant.Id, UserType.Merchant);
        }
    }
}
