using System.ComponentModel.DataAnnotations;
namespace ProjetoPizzaria.Models;

public class CarrinhoDTO
{
		
	
	[Required]
	public int ClienteId { get; set; }
	public int CardapioId { get; set; }
	public int Quantidade { get; set; }
	public Cardapio? Cardapio { get; set; }
	public double CardapioPreco { get; set; }

	[Range(0, 10000)]
	public double TotalPedido { get; set; }
	
	
	
}