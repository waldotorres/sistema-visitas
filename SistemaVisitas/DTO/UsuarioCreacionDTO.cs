using SistemaVisitas.Enums;
using SistemaVisitas.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaVisitas.DTO
{
	public class UsuarioCreacionDTO
	{

        public string? Id { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }

		[Required(ErrorMessage = "El campo {0} es requerido")]
		[MaxLength(20)]
		[Display(Name = "Codigo de Usuario")]
		public string CodUsuario { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[MaxLength(100)]
		public string Nombres { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[MaxLength(100)]
		public string Apellidos { get; set; }
		public EstadosEntidad EstadoUsuario { get; set; } = EstadosEntidad.Activo;
		[Display(Name = "¿Usuario Atención?")]
		public bool EsUsuarioAtencion { get; set; } = false;

		[Required(ErrorMessage ="El campo {0} es requerido")]
		[StringLength(maximumLength:20, MinimumLength =6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
		
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(maximumLength: 20, MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name ="Confirmar Password")]
		[Compare("Password", ErrorMessage = "Las contraseñas no coinciden. Verifique.")]
		public string PasswordVerificacion { get; set; }
		[Display(Name ="¿Es Admin?")]
		public bool EsUsuarioAdmin { get; set; } = false;

    }
}
