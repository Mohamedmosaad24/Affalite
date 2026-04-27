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
        private readonly IMapper _mapper;

        public AffiliateService(IAffiliateRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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

        public void UpdateAffiliate(UpdateAffiliateDTO affiliateDTO, int id)
        {
            if (affiliateDTO == null)
                throw new ArgumentNullException(nameof(affiliateDTO));

            var existingAffiliate = _repo.GetById(id);
            if (existingAffiliate != null)
            {
                _mapper.Map(affiliateDTO, existingAffiliate); 
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

            return orders.Select(o => new OrderReadDTO
            {
                Id = o.Id,
                TotalPrice = o.TotalPrice,
                AffiliateCommissionPct = o.AffiliateCommissionPct,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt
            });
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