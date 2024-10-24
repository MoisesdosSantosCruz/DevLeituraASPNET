using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Norget.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(11, 0)")]
        public decimal CPF { get; set; }

        [Required]
        [StringLength(120)]
        public string NomeCli { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(40)]
        public string EmailCli { get; set; }

        [Required]
        public int SenhaCli { get; set; }

        public int? Tel { get; set; }
    }
}
