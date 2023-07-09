using LimpiaMAS.Models;

namespace LimpiaMAS.Service
{
    public interface iLimpiador
    {
        IEnumerable<TbLimpiador> GetAllCleaners();
        IEnumerable<TbLimpiador> GetLimpiadoresFecha(DateTime fecha);
        IEnumerable<TbLimpiador> GetLimpiadoresFechaInicioFin(DateTime inicio, DateTime fin);

        void add(TbLimpiador limpiador);
        void remove(string id);
        TbLimpiador edit(string id);
        TbLimpiador getServ(string nombre);
        void EditDatails(TbLimpiador limpiador);
    }
}
