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

public class EmpresaController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public EmpresaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Empresa>>> Get0()
    {
        var Empresa = await unitofwork.Empresas.GetAllAsync();
        return mapper.Map<List<Empresa>>(Empresa);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Empresa>>> Get([FromQuery]Params EmpresaParams)
    {
        var Empresa = await unitofwork.Empresas.GetAllAsync(EmpresaParams.PageIndex,EmpresaParams.PageSize, EmpresaParams.Search);
        var listaEmpresas = mapper.Map<List<Empresa>>(Empresa.registros);
        return new Pager<Empresa>(listaEmpresas, Empresa.totalRegistros,EmpresaParams.PageIndex,EmpresaParams.PageSize,EmpresaParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Empresa>> Get(int id)
    {
        var Empresa = await unitofwork.Empresas.GetByIdAsync(id);
        return mapper.Map<Empresa>(Empresa);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Empresa>> Post(Empresa EmpresaDto)
    {
        var Empresa = this.mapper.Map<Empresa>(EmpresaDto);
        this.unitofwork.Empresas.Add(Empresa);
        await unitofwork.SaveAsync();
        if (Empresa == null){
            return BadRequest();
        }
        EmpresaDto.Id = Empresa.Id;
        return CreatedAtAction(nameof(Post), new { id = EmpresaDto.Id }, EmpresaDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Empresa>> Put (int id, [FromBody]Empresa EmpresaDto)
    {
        if(EmpresaDto == null)
            return NotFound();

        var Empresa = this.mapper.Map<Empresa>(EmpresaDto);
        unitofwork.Empresas.Update(Empresa);
        await unitofwork.SaveAsync();
        return EmpresaDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Empresa = await unitofwork.Empresas.GetByIdAsync(id);

        if (Empresa == null)
        {
            return Notfound();
        }

        unitofwork.Empresas.Remove(Empresa);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}