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

public class TipoEstadoController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TipoEstadoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoEstado>>> Get0()
    {
        var TipoEstado = await unitofwork.TiposEstados.GetAllAsync();
        return mapper.Map<List<TipoEstado>>(TipoEstado);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<TipoEstado>>> Get([FromQuery]Params TipoEstadoParams)
    {
        var TipoEstado = await unitofwork.TiposEstados.GetAllAsync(TipoEstadoParams.PageIndex,TipoEstadoParams.PageSize, TipoEstadoParams.Search);
        var listaTipoEstados = mapper.Map<List<TipoEstado>>(TipoEstado.registros);
        return new Pager<TipoEstado>(listaTipoEstados, TipoEstado.totalRegistros,TipoEstadoParams.PageIndex,TipoEstadoParams.PageSize,TipoEstadoParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoEstado>> Get(int id)
    {
        var TipoEstado = await unitofwork.TiposEstados.GetByIdAsync(id);
        return mapper.Map<TipoEstado>(TipoEstado);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoEstado>> Post(TipoEstado TipoEstadoDto)
    {
        var TipoEstado = this.mapper.Map<TipoEstado>(TipoEstadoDto);
        this.unitofwork.TiposEstados.Add(TipoEstado);
        await unitofwork.SaveAsync();
        if (TipoEstado == null){
            return BadRequest();
        }
        TipoEstadoDto.Id = TipoEstado.Id;
        return CreatedAtAction(nameof(Post), new { id = TipoEstadoDto.Id }, TipoEstadoDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoEstado>> Put (int id, [FromBody]TipoEstado TipoEstadoDto)
    {
        if(TipoEstadoDto == null)
            return NotFound();

        var TipoEstado = this.mapper.Map<TipoEstado>(TipoEstadoDto);
        unitofwork.TiposEstados.Update(TipoEstado);
        await unitofwork.SaveAsync();
        return TipoEstadoDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var TipoEstado = await unitofwork.TiposEstados.GetByIdAsync(id);

        if (TipoEstado == null)
        {
            return Notfound();
        }

        unitofwork.TiposEstados.Remove(TipoEstado);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}