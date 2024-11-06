using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Norget.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(11, 0)")]
        public decimal CPF { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        public int Senha { get; set; }

        [Required]
        public int Tel { get; set; }
    }
}
