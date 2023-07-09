using LimpiaMAS.Models;

namespace LimpiaMAS.Service
{
    public interface iRegister
    {
        void add_usr(TbUser obj);
        void add_cli(TbCliente obj);
        void add_limp(TbLimpiador obj);
        void add_dis(TbDisponibilidad obj);
        IEnumerable<TbCliente> GetAllClients();
        IEnumerable<TbLimpiador> GetAllCleaners();

        public TbUser getUser(string usr, string pwd);
        TbCliente getCli(string usr, string pwd);
        TbLimpiador getLimp(string usr, string pwd);
    }
}
