using System.ComponentModel.DataAnnotations;
using ProjetoPizzaria.Models;


namespace ProjetoPizzaria.DTOs;

public class PedidoDTO 
{
	[Required]
	public int AtendenteId { get; set; }
	
	// public int ClienteId { get; set; }
	
	// // TAVA RECLAMANDO DA PROPRIEDADE 'NON-NULLABLE' POR ISSO O " ? "
	// public int CardapioId { get; set; }
	// public int Quantidade { get; set; }
	
	public Cardapio? Cardapio { get; set; }
	
	// public double CardapioPreco { get; set; }
	public Carrinho? Carrinho { get; set; }
	public int CarrinhoId { get; set; }
	
	// [Range(0, 10000)]
	// public double totalPedido { get; set; }
}