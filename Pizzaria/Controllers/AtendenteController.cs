using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPizzaria.DTOs;
using ProjetoPizzaria.Data;
using ProjetoPizzaria.Models;

namespace ProjetoPizzaria.Controllers;

[ApiController]
[Route("api/atendente")]
public class AtendenteController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public AtendenteController(AppDataContext ctx) => _ctx = ctx;

   
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        List<Atendente> atendentes = _ctx.Atendentes.ToList();
        return atendentes.Count == 0 ? NotFound() : Ok(atendentes);
    }

[HttpPost]
[Route("cadastrar")]
public IActionResult Cadastrar([FromBody] AtendenteDTO atendenteDTO)
{
    try
    {
      Atendente atendente = new Atendente
        {
            Nome = atendenteDTO.Nome,
        };

        _ctx.Atendentes.Add(atendente);
        _ctx.SaveChanges();

        return Created("",atendente);
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
        foreach (Atendente atendenteCadatrado in _ctx.Atendentes.ToList())
        {
            if (atendenteCadatrado.Nome == nome)
            {
                return Ok(atendenteCadatrado);
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
           Atendente?atendenteCadastrado = _ctx.Atendentes.Find(id);
            if (atendenteCadastrado != null)
            {
                _ctx.Atendentes.Remove(atendenteCadastrado);
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
