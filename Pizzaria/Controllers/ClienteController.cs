using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPizzaria.DTOs;
using ProjetoPizzaria.Data;
using ProjetoPizzaria.Models;

namespace ProjetoPizzaria.Controllers;

[ApiController]
[Route("api/cliente")]
public class ClienteController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public ClienteController(AppDataContext context)
    {
        _ctx = context;
    } 

   
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        List<Cliente> clientes = _ctx.Clientes.ToList();
        return clientes.Count == 0 ? NotFound() : Ok(clientes);
    }

[HttpPost]
[Route("cadastrar")]
public IActionResult Cadastrar([FromBody] ClienteDTO clienteDTO)
{
    try
    {
       Cliente cliente = new Cliente
        {
            Nome = clienteDTO.Nome,
            Endereco = clienteDTO.Endereco,
            Telefone = clienteDTO.Telefone,
        };

        _ctx.Clientes.Add(cliente);
        _ctx.SaveChanges();

        return Created("",cliente);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return BadRequest(e.Message);
    }
}

    [HttpGet]
    [Route("buscar/{nome}")]
    public IActionResult Buscar([FromRoute] string nome)
    {
        //Expressão lambda para buscar um registro na base de dados com EF
        foreach (Cliente clienteCadatrado in _ctx.Clientes.ToList())
        {
            if (clienteCadatrado.Nome == nome)
            {
                return Ok(clienteCadatrado);
            }
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("deletar/{id}")]
    public IActionResult Deletar([FromRoute] int id)
    {
        try
        {
            Cliente? clienteCadastrado = _ctx.Clientes.Find(id);
            if (clienteCadastrado != null)
            {
                _ctx.Clientes.Remove(clienteCadastrado);
                _ctx.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("alterar/{id}")]
    public IActionResult Alterar([FromRoute] int id,
        [FromBody] Cliente cliente)
    {
        try
        {
            //Expressões lambda
            Cliente? clienteCadastrado =
                _ctx.Clientes.FirstOrDefault(x => x.ClienteId == id);
            if (clienteCadastrado != null)
            {
                clienteCadastrado.Nome = cliente.Nome;
                clienteCadastrado.Endereco = cliente.Endereco;
                clienteCadastrado.Telefone = cliente.Telefone;
                _ctx.Clientes.Update(clienteCadastrado);
                _ctx.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
