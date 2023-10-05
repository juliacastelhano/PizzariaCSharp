using System.ComponentModel.DataAnnotations;

namespace ProjetoPizzaria.DTOs;
public class CardapioDTO
{
 
    //DataAnnotations
[Required]
public string? Sabor { get; set; }
[Required]
public string? Descricao { get; set; }

[Required]
[Range(1, 1000)]
public double Preco { get; set; }
  
}