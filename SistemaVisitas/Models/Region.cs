using System.ComponentModel.DataAnnotations;

namespace SistemaVisitas.Models
{
	public class Region
	{
        public int Id { get; set; }

		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "La longitud debe ser entre {2} y {1}")]
		public string Nombre { get; set; }
	}
}
