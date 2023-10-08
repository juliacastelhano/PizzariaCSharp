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
			.Include(x => x.Carrinho)
			// .Include(x => x.Carrinho.Cliente)
			.ToList();

			return pedidos.Count == 0 ? NotFound("Pedido não encontrado") : Ok(pedidos);
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
			// Atendente atendenteId = _ctx.Atendentes.Find(atendente.AtendenteId);
			if (atendente == null)
			{
				return NotFound("Atendente não encontrado");
			}
			
			// Cliente? cliente =
			// _ctx.Clientes.Find(pedidoDTO.ClienteId);
			// if (cliente == null)
			// {
			// 	return NotFound("Cliente não encontrado");
			// }

			Carrinho? carrinho =
			_ctx.Carrinhos.Find(pedidoDTO.CarrinhoId);
			if (carrinho == null)
			{
				return NotFound("Carrinho não encontrado");
			}
			
			List<Carrinho> carrinhos =
			_ctx.Carrinhos
			.Include(x => x.Cliente)
			.Include(x => x.Cardapio)
			.ToList();


			// double valorTotal = carrinho.Quantidade * carrinho.TotalPedido;
			double valorTotal = carrinho.Quantidade * carrinho.Cardapio.Preco;

			Pedido pedido = new Pedido
			{
				Atendente = atendente,
				AtendenteId = atendente.AtendenteId,
				Cliente = carrinho.Cliente,
				ClienteId = carrinho.Cliente.ClienteId,
				// Cardapio = carrinho.Cardapio,
				CardapioId = carrinho.Cardapio.CardapioId,
				CarrinhoId = pedidoDTO.CarrinhoId,
				// Carrinhos = carrinhos,
				// Quantidade = carrinho.Quantidade,
				TotalPedido = valorTotal

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

	// [HttpPost]
	// [Route("cadastrar")]

	// public IActionResult Cadastrar([FromBody] PedidoDTO pedidoDTO)
	// {
	// 	try
	// 	{
	// 		Atendente? atendente =
	// 		_ctx.Atendentes.Find(pedidoDTO.AtendenteId);
	// 		Atendente atendenteNome = _ctx.Atendentes.Find(atendente.Nome);
	// 		if (atendente == null)
	// 		{
	// 			return NotFound("Atendente não encontrado");
	// 		}

	// 		Cliente? cliente =
	// 		_ctx.Clientes.Find(pedidoDTO.ClienteId);
	// 		if (cliente == null)
	// 		{
	// 			return NotFound("Cliente não encontrado");
	// 		}

	// 		Cardapio? cardapio =
	// 		_ctx.Cardapios.Find(pedidoDTO.CardapioId);
	// 		if (cardapio == null)
	// 		{
	// 			return NotFound("Sabor de pizza não encontrado");

	// 		}

	// 		double valorTotal = pedidoDTO.Quantidade * cardapio.Preco;


	// 		Pedido pedido = new Pedido
	// 		{
	// 			Atendente = atendente,
	// 			Cliente = cliente,
	// 			Cardapio = cardapio,
	// 			Quantidade = pedidoDTO.Quantidade,
	// 			TotalPedido = valorTotal

	// 		};
	// 		_ctx.Pedidos.Add(pedido);
	// 		_ctx.SaveChanges();
	// 		return Created("", pedido);

	// 	}
	// 	catch (Exception e)
	// 	{
	// 		Console.WriteLine(e);
	// 		return BadRequest(e.Message);
	// 	}
	// }

	[HttpGet]
	[Route("listarPorCliente/{nomeCliente}")]
	public IActionResult ListarPedidosPorNomeCliente(string nomeCliente)
	{
		double totalTodosPedidos = 0;

		try
		{
			List<Pedido> pedidos = _ctx.Pedidos
				.Include(x => x.Atendente)
				.Include(x => x.Cliente)
				.Include(x => x.Cardapio)
				.Include(x => x.Carrinho)
				// .Where(p => p.Cliente.Nome == nomeCliente)
				.Where(p => p.Carrinho.Cliente.Nome == nomeCliente)
				.ToList();

			var contagemPedidos = pedidos.Count;

			foreach (Pedido pedido in pedidos)
			{
				double precoDoCardapio = pedido.Cardapio.Preco;
				// double precoDoCardapio = pedido.Carrinho.Cardapio.Preco;
				double totalPedido = pedido.TotalPedido;
				totalTodosPedidos += totalPedido;
			}

			var resposta = new
			{
				TotalTodosItens = totalTodosPedidos,
				Pedidos = pedidos
			};
			
			// Pedido pedido2 = new Pedido
			// {
			// 	TotalTodosItens = totalTodosPedidos,
			// 	Pedidos = pedidos
			// };

			return pedidos.Count == 0 ? NotFound("Pedido(s) não encontrado(s) para o cliente especificado") : Ok(resposta);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	[HttpDelete]
	[Route("deletar/{id}")]
	public IActionResult Deletar([FromRoute] int id)
	{
		try
		{
			Pedido? pedidoCadastrado = _ctx.Pedidos.Find(id);
			if (pedidoCadastrado != null)
			{
				_ctx.Pedidos.Remove(pedidoCadastrado);
				_ctx.SaveChanges();
				return Ok();
			}
			return NotFound("Pedido não encontrado");
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}



}