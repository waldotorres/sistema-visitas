using Microsoft.AspNetCore.Identity;
using SistemaVisitas.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaVisitas.Models
{
	public class Usuario:IdentityUser
	{
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[MaxLength(20)]
		[Display(Name = "Codigo de Usuario")]
		public string CodUsuario { get; set; }
		[Required(ErrorMessage ="El campo {0} es requerido")]
		[MaxLength(100)]
		public string Nombres { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[MaxLength(100)]
		public string Apellidos { get; set; }
		public EstadosEntidad EstadoUsuario { get; set; } = EstadosEntidad.Activo;
		[Display(Name ="¿Usuario Atención?")]
		public bool EsUsuarioAtencion { get; set; } = false;


    }
}
