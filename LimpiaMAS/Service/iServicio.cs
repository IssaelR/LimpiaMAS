using LimpiaMAS.Models;

namespace LimpiaMAS.Service
{
    public interface iServicio
    {
        void add(TbServicio servicio);
        public int GetNextServId();
    }
}
