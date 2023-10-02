using System.ComponentModel.DataAnnotations;

namespace ProjetoPizzaria.DTOs;
public class AtendenteDTO
{

    //DataAnnotations
    [Required]
    public string? Nome { get; set; }

}
