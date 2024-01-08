using AutoMapper;
using DtoAPI.Models;
using DtoAPI.Models.DTO;

namespace DtoAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
