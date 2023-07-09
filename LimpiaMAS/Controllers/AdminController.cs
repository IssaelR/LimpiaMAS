using Microsoft.AspNetCore.Mvc;
using LimpiaMAS.Models;
using Newtonsoft.Json;

namespace LimpiaMAS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var objSession = HttpContext.Session.GetString("sUsuario");
            if (objSession != null)
            {
                //Deserializar
                var obj = JsonConvert.DeserializeObject<TbAdmin>(HttpContext.Session.GetString("sUsuario"));
                return View();
            }
            return RedirectToAction("login", "Limpia");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login", "Limpia");
        }
    }
}
