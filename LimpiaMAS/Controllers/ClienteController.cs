using LimpiaMAS.Service;
using LimpiaMAS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LimpiaMAS.Controllers
{
    public class ClienteController : Controller
    {
        private readonly iCliente _Cliente;
        public ClienteController(iCliente cliente)
        { 
            _Cliente = cliente;
        }
        public IActionResult IndexCliente()
        {
            return View(_Cliente.GetAllClientes());
        }
        [Route("Cliente/Eliminar/{Id}")]
        public IActionResult eliminar(string id) {
            _Cliente.remove(id);
             return RedirectToAction("IndexCliente");
        }
        public IActionResult nuevo() 
        {
            return View();
        }
        public IActionResult grabar(TbCliente cliente) {
            _Cliente.add(cliente);
            return RedirectToAction("IndexCliente");
        }
        [Route("Cliente/Editar/{Id}")]
        public IActionResult Editar(string id) {
            return View(_Cliente.edit(id));
        }
        public IActionResult editarCliente(TbCliente cliente) {
            _Cliente.EditDatails(cliente);
            return RedirectToAction("IndexCliente");
        }
        public IActionResult returnIndexAdmin()
        {
            return View("~/Views/Admin/Index.cshtml");
        }
        public IActionResult FormCliente()
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
