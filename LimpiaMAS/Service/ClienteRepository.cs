using LimpiaMAS.Models;
using Microsoft.EntityFrameworkCore;

namespace LimpiaMAS.Service
{
    public class ClienteRepository : iCliente
    {
        Limpia_MasC conexion = new Limpia_MasC();
        public void add(TbCliente cliente)
        {
            try { 
                cliente.IdCli = GetNextCliId().ToString();
                conexion.TbClientes.Add(cliente);
                conexion.SaveChanges();
            }catch (Exception ex) {
                Console.WriteLine("Ocurrio un error al grabar al archivo",ex.Message);
            }
        }

        public int GetNextCliId()
        {
            int nextId = 1;

            // hay registros?
            if (conexion.TbLimpiadors.Any())
            {
                // obtener el ultimo id
                string lastId = conexion.TbClientes.Max(u => u.IdCli);

                // generar el siguiente id + 1
                nextId = int.Parse(lastId) + 1;
            }

            return nextId;
        }

        public TbCliente edit(string id)
        {
            var obj = (from tCli in conexion.TbClientes where tCli.IdCli == id select tCli).Single();
            return obj;
        }

        public void EditDatails(TbCliente cliente)
        {
            var objModificar = (from tCli in conexion.TbClientes where tCli.IdCli == cliente.IdCli select tCli).Single();
            objModificar.NomCli = cliente.NomCli;
            objModificar.ApeCli = cliente.ApeCli;
            objModificar.DirCli = cliente.DirCli;
            objModificar.TelCli = cliente.TelCli;
            objModificar.Usr = cliente.Usr;
            objModificar.Pwd = cliente.Pwd;

            conexion.SaveChanges();
        }

        public IEnumerable<TbCliente> GetAllClientes()
        {
            return conexion.TbClientes;
        }

        public void remove(string id)
        {
            var obj = (from tbCli in conexion.TbClientes where tbCli.IdCli == id select tbCli).Single();
            conexion.Remove(obj);
            conexion.SaveChanges() ;
        }
        public TbCliente getCliente(string usr, string pwd)
        {
            var cliente = conexion.TbClientes.FirstOrDefault(l => l.Usr == usr && l.Pwd == pwd);
            return cliente;
        }

        public bool SearchCli(string usr, string pwd)
        {
            var obj = (from tcli in conexion.TbClientes where tcli.Usr == usr && tcli.Pwd == pwd select tcli).FirstOrDefault();
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
