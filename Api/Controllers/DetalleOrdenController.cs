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

public class DetalleOrdenController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public DetalleOrdenController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DetalleOrden>>> Get0()
    {
        var detalleOrden = await unitofwork.DetallesOrdenes.GetAllAsync();
        return mapper.Map<List<DetalleOrden>>(detalleOrden);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<DetalleOrden>>> Get([FromQuery]Params detalleOrdenParams)
    {
        var detalleOrden = await unitofwork.DetallesOrdenes.GetAllAsync(detalleOrdenParams.PageIndex,detalleOrdenParams.PageSize, detalleOrdenParams.Search);
        var listaDetallesOrdenes = mapper.Map<List<DetalleOrden>>(detalleOrden.registros);
        return new Pager<DetalleOrden>(listaDetallesOrdenes, detalleOrden.totalRegistros,detalleOrdenParams.PageIndex,detalleOrdenParams.PageSize,detalleOrdenParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetalleOrden>> Get(int id)
    {
        var detalleOrden = await unitofwork.DetallesOrdenes.GetByIdAsync(id);
        return mapper.Map<DetalleOrden>(detalleOrden);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetalleOrden>> Post(DetalleOrden detalleOrdenDto)
    {
        var detalleOrden = this.mapper.Map<DetalleOrden>(detalleOrdenDto);
        this.unitofwork.DetallesOrdenes.Add(detalleOrden);
        await unitofwork.SaveAsync();
        if (detalleOrden == null){
            return BadRequest();
        }
        detalleOrdenDto.Id = detalleOrden.Id;
        return CreatedAtAction(nameof(Post), new { id = detalleOrdenDto.Id }, detalleOrdenDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DetalleOrden>> Put (int id, [FromBody]DetalleOrden detalleOrdenDto)
    {
        if(detalleOrdenDto == null)
            return NotFound();

        var detalleOrden = this.mapper.Map<DetalleOrden>(detalleOrdenDto);
        unitofwork.DetallesOrdenes.Update(detalleOrden);
        await unitofwork.SaveAsync();
        return detalleOrdenDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var detalleOrden = await unitofwork.DetallesOrdenes.GetByIdAsync(id);

        if (detalleOrden == null)
        {
            return Notfound();
        }

        unitofwork.DetallesOrdenes.Remove(detalleOrden);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}