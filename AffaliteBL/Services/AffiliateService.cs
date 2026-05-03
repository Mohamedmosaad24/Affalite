using AffaliteBL.DTOs.AffiliateDTOs;
using AffaliteBL.DTOs.CommissionDTOs;
using AffaliteBL.DTOs.OrderDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using AutoMapper;



namespace AffaliteBL.Services
{
    public class AffiliateService : IAffiliateService
    {
        private readonly IAffiliateRepo _repo;
        private readonly IMapper mapper;

        public AffiliateService(IAffiliateRepo repo,IMapper mapper)
        {
            _repo = repo;
            this.mapper = mapper;
        }

        public IEnumerable<Affiliate> GetAllAffiliates()
        {
            return _repo.GetAllAffiliates();
        }

        public Affiliate? GetAffiliateById(int id)
        {
            return _repo.GetById(id);
        }

        public void CreateAffiliate(Affiliate affiliate)
        {
            if (affiliate == null)
                throw new ArgumentNullException(nameof(affiliate));

            _repo.Add(affiliate);
            _repo.SaveChanges();
        }

        public void UpdateAffiliate(Affiliate affiliate)
        {
            if (affiliate == null)
                throw new ArgumentNullException(nameof(affiliate));

            var existingAffiliate = _repo.GetById(affiliate.Id);

            if (existingAffiliate != null)
            {
                //existingAffiliate.Balance = affiliate.Balance;

                _repo.Update(existingAffiliate);
                _repo.SaveChanges();
            }
        }

        public void DeleteAffiliate(int id)
        {
            var affiliate = _repo.GetById(id);

            if (affiliate != null)
            {
                _repo.Delete(affiliate);
                _repo.SaveChanges();
            }
        }

        public IEnumerable<OrderReadDTO> GetAffiliateOrders(int affiliateId)
        {
            var orders = _repo.GetAffiliateOrders(affiliateId);
            var res = mapper.Map<List<OrderReadDTO>>(orders);
            return res;
        }

        public IEnumerable<CommissionReadDTO> GetAffiliateCommissions(int affiliateId)
        {
            var commissions = _repo.GetAffiliateCommissions(affiliateId);

            return commissions.Select(c => new CommissionReadDTO
            {
                Id = c.Id,
                OrderId = c.OrderId,
                AffiliateAmount = c.AffiliateAmount,
                PlatformAmount = c.PlatformAmount,
                MerchantAmount = c.MerchantAmount,
                Status = c.Status.ToString(),
                CreatedAt = c.CreatedAt
            });
        }

        public AffiliateBalanceDTO? GetAffiliateBalance(int affiliateId)
        {
            var balance = _repo.GetAffiliateBalance(affiliateId);

            if (balance == null)
                return null;

            return new AffiliateBalanceDTO
            {
                Balance = balance.Value
            };
        }
        public Affiliate? GetAffiliateUserId(string userId)
        {
            return _repo.GetAffiliateUserId(userId);
        }


    }
}