
using Api.Domain.Models;
using Api.Domain.DTOs.User;
using AutoMapper;

namespace Api.CrossCutting.Mapping
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDTO>().ReverseMap();

            CreateMap<UserModel, UserCreateDTO>().ReverseMap();
            
            CreateMap<UserModel, UserUpdateDTO>().ReverseMap();
        }

    }
}
