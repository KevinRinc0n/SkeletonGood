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

public class FormaPagoController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public FormaPagoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<FormaPago>>> Get0()
    {
        var FormaPago = await unitofwork.FormaPagos.GetAllAsync();
        return mapper.Map<List<FormaPago>>(FormaPago);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<FormaPago>>> Get([FromQuery]Params FormaPagoParams)
    {
        var FormaPago = await unitofwork.FormaPagos.GetAllAsync(FormaPagoParams.PageIndex,FormaPagoParams.PageSize, FormaPagoParams.Search);
        var listaFormaPagos = mapper.Map<List<FormaPago>>(FormaPago.registros);
        return new Pager<FormaPago>(listaFormaPagos, FormaPago.totalRegistros,FormaPagoParams.PageIndex,FormaPagoParams.PageSize,FormaPagoParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FormaPago>> Get(int id)
    {
        var FormaPago = await unitofwork.FormaPagos.GetByIdAsync(id);
        return mapper.Map<FormaPago>(FormaPago);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FormaPago>> Post(FormaPago FormaPagoDto)
    {
        var FormaPago = this.mapper.Map<FormaPago>(FormaPagoDto);
        this.unitofwork.FormaPagos.Add(FormaPago);
        await unitofwork.SaveAsync();
        if (FormaPago == null){
            return BadRequest();
        }
        FormaPagoDto.Id = FormaPago.Id;
        return CreatedAtAction(nameof(Post), new { id = FormaPagoDto.Id }, FormaPagoDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<FormaPago>> Put (int id, [FromBody]FormaPago FormaPagoDto)
    {
        if(FormaPagoDto == null)
            return NotFound();

        var FormaPago = this.mapper.Map<FormaPago>(FormaPagoDto);
        unitofwork.FormaPagos.Update(FormaPago);
        await unitofwork.SaveAsync();
        return FormaPagoDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var FormaPago = await unitofwork.FormaPagos.GetByIdAsync(id);

        if (FormaPago == null)
        {
            return Notfound();
        }

        unitofwork.FormaPagos.Remove(FormaPago);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}