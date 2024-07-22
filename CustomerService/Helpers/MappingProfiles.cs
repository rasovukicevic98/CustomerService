using AutoMapper;
using CustomerService.Dtos;
using CustomerService.Entities;

namespace CustomerService.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<DiscountDto, Discount>();
        }
    }
}
