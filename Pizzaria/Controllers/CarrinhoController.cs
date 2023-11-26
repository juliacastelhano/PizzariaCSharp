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
	
	public IActionResult Cadastrar([FromBody] CarrinhoDTO carrinhoDTO, int QuantidadeEstoque) 
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

			var quantidadeEmEstoque = cardapio.QuantidadeEstoque;
		if (quantidadeEmEstoque == 0)
		{
		return NotFound("Sem esse sabor de pizza em estoque");
		}

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


	// [HttpPut]
	// [Route("alterar/{id}")]
	// public IActionResult Alterar([FromRoute] int id,
	// 	[FromBody] Carrinho carrinho)
	// {
	// 	try
	// 	{
			
	// 		Carrinho? carrinhoCadastrado =
	// 			_ctx.Carrinhos.FirstOrDefault(x => x.CarrinhoId == id);
	// 		if (carrinhoCadastrado != null)
	// 		{
				
	// 			carrinhoCadastrado.ClienteId = carrinho.ClienteId;
	// 			carrinhoCadastrado.CardapioId = carrinho.CardapioId;
	// 			carrinhoCadastrado.Quantidade = carrinho.Quantidade;


	// 			_ctx.Carrinhos.Update(carrinhoCadastrado);
	// 			_ctx.SaveChanges();
	// 			return Ok(carrinhoCadastrado);

	
	// 		}
	// 		return NotFound("Carrinho não encontrado");
	// 	}
	// 	catch (Exception e)
	// 	{
	// 		return BadRequest(e.Message);
	// 	}
	// }
	
	[HttpPatch]
    [Route("finalizar/{id}")]
	public IActionResult FinalizarPedido([FromRoute] int id)
{
    try
    {
        Carrinho carrinho = _ctx.Carrinhos
            .Include(x => x.Cliente)
            .Include(x => x.Cardapio)
            .FirstOrDefault(c => c.CarrinhoId == id);

        if (carrinho == null)
        {
            return NotFound("Carrinho não encontrado");
        }

        if (carrinho.Finalizado)
        {
            return BadRequest("Este carrinho já foi finalizado anteriormente");
        }

    
        carrinho.Finalizado = true;
        _ctx.SaveChanges();

        var resposta = new
        {
            Mensagem = "Pedido finalizado com sucesso",
            DetalhesDoPedido = new
            {
                Cliente = carrinho.Cliente.Nome,
                Cardapio = carrinho.Cardapio.Sabor,
                Carrinho = carrinho.TotalPedido,
            }
        };

        return Ok(resposta);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}
	[HttpGet]
	[Route("listarFinalizados")]
	public IActionResult ListarPedidosFinalizados()
{
    try
    {
        List<Carrinho> carrinhosFinalizados = _ctx.Carrinhos
            .Include(x => x.Cliente)
            .Include(x => x.Cardapio)
            .Where(c => c.Finalizado)
            .ToList();

        var resposta = carrinhosFinalizados.Select(c => new
        {
            Cliente = c.Cliente.Nome,
            Cardapio = c.Cardapio.Sabor,
          
        });

        return carrinhosFinalizados.Count == 0
            ? NotFound("Nenhum pedido finalizado encontrado")
            : Ok(resposta);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}

	
}