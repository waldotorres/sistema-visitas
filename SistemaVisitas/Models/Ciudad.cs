using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVisitas.Models
{
	public class Ciudad
	{
        public int Id { get; set; }
        [Display(Name ="Region")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int IdRegion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido" )]
        [StringLength(maximumLength:50, MinimumLength = 1, ErrorMessage = "La longitud debe ser entre {2} y {1}")]
        public string Nombre { get; set; }
        [ForeignKey("IdRegion")]
        public Region Region { get; set; }
    }
}
