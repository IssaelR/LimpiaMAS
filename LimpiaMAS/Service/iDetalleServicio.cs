using LimpiaMAS.Models;

namespace LimpiaMAS.Service
{
    public interface iDetalleServicio
    {
        public IEnumerable<TbDetalleservicio> GetAllDetalles(string idCli);
        public IEnumerable<TbDetalleservicio> GetDetallesxGuid(Guid guid);
        public void AsignarId(Guid guid, string idServ);
        void add(TbDetalleservicio detalleServ);
        void remove(string id);
        TbDetalleservicio edit(String id);
        void EditDetails(TbDetalleservicio detalleServ);
    }
}
