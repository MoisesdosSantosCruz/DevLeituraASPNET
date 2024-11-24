using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Norget.Libraries.Login;
using Norget.Models;
using Norget.Repository;
using System.Diagnostics;

namespace Norget.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUsuarioRepositorio? _usuarioRepositorio;
        private ILivroRepositorio? _livroRepositorio;
        private LoginUsuario _loginUsuario;


        public HomeController(ILogger<HomeController> logger, IUsuarioRepositorio usuarioRepositorio, ILivroRepositorio livroRepositorio, LoginUsuario loginUsuario)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
            _livroRepositorio = livroRepositorio;
            _loginUsuario = loginUsuario;

        }

        public IActionResult Index()
        {

            return View(_livroRepositorio.ListarLivros());
        }

        /* Para que os botões funcionem, o controle deve devolver
         sua parte visual descrita abaixo*/
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            Usuario loginDB = _usuarioRepositorio.Login(usuario.EmailCli, usuario.SenhaCli);

            if (loginDB.EmailCli != null && loginDB.SenhaCli != null)
            {
                _loginUsuario.Login(loginDB);
                return new RedirectResult(Url.Action(nameof(PainelUsuario)));
            }
            else
            {
                //Erro na sessão
                ViewData["msg"] = "Usuário não encontrado"; //Mensagem de tratamento de erro
                return View();


            }
        }

        public IActionResult InfoSp()
        {
            return View(_usuarioRepositorio.TodosUsuarios());
        }

        public IActionResult PainelUsuario()
        {
            return View(_usuarioRepositorio.TodosUsuarios());
        }

        public IActionResult CadastroUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroUsuario(Usuario usuario)
        {
            _usuarioRepositorio.Cadastro(usuario);

            return RedirectToAction(nameof(PainelUsuario));
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {

            // Carrega a lista de Cliente
            var listaCliente = _usuarioRepositorio.TodosUsuarios();

            //metodo que atualiza cliente
            _usuarioRepositorio.Atualizar(usuario);
            //redireciona para a pagina home

            return RedirectToAction(nameof(PainelUsuario));

        }

        public IActionResult Editar(int id)
        {
            // Carrega a liista de Cliente
            var listaUsuario = _usuarioRepositorio.TodosUsuarios();
            var ObjUsuario = new Usuario
            {
                //metodo que lista cliente
                ListaUsuario = (List<Usuario>)listaUsuario

            };

            //Retorna o cliente pegando o id
            return View(_usuarioRepositorio.ObterUsuario(id));
        }

        public IActionResult Excluir(int id)
        {
            //metodo que exclui cliente
            _usuarioRepositorio.Excluir(id);
            //redireciona para a pagina home
            return RedirectToAction(nameof(PainelUsuario));
        }

        //Parte dos livros 
        public IActionResult PainelLivro() {

            return View(_livroRepositorio.ListarLivros());
        }
        public IActionResult DetalheLivro(int IdLiv) {
          
            var livro = _livroRepositorio.ObterLivro(IdLiv);

            return View(livro);
        }
     
        public IActionResult CadastroLivro() {

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