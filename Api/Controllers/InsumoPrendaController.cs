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

public class InsumoPrendaController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public InsumoPrendaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<InsumoPrenda>>> Get0()
    {
        var InsumoPrenda = await unitofwork.InsumosPrendas.GetAllAsync();
        return mapper.Map<List<InsumoPrenda>>(InsumoPrenda);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<InsumoPrenda>>> Get([FromQuery]Params InsumoPrendaParams)
    {
        var InsumoPrenda = await unitofwork.InsumosPrendas.GetAllAsync(InsumoPrendaParams.PageIndex,InsumoPrendaParams.PageSize, InsumoPrendaParams.Search);
        var listaInsumoPrendas = mapper.Map<List<InsumoPrenda>>(InsumoPrenda.registros);
        return new Pager<InsumoPrenda>(listaInsumoPrendas, InsumoPrenda.totalRegistros,InsumoPrendaParams.PageIndex,InsumoPrendaParams.PageSize,InsumoPrendaParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InsumoPrenda>> Get(int id)
    {
        var InsumoPrenda = await unitofwork.InsumosPrendas.GetByIdAsync(id);
        return mapper.Map<InsumoPrenda>(InsumoPrenda);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InsumoPrenda>> Post(InsumoPrenda InsumoPrendaDto)
    {
        var InsumoPrenda = this.mapper.Map<InsumoPrenda>(InsumoPrendaDto);
        this.unitofwork.InsumosPrendas.Add(InsumoPrenda);
        await unitofwork.SaveAsync();
        if (InsumoPrenda == null){
            return BadRequest();
        }
        InsumoPrendaDto.Id = InsumoPrenda.Id;
        return CreatedAtAction(nameof(Post), new { id = InsumoPrendaDto.Id }, InsumoPrendaDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<InsumoPrenda>> Put (int id, [FromBody]InsumoPrenda InsumoPrendaDto)
    {
        if(InsumoPrendaDto == null)
            return NotFound();

        var InsumoPrenda = this.mapper.Map<InsumoPrenda>(InsumoPrendaDto);
        unitofwork.InsumosPrendas.Update(InsumoPrenda);
        await unitofwork.SaveAsync();
        return InsumoPrendaDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var InsumoPrenda = await unitofwork.InsumosPrendas.GetByIdAsync(id);

        if (InsumoPrenda == null)
        {
            return Notfound();
        }

        unitofwork.InsumosPrendas.Remove(InsumoPrenda);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}