namespace SistemaVisitas.DTO
{
	public class PaginacionDTO
	{
		private int maxRegistrosPorPagina = 50;
		private int registrosPorPagina = 10;

		public int Pagina { get; set; } = 1;
        //public int canRegistrosOffset => ((Pagina - 1) * registrosPorPagina); 
        public int RegistrosPorPagina { 
			get
			{
				return registrosPorPagina;
			}
			set
			{
				maxRegistrosPorPagina = registrosPorPagina > maxRegistrosPorPagina?maxRegistrosPorPagina:value;
			}
		}
    }
}
