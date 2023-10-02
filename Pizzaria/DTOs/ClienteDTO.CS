using System.ComponentModel.DataAnnotations;

namespace ProjetoPizzaria.DTOs;
public class ClienteDTO
{

  
    //DataAnnotations
    [Required]
    public string? Nome { get; set; }

    [Required]
    public string? Endereco { get; set; }
    public int Telefone { get; set; }
  
   
}