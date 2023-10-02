using System.ComponentModel.DataAnnotations;

namespace ProjetoPizzaria.DTOs;
public class CardapioDTO
{

  
    //DataAnnotations
    [Required]
    public string? Sabor { get; set; }
    public string? Descricao { get; set; }

     [Range(1, 1000)]
    public double Preco { get; set; }

   
}