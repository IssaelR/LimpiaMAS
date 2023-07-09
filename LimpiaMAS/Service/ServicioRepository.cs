using LimpiaMAS.Models;

namespace LimpiaMAS.Service
{
    public class ServicioRepository : iServicio
    {
        private readonly Limpia_MasC conexion = new Limpia_MasC();

        public void add(TbServicio servicio)
        {
            try
            {
                //hemos quitado el que genere el id pq lo haremos en el ServicioController                
                conexion.TbServicios.Add(servicio);
                conexion.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al grabar al archivo", ex.Message);
            }
        }

        public int GetNextServId()
        {
            int nextId = 1;

            // hay registros?
            if (conexion.TbServicios.Any())
            {
                // obtener el ultimo id
                string lastId = conexion.TbServicios.Max(u => u.IdServ);

                // generar el siguiente id + 1
                nextId = int.Parse(lastId) + 1;
            }

            return nextId;
        }
    }
}
