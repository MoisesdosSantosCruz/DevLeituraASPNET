using System.ComponentModel.DataAnnotations;

namespace Norget.Models
{
    public class ItemVenda
    {
        public int NumeroVenda { get; set; }
        public Venda Venda { get; set; }

        public decimal ISBN { get; set; }
        public Livro Livro { get; set; }

        [Required]
        public decimal ValorItem { get; set; }

        [Required]
        public int Qtd { get; set; }
    }
}
