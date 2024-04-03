using AutoMapper;
using MyWebApiProject.Models;
using MyWebApiProject.Models.dtos;

namespace MyWebApiProject;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Villa, VillaCreateDto>().ReverseMap();
        CreateMap<Villa, VillaUpdateDto>().ReverseMap();
        CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
        CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
    }
}