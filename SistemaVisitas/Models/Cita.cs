using SistemaVisitas.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVisitas.Models
{
	public class Cita
	{
        public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name ="Cliente")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido" )]
		[Display(Name = "Local")]
		public int LocalId { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tipo Visita" )]
		public int TipoVisitaId { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Fecha y Hora de Inicio")]
		[DataType(DataType.DateTime)]
		public DateTime FechaHoraInicioCita { get; set; } = DateTime.Now;
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Fecha y Hora Final")]
		[DataType(DataType.DateTime)]	
		public DateTime FechaHoraFinCita { get; set; } = DateTime.Now.AddMinutes(30);
		public string? Observacion { get; set; }
        public EstadosCita EstadoCita { get; set; } = EstadosCita.Pendiente;

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
        [ForeignKey("LocalId")]
        public Local Local { get; set; }
        [ForeignKey("TipoVisitaId")]
        public TipoVisita TipoVisita { get; set; }

    }
}
