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

public class TipoPersonaController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TipoPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoPersona>>> Get0()
    {
        var TipoPersona = await unitofwork.TiposPersonas.GetAllAsync();
        return mapper.Map<List<TipoPersona>>(TipoPersona);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<TipoPersona>>> Get([FromQuery]Params TipoPersonaParams)
    {
        var TipoPersona = await unitofwork.TiposPersonas.GetAllAsync(TipoPersonaParams.PageIndex,TipoPersonaParams.PageSize, TipoPersonaParams.Search);
        var listaTipoPersonas = mapper.Map<List<TipoPersona>>(TipoPersona.registros);
        return new Pager<TipoPersona>(listaTipoPersonas, TipoPersona.totalRegistros,TipoPersonaParams.PageIndex,TipoPersonaParams.PageSize,TipoPersonaParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoPersona>> Get(int id)
    {
        var TipoPersona = await unitofwork.TiposPersonas.GetByIdAsync(id);
        return mapper.Map<TipoPersona>(TipoPersona);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoPersona>> Post(TipoPersona TipoPersonaDto)
    {
        var TipoPersona = this.mapper.Map<TipoPersona>(TipoPersonaDto);
        this.unitofwork.TiposPersonas.Add(TipoPersona);
        await unitofwork.SaveAsync();
        if (TipoPersona == null){
            return BadRequest();
        }
        TipoPersonaDto.Id = TipoPersona.Id;
        return CreatedAtAction(nameof(Post), new { id = TipoPersonaDto.Id }, TipoPersonaDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoPersona>> Put (int id, [FromBody]TipoPersona TipoPersonaDto)
    {
        if(TipoPersonaDto == null)
            return NotFound();

        var TipoPersona = this.mapper.Map<TipoPersona>(TipoPersonaDto);
        unitofwork.TiposPersonas.Update(TipoPersona);
        await unitofwork.SaveAsync();
        return TipoPersonaDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var TipoPersona = await unitofwork.TiposPersonas.GetByIdAsync(id);

        if (TipoPersona == null)
        {
            return Notfound();
        }

        unitofwork.TiposPersonas.Remove(TipoPersona);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}