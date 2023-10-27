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

public class EmpleadoController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Empleado>>> Get0()
    {
        var Empleado = await unitofwork.Empleados.GetAllAsync();
        return mapper.Map<List<Empleado>>(Empleado);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Empleado>>> Get([FromQuery]Params EmpleadoParams)
    {
        var Empleado = await unitofwork.Empleados.GetAllAsync(EmpleadoParams.PageIndex,EmpleadoParams.PageSize, EmpleadoParams.Search);
        var listaEmpleados = mapper.Map<List<Empleado>>(Empleado.registros);
        return new Pager<Empleado>(listaEmpleados, Empleado.totalRegistros,EmpleadoParams.PageIndex,EmpleadoParams.PageSize,EmpleadoParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Empleado>> Get(int id)
    {
        var Empleado = await unitofwork.Empleados.GetByIdAsync(id);
        return mapper.Map<Empleado>(Empleado);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Empleado>> Post(Empleado EmpleadoDto)
    {
        var Empleado = this.mapper.Map<Empleado>(EmpleadoDto);
        this.unitofwork.Empleados.Add(Empleado);
        await unitofwork.SaveAsync();
        if (Empleado == null){
            return BadRequest();
        }
        EmpleadoDto.Id = Empleado.Id;
        return CreatedAtAction(nameof(Post), new { id = EmpleadoDto.Id }, EmpleadoDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Empleado>> Put (int id, [FromBody]Empleado EmpleadoDto)
    {
        if(EmpleadoDto == null)
            return NotFound();

        var Empleado = this.mapper.Map<Empleado>(EmpleadoDto);
        unitofwork.Empleados.Update(Empleado);
        await unitofwork.SaveAsync();
        return EmpleadoDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var Empleado = await unitofwork.Empleados.GetByIdAsync(id);

        if (Empleado == null)
        {
            return Notfound();
        }

        unitofwork.Empleados.Remove(Empleado);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("ventas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> GetNombre(int empleadoId)
    {
        var empleadosXCargo = await unitofwork.Empleados.ventas(empleadoId);
        return mapper.Map<List<EmpleadoDto>>(empleadosXCargo);
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}