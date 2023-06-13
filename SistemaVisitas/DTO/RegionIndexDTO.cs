using SistemaVisitas.Models;

namespace SistemaVisitas.DTO
{
	public class RegionIndexDTO
	{
        public int paginaAnterior { get; set; }
		public int paginaSiguiente { get; set; }
        public int totalRegistros { get; set; }
        public List<Region> Regiones { get; set; }
    }
}
