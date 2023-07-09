using LimpiaMAS.Models;
using LimpiaMAS.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LimpiaMAS.Controllers
{
    public class PerfilController : Controller
    {
        private readonly iRegister _register;
        public PerfilController(iRegister register)
        {
            _register = register;
        }

        public IActionResult PerfilCliente()
        {
            string usuarioJson = HttpContext.Session.GetString("sUsuario");
            TbCliente cliente = JsonConvert.DeserializeObject<TbCliente>(usuarioJson);
            TbCliente cli = _register.getCli(cliente.Usr, cliente.Pwd);

            ViewData["Usr"] = cli.Usr;
            ViewData["NomApe"] = cli.NomCli + " " + cli.ApeCli;
            ViewData["Dir"] = cli.DirCli;
            ViewData["Tel"] = cli.TelCli;

            return View();
        }
        public IActionResult PerfilLimpiador()
        {
            string usuarioJson = HttpContext.Session.GetString("sUsuario");
            TbLimpiador limpiador = JsonConvert.DeserializeObject<TbLimpiador>(usuarioJson);
            TbLimpiador limp = _register.getLimp(limpiador.Usr, limpiador.Pwd);

            ViewData["Usr"] = limp.Usr;
            ViewData["NomApe"] = limp.NomLimp + " " + limp.ApeLimp;
            ViewData["Dir"] = limp.DirLimp;
            ViewData["Dist"] = limp.DistrLimp;
            ViewData["Tel"] = limp.TelLimp;
            ViewData["Serv"] = limp.ServLimp;
            ViewData["Tar"] = limp.TarLimp;
            return View();
        }
        public IActionResult returnIndexUsr()
        {
            return RedirectToAction("Index", "Usuario");
        }
    }
}
