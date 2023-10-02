using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPizzaria.DTOs;
using ProjetoPizzaria.Data;
using ProjetoPizzaria.Models;

namespace ProjetoPizzaria.Controllers;

[ApiController]
[Route("api/cardapio")]
public class CardapioController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public CardapioController(AppDataContext context)
    {
        _ctx = context;
    } 

   
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        List<Cardapio> cardapios = _ctx.Cardapios.ToList();
        return cardapios.Count == 0 ? NotFound() : Ok(cardapios);
    }

[HttpPost]
[Route("cadastrar")]
public IActionResult Cadastrar([FromBody] CardapioDTO cardapioDTO)
{
    try
    {
       Cardapio cardapio = new Cardapio
        {
            Sabor = cardapioDTO.Sabor,
            Preco = cardapioDTO.Preco,
            Descricao = cardapioDTO.Descricao,
        };

        _ctx.Cardapios.Add(cardapio);
        _ctx.SaveChanges();

        return Created("",cardapio);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return BadRequest(e.Message);
    }
}

    [HttpGet]
    [Route("buscar/{sabor}")]
    public IActionResult Buscar([FromRoute] string sabor)
    {
        //Expressão lambda para buscar um registro na base de dados com EF
        foreach (Cardapio cardapioCadatrado in _ctx.Cardapios.ToList())
        {
            if (cardapioCadatrado.Sabor == sabor)
            {
                return Ok(cardapioCadatrado);
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
            Cardapio? cardapioCadatrado = _ctx.Cardapios.Find(id);
            if (cardapioCadatrado != null)
            {
                _ctx.Cardapios.Remove(cardapioCadatrado);
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
        [FromBody] Cardapio cardapio)
    {
        try
        {
            //Expressões lambda
            Cardapio? cardapioCadatrado =
                _ctx.Cardapios.FirstOrDefault(x => x.CardapioId == id);
            if (cardapioCadatrado != null)
            {
                cardapioCadatrado.Sabor = cardapio.Sabor;
                cardapioCadatrado.Descricao = cardapio.Descricao;
                cardapioCadatrado.Preco = cardapio.Preco;
                _ctx.Cardapios.Update(cardapioCadatrado);
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
