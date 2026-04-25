using AutoMapper;
using AffaliteDAL.Entities;
using AffaliteBL.DTOs.MatchingDTOs;

namespace AffaliteBL.Mapper
{
    public class MatchingMappingProfile : Profile
    {
        public MatchingMappingProfile()
        {
            // AffiliateMerchantMatch ↔ MatchRecommendationDTO
            CreateMap<AffiliateMerchantMatch, MatchRecommendationDTO>()
                .ForMember(dest => dest.TargetId,
                    opt => opt.MapFrom(src => src.MerchantId))
                .ForMember(dest => dest.TargetName,
                    opt => opt.MapFrom(src => src.Merchant.AppUser != null ? src.Merchant.AppUser.FullName : "Unknown"))
                .ForMember(dest => dest.TargetType,
                    opt => opt.MapFrom(src => "Merchant"))
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .ForMember(dest => dest.ExpectedCommission,
                    opt => opt.MapFrom(src => src.Product != null
                        ? src.Product.Price * (src.Product.PlatformCommissionPct / 100)
                        : (decimal?)null));

            // MatchResponseRequest ← لا يحتاج Mapping (بيجي من الـ Frontend)
        }
    }
}