using LimpiaMAS.Models;
using Microsoft.AspNetCore.Mvc;

namespace LimpiaMAS.Service
{
    public class UserRepository : iUsuario
    {
        private Limpia_MasC conexion = new Limpia_MasC();

        public void add(TbUser user)
        {
            try
            {
                user.IdUsr = GetNextUsrId().ToString();
                conexion.TbUsers.Add(user);
                conexion.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al graba los datos");
            }
        }

        public int GetNextUsrId()
        {
            int nextId = 1;

            // hay registros?
            if (conexion.TbLimpiadors.Any())
            {
                // obtener el ultimo id
                string lastId = conexion.TbUsers.Max(u => u.IdUsr);

                // generar el siguiente id + 1
                nextId = int.Parse(lastId) + 1;
            }

            return nextId;
        }

        public TbUser edit(string id)
        {
            var obj = (from tUser in conexion.TbUsers
                       where tUser.IdUsr == id
                       select tUser).Single();
            return obj;
        }

        public void EditDetails(TbUser user)
        {
            var objAModificar = (from tUser in conexion.TbUsers
                                 where tUser.IdUsr == user.IdUsr
                                 select tUser).Single();
            objAModificar.Nom = user.Nom;
            objAModificar.Ape = user.Ape;
            objAModificar.Usr = user.Usr;
            objAModificar.Pwd = user.Pwd;
            conexion.SaveChanges();
        }

        public IEnumerable<TbUser> GetAllUsers()
        {
            return conexion.TbUsers;
        }
        public void remove(string id)
        {
            var obj = (from tUser in conexion.TbUsers
                       where tUser.IdUsr == id
                       select tUser).Single();
            conexion.Remove(obj);
            conexion.SaveChanges();
        }
    }
}
