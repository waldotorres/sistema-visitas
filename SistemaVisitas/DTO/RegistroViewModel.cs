using System.ComponentModel.DataAnnotations;

namespace SistemaVisitas.DTO
{
	public class RegistroViewModel
	{
		[Required(ErrorMessage ="El campo {0} es requerido.")]
		[StringLength(50, ErrorMessage ="El campo {0} debe tener como máximo {1} caracteres.")]
		[EmailAddress(ErrorMessage = "Ingrese un email válido")]
		public string Email { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido.")]
		[StringLength(maximumLength:50, MinimumLength =6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
