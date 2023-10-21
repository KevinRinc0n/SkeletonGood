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

        CreateMap<Cliente, ClienteDto>().ReverseMap();

        CreateMap<Estado, EstadoDto>().ReverseMap();

        CreateMap<Estado, EstaadoDto>().ReverseMap();

        CreateMap<Orden, OrdenDto>().ReverseMap();

        CreateMap<Cliente, ClienteOrdenDto>().ReverseMap();

        CreateMap<DetalleOrden, DetallesOrdenesDto>().ReverseMap();

        CreateMap<InsumoPrenda, InsumoPrendaDto>().ReverseMap();

        CreateMap<Municipio, MunicipioDto>().ReverseMap();

        CreateMap<Orden, OrdenClienteDto>().ReverseMap();

        CreateMap<Prenda, PrendaDto>().ReverseMap();
    }
}
