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

public class DetalleVentaController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public DetalleVentaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DetalleVenta>>> Get0()
    {
        var detalleVenta = await unitofwork.DetallesVentas.GetAllAsync();
        return mapper.Map<List<DetalleVenta>>(detalleVenta);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<DetalleVenta>>> Get([FromQuery]Params detalleVentaParams)
    {
        var detalleVenta = await unitofwork.DetallesVentas.GetAllAsync(detalleVentaParams.PageIndex,detalleVentaParams.PageSize, detalleVentaParams.Search);
        var listaDetallesVentas = mapper.Map<List<DetalleVenta>>(detalleVenta.registros);
        return new Pager<DetalleVenta>(listaDetallesVentas, detalleVenta.totalRegistros,detalleVentaParams.PageIndex,detalleVentaParams.PageSize,detalleVentaParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetalleVenta>> Get(int id)
    {
        var detalleVenta = await unitofwork.DetallesVentas.GetByIdAsync(id);
        return mapper.Map<DetalleVenta>(detalleVenta);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetalleVenta>> Post(DetalleVenta detalleVentaDto)
    {
        var detalleVenta = this.mapper.Map<DetalleVenta>(detalleVentaDto);
        this.unitofwork.DetallesVentas.Add(detalleVenta);
        await unitofwork.SaveAsync();
        if (detalleVenta == null){
            return BadRequest();
        }
        detalleVentaDto.Id = detalleVenta.Id;
        return CreatedAtAction(nameof(Post), new { id = detalleVentaDto.Id }, detalleVentaDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DetalleVenta>> Put (int id, [FromBody]DetalleVenta detalleVentaDto)
    {
        if(detalleVentaDto == null)
            return NotFound();

        var detalleVenta = this.mapper.Map<DetalleVenta>(detalleVentaDto);
        unitofwork.DetallesVentas.Update(detalleVenta);
        await unitofwork.SaveAsync();
        return detalleVentaDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var detalleVenta = await unitofwork.DetallesVentas.GetByIdAsync(id);

        if (detalleVenta == null)
        {
            return Notfound();
        }

        unitofwork.DetallesVentas.Remove(detalleVenta);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}