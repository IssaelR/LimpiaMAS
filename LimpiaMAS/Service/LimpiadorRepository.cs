using LimpiaMAS.Models;
using Microsoft.EntityFrameworkCore;

namespace LimpiaMAS.Service
{
    public class LimpiadorRepository : iLimpiador
    {
        Limpia_MasC conexion = new Limpia_MasC();

        public void add(TbLimpiador limpiador)
        {
            try
            {
                limpiador.IdLimp = GetNextLimpId().ToString();
                conexion.TbLimpiadors.Add(limpiador);
                conexion.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al grabar al archivo", ex.Message);
            }
        }

        public int GetNextLimpId()
        {
            int nextId = 1;

            // hay registros?
            if (conexion.TbLimpiadors.Any())
            {
                // obtener el ultimo id
                string lastId = conexion.TbLimpiadors.Max(u => u.IdLimp);

                // generar el siguiente id + 1
                nextId = int.Parse(lastId) + 1;
            }

            return nextId;
        }

        public TbLimpiador edit(string id)
        {
            var obj = (from tLimp in conexion.TbLimpiadors where tLimp.IdLimp == id select tLimp).Single();
            return obj;
        }
        public TbLimpiador getServ(string nombre)
        {
            var obj = (from tLimp in conexion.TbLimpiadors where tLimp.NomLimp == nombre select tLimp).Single();
            return obj;
        }
        public void EditDatails(TbLimpiador limpiador)
        {
            var objModificar = (from tLimp in conexion.TbLimpiadors where tLimp.IdLimp == limpiador.IdLimp select tLimp).Single();
            objModificar.NomLimp = limpiador.NomLimp;
            objModificar.ApeLimp = limpiador.ApeLimp;
            objModificar.DirLimp = limpiador.DirLimp;
            objModificar.TelLimp = limpiador.TelLimp;
            objModificar.DisLimp = limpiador.DisLimp;
            objModificar.TarLimp = limpiador.TarLimp;
            /*bjModificar.FotLimp = limpiador.FotLimp;*/

            conexion.SaveChanges();
        }

        public IEnumerable<TbLimpiador> GetTodayCleaners()
        {
            //se genera la fecha actual
            DateTime fechaActual = DateTime.Now;
            var limpiadoresDisponibles = (from limpiador in conexion.TbLimpiadors
                                          join disponibilidad in conexion.TbDisponibilidads
                                              on limpiador.IdLimp equals disponibilidad.IdLimp
                                          where disponibilidad.FecDis.Date == fechaActual
                                          select limpiador).ToList();

            return limpiadoresDisponibles;
        }

        public IEnumerable<TbLimpiador> GetAllCleaners()
        {
            return conexion.TbLimpiadors.Include(l => l.TbDisponibilidads).ToList();
        }

        public void remove(string id)
        {
            var obj = (from tLimp in conexion.TbLimpiadors where tLimp.IdLimp == id select tLimp).Single();
            conexion.Remove(obj);
            conexion.SaveChanges();
        }

        /*         FILTRAMOS LOS LIMPIADORES POR FECHA        */
        public IEnumerable<TbLimpiador> GetLimpiadoresFecha(DateTime fecha)
        {
            var limpiadores = conexion.TbLimpiadors
                .Where(l => l.TbDisponibilidads.Any(d => d.FecDis == fecha))
                .ToList();

            return limpiadores;
        }

        public IEnumerable<TbLimpiador> GetLimpiadoresFechaInicioFin(DateTime inicio, DateTime fin)
        {
            var limpiadores = conexion.TbLimpiadors
            .Where(l => l.TbDisponibilidads.Any(d => d.FecDis >= inicio && d.FecDis <= fin))
            .ToList();

            return limpiadores;
        }
    }
}
