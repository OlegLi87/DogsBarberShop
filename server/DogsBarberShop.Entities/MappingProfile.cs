using System.Linq;
using AutoMapper;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Dtos.OrderDtos;
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

            CreateMap<OrderInputDto, Order>();
            CreateMap<Order, OrderOutputDto>()
                     .ForMember(dest => dest.PetNickname,
                                opts => opts.MapFrom(src => src.Pet.NickName))
                     .ForMember(dest => dest.UserName,
                                opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<IdentityResult, AuthenticationException>()
                     .ForMember(dest => dest.ExceptionMessages,
                                opt => opt.MapFrom(src => src.Errors.Select(e => e.Description)));
        }
    }
}