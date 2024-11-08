using Microsoft.AspNetCore.Mvc;
using Norget.Models;
using Norget.Repository;

namespace Norget.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index(int Id)
        {
            return View(_usuarioRepositorio.ObterUsuario(Id));
        }
        public IActionResult CadastroUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroUsuario(Usuario usuario)
        {
            _usuarioRepositorio.Cadastro(usuario);
            return View();
        }
    }
}
