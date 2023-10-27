using Dominio.Entities;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.Helpers;
using Api.Dtos;

namespace Api.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize (Roles= "Administrador")]        

public class ProveedorController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Proveedor>>> Get0()
    {
        var Proveedor = await unitofwork.Proveedores.GetAllAsync();
        return mapper.Map<List<Proveedor>>(Proveedor);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Proveedor>>> Get([FromQuery]Params ProveedorParams)
    {
        var Proveedor = await unitofwork.Proveedores.GetAllAsync(ProveedorParams.PageIndex,ProveedorParams.PageSize, ProveedorParams.Search);
        var listaProveedors = mapper.Map<List<Proveedor>>(Proveedor.registros);
        return new Pager<Proveedor>(listaProveedors, Proveedor.totalRegistros,ProveedorParams.PageIndex,ProveedorParams.PageSize,ProveedorParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Proveedor>> Get(int id)
    {
        var Proveedor = await unitofwork.Proveedores.GetByIdAsync(id);
        return mapper.Map<Proveedor>(Proveedor);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Proveedor>> Post(Proveedor ProveedorDto)
    {
        var Proveedor = this.mapper.Map<Proveedor>(ProveedorDto);
        this.unitofwork.Proveedores.Add(Proveedor);
        await unitofwork.SaveAsync();
        if (Proveedor == null){
            return BadRequest();
        }
        ProveedorDto.Id = Proveedor.Id;
        return CreatedAtAction(nameof(Post), new { id = ProveedorDto.Id }, ProveedorDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Proveedor>> Put (int id, [FromBody]Proveedor ProveedorDto)
    {
        if(ProveedorDto == null)
            return NotFound();

        var Proveedor = this.mapper.Map<Proveedor>(ProveedorDto);
        unitofwork.Proveedores.Update(Proveedor);
        await unitofwork.SaveAsync();
        return ProveedorDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Proveedor = await unitofwork.Proveedores.GetByIdAsync(id);

        if (Proveedor == null)
        {
            return Notfound();
        }

        unitofwork.Proveedores.Remove(Proveedor);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("insumosVenXProveedor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InsumoProveedorDto>>> GetInsumosVenXProveedor(string nitProveedor)
    {
        var insumosXProveedor = await unitofwork.InsumosProveedores.insumosXProveedor(nitProveedor);
        return mapper.Map<List<InsumoProveedorDto>>(insumosXProveedor);
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}