using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO.DTOs;


namespace WebAPP.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}