using Microsoft.AspNetCore.Identity;
using SistemaVisitas.Enums;
using SistemaVisitas.Models.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVisitas.Models
{
	public class Visita: VisitaBase
	{
        public string IdUsuario { get; set; }
		public DateTime fechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioModificacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
		public string? IdUsuarioAnulacion { get; set; }
		public DateTime? fechaAnulacion { get; set; }

		[ForeignKey("IdTipoVisita")]
		public TipoVisita TipoVisita { get; set; }
        [ForeignKey("IdLocal")]
        public Local Local { get; set; }
		[ForeignKey("IdCita")]
		public Cita Cita { get; set; }
		[ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
		[ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
		[ForeignKey("IdUsuarioModificacion")]
		public Usuario UsuarioModificacion { get; set; }
		[ForeignKey("IdUsuarioAtencion")]
		public Usuario UsuarioAtencion { get; set; }

	}
}
