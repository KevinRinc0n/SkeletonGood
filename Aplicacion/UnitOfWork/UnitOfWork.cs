using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiDbContext _context;
    private ICargo _cargo;
    private ICliente _cliente;
    private IColor _color;
    private IDepartamento _departamento;
    private IDetalleOrden _detalleOrden;
    private IDetalleVenta _detalleVenta;
    private IEmpresa _empresa;
    private IEstado _estado;
    private IFormaPago _formaPago;
    private IGenero _genero;
    private IInsumo _insumo;
    private IInsumoPrenda _insumoPrenda;
    private IInsumoProveedor _insumoProveedor;
    private IInventarioTalla _inventarioTalla;
    private IMunicipio _municipio;
    private ITalla _talla;
    private IPais _pais;
    private ITipoEstado _tipoEstado;
    private ITipoPersona _tipoPersona;
    private ITipoProteccion _tipoProteccion;
    private IVenta _venta;
    private IEmpleado _empleado;
    private IInventario _inventario;
    private IOrden _orden;
    private IPrenda _prenda;
    private IProveedor _proveedor;
    private IRol _roles;
    private IUsersRols _rolesUsuarios;
    private IUser _usuarios;

    public UnitOfWork(ApiDbContext context)
    {
        _context = context;
    }

    public ICargo Cargos
    {
        get
        {
            if (_cargo == null)
            {
                _cargo = new CargoRepository(_context);
            }
            return _cargo;
        }
    }

    public ICliente Clientes
    {
        get
        {
            if (_cliente == null)
            {
                _cliente = new ClienteRepository(_context);
            }
            return _cliente;
        }
    }

    public IColor Colores
    {
        get
        {
            if (_color == null)
            {
                _color = new ColorRepository(_context);
            }
            return _color;
        }
    }

    public IDepartamento Departamentos
    {
        get
        {
            if (_departamento == null)
            {
                _departamento = new DepartamentoRepository(_context);
            }
            return _departamento;
        }
    }

    public IDetalleOrden DetallesOrdenes
    {
        get
        {
            if (_detalleOrden == null)
            {
                _detalleOrden = new DetalleOrdenRepository(_context);
            }
            return _detalleOrden;
        }
    }

    public IDetalleVenta DetallesVentas
    {
        get
        {
            if (_detalleVenta == null)
            {
                _detalleVenta = new DetalleVentaRepository(_context);
            }
            return _detalleVenta;
        }
    }

    public IEmpresa Empresas
    {
        get
        {
            if (_empresa == null)
            {
                _empresa = new EmpresaRepository(_context);
            }
            return _empresa;
        }
    }

    public IEstado Estados
    {
        get
        {
            if (_estado == null)
            {
                _estado = new EstadoRepository(_context);
            }
            return _estado;
        }
    }

    public IFormaPago FormaPagos
    {
        get
        {
            if (_formaPago == null)
            {
                _formaPago = new FormaPagoRepository(_context);
            }
            return _formaPago;
        }
    }

    public IGenero Generos
    {
        get
        {
            if (_genero == null)
            {
                _genero = new GeneroRepository(_context);
            }
            return _genero;
        }
    }

    public IInsumo Insumos
    {
        get
        {
            if (_insumo == null)
            {
                _insumo = new InsumoRepository(_context);
            }
            return _insumo;
        }
    }

    public IInsumoPrenda InsumosPrendas
    {
        get
        {
            if (_insumoPrenda == null)
            {
                _insumoPrenda = new InsumoPrendaRepository(_context);
            }
            return _insumoPrenda;
        }
    }

    public IInsumoProveedor InsumosProveedores
    {
        get
        {
            if (_insumoProveedor == null)
            {
                _insumoProveedor = new InsumoProveedorRepository(_context);
            }
            return _insumoProveedor;
        }
    }

    public IInventarioTalla InventariosTallas
    {
        get
        {
            if (_inventarioTalla == null)
            {
                _inventarioTalla = new InventarioTallaRepository(_context);
            }
            return _inventarioTalla;
        }
    }

    public IMunicipio Municipios
    {
        get
        {
            if (_municipio == null)
            {
                _municipio = new MunicipioRepository(_context);
            }
            return _municipio;
        }
    }

    public ITalla Tallas
    {
        get
        {
            if (_talla == null)
            {
                _talla = new TallaRepository(_context);
            }
            return _talla;
        }
    }

    public IPais Paises
    {
        get
        {
            if (_pais == null)
            {
                _pais = new PaisRepository(_context);
            }
            return _pais;
        }
    }

    public ITipoEstado TiposEstados
    {
        get
        {
            if (_tipoEstado == null)
            {
                _tipoEstado = new TipoEstadoRepository(_context);
            }
            return _tipoEstado;
        }
    }

    public ITipoPersona TiposPersonas
    {
        get
        {
            if (_tipoPersona == null)
            {
                _tipoPersona = new TipoPersonaRepository(_context);
            }
            return _tipoPersona;
        }
    }

    public ITipoProteccion TiposProtecciones
    {
        get
        {
            if (_tipoProteccion == null)
            {
                _tipoProteccion = new TipoProteccionRepository(_context);
            }
            return _tipoProteccion;
        }
    }

    public IVenta Ventas
    {
        get
        {
            if (_venta == null)
            {
                _venta = new VentaRepository(_context);
            }
            return _venta;
        }
    }

    public IEmpleado Empleados
    {
        get
        {
            if (_empleado == null)
            {
                _empleado = new EmpleadoRepository(_context);
            }
            return _empleado;
        }
    }

    public IInventario Inventarios
    {
        get
        {
            if (_inventario == null)
            {
                _inventario = new InventarioRepository(_context);
            }
            return _inventario;
        }
    }

    public IOrden Ordenes
    {
        get
        {
            if (_orden == null)
            {
                _orden = new OrdenRepository(_context);
            }
            return _orden;
        }
    }

    public IPrenda Prendas
    {
        get
        {
            if (_prenda == null)
            {
                _prenda = new PrendaRepository(_context);
            }
            return _prenda;
        }
    }

    public IProveedor Proveedores
    {
        get
        {
            if (_proveedor == null)
            {
                _proveedor = new ProveedorRepository(_context);
            }
            return _proveedor;
        }
    }

    public IRol Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }

    public IUsersRols RolesUsuarios
    {
        get
        {
            if (_rolesUsuarios == null)
            {
                _rolesUsuarios = new UsersRolsRepository(_context);
            }
            return _rolesUsuarios;
        }
    }

    public IUser Usuarios
    {
        get
        {
            if (_usuarios == null)
            {
                _usuarios = new UserRepository(_context);
            }
            return _usuarios;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}