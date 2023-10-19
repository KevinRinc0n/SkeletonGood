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

public class porDefecto1Controller : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public porDefecto1Controller(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<porDefecto1Dto>>> Get0()
    {
        var porDefecto1 = await unitofwork.porDefectos1.GetAllAsync();
        return mapper.Map<List<porDefecto1Dto>>(porDefecto1);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<porDefecto1>>> Get([FromQuery]Params porDefecto1Params)
    {
        var porDefecto1 = await unitofwork.porDefectos1.GetAllAsync(porDefecto1Params.PageIndex,porDefecto1Params.PageSize, porDefecto1Params.Search);
        var listaPorDefecto1Params = mapper.Map<List<porDefecto1>>(porDefecto1.registros);
        return new Pager<porDefecto1>(listaPorDefecto1Params, porDefecto1.totalRegistros,porDefecto1Params.PageIndex,porDefecto1Params.PageSize,porDefecto1Params.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<porDefecto1>> Get(int id)
    {
        var porDefecto1 = await unitofwork.porDefectos1.GetByIdAsync(id);
        return mapper.Map<porDefecto1>(porDefecto1);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<porDefecto1>> Post(porDefecto1 porDefecto1Dto)
    {
        var porDefecto1 = this.mapper.Map<porDefecto1>(porDefecto1Dto);
        this.unitofwork.porDefectos1.Add(porDefecto1);
        await unitofwork.SaveAsync();
        if (porDefecto1 == null){
            return BadRequest();
        }
        porDefecto1Dto.Id = porDefecto1.Id;
        return CreatedAtAction(nameof(Post), new { id = porDefecto1Dto.Id }, porDefecto1Dto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<porDefecto1>> Put (int id, [FromBody]porDefecto1 porDefecto1Dto)
    {
        if(porDefecto1Dto == null)
            return NotFound();

        var porDefecto1 = this.mapper.Map<porDefecto1>(porDefecto1Dto);
        unitofwork.porDefectos1.Update(porDefecto1);
        await unitofwork.SaveAsync();
        return porDefecto1Dto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var porDefecto1 = await unitofwork.porDefectos1.GetByIdAsync(id);

        if (porDefecto1 == null)
        {
            return Notfound();
        }

        unitofwork.porDefectos1.Remove(porDefecto1);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}