namespace ProjetoPizzaria.Models;
public class Cardapio
{
	public Cardapio() =>
		CriadoEm = DateTime.Now;

	public int CardapioId { get; set; }
	
	// TIREI O " ? " DO SABOR
	public string? Sabor { get; set; } 
	public string? Descricao { get; set; } 
	public double Preco { get; set; }
	public DateTime CriadoEm { get; set; }
}
