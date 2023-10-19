// using Dominio.Entities;
// using AutoMapper;
// using Dominio.Interfaces;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;
// using Api.Helpers;
// using Api.Dtos;
// using System.Text.Json;
// using System.Text.Json.Serialization;

// namespace Api.Controllers;

// [ApiVersion("1.0")]
// [ApiVersion("1.1")]

// public class porDefecto2Controller : BaseApiController
// {
//     private IUnitOfWork unitofwork;
//     private readonly IMapper mapper;

//     public porDefecto2Controller(IUnitOfWork unitOfWork, IMapper mapper)
//     {
//         this.unitofwork = unitOfWork;
//         this.mapper = mapper;
//     } 

//     [HttpGet]
//     [MapToApiVersion("1.0")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]

//     public async Task<ActionResult<IEnumerable<porDefecto2Dto>>> Get0()
//     {
//         var porDefecto2 = await unitofwork.porDefectos2.GetAllAsync();
//         return mapper.Map<List<porDefecto2Dto>>(porDefecto2);
//     }

//     [HttpGet]
//     [MapToApiVersion("1.1")]
//     [Authorize (Roles= "Administrador")]    
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]

//     public async Task<ActionResult<Pager<porDefecto2>>> Get([FromQuery]Params porDefecto2Params)
//     {
//         var porDefecto2 = await unitofwork.porDefectos2.GetAllAsync(porDefecto2Params.PageIndex,porDefecto2Params.PageSize, porDefecto2Params.Search);
//         var listaporDefecto2 = mapper.Map<List<porDefecto2>>(porDefecto2.registros);
//         return new Pager<porDefecto2>(listaporDefecto2, porDefecto2.totalRegistros,porDefecto2Params.PageIndex,porDefecto2Params.PageSize,porDefecto2Params.Search);
//     }

//     [HttpGet("{id}")]
//     [Authorize (Roles= "Administrador")]    
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     public async Task<ActionResult<porDefecto2>> Get(int id)
//     {
//         var porDefecto2 = await unitofwork.porDefectos2.GetByIdAsync(id);
//         return mapper.Map<porDefecto2>(porDefecto2);
//     }

//     [HttpPost]
//     [Authorize (Roles= "Administrador")]    
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     public async Task<ActionResult<porDefecto2>> Post(porDefecto2 porDefecto2Dto)
//     {
//         var porDefecto2 = this.mapper.Map<porDefecto2>(porDefecto2Dto);
//         this.unitofwork.porDefectos2.Add(porDefecto2);
//         await unitofwork.SaveAsync();
//         if (porDefecto2 == null){
//             return BadRequest();
//         }
//         porDefecto2Dto.Id = porDefecto2.Id;
//         return CreatedAtAction(nameof(Post), new { id = porDefecto2Dto.Id }, porDefecto2Dto);
//     }

//     [HttpPut]
//     [Authorize (Roles= "Administrador")]    
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]

//     public async Task<ActionResult<porDefecto2>> Put (int id, [FromBody]porDefecto2 porDefecto2Dto)
//     {
//         if(porDefecto2Dto == null)
//             return NotFound();

//         var porDefecto2 = this.mapper.Map<porDefecto2>(porDefecto2Dto);
//         unitofwork.porDefectos2.Update(porDefecto2);
//         await unitofwork.SaveAsync();
//         return porDefecto2Dto;     
//     }

//     [HttpDelete("{id}")]
//     [Authorize (Roles= "Administrador")]    
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     [ProducesResponseType(StatusCodes.Status204NoContent)]

//     public async Task<ActionResult> Delete (int id)
//     {
//         var porDefecto2 = await unitofwork.porDefectos2.GetByIdAsync(id);

//         if (porDefecto2 == null)
//         {
//             return Notfound();
//         }

//         unitofwork.porDefectos2.Remove(porDefecto2);
//         await unitofwork.SaveAsync();
//         return NoContent();
//     }

//     [HttpGet("nombrepeticion")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     public async Task<ActionResult<IEnumerable<porDefecto2Dto>>> Getnom()
//     {
//         var porDefecto2 = await unitofwork.porDefectos2.metodo();
//         return mapper.Map<List<porDefecto2Dto>>(porDefecto2);
//     }

//     [HttpGet("nombrepeticion")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     public async Task<ActionResult<IEnumerable<porDefecto2>>> getconparatmetro(string nombre)
//     {
//         var porDefecto2 = await unitofwork.porDefectos2.metodo(nombre);
//         return mapper.Map<List<porDefecto2>>(porDefecto2);
//     }

//     [HttpGet("nombrepeticion")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     public async Task<ActionResult<IEnumerable<Object>>> Getnomm()
//     {
//         var porDefecto2 = await unitofwork.porDefectos2.metodo();
//         return Ok(porDefecto2);
//     }

//     private ActionResult Notfound()
//     {
//         throw new NotImplementedException();
//     }
// }