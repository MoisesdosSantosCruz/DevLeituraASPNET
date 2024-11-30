using Microsoft.AspNetCore.Mvc;
using Norget.Libraries.Login;
using Norget.Models;
using Norget.Repository;
using System.Diagnostics;

namespace Norget.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILogger<LivroController> _logger;
        private ILivroRepositorio? _livroRepositorio;


        public LivroController(ILogger<LivroController> logger, ILivroRepositorio livroRepositorio)
        {
            _logger = logger;
            _livroRepositorio = livroRepositorio;

        }


        [HttpPost]
        public IActionResult EditarLivro(Livro livro)
        {

            // Carrega a lista de Cliente
            var listaLivro = _livroRepositorio.ListarLivros();

            //metodo que atualiza cliente
            _livroRepositorio.AtualizarLivro(livro);
            //redireciona para a pagina home

            return RedirectToAction(nameof(PainelLivro));

        }

        public IActionResult EditarLivro(int idliv)
        {
            // Carrega a liista de Cliente
            var listaLivro = _livroRepositorio.ListarLivros();
            var ObjLivro = new Livro
            {
                //metodo que lista cliente
                ListaLivro = (List<Livro>)listaLivro

            };

            //Retorna o cliente pegando o id
            return View(_livroRepositorio.ObterLivro(idliv));
        }

        public IActionResult PainelLivro()
        {

            return View(_livroRepositorio.ListarLivros());
        }
       
        
        public IActionResult DetalheLivro(int IdLiv)
        {

            ;
            return View(_livroRepositorio.ObterLivro(IdLiv));

        }
        
        public IActionResult CadastroLivro()
        {

            return View();

        }
        [HttpPost]
        public IActionResult CadastroLivro(Livro livro)
        {
            _livroRepositorio.CadastroLivro(livro);

            return RedirectToAction(nameof(PainelLivro));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
