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

public class PaisController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PaisController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Pais>>> Get0()
    {
        var Pais = await unitofwork.Paises.GetAllAsync();
        return mapper.Map<List<Pais>>(Pais);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Pais>>> Get([FromQuery]Params PaisParams)
    {
        var Pais = await unitofwork.Paises.GetAllAsync(PaisParams.PageIndex,PaisParams.PageSize, PaisParams.Search);
        var listaPaiss = mapper.Map<List<Pais>>(Pais.registros);
        return new Pager<Pais>(listaPaiss, Pais.totalRegistros,PaisParams.PageIndex,PaisParams.PageSize,PaisParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pais>> Get(int id)
    {
        var Pais = await unitofwork.Paises.GetByIdAsync(id);
        return mapper.Map<Pais>(Pais);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pais>> Post(Pais PaisDto)
    {
        var Pais = this.mapper.Map<Pais>(PaisDto);
        this.unitofwork.Paises.Add(Pais);
        await unitofwork.SaveAsync();
        if (Pais == null){
            return BadRequest();
        }
        PaisDto.Id = Pais.Id;
        return CreatedAtAction(nameof(Post), new { id = PaisDto.Id }, PaisDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pais>> Put (int id, [FromBody]Pais PaisDto)
    {
        if(PaisDto == null)
            return NotFound();

        var Pais = this.mapper.Map<Pais>(PaisDto);
        unitofwork.Paises.Update(Pais);
        await unitofwork.SaveAsync();
        return PaisDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Pais = await unitofwork.Paises.GetByIdAsync(id);

        if (Pais == null)
        {
            return Notfound();
        }

        unitofwork.Paises.Remove(Pais);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}