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

public class InventarioController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public InventarioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Inventario>>> Get0()
    {
        var Inventario = await unitofwork.Inventarios.GetAllAsync();
        return mapper.Map<List<Inventario>>(Inventario);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Inventario>>> Get([FromQuery]Params InventarioParams)
    {
        var Inventario = await unitofwork.Inventarios.GetAllAsync(InventarioParams.PageIndex,InventarioParams.PageSize, InventarioParams.Search);
        var listaInventarios = mapper.Map<List<Inventario>>(Inventario.registros);
        return new Pager<Inventario>(listaInventarios, Inventario.totalRegistros,InventarioParams.PageIndex,InventarioParams.PageSize,InventarioParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Inventario>> Get(int id)
    {
        var Inventario = await unitofwork.Inventarios.GetByIdAsync(id);
        return mapper.Map<Inventario>(Inventario);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Inventario>> Post(Inventario InventarioDto)
    {
        var Inventario = this.mapper.Map<Inventario>(InventarioDto);
        this.unitofwork.Inventarios.Add(Inventario);
        await unitofwork.SaveAsync();
        if (Inventario == null){
            return BadRequest();
        }
        InventarioDto.Id = Inventario.Id;
        return CreatedAtAction(nameof(Post), new { id = InventarioDto.Id }, InventarioDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Inventario>> Put (int id, [FromBody]Inventario InventarioDto)
    {
        if(InventarioDto == null)
            return NotFound();

        var Inventario = this.mapper.Map<Inventario>(InventarioDto);
        unitofwork.Inventarios.Update(Inventario);
        await unitofwork.SaveAsync();
        return InventarioDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Inventario = await unitofwork.Inventarios.GetByIdAsync(id);

        if (Inventario == null)
        {
            return Notfound();
        }

        unitofwork.Inventarios.Remove(Inventario);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("prendasYTallas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InventarioDto>>> GetTallasPrendas()
    {
        var prendasTallas = await unitofwork.Inventarios.productosYTallas();
        return mapper.Map<List<InventarioDto>>(prendasTallas);
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}