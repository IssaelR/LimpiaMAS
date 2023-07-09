//using AspNetCore;
using LimpiaMAS.Models;
using LimpiaMAS.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data.SqlTypes;
using System.Text;

namespace LimpiaMAS.Controllers
{
    public class ServicioController : Controller
    {
        private readonly iLimpiador _limpiador;
        private readonly iCliente _cliente;
        private readonly iDetalleServicio _detalleServicio;
        private readonly iServicio _servicio;

        private bool _esPrimeraVez;
        public ServicioController(iLimpiador limpiador, iCliente cliente, iDetalleServicio detalleServicio, iServicio servicio)
        {
            _limpiador = limpiador;
            _detalleServicio = detalleServicio;
            _cliente = cliente;
            _servicio = servicio;
        }
        public IActionResult Index()
        {
            return View(_limpiador.GetAllCleaners());
        }
        public IActionResult ListadoLimp_Serv()
        {
            return View(_limpiador.GetAllCleaners());
        }
        [Route("Servicio/VerServicios/{Nombre}")]
        public IActionResult VerServicios(string Nombre) {
            return View(_limpiador.getServ(Nombre));
        }

        public IActionResult Carrito(
            //variables que recibimos del listado de limpiadores
            string idLimp, string Nom, string Ape, DateTime TInicial, DateTime TFinal, string catServicio, double Tarifa, DateTime Fecha)
        {
            //Lista para los intervalos de tiempo
            List<string> opcionesTiempo = new List<string>();

            while (TInicial <= TFinal)
            {
                opcionesTiempo.Add(TInicial.ToString("HH:mm"));
                TInicial = TInicial.AddMinutes(30);
            }
            Console.Write(opcionesTiempo.Count());
            //verificar si la sesion de usuario existe
            var objSession = HttpContext.Session.GetString("sUsuario");
            if (objSession != null)
            {
                //Deserializar
                TbUser usuario = JsonConvert.DeserializeObject<TbUser>(objSession);
                TbCliente cliente = _cliente.getCliente(usuario.Usr, usuario.Pwd);

                if (_cliente.SearchCli(usuario.Usr, usuario.Pwd) == false)
                {
                    return RedirectToAction("FormCliente", "Cliente");
                }
                else
                {
                    //USAREMOS UN GUID PARA IDENTIFICAR NUESTRO SERVICIO Y DETALLE SERVICIO
                    string sessionIdString = HttpContext.Session.GetString("SessionId");
                    Guid sessionId;

                    if (string.IsNullOrEmpty(sessionIdString))
                    {
                        // Si no existe, generar un nuevo GUID
                        sessionId = Guid.NewGuid();

                        // Guardar el GUID en la sesión
                        HttpContext.Session.SetString("SessionId", sessionId.ToString());
                    }
                    else
                    {
                        // Si ya existe, obtener el GUID de la sesión
                        sessionId = Guid.Parse(sessionIdString);
                    }

                    //VIEWBAGS PARA LA VISTA DEL CARRITO PARA GRABAR
                    //ViewBag para el GUID
                    ViewBag.GUID = sessionId;
                    //ViewBag para el ID
                    ViewBag.idLimp = idLimp;
                    ViewBag.IdCli = cliente.IdCli;
                    //agregamos a un ViewBag para pasar el nombre y apellido del limpiador
                    ViewBag.NombreApellidoLimpiador = Nom + " " + Ape;
                    //ViewBag para el NomApe del cliente, obtenemos de la sesion
                    ViewBag.NombreApellidoCliente = cliente.NomCli + " " + cliente.ApeCli;
                    //ViewBag para la dirCli, obtenemos de la sesion
                    ViewBag.DireccionCliente = cliente.DirCli;
                    //ViewBag para la categoria del servicio
                    ViewBag.CategoriaServicio = catServicio;
                    Console.WriteLine(catServicio);
                    //ViewBag para la fecha del servicio
                    ViewBag.FechaServicio = Fecha.Date;
                    Console.WriteLine(Fecha.Date);
                    //tarifa
                    ViewBag.Tarifa = Tarifa;
                    Console.WriteLine("la tarifa es: " + Tarifa);
                    //usamos nuestra lista y eliminamos el ultimo element
                    opcionesTiempo.RemoveAt(opcionesTiempo.Count - 1);
                    //ViewBag para el tiempo
                    ViewBag.OpcionesTiempo = opcionesTiempo;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Limpia");
            }
        }

        public IActionResult Carrito_grabado(
            TbServicio obj,
            string nomApeLimp, string dirCli, string idCli, string area)
        {
            var objSession = HttpContext.Session.GetString("sUsuario");
            if (objSession != null)
            {
                //Deserializar
                TbUser usuario = JsonConvert.DeserializeObject<TbUser>(objSession);
                TbCliente cliente = _cliente.getCliente(usuario.Usr, usuario.Pwd);

                if (_cliente.SearchCli(usuario.Usr, usuario.Pwd) == false)
                {
                    return RedirectToAction("FormCliente", "Cliente");
                }
                else
                {
                    // Obtener el GUID de la sesión
                    string sessionIdString = HttpContext.Session.GetString("SessionId");
                    //tratamos de convertir el string a guid
                    if (!Guid.TryParse(sessionIdString, out Guid sessionId))
                    {
                        // Manejar el caso en el que no se pueda obtener el GUID de la sesión
                        Console.WriteLine("Error al tratar de convertir el guid");                        
                    }
                    TimeSpan Standardtime = new TimeSpan(0, 30, 0);
                    //Luego dependiendo de los datos se extraen para implementarlo para el TB_DetalleServicio
                    TbDetalleservicio detServicio = new TbDetalleservicio();
                    //asignamos los valores al detalle servicio
                    //el id va a ir nulo por mientras, cuando se haga el pago se crea el servicio y se jala el id
                    // para el detServicio
                    //DETALLES DE NUESTRO SERVICIO
                    detServicio.IdServ = null;
                    detServicio.NomapeLim = nomApeLimp;
                    detServicio.NomapeCli = cliente.NomCli + " " + cliente.ApeCli;
                    detServicio.CatServ = obj.CatServ;
                    detServicio.TarifaLimp = obj.PreServ;
                    detServicio.Area = decimal.Parse(area);
                    decimal impServ = (decimal)((decimal)detServicio.TarifaLimp * detServicio.Area);

                    detServicio.ImpServ = impServ;
                    detServicio.DirCli = dirCli;
                    detServicio.DurServ = Standardtime;
                    //luego cambiar obj.horaServ con la hora en la que se pago
                    detServicio.HoraDetserv = obj.HoraServ;
                    detServicio.FecServ = obj.FecServ.Date;
                    detServicio.IdCli = cliente.IdCli;
                    detServicio.Guidetserv = obj.Guidserv;
                    detServicio.IdLimp = obj.IdLimp;
                    //Con los datos implementados al objeto "detServicio" se añaden a la base de datos
                    _detalleServicio.add(detServicio);
                }
                //retornar con respecto al GUID
                return View(_detalleServicio.GetDetallesxGuid((Guid)obj.Guidserv));
            }
            else
            {
                return RedirectToAction("Login", "Limpia");
            }
            return View();
        }

        public IActionResult Pago()
        {
            // Obtener el identificador de sesión
            string sessionIdString = HttpContext.Session.GetString("SessionId");

            // Verificar si el identificador de sesión es válido
            if (!string.IsNullOrEmpty(sessionIdString) && Guid.TryParse(sessionIdString, out Guid sessionGuid))
            {
                //Crear el objeto servicio, obtenemos la informacion de la tabla detalle servicio
                TbServicio objServ = new TbServicio();
                /*ya que tenemos el objeto servicio, y generado el id, se lo asignamos a los elementos de la 
                tabla detalle servicio que tengan en mismo GUID*/
                objServ.IdServ = _servicio.GetNextServId().ToString();
                Console.WriteLine("el id del servicio es: " + objServ.IdServ);
                objServ.IdCli = _detalleServicio.GetDetallesxGuid(sessionGuid).FirstOrDefault(detalle => detalle.IdCli != null).IdCli;
                Console.WriteLine("el id del cliente es: " + objServ.IdCli);
                objServ.IdLimp = _detalleServicio.GetDetallesxGuid(sessionGuid).FirstOrDefault(detalle => detalle.IdLimp != null).IdLimp;
                //contar cuantas categorias de servicio tenemos
                int cantidadRegistros = _detalleServicio.GetDetallesxGuid(sessionGuid).Count(detalle => detalle.CatServ != null);
                //si hay 1 registro entonces le pasamos el valor de catServ al objeto
                if (cantidadRegistros == 1)
                {
                    //ahora que sabemos que hay 1 registro, extraemos ese valor con first or default
                    objServ.CatServ = _detalleServicio.GetDetallesxGuid(sessionGuid).FirstOrDefault(detalle => detalle.CatServ != null).CatServ;
                }
                else
                {
                    objServ.CatServ = "multiple";
                }
                //Acumular el PrecServ respecto al guid
                objServ.PreServ = _detalleServicio.GetDetallesxGuid(sessionGuid)
                .Sum(detalle => detalle.ImpServ);
                Console.WriteLine("el acumulado es: " + objServ.PreServ);
                //agregamos la fecha de la transaccion (servicio)
                objServ.FecServ = DateTime.Now.Date;
                //sumamos la duracion de los detalles del servicio para tener el total
                TimeSpan totalDurServ = _detalleServicio.GetDetallesxGuid(sessionGuid)
                .Select(detalle => detalle.DurServ)
                .Aggregate(TimeSpan.Zero, (subtotal, durServ) => subtotal + durServ);

                objServ.DurServ = totalDurServ;
                //agregamos la hora de la transaccion (servicio)
                objServ.HoraServ = DateTime.Now.TimeOfDay;
                objServ.Guidserv = sessionGuid;
                Console.WriteLine("el guid es es: " + objServ.Guidserv);
                //Se agregan primero los datos al TB_Servicio, esto va a la hora de hacer el pago
                _servicio.add(objServ);
                /*asignamos el  id del detalleservicio despues de agregar el servicio a la BD
                para no tener problemas al añadir el idservicio en tbdetalleservicio ya que
                es una foreign key*/
                _detalleServicio.AsignarId(sessionGuid, objServ.IdServ);
                // Eliminar la sesión
                HttpContext.Session.Remove("SessionId");
            }
            return View();
        }

        public IActionResult Filtrado(DateTime fecha, DateTime fecha_inicio, DateTime fecha_fin)
        {
            //la fecha tiene un valor valido?
            if (fecha != DateTime.MinValue)
            {
                ViewBag.Fecha = fecha;                
                return View(_limpiador.GetLimpiadoresFecha(fecha));
            }
            else
            {
                return FiltradoFechas(fecha_inicio, fecha_fin);
            }
        }

        public IActionResult FiltradoFechas(DateTime fecha_inicio, DateTime fecha_fin)
        {
            Console.WriteLine("filtradofechas");
            ViewBag.FechaInicio = fecha_inicio;
            ViewBag.FechaFin = fecha_fin;
            return View("FiltradoFechas", _limpiador.GetLimpiadoresFechaInicioFin(fecha_inicio, fecha_fin));
        }


        [Route("Servicio/Eliminar/{Id}")]
        public IActionResult eliminar(string id)
        {
            // Obtener el GUID de la sesión
            string sessionIdString = HttpContext.Session.GetString("SessionId");
            if (!Guid.TryParse(sessionIdString, out Guid sessionId))
            {
                // Manejar el caso en el que no se pueda obtener el GUID de la sesión
                Console.WriteLine("Error al tratar de convertir el guid");
            }
            _detalleServicio.remove(id);
            //si hay elementos en el carrito:
            if (_detalleServicio.GetDetallesxGuid(sessionId).Any())
            {
                return View("Carrito_grabado", _detalleServicio.GetDetallesxGuid(sessionId));
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login", "Limpia");
        }
    }
}
