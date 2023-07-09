using LimpiaMAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LimpiaMAS.Service
{
    public class DetalleServicioRepository : iDetalleServicio
    {
        Limpia_MasC conexion = new Limpia_MasC();
        public void add(TbDetalleservicio detalleServ)
        {
            try
            {
                detalleServ.IdDetserv = GetNextDetId().ToString();
                conexion.TbDetalleservicios.Add(detalleServ);
                conexion.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al grabar al archivo", ex.Message);
            }
        }


        public int GetNextDetId()
        {
            int nextId = 1;

            // hay registros?
            if (conexion.TbDetalleservicios.Any())
            {
                // obtener el ultimo id
                string lastId = conexion.TbDetalleservicios.Max(u => u.IdDetserv);

                // generar el siguiente id + 1
                nextId = int.Parse(lastId) + 1;
            }

            return nextId;
        }

        public TbDetalleservicio edit(string id)
        {
            var obj = (from tDetServ in conexion.TbDetalleservicios where tDetServ.IdDetserv == id select tDetServ).Single();
            return obj;
        }

        public void EditDetails(TbDetalleservicio detalleServ)
        {
            var objModificar = (from tDetServ in conexion.TbDetalleservicios where tDetServ.IdDetserv == detalleServ.IdDetserv select tDetServ).Single();
            objModificar.CatServ = detalleServ.CatServ;
            objModificar.ImpServ = detalleServ.ImpServ;
            objModificar.NomapeCli = detalleServ.NomapeCli;
            objModificar.DirCli = detalleServ.DirCli;
            objModificar.NomapeLim = detalleServ.NomapeLim;

            conexion.SaveChanges();
        }

        public void AsignarId(Guid guid, string idServ)
        {
            try
            {
                conexion.TbDetalleservicios.Where(d => d.Guidetserv == guid).ToList().ForEach(detalle => detalle.IdServ = idServ);
                conexion.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al grabar al archivo", ex.Message);
            }
        }

        public IEnumerable<TbDetalleservicio> GetDetallesxGuid(Guid guid)
        {
            return conexion.TbDetalleservicios.Where(d => d.Guidetserv == guid);
        }

        public IEnumerable<TbDetalleservicio> GetAllDetalles(string IdCli)
        {
            return conexion.TbDetalleservicios.Where(d => d.IdCli == IdCli);
        }

        public void remove(string id)
        {
            var obj = (from tDetServ  in conexion.TbDetalleservicios where tDetServ.IdDetserv == id select tDetServ).Single();
            conexion.Remove(obj);
            conexion.SaveChanges();
        }
    }
}
