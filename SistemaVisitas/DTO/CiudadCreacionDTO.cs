using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVisitas.Models;

namespace SistemaVisitas.DTO
{
	public class CiudadCreacionDTO:Ciudad
	{
        public IEnumerable <SelectListItem> ListaRegiones { get; set; }
    }
}
