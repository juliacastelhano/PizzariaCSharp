namespace ProjetoPizzaria.Models;

public class Pedido 
{
	public Pedido() =>
		CriadoEm = DateTime.Now;
				
	public int PedidoId { get; set; }
	public Atendente? Atendente { get; set; }
	public int AtendenteId { get; set; }
	public Cliente? Cliente { get; set; }
	public int ClienteId { get; set; }
	public Cardapio? Cardapio { get; set; }
	public int CardapioId { get; set; }
	public Carrinho? Carrinho { get; set; }
	public int CarrinhoId { get; set; }
	
	public double TotalPedido { get; set; }
	
	public DateTime CriadoEm { get; set; }
	
}