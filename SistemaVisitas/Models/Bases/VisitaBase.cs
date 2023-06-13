using SistemaVisitas.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaVisitas.Models.Bases
{
	public class VisitaBase
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido.")]
		[Display(Name = "Cliente")]
		public int IdCliente { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido.")]
		[Display(Name = "Tipo Visita")]
		public int IdTipoVisita { get; set; }
		[Display(Name = "Cita")]
		public int? IdCita { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido.")]
		[Display(Name = "Local")]
		public int? IdLocal { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido.")]
		[DataType(DataType.DateTime)]
		[Display(Name = "Fecha Hora Inicio")]
		public DateTime FechHoraInicio { get; set; } = DateTime.Parse(DateTime.Now.AddMinutes(-30).ToString("g"));
		[Required(ErrorMessage = "El campo {0} es requerido.")]
		[DataType(DataType.DateTime)]
		[Display(Name = "Fecha Hora Fin")]
		public DateTime FechHoraFin { get; set; } = DateTime.Parse(DateTime.Now.ToString("g"));
		[StringLength(1000)]
		public string? Observacion { get; set; }
		public EstadosCita EstadoVisita { get; set; } = EstadosCita.Atendido;
		[Required(ErrorMessage = "El campo {0} es requerido.")]
		[Display(Name = "Usuario Atencion")]
		public string IdUsuarioAtencion { get; set; }
	}
}
