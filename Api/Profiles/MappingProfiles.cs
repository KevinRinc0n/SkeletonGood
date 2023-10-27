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

        CreateMap<InsumoPrenda, InsumooPrendaDto>().ReverseMap();

        CreateMap<Insumo, InsumoDto>().ReverseMap();

        CreateMap<Proveedor, ProveedorDto>().ReverseMap();

        CreateMap<InsumoProveedor, InsumoProveedorDto>().ReverseMap();

        CreateMap<Prenda, PrendaaDto>().ReverseMap();

        CreateMap<Venta, VentaDto>().ReverseMap();

        CreateMap<DetalleVenta, DetalleVentaDto>().ReverseMap();

        CreateMap<Inventario, InventarioDto>().ReverseMap();

        CreateMap<Talla, TallaDto>().ReverseMap();

        CreateMap<Prenda, PrendaInventarioDto>().ReverseMap();

        CreateMap<Inventario, InventarioPrendaDto>().ReverseMap();
    }
}
