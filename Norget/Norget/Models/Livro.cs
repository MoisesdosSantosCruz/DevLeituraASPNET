using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Norget.Models;

public class Livro
{
    [Key]
    [Column(TypeName = "decimal(13, 0)")]
    public decimal ISBN { get; set; }

    [Required]
    [StringLength(60)]
    public string NomeLiv { get; set; }

    [Required]
    [Column(TypeName = "decimal(6, 2)")]
    public decimal PrecoLiv { get; set; }

    [Required]
    [StringLength(100)]
    public string DescLiv { get; set; }

    /*
    [Required]
    [StringLength(???)] // Esse eu não sei se precisa colocar o tamanho da string
    public string ImgLiv { get; set; } // Não sei como funcionaria esse
    */

    [Required]
    [StringLength(30)]
    public string Categoria { get; set; }

    [Required]
    [StringLength(40)]
    public string Autor { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DataPubli { get; set; }

    // Chave estrangeira para Editora
    public int? EditoraId { get; set; }
    public Editora Editora { get; set; }
}