namespace ProjetoPizzaria.Models;

public class Carrinho
{
	public Carrinho() =>
		CriadoEm = DateTime.Now;
		
	public int CarrinhoId { get; set; }
	public Cliente Cliente { get; set; }
	public int ClienteId { get; set; }
	public Cardapio Cardapio { get; set; }
	public int CardapioId { get; set; }
	public int Quantidade { get; set; }
	public double TotalPedido { get; set; }
	
	public DateTime CriadoEm { get; set; }
	
	
}