using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPizzaria.Data;
using ProjetoPizzaria.DTOs;
using ProjetoPizzaria.Models;

namespace ProjetoPizzaria.Controllers;

[ApiController]
[Route("api/carrinho")]
public class CarrinhoController : ControllerBase 
{
	private readonly AppDataContext _ctx;
	
	public CarrinhoController(AppDataContext ctx) => _ctx = ctx;
	
	[HttpGet]
	[Route("listar")]
	
	public IActionResult Listar() 
	{
		try 
		{
			List<Carrinho> carrinhos = 
			_ctx.Carrinhos
			.Include(x => x.Cliente)
			.Include(x => x.Cardapio)
			.ToList();
		
			return carrinhos.Count == 0 ? NotFound("Carrinho não encontrado") : Ok(carrinhos);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}

	}
	
	
	[HttpPost]
	[Route("cadastrar")]
	
	public IActionResult Cadastrar([FromBody] CarrinhoDTO carrinhoDTO) 
	{
		try 
		{
			Cliente? cliente =
			_ctx.Clientes.Find(carrinhoDTO.ClienteId);
			if (cliente == null)
			{
				return NotFound("Cliente não encontrado");
			}
			
			Cardapio? cardapio =
			_ctx.Cardapios.Find(carrinhoDTO.CardapioId);
			if (cardapio == null) 
			{
				return NotFound("Sabor de pizza não encontrado");
				
			}
			
			double valorTotal = carrinhoDTO.Quantidade * cardapio.Preco;
			
			
			Carrinho carrinho = new Carrinho 
			{
				Cliente = cliente,
				Cardapio = cardapio,
				Quantidade = carrinhoDTO.Quantidade,
				TotalPedido = valorTotal
				
			};
			_ctx.Carrinhos.Add(carrinho);
			_ctx.SaveChanges();
			return Created("", carrinho);
			
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return BadRequest(e.Message);
		}
	}
	
	
	[HttpDelete]
	[Route("deletar/{id}")]
	public IActionResult Deletar([FromRoute] int id)
	{
		try
		{
			Carrinho? carrinhocadastrado = _ctx.Carrinhos.Find(id);
			if (carrinhocadastrado != null)
			{
				_ctx.Carrinhos.Remove(carrinhocadastrado);
				_ctx.SaveChanges();
				return Ok();
			}
			return NotFound("Carrinho não encontrado");
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	
	[HttpGet]
	[Route("listarPorCliente/{nomeCliente}")]
	public IActionResult ListarPedidosPorNomeCliente(string nomeCliente)
	{
		double totalTodosPedidos = 0;

		try
		{
			List<Carrinho> carrinhos = _ctx.Carrinhos
				// .Include(x => x.Atendente)
				.Include(x => x.Cliente)
				.Include(x => x.Cardapio)
				.Where(p => p.Cliente.Nome == nomeCliente)
				.ToList();

			var contagemPedidos = carrinhos.Count;

			foreach (Carrinho carrinho in carrinhos)
			{
				double precoDoCardapio = carrinho.Cardapio.Preco;
				double totalPedido = carrinho.TotalPedido;
				totalTodosPedidos += totalPedido;
			}

			var resposta = new
			{
				TotalTodosPedidos = totalTodosPedidos,
				Pedidos = carrinhos
			};

			return carrinhos.Count == 0 ? NotFound("Carrinho não encontrado para o cliente especificado") : Ok(resposta);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	
}