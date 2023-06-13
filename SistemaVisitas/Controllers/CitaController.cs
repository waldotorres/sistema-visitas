using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVisitas.DTO;
using SistemaVisitas.Enums;
using SistemaVisitas.Migrations;
using SistemaVisitas.Models;
using System.Text.Json;

namespace SistemaVisitas.Controllers
{
	public class CitaController : CustomController
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public CitaController(ApplicationDbContext context, IMapper mapper)
        {
			this.context = context;
			this.mapper = mapper;
		}

        public async Task<IActionResult> Index(int? idLocal )
		{
			var modelo = new CitaIndexDTO();
			var queryable = context.Citas
							.Include(x=> x.Cliente)
							.Include(x=> x.Local)
							.Include(x=> x.TipoVisita)
							.Where(x=> x.EstadoCita != EstadosCita.Cancelado )
							.OrderByDescending(x=> x.FechaHoraInicioCita)
							.AsQueryable();
			if(idLocal != null)
			{
				queryable = queryable.Where(x => x.LocalId == idLocal );
			}

			var resultado = await queryable.ToListAsync();
			
			modelo.NombreLocal = idLocal == null? "": resultado[0].Local.Nombre;

			modelo.ListaCitas = mapper.Map<IEnumerable<Cita>>(resultado);
			
			return View(modelo);
		}

		public async Task<IActionResult> CrearActualizar(int? id)
		{
			CitaCreacionDTO modelo = new CitaCreacionDTO();
			if( id!= null)
			{
				var cita = await context.Citas.Include(x=> x.Cliente).FirstOrDefaultAsync(x => x.Id == id);

				if(cita== null)
				{
					return RedirectToAction("NoEncontrado", "Home");
				}
				modelo = mapper.Map<CitaCreacionDTO>(cita);
				modelo.NombreClienteVista = cita.Cliente.NombreCompleto;
			}

			modelo.ListaLocales = await  this.GetListaLocales();
			modelo.ListaTipoVisita = await this.GetListaTipoVisita();

			return View(modelo);
		}

		[HttpPost]
		public async Task<IActionResult> CrearActualizar( CitaCreacionDTO citaCreacion )
		{

			if( citaCreacion.FechaHoraInicioCita > citaCreacion.FechaHoraFinCita)
			{
				ModelState.AddModelError("FechaHoraInicioCita", "La fechaHora de Inicio debe ser menor a la fechaHora final");
			}
			if (citaCreacion.FechaHoraInicioCita <  DateTime.Now.AddMinutes(-15) )
			{
				ModelState.AddModelError("FechaHoraInicioCita", "La fechaHora de Inicio debe ser mayor que la hora actual");
			}
			if (  (citaCreacion.FechaHoraFinCita - citaCreacion.FechaHoraInicioCita).TotalHours >= 24 )
			{
				ModelState.AddModelError(string.Empty, "La cita tiene una duracion muy larga. Verifique");
			}

			if (!ModelState.IsValid)
			{
				citaCreacion.ListaLocales = await this.GetListaLocales();
				citaCreacion.ListaTipoVisita = await this.GetListaTipoVisita();
				return View(citaCreacion);
			}

			var cita = mapper.Map<Cita>(citaCreacion);

			// validar que exista la cita
			if(citaCreacion.Id > 0)
			{
				var citaDb = await context.Citas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == citaCreacion.Id);
				if(citaDb == null)
				{
					return RedirectToAction("NoEncontrado", "Home");
				}

				context.Citas.Update(cita);
			}
			else
			{
				context.Citas.Add(cita);
			}

			await context.SaveChangesAsync();
			return RedirectToAction("Index");	
		}
		
		[HttpDelete]
		public async Task<IActionResult> Borrar(int id)
		{
			var cita = await context.Citas.FirstOrDefaultAsync(x => x.Id == id);
			if(cita == null)
			{
				return Json(new { ok = false,  msgError ="Recurso no encontrado" });
			}

			cita.EstadoCita = EstadosCita.Cancelado;
			await context.SaveChangesAsync();
			return Ok();

		}
		// apis
		public async Task<IActionResult> BuscarCita(string textSearch)
		{
			var citas = await context.Citas
							.Include(x => x.Cliente)
							.Include(x => x.TipoVisita)
							.Include(x => x.Local)
							.Where(x => x.EstadoCita == EstadosCita.Pendiente
									&& (x.Cliente.Nombres + " " + x.Cliente.Apellidos).Contains(textSearch))
							.ToListAsync();

			if (citas.Count == 0)
			{
				return Json(new { msgError = "No se encontraron resultados" });
			}

			var retorno = citas.Select(x => new ResultadoModalBusquedaDTO()
			{
				Id = x.Id.ToString(),
				Descripcion = x.Cliente.Nombres + " " + x.Cliente.Apellidos,
				InformacionAdicionalJson = JsonSerializer.Serialize(
													new
													{
														idLocal = x.LocalId,
														idTipoVisita = x.TipoVisitaId,
														cliente = new { id = x.ClienteId, NombreCompleto = x.Cliente.Nombres + " " + x.Cliente.Apellidos }
													})

			});
			return Json(new { data = retorno });

		}
		//
		private async Task<IEnumerable<SelectListItem>> GetListaLocales()
		{
			var locales = await context.Locales
						.Include( x=> x.Ciudad)
						.Where(x => x.EstadoLocal == EstadosEntidad.Activo)
						.Select(x => new SelectListItem() { Text = $"{x.Nombre}-{x.Ciudad.Nombre}" ,  Value = x.Id.ToString() })
						.ToListAsync();
			locales = locales.Prepend(new SelectListItem() { Text = "<Seleccione un Local>", Value = "" }).ToList();
			return locales;


		}
		private async Task<IEnumerable<SelectListItem>> GetListaTipoVisita()
		{
			var tipoVisita = await context.TiposVisita
						.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() })
						.ToListAsync();

			tipoVisita = tipoVisita.Prepend(new SelectListItem() { Text = "<Seleccione un Tipo>", Value = "" }).ToList();
			return tipoVisita;


		}

	}
}
