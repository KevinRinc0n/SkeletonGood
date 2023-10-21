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

public class InsumoController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public InsumoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Insumo>>> Get0()
    {
        var Insumo = await unitofwork.Insumos.GetAllAsync();
        return mapper.Map<List<Insumo>>(Insumo);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Insumo>>> Get([FromQuery]Params InsumoParams)
    {
        var Insumo = await unitofwork.Insumos.GetAllAsync(InsumoParams.PageIndex,InsumoParams.PageSize, InsumoParams.Search);
        var listaInsumos = mapper.Map<List<Insumo>>(Insumo.registros);
        return new Pager<Insumo>(listaInsumos, Insumo.totalRegistros,InsumoParams.PageIndex,InsumoParams.PageSize,InsumoParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Insumo>> Get(int id)
    {
        var Insumo = await unitofwork.Insumos.GetByIdAsync(id);
        return mapper.Map<Insumo>(Insumo);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Insumo>> Post(Insumo InsumoDto)
    {
        var Insumo = this.mapper.Map<Insumo>(InsumoDto);
        this.unitofwork.Insumos.Add(Insumo);
        await unitofwork.SaveAsync();
        if (Insumo == null){
            return BadRequest();
        }
        InsumoDto.Id = Insumo.Id;
        return CreatedAtAction(nameof(Post), new { id = InsumoDto.Id }, InsumoDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Insumo>> Put (int id, [FromBody]Insumo InsumoDto)
    {
        if(InsumoDto == null)
            return NotFound();

        var Insumo = this.mapper.Map<Insumo>(InsumoDto);
        unitofwork.Insumos.Update(Insumo);
        await unitofwork.SaveAsync();
        return InsumoDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Insumo = await unitofwork.Insumos.GetByIdAsync(id);

        if (Insumo == null)
        {
            return Notfound();
        }

        unitofwork.Insumos.Remove(Insumo);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    // [HttpGet("insumosXPrenda")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<IEnumerable<Insumo>>> GetCodigo(int CodigoDeterminado)
    // {
    //     var insumosPorPrenda = await unitofwork.Insumos.mostrarDeterminado(CodigoDeterminado);
    //     return mapper.Map<List<Insumo>>(insumosPorPrenda);
    // }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}