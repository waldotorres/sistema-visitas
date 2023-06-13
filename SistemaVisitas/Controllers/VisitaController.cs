using AutoMapper;
using Azure.Core.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVisitas.DTO;
using SistemaVisitas.Enums;
using SistemaVisitas.Models;
using SistemaVisitas.Servicios;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SistemaVisitas.Controllers
{
	public class VisitaController:CustomController
	{
		private readonly ApplicationDbContext context;

		private readonly IMapper mapper;
		private readonly IServicioUsuario servicioUsuario;

		public VisitaController( ApplicationDbContext context, IMapper mapper, 
								 IServicioUsuario servicioUsuario )
        {
			this.context = context;
			this.mapper = mapper;
			this.servicioUsuario = servicioUsuario;
		}

		public async Task<IActionResult> Index()
		{
			var modelo = await context.Visitas
							.Include(x=> x.Local)
							.Include(x=> x.TipoVisita )
							.Include( x=> x.UsuarioAtencion )
							.Include(x=> x.Cliente)
							.Where(x => x.EstadoVisita != EstadosCita.Anulado && x.FechHoraFin > DateTime.Now.AddDays(-10))
							.OrderByDescending( x=> x.FechHoraInicio )
							.ToListAsync();

			return View(modelo);	
		} 

		public async Task<IActionResult> CrearActualizar( int? id)
		{
			var visitaDto = new VisitaCreacionDTO();
			

			if ( id == null)
			{
				visitaDto.ListaLocales = await this.ObtenerLocales();
				visitaDto.ListaTipoVisita = await this.ObtenerTipoVisita();
				return View(visitaDto);
			}
			//actualizacion
			var visitaDB = await context.Visitas
								.Include(x=> x.UsuarioAtencion)
								.Include(x=> x.Cliente)
								.FirstOrDefaultAsync(x => x.Id == id);
			if (visitaDB == null)
			{
				return RedirectToAction("NoEncontrado", "Home");
			}

			visitaDto = mapper.Map<VisitaCreacionDTO>(visitaDB );
			visitaDto.ListaLocales = await this.ObtenerLocales();
			visitaDto.ListaTipoVisita = await this.ObtenerTipoVisita();
			visitaDto.IdCitaOriginal = visitaDto.IdCita;
			return View(visitaDto);

		}

		[HttpPost]
		public async Task<IActionResult> CrearActualizar( VisitaCreacionDTO visitaCreacion)
		{
			var idUsuario =  this.servicioUsuario.ObtenerIdUsuario();
			var visita = mapper.Map<Visita>(visitaCreacion);

			if ( !ModelState.IsValid)
			{
				visitaCreacion.ListaTipoVisita = await this.ObtenerTipoVisita();
				visitaCreacion.ListaLocales= await this.ObtenerLocales();
				return View(visitaCreacion);
			}
			// validamos las fechas
			string mensajeError = visitaCreacion.FechHoraInicio >= visitaCreacion.FechHoraFin ? "La fecha de Inicio debe ser menor que la fecha final."
									: visitaCreacion.FechHoraInicio < DateTime.Now.AddDays(-30) ? "La fecha de Inicio no debe tener una antiguedad mayor a 30 dias."
									: visitaCreacion.FechHoraInicio >= DateTime.Now ? "La hora inicio no debe ser mayor a la hora actual"
									: visitaCreacion.FechHoraFin >= DateTime.Now.AddMinutes(5) ? "La hora final no puede ser mayor a la hora actual."
									: "";
			
			if (mensajeError.Length>0 )
			{
				ModelState.AddModelError( string.Empty,  mensajeError  );
				visitaCreacion.ListaTipoVisita = await this.ObtenerTipoVisita();
				visitaCreacion.ListaLocales = await this.ObtenerLocales();

				return View(visitaCreacion);
			}
			 

			if ( visitaCreacion.Id> 0)
			{
				visita = await context.Visitas.FirstOrDefaultAsync(x => x.Id == visitaCreacion.Id);
				if(visita == null)
				{
					return RedirectToAction("Index");
				}
				visita = mapper.Map(visitaCreacion, visita);
				

				visita.IdUsuarioModificacion = idUsuario;
				visita.fechaModificacion = DateTime.Now;
				//context.Visitas.Update(visita);
			}
			else
			{
				visita.IdUsuario = idUsuario;
				context.Visitas.Add(visita);
			}
			// si hay cita la buscamos
			if( visitaCreacion.IdCita > 0)
			{
				var cita = await context.Citas
							.FirstOrDefaultAsync(x => x.Id == visitaCreacion.IdCita
							&& x.ClienteId == visitaCreacion.IdCliente
							//&& x.EstadoCita == EstadosCita.Pendiente
							&& x.LocalId == visitaCreacion.IdLocal );

				string msgError = cita == null ? "La cita no conrresponde al cliente ó \n el local no corresponde con la cita"
											: visitaCreacion.IdCitaOriginal != null
												&& visitaCreacion.IdCitaOriginal != cita.Id
												&& cita.EstadoCita != EstadosCita.Pendiente ? "La cita no esta disponible" 
											: "";

				if (msgError.Length> 0 )
				{
					ModelState.AddModelError(string.Empty, msgError );
					visitaCreacion.ListaTipoVisita = await this.ObtenerTipoVisita();
					visitaCreacion.ListaLocales = await this.ObtenerLocales();

					return View(visitaCreacion);
				}
				//activa la cita anterior
				if(visitaCreacion.IdCitaOriginal != null)
				{
					var citaAnterior = await context.Citas.FirstOrDefaultAsync(x => x.Id == visitaCreacion.IdCitaOriginal);
					if(citaAnterior != null)
					{
						citaAnterior.EstadoCita = EstadosCita.Pendiente;
					}
				}
				

				cita.EstadoCita = EstadosCita.Atendido;

			}

			await context.SaveChangesAsync();

			return RedirectToAction("Index");

		}

		[HttpDelete]
		public async Task<IActionResult> Borrar(int id)
		{
			var visita = await context.Visitas.FirstOrDefaultAsync(x => x.Id == id);
			if(visita== null)
			{
				return Json(new { msgError = "Recurso no encontrado" });
			}

			visita.EstadoVisita = EstadosCita.Anulado;
			visita.IdUsuarioAnulacion = servicioUsuario.ObtenerIdUsuario();
			visita.fechaAnulacion = DateTime.Now;

			await context.SaveChangesAsync();
			return Ok();
		}

		//metodos de busqueda
		

		//metodos privados
		private async Task<List<SelectListItem>> ObtenerLocales()
		{
			List<SelectListItem> listaLocales = await context.Locales
												.Where( x=> x.EstadoLocal == EstadosEntidad.Activo )
												.Include( x=> x.Ciudad)
												.Select( x=> new SelectListItem() {  Text = $"{x.Nombre} / {x.Ciudad.Nombre}"  , Value = x.Id.ToString() } )
												.ToListAsync();

			listaLocales = listaLocales.Prepend(new SelectListItem() { Text = "<Seleccione un Local>", Value = "" }).ToList();
			return listaLocales;

		}
		private async Task<List<SelectListItem>> ObtenerTipoVisita()
		{
			List<SelectListItem> listaTiposVisita = await context.TiposVisita
												.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() })
												.ToListAsync();

			listaTiposVisita = listaTiposVisita.Prepend(new SelectListItem() { Text = "<Seleccione un tipo de Visita>", Value = string.Empty }).ToList();
			return listaTiposVisita;
		}

	}
}
