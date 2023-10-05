using System.ComponentModel.DataAnnotations;

namespace ProjetoPizzaria.DTOs;

public class PedidoDTO 
{
	public int AtendenteId { get; set; }
	
	public int ClienteId { get; set; }
	
	// TAVA RECLAMANDO DA PROPRIEDADE 'NON-NULLABLE' POR ISSO O " ? "
	public int CardapioId {get; set; }
	
	[Range(1, 10000)]
	public double totalPedido { get; set; }
}