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

        
        //   public IEnumerable<WithdrawRequestDto> GetAl()
        //{
            //var requests = _repo.GetAll();

            //return requests.Select(r =>
            //{
            //    string name = "";

            //    if (r.UserType == UserType.Affiliate)
            //    {
            //        var aff = _affiliateRepo.GetById(r.UserRefId);
        //            name = aff?.FullName ?? "Unknown";
        //        }
        //        else
        //        {
        //            var mer = _merchantRepo.GetById(r.UserRefId);
        //            name = mer?.FullName ?? "Unknown";
        //        }

        //        return new WithdrawRequestDto
        //        {
        //            Number = r.Number,
        //            Amount = r.Amount,
        //            PaymentMethod = r.PaymentMethod,
        //            UserType = r.UserType,
        //            UserRefId = r.UserRefId,
        //            Status = r.Status,
        //            CreatedAt = r.CreatedAt,
        //            UserName = name 
        //        };
        //    });
        //}
        

        public WithdrawRequest Add(string userId, CreateWithdrawalDto dto)
        {
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

            // ✅ مش بنخصم هنا — بس بنحفظ الطلب
            var request = new WithdrawRequest
            {
                Amount = dto.Amount,
                PaymentMethod = dto.Method,
                Number = dto.Number,
                UserRefId = entityId,
                UserType = userType,
                Status = WithdrawalStatus.Pending  // ← Pending دايماً
            };

            _repo.Add(request);
            _repo.SaveChanges();

            return request;
        }

        // ✅ UPDATE — الخصم بيحصل هنا بس
        public WithdrawRequest Update(UpdateWithdrawalDto dto)
        {
            var request = _repo.GetById(dto.Id);

            if (request == null)
                throw new Exception("Request not found");

            // ❌ منع التعديل لو مش Pending
            if (request.Status != WithdrawalStatus.Pending)
                throw new Exception("Request already processed");

            request.Status = dto.Status;

            if (dto.Status == WithdrawalStatus.Approved)
            {
                // ✅ Approved → اخصم الرصيد دلوقتي
                if (request.UserType == UserType.Affiliate)
                {
                    var aff = _affiliateRepo.GetById(request.UserRefId);
                    if (aff.Balance < request.Amount)
                        throw new Exception("Insufficient balance");
                    aff.Balance -= request.Amount;
                }
                else
                {
                    var mer = _merchantRepo.GetById(request.UserRefId);
                    if (mer.Balance < request.Amount)
                        throw new Exception("Insufficient balance");
                    mer.Balance -= request.Amount;
                }
            }
            // ✅ Rejected → مفيش خصم ومفيش رجوع (الرصيد لم يتخصم أصلاً)

            _repo.Update(request);
            _repo.SaveChanges();

            return request;
        }

        public async Task<IEnumerable<WithdrawRequest>> GetByAffiliateId(int affiliateId)
        {
            //var affiliate = _affiliateRepo.GetAffiliateUserId(affiliateId);

            return await withdrawalRepo.GetByUserAsync(affiliateId, UserType.Affiliate);
        }

        public async Task<IEnumerable<WithdrawRequest>> GetByMerchantId(int merchantId)
        {
            //var merchant = _merchantRepo.GetMerchantByUserId(merchantId);

            return await withdrawalRepo.GetByUserAsync(merchantId, UserType.Merchant);
        }

        public IEnumerable<WithdrawRequestDto> GetAll()
        {
            var requests = _repo.GetAll();

            return requests.Select(r =>
            {
                string name = "";

                if (r.UserType == UserType.Affiliate)
                {
                    var aff = _affiliateRepo.GetById(r.UserRefId); 
                    name = aff?.AppUser?.FullName ?? aff?.AppUser?.UserName ?? "Unknown";
                }
                else
                {
                    var mer = _merchantRepo.GetById(r.UserRefId); 
                    name = mer?.AppUser?.FullName ?? mer?.AppUser?.UserName ?? "Unknown";
                }

                return new WithdrawRequestDto
                {
                    Id=r.Id,
                    Number = r.Number,
                    Amount = r.Amount,
                    PaymentMethod = r.PaymentMethod,
                    UserType = r.UserType,
                    UserRefId = r.UserRefId,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt,
                    UserName = name
                };
            });
        }


    }
    }

