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

public class ColorController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public ColorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Color>>> Get0()
    {
        var color = await unitofwork.Colores.GetAllAsync();
        return mapper.Map<List<Color>>(color);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Color>>> Get([FromQuery]Params colorParams)
    {
        var color = await unitofwork.Colores.GetAllAsync(colorParams.PageIndex,colorParams.PageSize, colorParams.Search);
        var listaColores = mapper.Map<List<Color>>(color.registros);
        return new Pager<Color>(listaColores, color.totalRegistros,colorParams.PageIndex,colorParams.PageSize,colorParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Color>> Get(int id)
    {
        var color = await unitofwork.Colores.GetByIdAsync(id);
        return mapper.Map<Color>(color);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Color>> Post(Color colorDto)
    {
        var color = this.mapper.Map<Color>(colorDto);
        this.unitofwork.Colores.Add(color);
        await unitofwork.SaveAsync();
        if (color == null){
            return BadRequest();
        }
        colorDto.Id = color.Id;
        return CreatedAtAction(nameof(Post), new { id = colorDto.Id }, colorDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Color>> Put (int id, [FromBody]Color colorDto)
    {
        if(colorDto == null)
            return NotFound();

        var color = this.mapper.Map<Color>(colorDto);
        unitofwork.Colores.Update(color);
        await unitofwork.SaveAsync();
        return colorDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var color = await unitofwork.Colores.GetByIdAsync(id);

        if (color == null)
        {
            return Notfound();
        }

        unitofwork.Colores.Remove(color);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}