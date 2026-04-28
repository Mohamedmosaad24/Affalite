using AutoMapper;
using AffaliteDAL.Entities;
using AffaliteBL.DTOs.AiDTOs;

namespace AffaliteBL.Mapper
{
    public class AiMappingProfile : Profile
    {
        public AiMappingProfile()
        {
            // AiContentHistory ↔ AiContentHistoryDTO
            CreateMap<AiContentHistory, AiContentHistoryDTO>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : "Unknown"))
                .ForMember(dest => dest.GeneratedContent,
                    opt => opt.MapFrom(src => src.GeneratedContent ?? string.Empty));

            // need edites

            //ContentGenerationRequest ← لا يحتاج Mapping(بيجي من الـ Frontend)

            // AiContentResponse → لا يحتاج Mapping(بيروح للـ Frontend مباشرة)
        }
    }
}