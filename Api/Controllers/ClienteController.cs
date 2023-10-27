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

public class ClienteController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Cliente>>> Get0()
    {
        var cliente = await unitofwork.Clientes.GetAllAsync();
        return mapper.Map<List<Cliente>>(cliente);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<Cliente>>> Get([FromQuery]Params clienteParams)
    {
        var cliente = await unitofwork.Clientes.GetAllAsync(clienteParams.PageIndex,clienteParams.PageSize, clienteParams.Search);
        var listaCliente = mapper.Map<List<Cliente>>(cliente.registros);
        return new Pager<Cliente>(listaCliente, cliente.totalRegistros,clienteParams.PageIndex,clienteParams.PageSize,clienteParams.Search);
    }

    [HttpGet("{id}")]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Cliente>> Get(int id)
    {
        var cliente = await unitofwork.Clientes.GetByIdAsync(id);
        return mapper.Map<Cliente>(cliente);
    }

    [HttpPost]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Post(Cliente clienteDto)
    {
        var Cliente = this.mapper.Map<Cliente>(clienteDto);
        this.unitofwork.Clientes.Add(Cliente);
        await unitofwork.SaveAsync();
        if (Cliente == null){
            return BadRequest();
        }
        clienteDto.Id = Cliente.Id;
        return CreatedAtAction(nameof(Post), new { id = clienteDto.Id }, clienteDto);
    }

    [HttpPut]       
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Cliente>> Put (int id, [FromBody]Cliente ClienteDto)
    {
        if(ClienteDto == null)
            return NotFound();

        var cliente = this.mapper.Map<Cliente>(ClienteDto);
        unitofwork.Clientes.Update(cliente);
        await unitofwork.SaveAsync();
        return ClienteDto;     
    }

    [HttpDelete("{id}")]       
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var cliente = await unitofwork.Clientes.GetByIdAsync(id);

        if (cliente == null)
        {
            return Notfound();
        }

        unitofwork.Clientes.Remove(cliente);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("clienteEspecifico")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteOrdenDto>>> GetClienteEspeci(string idClientee)
    {
        var clienteEspe = await unitofwork.Clientes.ordenesClienteEspecifico(idClientee);
        return mapper.Map<List<ClienteOrdenDto>>(clienteEspe);
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}