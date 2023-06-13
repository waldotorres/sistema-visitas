using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVisitas.Enums;
using SistemaVisitas.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaVisitas.DTO
{
	public class CitaCreacionDTO
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Cliente")]
		public int ClienteId { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Local")]
		public int LocalId { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Tipo Visita")]
		public int TipoVisitaId { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Fecha y Hora de Inicio")]
		[DataType(DataType.DateTime )]
		public DateTime FechaHoraInicioCita { get; set; } =  DateTime.Parse( DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Fecha y Hora Final")]
		[DataType(DataType.DateTime)]
		public DateTime FechaHoraFinCita { get; set; } = DateTime.Parse(DateTime.Now.AddMinutes(30).ToString("dd/MM/yyyy HH:mm"));
		public string? Observacion { get; set; }
		public EstadosCita EstadoCita { get; set; } = EstadosCita.Pendiente;

		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name ="Cliente")]
		public string NombreClienteVista { get; set; }

        public Cliente? Cliente { get; set; }
        public IEnumerable<SelectListItem>? ListaLocales { get; set; }
		public IEnumerable<SelectListItem>? ListaTipoVisita { get; set; }
	}
}


