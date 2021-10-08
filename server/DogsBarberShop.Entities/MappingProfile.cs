using System.Linq;
using AutoMapper;
using DogsBarberShop.Entities.InfastructureModels;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop.Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityResult, AuthenticationException>()
                     .ForMember(dest => dest.ExceptionMessages,
                                opt => opt.MapFrom(src => src.Errors.Select(e => e.Description)));
        }
    }
}