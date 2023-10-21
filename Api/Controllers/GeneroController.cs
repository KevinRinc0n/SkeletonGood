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

public class GeneroController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public GeneroController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Genero>>> Get0()
    {
        var Genero = await unitofwork.Generos.GetAllAsync();
        return mapper.Map<List<Genero>>(Genero);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Genero>>> Get([FromQuery]Params GeneroParams)
    {
        var Genero = await unitofwork.Generos.GetAllAsync(GeneroParams.PageIndex,GeneroParams.PageSize, GeneroParams.Search);
        var listaGeneros = mapper.Map<List<Genero>>(Genero.registros);
        return new Pager<Genero>(listaGeneros, Genero.totalRegistros,GeneroParams.PageIndex,GeneroParams.PageSize,GeneroParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Genero>> Get(int id)
    {
        var Genero = await unitofwork.Generos.GetByIdAsync(id);
        return mapper.Map<Genero>(Genero);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Genero>> Post(Genero GeneroDto)
    {
        var Genero = this.mapper.Map<Genero>(GeneroDto);
        this.unitofwork.Generos.Add(Genero);
        await unitofwork.SaveAsync();
        if (Genero == null){
            return BadRequest();
        }
        GeneroDto.Id = Genero.Id;
        return CreatedAtAction(nameof(Post), new { id = GeneroDto.Id }, GeneroDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Genero>> Put (int id, [FromBody]Genero GeneroDto)
    {
        if(GeneroDto == null)
            return NotFound();

        var Genero = this.mapper.Map<Genero>(GeneroDto);
        unitofwork.Generos.Update(Genero);
        await unitofwork.SaveAsync();
        return GeneroDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Genero = await unitofwork.Generos.GetByIdAsync(id);

        if (Genero == null)
        {
            return Notfound();
        }

        unitofwork.Generos.Remove(Genero);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}