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

public class InventarioTallaController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public InventarioTallaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<InventarioTalla>>> Get0()
    {
        var InventarioTalla = await unitofwork.InventariosTallas.GetAllAsync();
        return mapper.Map<List<InventarioTalla>>(InventarioTalla);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<InventarioTalla>>> Get([FromQuery]Params InventarioTallaParams)
    {
        var InventarioTalla = await unitofwork.InventariosTallas.GetAllAsync(InventarioTallaParams.PageIndex,InventarioTallaParams.PageSize, InventarioTallaParams.Search);
        var listaInventarioTallas = mapper.Map<List<InventarioTalla>>(InventarioTalla.registros);
        return new Pager<InventarioTalla>(listaInventarioTallas, InventarioTalla.totalRegistros,InventarioTallaParams.PageIndex,InventarioTallaParams.PageSize,InventarioTallaParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventarioTalla>> Get(int id)
    {
        var InventarioTalla = await unitofwork.InventariosTallas.GetByIdAsync(id);
        return mapper.Map<InventarioTalla>(InventarioTalla);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventarioTalla>> Post(InventarioTalla InventarioTallaDto)
    {
        var InventarioTalla = this.mapper.Map<InventarioTalla>(InventarioTallaDto);
        this.unitofwork.InventariosTallas.Add(InventarioTalla);
        await unitofwork.SaveAsync();
        if (InventarioTalla == null){
            return BadRequest();
        }
        InventarioTallaDto.Id = InventarioTalla.Id;
        return CreatedAtAction(nameof(Post), new { id = InventarioTallaDto.Id }, InventarioTallaDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<InventarioTalla>> Put (int id, [FromBody]InventarioTalla InventarioTallaDto)
    {
        if(InventarioTallaDto == null)
            return NotFound();

        var InventarioTalla = this.mapper.Map<InventarioTalla>(InventarioTallaDto);
        unitofwork.InventariosTallas.Update(InventarioTalla);
        await unitofwork.SaveAsync();
        return InventarioTallaDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var InventarioTalla = await unitofwork.InventariosTallas.GetByIdAsync(id);

        if (InventarioTalla == null)
        {
            return Notfound();
        }

        unitofwork.InventariosTallas.Remove(InventarioTalla);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}