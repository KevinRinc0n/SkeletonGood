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

public class InsumoProveedorController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public InsumoProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<InsumoProveedor>>> Get0()
    {
        var InsumoProveedor = await unitofwork.InsumosProveedores.GetAllAsync();
        return mapper.Map<List<InsumoProveedor>>(InsumoProveedor);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<InsumoProveedor>>> Get([FromQuery]Params InsumoProveedorParams)
    {
        var InsumoProveedor = await unitofwork.InsumosProveedores.GetAllAsync(InsumoProveedorParams.PageIndex,InsumoProveedorParams.PageSize, InsumoProveedorParams.Search);
        var listaInsumoProveedors = mapper.Map<List<InsumoProveedor>>(InsumoProveedor.registros);
        return new Pager<InsumoProveedor>(listaInsumoProveedors, InsumoProveedor.totalRegistros,InsumoProveedorParams.PageIndex,InsumoProveedorParams.PageSize,InsumoProveedorParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InsumoProveedor>> Get(int id)
    {
        var InsumoProveedor = await unitofwork.InsumosProveedores.GetByIdAsync(id);
        return mapper.Map<InsumoProveedor>(InsumoProveedor);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InsumoProveedor>> Post(InsumoProveedor InsumoProveedorDto)
    {
        var InsumoProveedor = this.mapper.Map<InsumoProveedor>(InsumoProveedorDto);
        this.unitofwork.InsumosProveedores.Add(InsumoProveedor);
        await unitofwork.SaveAsync();
        if (InsumoProveedor == null){
            return BadRequest();
        }
        InsumoProveedorDto.Id = InsumoProveedor.Id;
        return CreatedAtAction(nameof(Post), new { id = InsumoProveedorDto.Id }, InsumoProveedorDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<InsumoProveedor>> Put (int id, [FromBody]InsumoProveedor InsumoProveedorDto)
    {
        if(InsumoProveedorDto == null)
            return NotFound();

        var InsumoProveedor = this.mapper.Map<InsumoProveedor>(InsumoProveedorDto);
        unitofwork.InsumosProveedores.Update(InsumoProveedor);
        await unitofwork.SaveAsync();
        return InsumoProveedorDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var InsumoProveedor = await unitofwork.InsumosProveedores.GetByIdAsync(id);

        if (InsumoProveedor == null)
        {
            return Notfound();
        }

        unitofwork.InsumosProveedores.Remove(InsumoProveedor);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}