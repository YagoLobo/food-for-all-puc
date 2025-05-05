using Microsoft.AspNetCore.Mvc;

namespace FoodforAll.Controllers
{
    [Route("Cadastro")]
    public class CadastroController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Cadastro");
        }
    }
}
