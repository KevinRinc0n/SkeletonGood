namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    ICargo Cargos { get; }
    IColor Colores { get; }
    ICliente Clientes { get; }
    IDepartamento Departamentos { get; }
    IDetalleOrden DetallesOrdenes { get; }
    IDetalleVenta DetallesVentas { get; }
    IEmpresa Empresas { get; }
    IEstado Estados { get; }
    IFormaPago FormaPagos { get; }
    IGenero Generos { get; }
    IInsumo Insumos { get; }
    IInsumoPrenda InsumosPrendas { get; }
    IInsumoProveedor InsumosProveedores { get; }
    IInventarioTalla InventariosTallas { get; }
    IMunicipio Municipios { get; }
    IPrenda Prendas { get; }
    IOrden Ordenes { get; }
    IProveedor Proveedores { get; }
    ITalla Tallas { get; }
    IPais Paises { get; }
    ITipoEstado TiposEstados { get; }
    ITipoPersona TiposPersonas { get; }
    ITipoProteccion TiposProtecciones { get; }
    IEmpleado Empleados { get; }
    IInventario Inventarios { get; }
    IVenta Ventas { get; }
    IRol Roles { get; }
    IUsersRols RolesUsuarios { get; }
    IUser Usuarios { get; }


    Task<int> SaveAsync();
}
