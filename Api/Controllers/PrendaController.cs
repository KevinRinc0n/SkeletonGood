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

public class PrendaController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PrendaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Prenda>>> Get0()
    {
        var Prenda = await unitofwork.Prendas.GetAllAsync();
        return mapper.Map<List<Prenda>>(Prenda);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Prenda>>> Get([FromQuery]Params PrendaParams)
    {
        var Prenda = await unitofwork.Prendas.GetAllAsync(PrendaParams.PageIndex,PrendaParams.PageSize, PrendaParams.Search);
        var listaPrendas = mapper.Map<List<Prenda>>(Prenda.registros);
        return new Pager<Prenda>(listaPrendas, Prenda.totalRegistros,PrendaParams.PageIndex,PrendaParams.PageSize,PrendaParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Prenda>> Get(int id)
    {
        var Prenda = await unitofwork.Prendas.GetByIdAsync(id);
        return mapper.Map<Prenda>(Prenda);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Prenda>> Post(Prenda PrendaDto)
    {
        var Prenda = this.mapper.Map<Prenda>(PrendaDto);
        this.unitofwork.Prendas.Add(Prenda);
        await unitofwork.SaveAsync();
        if (Prenda == null){
            return BadRequest();
        }
        PrendaDto.Id = Prenda.Id;
        return CreatedAtAction(nameof(Post), new { id = PrendaDto.Id }, PrendaDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Prenda>> Put (int id, [FromBody]Prenda PrendaDto)
    {
        if(PrendaDto == null)
            return NotFound();

        var Prenda = this.mapper.Map<Prenda>(PrendaDto);
        unitofwork.Prendas.Update(Prenda);
        await unitofwork.SaveAsync();
        return PrendaDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Prenda = await unitofwork.Prendas.GetByIdAsync(id);

        if (Prenda == null)
        {
            return Notfound();
        }

        unitofwork.Prendas.Remove(Prenda);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}