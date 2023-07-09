using Microsoft.AspNetCore.Mvc;

namespace LimpiaMAS.Controllers
{
    public class LimpiaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Register")]
        public IActionResult Register() 
        {
            return View();
        }
        [Route("Vendedor")]
        public IActionResult Vendedor()
        {
            return View();
        }
        [Route("Cliente")]
        public IActionResult Cliente()
        {
            return View();
        }
        [Route("Admin")]
        public IActionResult Admin()
        {
            return View();
        }
        [Route("Pagos")]
        public IActionResult Pagos()
        {
            return View();
        }
        [Route("Servicios")]
        public IActionResult Servicios() 
        {
            return View();
        }
        [Route("Nosotros")]
        public IActionResult Nosotros()
        {
            return View();
        }
    }
}
