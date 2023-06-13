using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVisitas.Models
{
	public class Cliente
	{
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength( maximumLength:100, MinimumLength = 2, ErrorMessage = "El campo {0} debe tener una logitud entre {0} y {1} caracteres." )]
        public string Nombres { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "El campo {0} debe tener una logitud entre {0} y {1} caracteres.")]
		public string Apellidos { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [Display(Name ="Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; } = DateTime.Today.AddYears(-18);
        
        [StringLength(maximumLength: 10, ErrorMessage = "El campo {0} debe tener una logitud maxima de {0} caracteres.")]
		public string? Dni { get; set; }
		[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Debe ser un email válido")]
        [StringLength(maximumLength:50)]
		public string? Email { get; set; }
		//[Phone]
		[StringLength(maximumLength: 50)]
		public string? Telefono { get; set; }
        [StringLength (maximumLength: 100, ErrorMessage ="El campo {0} debe tener una logitud maxima de {0} caracteres.")]
		public string? Direccion { get; set; }
        [StringLength(maximumLength:100)]
		public string? CiudadRegion { get; set; }
        public bool Anulado { get; set; } = false;
        //
        [NotMapped]
        public string NombreCompleto => $"{ Nombres } { Apellidos}";

    }
}
