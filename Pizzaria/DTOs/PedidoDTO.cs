using System.ComponentModel.DataAnnotations;
using ProjetoPizzaria.Models;


namespace ProjetoPizzaria.DTOs;

public class PedidoDTO 
{
	[Required]
	public int AtendenteId { get; set; }
	public Cardapio? Cardapio { get; set; }
	public Carrinho? Carrinho { get; set; }
	public int CarrinhoId { get; set; }
	
}