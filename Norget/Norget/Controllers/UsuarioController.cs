using Microsoft.AspNetCore.Mvc;
using Norget.Models;
using Norget.Repository;

namespace Norget.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index(int id)
        {
            var usuario = _usuarioRepositorio.ObterUsuario(id);
            return View(usuario);
        }

        public IActionResult CadastroUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroUsuario(Usuario usuario)
        {
            _usuarioRepositorio.Cadastro(usuario);

            // Redireciona para a página de Index ou Login após o cadastro
            return RedirectToAction("Login", "Usuario");

        }
    }
}
