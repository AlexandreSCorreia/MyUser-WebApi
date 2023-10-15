using AutoMapper;
using MyUserWebApi.Domain.Entities;
using MyUserWebApi.Domain.Models;

namespace MyUserWebApi.CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ReverseMap();
        }
    }
}