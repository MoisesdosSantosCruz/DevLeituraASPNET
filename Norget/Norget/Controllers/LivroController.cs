using Microsoft.AspNetCore.Mvc;
using Norget.Models; // Indica que está puxando as coisas das Models para esta Controller

namespace Norget.Controllers
{
    public class LivroController : Controller
    {
        private static List<Livro> _livros = new List<Livro>
        {
            new Livro { Id = 1, Nome = "Produto A", Preco = 10M },
            new Livro { Id = 2, Nome = "Produto B", Preco = 20M }
        };

        public IActionResult Index()
        {           
            return View(_livros);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(Livro livro)
        {
            _livros.Add(livro);
            return RedirectToAction("Index");
        }
    }
}