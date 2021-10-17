using System.Linq;
using AutoMapper;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Dtos.PetDtos;
using DogsBarberShop.Entities.InfastructureModels;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop.Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SignUpCredentials, User>();

            CreateMap<PetInputDto, Pet>();
            CreateMap<Pet, PetOutputDto>();

            CreateMap<IdentityResult, AuthenticationException>()
                     .ForMember(dest => dest.ExceptionMessages,
                                opt => opt.MapFrom(src => src.Errors.Select(e => e.Description)));
        }
    }
}