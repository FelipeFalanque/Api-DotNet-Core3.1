using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mapping
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDTO, UserEntity>().ReverseMap();

            CreateMap<UserCreateDTO, UserEntity>().ReverseMap();
            
            CreateMap<UserUpdateDTO, UserEntity>().ReverseMap();

            CreateMap<UserCreateResultDTO, UserEntity>().ReverseMap();

            CreateMap<UserUpdateResultDTO, UserEntity>().ReverseMap();

        }
    }
}
