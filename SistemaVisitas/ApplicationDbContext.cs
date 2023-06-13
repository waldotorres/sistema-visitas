using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaVisitas.Models;

namespace SistemaVisitas
{
	public class ApplicationDbContext: IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
		}

		public DbSet<Ciudad> Ciudades { get; set; }
		public DbSet<Region> Regiones { get; set; }
		public DbSet<Local> Locales { get; set; }


		public DbSet<TipoVisita> TiposVisita { get; set; }
		public DbSet<Cita> Citas { get; set; }
		public DbSet<Cliente> Clientes { get; set; }
		public DbSet<ProductoServicio> ProductosServicios { get; set; }
		public DbSet<Visita> Visitas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
