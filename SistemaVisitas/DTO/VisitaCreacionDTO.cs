using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVisitas.Enums;
using SistemaVisitas.Models;
using SistemaVisitas.Models.Bases;
using System.ComponentModel.DataAnnotations;

namespace SistemaVisitas.DTO
{
	public class VisitaCreacionDTO: VisitaBase
	{

		public List<SelectListItem>? ListaLocales { get; set; }
		public List<SelectListItem>? ListaTipoVisita { get; set; }
        public int? IdCitaOriginal { get; set; } 
        public Usuario? UsuarioAtencion { get; set; }
		public Cliente? Cliente { get; set; }
	}
}
