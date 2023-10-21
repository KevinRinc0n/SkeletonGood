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

public class CargoController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public CargoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Cargo>>> Get0()
    {
        var cargo = await unitofwork.Cargos.GetAllAsync();
        return mapper.Map<List<Cargo>>(cargo);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Cargo>>> Get([FromQuery]Params cargoParams)
    {
        var cargo = await unitofwork.Cargos.GetAllAsync(cargoParams.PageIndex,cargoParams.PageSize, cargoParams.Search);
        var listaCargos = mapper.Map<List<Cargo>>(cargo.registros);
        return new Pager<Cargo>(listaCargos, cargo.totalRegistros,cargoParams.PageIndex,cargoParams.PageSize,cargoParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Cargo>> Get(int id)
    {
        var cargo = await unitofwork.Cargos.GetByIdAsync(id);
        return mapper.Map<Cargo>(cargo);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cargo>> Post(Cargo cargoDto)
    {
        var cargo = this.mapper.Map<Cargo>(cargoDto);
        this.unitofwork.Cargos.Add(cargo);
        await unitofwork.SaveAsync();
        if (cargo == null){
            return BadRequest();
        }
        cargoDto.Id = cargo.Id;
        return CreatedAtAction(nameof(Post), new { id = cargoDto.Id }, cargoDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Cargo>> Put (int id, [FromBody]Cargo cargoDto)
    {
        if(cargoDto == null)
            return NotFound();

        var cargo = this.mapper.Map<Cargo>(cargoDto);
        unitofwork.Cargos.Update(cargo);
        await unitofwork.SaveAsync();
        return cargoDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var cargo = await unitofwork.Cargos.GetByIdAsync(id);

        if (cargo == null)
        {
            return Notfound();
        }

        unitofwork.Cargos.Remove(cargo);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }

    [HttpGet("XCargo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CargoDto>>> GetNombre(string NombreDeterminado)
    {
        var empleadosXCargo = await unitofwork.Cargos.mostrarDeterminado(NombreDeterminado);
        return mapper.Map<List<CargoDto>>(empleadosXCargo);
    }
}