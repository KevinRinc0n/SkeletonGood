using Api.Dtos;
using Dominio.Entities;
using AutoMapper;

namespace Api.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Empleado, EmpleadoDto>().ReverseMap();
        CreateMap<Cargo, CargoDto>().ReverseMap();
    }
}
