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

public class VentaController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public VentaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Venta>>> Get0()
    {
        var Venta = await unitofwork.Ventas.GetAllAsync();
        return mapper.Map<List<Venta>>(Venta);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Venta>>> Get([FromQuery]Params VentaParams)
    {
        var Venta = await unitofwork.Ventas.GetAllAsync(VentaParams.PageIndex,VentaParams.PageSize, VentaParams.Search);
        var listaVentas = mapper.Map<List<Venta>>(Venta.registros);
        return new Pager<Venta>(listaVentas, Venta.totalRegistros,VentaParams.PageIndex,VentaParams.PageSize,VentaParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Venta>> Get(int id)
    {
        var Venta = await unitofwork.Ventas.GetByIdAsync(id);
        return mapper.Map<Venta>(Venta);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Venta>> Post(Venta VentaDto)
    {
        var Venta = this.mapper.Map<Venta>(VentaDto);
        this.unitofwork.Ventas.Add(Venta);
        await unitofwork.SaveAsync();
        if (Venta == null){
            return BadRequest();
        }
        VentaDto.Id = Venta.Id;
        return CreatedAtAction(nameof(Post), new { id = VentaDto.Id }, VentaDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Venta>> Put (int id, [FromBody]Venta VentaDto)
    {
        if(VentaDto == null)
            return NotFound();

        var Venta = this.mapper.Map<Venta>(VentaDto);
        unitofwork.Ventas.Update(Venta);
        await unitofwork.SaveAsync();
        return VentaDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Venta = await unitofwork.Ventas.GetByIdAsync(id);

        if (Venta == null)
        {
            return Notfound();
        }

        unitofwork.Ventas.Remove(Venta);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}