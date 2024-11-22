using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Norget.Models;

public class Livro
{
    [Key]
    [Column(TypeName = "decimal(13, 0)")]
    public decimal ISBN { get; set; }

    [Required]
    [StringLength(100)]
    public string? NomeLiv { get; set; }

    [Required]
    [Column(TypeName = "decimal(6, 2)")]
    public decimal PrecoLiv { get; set; }

    [Required]
    [StringLength(250)]
    public string? DescLiv { get; set; }

    
    [Required]
    public string? ImgLiv { get; set; } 
    

    [Required]
    [StringLength(100)]
    public string? Categoria { get; set; }

    [Required]
    [StringLength(100)]
    public string? Autor { get; set; }

    [Required]
    public string? DataPubli { get; set; }

    // Chave estrangeira para Editora
    public int? EditoraId { get; set; }
    public Editora? Editora { get; set; }

    public List<Livro>? ListaLivro { get; set; }
}