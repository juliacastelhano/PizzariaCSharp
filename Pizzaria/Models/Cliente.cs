namespace ProjetoPizzaria.Models;
public class Cliente
{
    // public Cliente() =>
    //     CriadoEm = DateTime.Now;

    public int ClienteId { get; set; }
    public string? Nome { get; set; } 
    public string? Endereco { get; set; } 
    public int Telefone { get; set; }
    // public DateTime CriadoEm { get; set; }
}
