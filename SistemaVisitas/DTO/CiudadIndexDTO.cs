using SistemaVisitas.Models;

namespace SistemaVisitas.DTO
{
	public class CiudadIndexDTO
	{
        public string RegionNombre { get; set; }
        public IEnumerable<Ciudad> Ciudades { get; set; }
    }
}
