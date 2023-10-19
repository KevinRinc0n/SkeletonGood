using Api.Dtos;
using Dominio.Entities;
using AutoMapper;

namespace Api.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<porDefecto1, porDefecto1Dto>().ReverseMap();

        CreateMap<porDefecto2, porDefecto2Dto>().ReverseMap();
    }
}
