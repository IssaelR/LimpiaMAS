using LimpiaMAS.Models;
using LimpiaMAS.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LimpiaMAS.Controllers
{
    public class TrabajadorController : Controller
    {
        public IActionResult FormTrabajador()
        {
            string usuarioJson = HttpContext.Session.GetString("sUsuario");
            if (!string.IsNullOrEmpty(usuarioJson))
            {
                TbUser usuario = JsonConvert.DeserializeObject<TbUser>(usuarioJson);
                Console.WriteLine(usuario.Usr.ToString());//prueba
                return View();
            }
            else
            {
                return RedirectToAction("login", "Limpia");
            }
        }
    }
}
