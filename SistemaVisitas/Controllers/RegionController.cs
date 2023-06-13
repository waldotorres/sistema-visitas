using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using SistemaVisitas.DTO;
using SistemaVisitas.Extensiones;
using SistemaVisitas.Models;

namespace SistemaVisitas.Controllers
{
	public class RegionController: CustomController
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public RegionController( ApplicationDbContext context, IMapper mapper )
        {
			this.context = context;
			this.mapper = mapper;
		}
		public async Task<ActionResult> Index([FromQuery] PaginacionDTO paginacionDTO) 
		{
			var usuario = base.ObtenerUsuario();
			//
			var queryable = context.Regiones.OrderBy(x => x.Nombre).AsQueryable();

			var regiones = await queryable.Paginar(paginacionDTO)							
							.ToListAsync();

			var modelo = new RegionIndexDTO();

			modelo.Regiones = regiones;
			modelo.totalRegistros = await queryable.CountAsync();
			modelo.paginaAnterior = paginacionDTO.Pagina - 1 > 0 ? paginacionDTO.Pagina - 1 : -1;
			modelo.paginaSiguiente = paginacionDTO.Pagina + 1 <= Math.Ceiling(modelo.totalRegistros * 1.00 / paginacionDTO.RegistrosPorPagina) ? paginacionDTO.Pagina + 1 : -1;


			return View(modelo);

		}

		public  async Task<ActionResult<Region>>  CrearActualizar(int? id)
		{
			var modelo = new Region();
			if ( id != null)
			{
				modelo = await context.Regiones.FirstOrDefaultAsync(x => x.Id == id);
			}
			
			return View(modelo);
		}

		[HttpPost]
		public async Task<IActionResult> CrearActualizar(Region regionCreacion)
		{
			if(regionCreacion.Id > 0)
			{
				context.Regiones.Update(regionCreacion);
			}
			else
			{
				context.Regiones.Add(regionCreacion);
			}
			
			await context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		[HttpDelete]
		public async Task<IActionResult> Borrar(int id)
		{			 
			var region = context.Regiones.FirstOrDefault(x => x.Id == id);
			if (region == null)
			{
				return BadRequest(new { msgError = "No existe el recurso" });
			}
			context.Regiones.Remove(region);
			await context.SaveChangesAsync();
			return Ok();
		}

	}
}
