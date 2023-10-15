using AutoMapper;
using MyUserWebApi.Domain.Dtos.User;
using MyUserWebApi.Domain.Models;

namespace MyUserWebApi.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDto>()
                .ReverseMap();
        }
    }
}