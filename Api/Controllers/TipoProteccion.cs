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

public class TipoProteccionController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TipoProteccionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoProteccion>>> Get0()
    {
        var TipoProteccion = await unitofwork.TiposProtecciones.GetAllAsync();
        return mapper.Map<List<TipoProteccion>>(TipoProteccion);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<TipoProteccion>>> Get([FromQuery]Params TipoProteccionParams)
    {
        var TipoProteccion = await unitofwork.TiposProtecciones.GetAllAsync(TipoProteccionParams.PageIndex,TipoProteccionParams.PageSize, TipoProteccionParams.Search);
        var listaTipoProteccions = mapper.Map<List<TipoProteccion>>(TipoProteccion.registros);
        return new Pager<TipoProteccion>(listaTipoProteccions, TipoProteccion.totalRegistros,TipoProteccionParams.PageIndex,TipoProteccionParams.PageSize,TipoProteccionParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoProteccion>> Get(int id)
    {
        var TipoProteccion = await unitofwork.TiposProtecciones.GetByIdAsync(id);
        return mapper.Map<TipoProteccion>(TipoProteccion);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoProteccion>> Post(TipoProteccion TipoProteccionDto)
    {
        var TipoProteccion = this.mapper.Map<TipoProteccion>(TipoProteccionDto);
        this.unitofwork.TiposProtecciones.Add(TipoProteccion);
        await unitofwork.SaveAsync();
        if (TipoProteccion == null){
            return BadRequest();
        }
        TipoProteccionDto.Id = TipoProteccion.Id;
        return CreatedAtAction(nameof(Post), new { id = TipoProteccionDto.Id }, TipoProteccionDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoProteccion>> Put (int id, [FromBody]TipoProteccion TipoProteccionDto)
    {
        if(TipoProteccionDto == null)
            return NotFound();

        var TipoProteccion = this.mapper.Map<TipoProteccion>(TipoProteccionDto);
        unitofwork.TiposProtecciones.Update(TipoProteccion);
        await unitofwork.SaveAsync();
        return TipoProteccionDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var TipoProteccion = await unitofwork.TiposProtecciones.GetByIdAsync(id);

        if (TipoProteccion == null)
        {
            return Notfound();
        }

        unitofwork.TiposProtecciones.Remove(TipoProteccion);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}