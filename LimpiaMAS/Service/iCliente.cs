using LimpiaMAS.Models;

namespace LimpiaMAS.Service
{
    public interface iCliente
    {
        IEnumerable<TbCliente> GetAllClientes();
        void add(TbCliente cliente);
        void remove(string id);
        TbCliente edit(String id);
        void EditDatails(TbCliente cliente);
        TbCliente getCliente(string usr, string pwd);
        bool SearchCli(string usr, string pwd);
    }
}
