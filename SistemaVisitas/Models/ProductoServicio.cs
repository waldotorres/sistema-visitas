namespace SistemaVisitas.Models
{
	public class ProductoServicio
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EsServicio { get; set; } = false;
        public string UrlImagen { get; set; }
	}
}
