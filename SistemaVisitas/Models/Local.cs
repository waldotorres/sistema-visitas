using SistemaVisitas.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVisitas.Models
{
	public class Local
	{
        public int Id { get; set; }
		
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "La longitud debe ser entre {2} y {1}")]
		public string Nombre { get; set; }
		
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "La longitud debe ser entre {2} y {1}")]
		public string Direccion { get; set; }

		[Display(Name ="Ciudad")]
		[Required(ErrorMessage = "El campo {0} es requerido.")]
        public int IdCiudad { get; set; }

		public EstadosEntidad EstadoLocal { get; set; } = EstadosEntidad.Activo;


        [ForeignKey("IdCiudad")]
        public Ciudad Ciudad { get; set; }



    }
}
