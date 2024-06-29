using AutoMapper;
using COREAPI.DTOs;
using COREAPI.Models;

namespace COREAPI.Configurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
