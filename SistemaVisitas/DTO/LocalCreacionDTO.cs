using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVisitas.Models;

namespace SistemaVisitas.DTO
{
	public class LocalCreacionDTO:Local
	{
        public IEnumerable<SelectListItem> ListaCiudades { get; set; }
        public string NombreCiudadRegion { get; set; }

    }
}
