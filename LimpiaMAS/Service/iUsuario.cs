using LimpiaMAS.Models;

namespace LimpiaMAS.Service
{
    public interface iUsuario
    {
        IEnumerable<TbUser> GetAllUsers();
        void add(TbUser user);
        void remove(string id);
        TbUser edit(String id);
        void EditDetails(TbUser user);
    }
}
