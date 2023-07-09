using LimpiaMAS.Models;

namespace LimpiaMAS.Service
{
    public class LogeoRepository : iLogeo
    {
        private readonly Limpia_MasC conexion = new Limpia_MasC();

        public int LoginComparision(TbUser user)
        {
            var obj_usr = (from tusu in conexion.TbUsers where tusu.Usr == user.Usr && tusu.Pwd == user.Pwd select tusu).FirstOrDefault();
            var obj_adm = (from tadm in conexion.TbAdmins where tadm.UsrAdm == user.Usr && tadm.PwdAdm == user.Pwd select tadm).FirstOrDefault();
            if (obj_usr == null)
            {
                if (obj_adm == null)
                {
                    return 0;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 1;
            }
        }
    }
}
