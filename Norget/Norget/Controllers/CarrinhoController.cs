using Microsoft.AspNetCore.Mvc;

namespace Norget.Controllers
{
    public class CarrinhoController : Controller
    {
        public IActionResult Carrinho()
        {
            return View();
        }
        //Caso de oportunidade de teste
    }
}
