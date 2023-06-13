using SistemaVisitas.Models;

namespace SistemaVisitas.DTO
{
	public class CitaIndexDTO
	{
        public string NombreLocal { get; set; }
        public IEnumerable<Cita> ListaCitas { get; set; }
    }
}
