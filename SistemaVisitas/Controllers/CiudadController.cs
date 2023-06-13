using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVisitas.DTO;
using SistemaVisitas.Models;

namespace SistemaVisitas.Controllers
{
	public class CiudadController:CustomController
	{
		private readonly IMapper mapper;
		private readonly ApplicationDbContext context;

		public CiudadController( IMapper mapper, ApplicationDbContext context )
        {
			this.mapper = mapper;
			this.context = context;
		}
		public async Task<IActionResult> Index()
		{
			var ciudades = await context.Ciudades.Include(x => x.Region).ToListAsync();
			var modelo = ciudades.GroupBy(x => x.Region.Nombre)
						.Select(x => new CiudadIndexDTO()
						{
							RegionNombre = x.Key,
							Ciudades = x.OrderBy(x=> x.Nombre).AsEnumerable()
						})
						.OrderBy( x=> x.RegionNombre)
						.ToList();


			return View(modelo);
		}

		public async Task<IActionResult> CrearActualizar(int? id)
		{
			var modelo = new CiudadCreacionDTO();
			if(id != null)
			{
				var ciudad = await context.Ciudades.FirstOrDefaultAsync(x => x.Id == id);
				modelo = mapper.Map<CiudadCreacionDTO>(ciudad);
				
			}
			// obtiene la lista de regiones
			modelo.ListaRegiones = await context.Regiones.AsNoTracking()
									.OrderBy(x=> x.Nombre)
									.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToListAsync();

			modelo.ListaRegiones = modelo.ListaRegiones.Prepend(new SelectListItem { Text = "<Seleccione una Region>", Value = "" }) ;

			return View(modelo);

		}

		[HttpPost]
		public async Task<IActionResult> CrearActualizar( CiudadCreacionDTO ciudadCreacion )
		{

			var ciudad = new Ciudad();
			if (ciudadCreacion.Id > 0)
			{
				ciudad = await context.Ciudades.FirstOrDefaultAsync(x => x.Id == ciudadCreacion.Id);
				if(ciudad == null)
				{
					return RedirectToAction("NoEncontrado", "Home");
				}
				ciudad = mapper.Map(ciudadCreacion, ciudad);

			}
			else
			{
				ciudad = mapper.Map<Ciudad>(ciudadCreacion);
				context.Ciudades.Add(ciudad);
			}
			

			await context.SaveChangesAsync();

			return RedirectToAction("Index");

		}

		[HttpDelete]
		public async Task<IActionResult> Borrar(int id)
		{
			var ciudad = context.Ciudades.FirstOrDefault(x => x.Id == id);
			if( ciudad == null)
			{
				return BadRequest(new { ok=false, msgError = "Recurso no encontrado" });
			}
			context.Ciudades.Remove(ciudad);
			await context.SaveChangesAsync();	
			return RedirectToAction("Index");
		}

	}
}
