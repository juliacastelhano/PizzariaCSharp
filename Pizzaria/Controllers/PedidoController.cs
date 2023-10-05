using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPizzaria.Data;
using ProjetoPizzaria.DTOs;
using ProjetoPizzaria.Models;

namespace ProjetoPizzaria.Controllers;

[ApiController]
[Route("api/pedido")]
public class PedidoController : ControllerBase 
{
	private readonly AppDataContext _ctx;
	
	public PedidoController(AppDataContext ctx) => _ctx = ctx;
	
	[HttpGet]
	[Route("listar")]
	
	public IActionResult Listar() 
	{
		try 
		{
			List<Pedido> pedidos = 
			_ctx.Pedidos
			.Include(x => x.Atendente)
			.Include(x => x.Cliente)
			.Include(x => x.Cardapio)
			.ToList();
		
			return pedidos.Count == 0 ? NotFound() : Ok(pedidos);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}

	}
	
	
	[HttpPost]
	[Route("cadastrar")]
	
	public IActionResult Cadastrar([FromBody] PedidoDTO pedidoDTO) 
	{
		try 
		{
			Atendente? atendente =
			_ctx.Atendentes.Find(pedidoDTO.AtendenteId);
			if (atendente == null) 
			{
				return NotFound("Atendente não encontrado");
			}
			
			Cliente? cliente =
			_ctx.Clientes.Find(pedidoDTO.ClienteId);
			if (cliente == null)
			{
				return NotFound("Cliente não encontrado");
			}
			
			Cardapio? cardapio =
			_ctx.Cardapios.Find(pedidoDTO.CardapioId);
			if (cardapio == null) 
			{
				return NotFound("Sabor de pizza não encontrado");
			}
			else 
			{
				// Cardapio totalPedido =
				// _ctx.Cardapios.Find(cardapio);

				// Cardapio totalPedido =
				// _ctx.Cardapios.Find(pedidoDTO.SaborCardapio.)
			}
			
			Pedido pedido = new Pedido 
			{
				Atendente = atendente,
				Cliente = cliente,
				Cardapio = cardapio,
				TotalPedido = 1
				
			};
			_ctx.Pedidos.Add(pedido);
			_ctx.SaveChanges();
			return Created("", pedido);
			
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return BadRequest(e.Message);
		}
	}
			
}