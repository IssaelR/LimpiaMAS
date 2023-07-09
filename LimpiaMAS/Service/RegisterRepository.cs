using LimpiaMAS.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LimpiaMAS.Service
{
    public class RegisterRepository : iRegister
    {
        private readonly Limpia_MasC conexion = new Limpia_MasC();
        public void add_limp(TbLimpiador obj)
        {
            obj.IdLimp = GetNextLimpId().ToString();
            conexion.TbLimpiadors.Add(obj);
            conexion.SaveChanges();
        }

        public void add_usr(TbUser obj)
        {
            obj.IdUsr = GetNextUsrId().ToString(); // Obtener el siguiente valor de la secuencia
            conexion.TbUsers.Add(obj);
            conexion.SaveChanges();
        }
        public void add_cli(TbCliente obj)
        {
            obj.IdCli = GetNextCliId().ToString(); // Obtener el siguiente valor de la secuencia
            conexion.TbClientes.Add(obj);
            conexion.SaveChanges();
        }
        public void add_dis(TbDisponibilidad obj)
        {
            obj.Id = GetNextDisId().ToString();
            conexion.TbDisponibilidads.Add(obj);
            conexion.SaveChanges();
        }
        public int GetNextCliId()
        {
            int nextId = 1;

            // hay registros?
            if (conexion.TbClientes.Any())
            {
                // obtener el ultimo id
                string lastId = conexion.TbClientes.Max(u => u.IdCli);

                // generar el siguiente id + 1
                nextId = int.Parse(lastId) + 1;
            }

            return nextId;
        }
        public int GetNextDisId()
        {
            int nextId = 1;

            // hay registros?
            if (conexion.TbDisponibilidads.Any())
            {
                // obtener el ultimo id
                string lastId = conexion.TbDisponibilidads.Max(u => u.Id);

                // generar el siguiente id + 1
                nextId = int.Parse(lastId) + 1;
            }

            return nextId;
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
        public int GetNextUsrId()
        {
            int nextId = 1;

            // hay registros?
            if (conexion.TbUsers.Any())
            {
                // obtener el ultimo id
                string lastId = conexion.TbUsers.Max(u => u.IdUsr);

                // generar el siguiente id + 1
                nextId = int.Parse(lastId) + 1;
            }

            return nextId;
        }

        public TbUser getUser(string usr, string pwd)
        {
            var user = conexion.TbUsers.FirstOrDefault(l => l.Usr == usr && l.Pwd == pwd);
            return user;
        }

        public TbCliente getCli(string usr, string pwd)
        {
            var cli = conexion.TbClientes.FirstOrDefault(l => l.Usr == usr && l.Pwd == pwd);
            return cli;
        }
        public TbLimpiador getLimp(string usr, string pwd)
        {
            var limp = conexion.TbLimpiadors.FirstOrDefault(l => l.Usr == usr && l.Pwd == pwd);
            return limp;
        }

        public IEnumerable<TbLimpiador> GetAllCleaners()
        {
            return conexion.TbLimpiadors;
        }

        public IEnumerable<TbCliente> GetAllClients()
        {
            return conexion.TbClientes;
        }


    }
}
