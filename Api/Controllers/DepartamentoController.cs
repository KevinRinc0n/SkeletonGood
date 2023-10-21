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

public class DepartamentoController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Departamento>>> Get0()
    {
        var departamento = await unitofwork.Departamentos.GetAllAsync();
        return mapper.Map<List<Departamento>>(departamento);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Departamento>>> Get([FromQuery]Params departamentoParams)
    {
        var departamento = await unitofwork.Departamentos.GetAllAsync(departamentoParams.PageIndex,departamentoParams.PageSize, departamentoParams.Search);
        var listaDepartamentos = mapper.Map<List<Departamento>>(departamento.registros);
        return new Pager<Departamento>(listaDepartamentos, departamento.totalRegistros,departamentoParams.PageIndex,departamentoParams.PageSize,departamentoParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Departamento>> Get(int id)
    {
        var departamento = await unitofwork.Departamentos.GetByIdAsync(id);
        return mapper.Map<Departamento>(departamento);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Departamento>> Post(Departamento departamentoDto)
    {
        var departamento = this.mapper.Map<Departamento>(departamentoDto);
        this.unitofwork.Departamentos.Add(departamento);
        await unitofwork.SaveAsync();
        if (departamento == null){
            return BadRequest();
        }
        departamentoDto.Id = departamento.Id;
        return CreatedAtAction(nameof(Post), new { id = departamentoDto.Id }, departamentoDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Departamento>> Put (int id, [FromBody]Departamento departamentoDto)
    {
        if(departamentoDto == null)
            return NotFound();

        var departamento = this.mapper.Map<Departamento>(departamentoDto);
        unitofwork.Departamentos.Update(departamento);
        await unitofwork.SaveAsync();
        return departamentoDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var departamento = await unitofwork.Departamentos.GetByIdAsync(id);

        if (departamento == null)
        {
            return Notfound();
        }

        unitofwork.Departamentos.Remove(departamento);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}