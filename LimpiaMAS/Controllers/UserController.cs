using LimpiaMAS.Models;
using LimpiaMAS.Service;
using Microsoft.AspNetCore.Mvc;

namespace LimpiaMAS.Controllers
{
    public class UserController : Controller
    {
        private readonly iUsuario _usuario;
        public UserController(iUsuario usuario)
        {
            _usuario = usuario;
        }

        public IActionResult IndexUser()
        {
            return View(_usuario.GetAllUsers());
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        public IActionResult grabar(TbUser user)
        {
            _usuario.add(user);
            return RedirectToAction("IndexUser");
        }

        [Route("Usuario/Eliminar/{Id}")]
        public IActionResult eliminar(string id)
        {
            _usuario.remove(id);
            return RedirectToAction("IndexUser");
        }

        [Route("Usuario/Editar/{Id}")]
        public IActionResult Editar(string id)
        {
            return View(_usuario.edit(id));
        }

        public IActionResult editarUser(TbUser user)
        {
            _usuario.EditDetails(user);
            return RedirectToAction("IndexUser");
        }
        public IActionResult returnIndexAdmin()
        {
            return View("~/Views/Admin/Index.cshtml");
        }
    }
}
