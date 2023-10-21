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

public class TallaController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TallaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Talla>>> Get0()
    {
        var Talla = await unitofwork.Tallas.GetAllAsync();
        return mapper.Map<List<Talla>>(Talla);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Talla>>> Get([FromQuery]Params TallaParams)
    {
        var Talla = await unitofwork.Tallas.GetAllAsync(TallaParams.PageIndex,TallaParams.PageSize, TallaParams.Search);
        var listaTallas = mapper.Map<List<Talla>>(Talla.registros);
        return new Pager<Talla>(listaTallas, Talla.totalRegistros,TallaParams.PageIndex,TallaParams.PageSize,TallaParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Talla>> Get(int id)
    {
        var Talla = await unitofwork.Tallas.GetByIdAsync(id);
        return mapper.Map<Talla>(Talla);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Talla>> Post(Talla TallaDto)
    {
        var Talla = this.mapper.Map<Talla>(TallaDto);
        this.unitofwork.Tallas.Add(Talla);
        await unitofwork.SaveAsync();
        if (Talla == null){
            return BadRequest();
        }
        TallaDto.Id = Talla.Id;
        return CreatedAtAction(nameof(Post), new { id = TallaDto.Id }, TallaDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Talla>> Put (int id, [FromBody]Talla TallaDto)
    {
        if(TallaDto == null)
            return NotFound();

        var Talla = this.mapper.Map<Talla>(TallaDto);
        unitofwork.Tallas.Update(Talla);
        await unitofwork.SaveAsync();
        return TallaDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Talla = await unitofwork.Tallas.GetByIdAsync(id);

        if (Talla == null)
        {
            return Notfound();
        }

        unitofwork.Tallas.Remove(Talla);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}