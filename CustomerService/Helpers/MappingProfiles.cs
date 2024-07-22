using AutoMapper;
using CustomerService.Dtos;
using CustomerService.Entities;

namespace CustomerService.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<DiscountDto, Discount>();
        CreateMap<Discount, CSVDto>()
            .ForMember(dest => dest.DiscountCoupon, opt => opt.MapFrom(src => src.DiscountCoupon))
            .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }
    
}
