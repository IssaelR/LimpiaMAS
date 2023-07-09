using LimpiaMAS.Models;
using LimpiaMAS.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LimpiaMAS.Controllers
{
    public class RegisterController : Controller
    {
        private readonly iRegister _register;
        public RegisterController(iRegister register)
        {
            _register = register;
        }

        public IActionResult new_usr(TbUser obj)
        {
            _register.add_usr(obj);
            return RedirectToAction("login", "Limpia");
        }

        public IActionResult new_dis(TbDisponibilidad obj/*TimeSpan TStart, TimeSpan TDone, DateTime FecDis*/)
        {
            string usuarioJson = HttpContext.Session.GetString("sUsuario");
            TbUser usuario = JsonConvert.DeserializeObject<TbUser>(usuarioJson);
            TbLimpiador user = _register.getLimp(usuario.Usr, usuario.Pwd);
            obj.IdLimp = user.IdLimp;
            //obj.FecDis = FecDis;
            //obj.TStart = TStart;
            //obj.TDone = TDone;
            _register.add_dis(obj);
            return RedirectToAction("Index", "Usuario");
            
        }
        public IActionResult new_limp(TbLimpiador obj, IFormFile FotoLimpiador, string genero)
        {
            string usuarioJson = HttpContext.Session.GetString("sUsuario");
            TbUser usuario = JsonConvert.DeserializeObject<TbUser>(usuarioJson);
            // se selecciono algun archivo?
            if (FotoLimpiador != null && FotoLimpiador.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    FotoLimpiador.CopyTo(memoryStream);
                    byte[] fotoBytes = memoryStream.ToArray();
                    //asignamos la foto a nuestro modelo
                    obj.FotLimp = fotoBytes;
                }
            }
            /*llamo a la funcion para que me de el user completo, con el user y pwd
             * que me dio la sesion deserializada*/
            TbUser user = _register.getUser(usuario.Usr, usuario.Pwd);
            obj.Usr = user.Usr;
            obj.Pwd = user.Pwd;
            obj.NomLimp = user.Nom;
            obj.ApeLimp = user.Ape;
            obj.GenLimp = genero;
            Console.WriteLine(genero);
            Console.WriteLine(obj.NomLimp.ToString() + obj.ApeLimp.ToString());
            obj.ServLimp = JsonConvert.SerializeObject(obj.ServiciosAJSON);
            _register.add_limp(obj);
            return RedirectToAction("Index", "Usuario");
        }
        public IActionResult new_cli(TbCliente obj)
        {
            string usuarioJson = HttpContext.Session.GetString("sUsuario");
            TbUser usuario = JsonConvert.DeserializeObject<TbUser>(usuarioJson);
            TbUser usr = _register.getUser(usuario.Usr, usuario.Pwd);

            obj.Usr = usr.Usr;
            obj.Pwd = usr.Pwd;
            obj.NomCli = usr.Nom;
            obj.ApeCli = usr.Ape;
            _register.add_cli(obj);
            return RedirectToAction("Index","Servicio");
        }
    }
}
