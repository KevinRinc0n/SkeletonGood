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

public class OrdenController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public OrdenController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Orden>>> Get0()
    {
        var Orden = await unitofwork.Ordenes.GetAllAsync();
        return mapper.Map<List<Orden>>(Orden);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Orden>>> Get([FromQuery]Params OrdenParams)
    {
        var Orden = await unitofwork.Ordenes.GetAllAsync(OrdenParams.PageIndex,OrdenParams.PageSize, OrdenParams.Search);
        var listaOrdens = mapper.Map<List<Orden>>(Orden.registros);
        return new Pager<Orden>(listaOrdens, Orden.totalRegistros,OrdenParams.PageIndex,OrdenParams.PageSize,OrdenParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Orden>> Get(int id)
    {
        var Orden = await unitofwork.Ordenes.GetByIdAsync(id);
        return mapper.Map<Orden>(Orden);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Orden>> Post(Orden OrdenDto)
    {
        var Orden = this.mapper.Map<Orden>(OrdenDto);
        this.unitofwork.Ordenes.Add(Orden);
        await unitofwork.SaveAsync();
        if (Orden == null){
            return BadRequest();
        }
        OrdenDto.Id = Orden.Id;
        return CreatedAtAction(nameof(Post), new { id = OrdenDto.Id }, OrdenDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Orden>> Put (int id, [FromBody]Orden OrdenDto)
    {
        if(OrdenDto == null)
            return NotFound();

        var Orden = this.mapper.Map<Orden>(OrdenDto);
        unitofwork.Ordenes.Update(Orden);
        await unitofwork.SaveAsync();
        return OrdenDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Orden = await unitofwork.Ordenes.GetByIdAsync(id);

        if (Orden == null)
        {
            return Notfound();
        }

        unitofwork.Ordenes.Remove(Orden);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("proceso")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OrdenDto>>> GetOrdenesProceso()
    {
        var ordenesEnProce = await unitofwork.Ordenes.enProceso();
        return mapper.Map<List<OrdenDto>>(ordenesEnProce);
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}