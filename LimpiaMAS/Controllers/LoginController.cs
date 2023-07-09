using LimpiaMAS.Models;
using LimpiaMAS.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LimpiaMAS.Controllers
{
    public class LoginController : Controller
    {
        private readonly iLogeo _logeo;
        public LoginController(iLogeo logeo) 
        { 
            _logeo = logeo;
        }

        public IActionResult Logeo(TbUser obj)
        {
            if (_logeo.LoginComparision(obj) == 1)
            {
                //Crear variable de sesion
                //pasar de un obj tabla de usuario a un obj user para guardar solo usr y pwd
                /*User user = new User();
                user.Usr = obj.Usr;
                user.Pwd = obj.Pwd;*/
                Console.WriteLine(obj.Usr.ToString());
                HttpContext.Session.SetString("sUsuario",JsonConvert.SerializeObject(obj));
                return View("~/Views/Usuario/Index.cshtml");
            }
            else if(_logeo.LoginComparision(obj) == 2)
            {
                HttpContext.Session.SetString("sUsuario", JsonConvert.SerializeObject(obj));
                return View("~/Views/Admin/Index.cshtml");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
