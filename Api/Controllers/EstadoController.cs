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

public class EstadoController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public EstadoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Estado>>> Get0()
    {
        var Estado = await unitofwork.Estados.GetAllAsync();
        return mapper.Map<List<Estado>>(Estado);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Estado>>> Get([FromQuery]Params EstadoParams)
    {
        var Estado = await unitofwork.Estados.GetAllAsync(EstadoParams.PageIndex,EstadoParams.PageSize, EstadoParams.Search);
        var listaEstados = mapper.Map<List<Estado>>(Estado.registros);
        return new Pager<Estado>(listaEstados, Estado.totalRegistros,EstadoParams.PageIndex,EstadoParams.PageSize,EstadoParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Estado>> Get(int id)
    {
        var Estado = await unitofwork.Estados.GetByIdAsync(id);
        return mapper.Map<Estado>(Estado);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Estado>> Post(Estado EstadoDto)
    {
        var Estado = this.mapper.Map<Estado>(EstadoDto);
        this.unitofwork.Estados.Add(Estado);
        await unitofwork.SaveAsync();
        if (Estado == null){
            return BadRequest();
        }
        EstadoDto.Id = Estado.Id;
        return CreatedAtAction(nameof(Post), new { id = EstadoDto.Id }, EstadoDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Estado>> Put (int id, [FromBody]Estado EstadoDto)
    {
        if(EstadoDto == null)
            return NotFound();

        var Estado = this.mapper.Map<Estado>(EstadoDto);
        unitofwork.Estados.Update(Estado);
        await unitofwork.SaveAsync();
        return EstadoDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Estado = await unitofwork.Estados.GetByIdAsync(id);

        if (Estado == null)
        {
            return Notfound();
        }

        unitofwork.Estados.Remove(Estado);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}