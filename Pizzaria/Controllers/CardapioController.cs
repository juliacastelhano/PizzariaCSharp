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
		return cardapios.Count == 0 ? NotFound("Nenhuma pizza cadastrada") : Ok(cardapios);
	}

	[HttpPost]
	[Route("cadastrar")]
	public IActionResult Cadastrar([FromBody] CardapioDTO cardapioDTO)
	{

		try
		{
			foreach (Cardapio cardapioCadastrado in _ctx.Cardapios.ToList())
			{
				if (cardapioCadastrado.Sabor == cardapioDTO.Sabor)
				{
					return Conflict("Pizza já cadastrada!");
				}
			}

			Cardapio cardapio = new Cardapio
			{
				Sabor = cardapioDTO.Sabor,
				Descricao = cardapioDTO.Descricao,
				Preco = cardapioDTO.Preco
			};

			_ctx.Cardapios.Add(cardapio);
			_ctx.SaveChanges();

			return Created("", cardapio);
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
		
		foreach (Cardapio cardapiocadastrado in _ctx.Cardapios.ToList())
		{
			if (cardapiocadastrado.Sabor == sabor)
			{
				return Ok(cardapiocadastrado);
			}
		}
		return NotFound("Pizza não encontrada");
	}

	[HttpDelete]
	[Route("deletar/{id}")]
	public IActionResult Deletar([FromRoute] int id)
	{
		try
		{
			Cardapio? cardapiocadastrado = _ctx.Cardapios.Find(id);
			if (cardapiocadastrado != null)
			{
				_ctx.Cardapios.Remove(cardapiocadastrado);
				_ctx.SaveChanges();
				return Ok();
			}
			return NotFound("Pizza não encontrada");
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
			
			Cardapio? cardapioCadastrado =
				_ctx.Cardapios.FirstOrDefault(x => x.CardapioId == id);
			if (cardapioCadastrado != null)
			{
				cardapioCadastrado.Sabor = cardapio.Sabor;
				cardapioCadastrado.Preco = cardapio.Preco;
				cardapioCadastrado.Descricao = cardapio.Descricao;
				_ctx.Cardapios.Update(cardapioCadastrado);
				_ctx.SaveChanges();
				return Ok(cardapioCadastrado);
			}
			return NotFound("Pizza não encontrada");
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
}
