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

public class MunicipioController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public MunicipioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Municipio>>> Get0()
    {
        var Municipio = await unitofwork.Municipios.GetAllAsync();
        return mapper.Map<List<Municipio>>(Municipio);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Municipio>>> Get([FromQuery]Params MunicipioParams)
    {
        var Municipio = await unitofwork.Municipios.GetAllAsync(MunicipioParams.PageIndex,MunicipioParams.PageSize, MunicipioParams.Search);
        var listaMunicipios = mapper.Map<List<Municipio>>(Municipio.registros);
        return new Pager<Municipio>(listaMunicipios, Municipio.totalRegistros,MunicipioParams.PageIndex,MunicipioParams.PageSize,MunicipioParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Municipio>> Get(int id)
    {
        var Municipio = await unitofwork.Municipios.GetByIdAsync(id);
        return mapper.Map<Municipio>(Municipio);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Municipio>> Post(Municipio MunicipioDto)
    {
        var Municipio = this.mapper.Map<Municipio>(MunicipioDto);
        this.unitofwork.Municipios.Add(Municipio);
        await unitofwork.SaveAsync();
        if (Municipio == null){
            return BadRequest();
        }
        MunicipioDto.Id = Municipio.Id;
        return CreatedAtAction(nameof(Post), new { id = MunicipioDto.Id }, MunicipioDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Municipio>> Put (int id, [FromBody]Municipio MunicipioDto)
    {
        if(MunicipioDto == null)
            return NotFound();

        var Municipio = this.mapper.Map<Municipio>(MunicipioDto);
        unitofwork.Municipios.Update(Municipio);
        await unitofwork.SaveAsync();
        return MunicipioDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Municipio = await unitofwork.Municipios.GetByIdAsync(id);

        if (Municipio == null)
        {
            return Notfound();
        }

        unitofwork.Municipios.Remove(Municipio);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}