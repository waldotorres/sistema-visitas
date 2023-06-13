using SistemaVisitas.Models;

namespace SistemaVisitas.DTO
{
	public class ClienteIndexDTO
	{
        public List<Cliente> ListaClientes { get; set; }
        public int CantidadRegistros { get; set; }
        public int paginaAnterior { get; set; }
		public int paginaSiguiente { get; set; }
	}
}
