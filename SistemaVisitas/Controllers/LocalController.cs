using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVisitas.DTO;
using SistemaVisitas.Enums;
using SistemaVisitas.Models;
using System.Collections;

namespace SistemaVisitas.Controllers
{
	public class LocalController:CustomController
	{
		private readonly IMapper mapper;
		private readonly ApplicationDbContext context;

		public LocalController( IMapper mapper, ApplicationDbContext context )
        {
			this.mapper = mapper;
			this.context = context;
		}

		public async Task<IActionResult> Index()
		{
			var locales = await context.Locales
							.Include( x=> x.Ciudad)
							.ThenInclude(x=> x.Region).ToListAsync();

			var modelo = locales.GroupBy(x => x.Ciudad.Region.Nombre)
						.Select(x => new LocalIndexDTO()
						{
							Locales = x.OrderBy(x=> x.Nombre).AsEnumerable(),
							NombreRegion = x.Key
						})
						.OrderBy(x => x.NombreRegion);
						 
						;
			return View(modelo);	
		}

		public async Task<IActionResult> CrearActualizar(int? id)
		{
			var modelo = new LocalCreacionDTO();

			if( id != null)
			{
				var local = await context.Locales.FirstOrDefaultAsync(x => x.Id == id);
				modelo = mapper.Map(local, modelo);
			}

			modelo.ListaCiudades = await GetListaCiudades();

			modelo.ListaCiudades = modelo.ListaCiudades.Prepend(new SelectListItem() { Text = "<Seleccione una Ciudad>", Value = "" });

			return View(modelo);
		}

		private async Task<IEnumerable<SelectListItem>> GetListaCiudades()
		{
			return await context.Ciudades
					.Include(x => x.Region)
					.OrderBy(x => x.Nombre)
					.Select(x => new SelectListItem() { Text = $"{x.Nombre} - {x.Region.Nombre}", Value = x.Id.ToString() }).ToListAsync();
		}

		[HttpPost]
		public async Task<IActionResult> CrearActualizar(LocalCreacionDTO localCreacion)
		{

			var local = new Local();
			if (localCreacion.Id > 0)
			{
				local = await context.Locales.FirstOrDefaultAsync(x=> x.Id == localCreacion.Id);
				if(local == null)
				{
					return RedirectToAction("NoEncontrado", "Home");
				}
				local = mapper.Map(localCreacion, local);

				//if (!ModelState.IsValid)
				//{
				//	localCreacion.ListaCiudades = await GetListaCiudades();
				//	return View(localCreacion);
				//}
			}
			else
			{
				local = mapper.Map<Local>(localCreacion);
				context.Locales.Add(local);
			}
			//
			await context.SaveChangesAsync();

			return RedirectToAction("Index");


		}

		[HttpDelete]
		public async Task<IActionResult> Borrar(int id)
		{
			var local = await context.Locales.FirstOrDefaultAsync(x => x.Id == id);
			if(local== null)
			{
				return Json(new { ok=false, msgError = "Recurso no encontrado." });
			}
			//context.Locales.Remove(local);
			local.EstadoLocal = EstadosEntidad.Anulado;
			await context.SaveChangesAsync();
			return Ok();
		}


	}
}
