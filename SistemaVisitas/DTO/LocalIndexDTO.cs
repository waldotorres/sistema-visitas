using SistemaVisitas.Models;

namespace SistemaVisitas.DTO
{
	public class LocalIndexDTO
	{
        public string NombreRegion { get; set; }
		public IEnumerable<Local> Locales { get; set; }
	}
}
