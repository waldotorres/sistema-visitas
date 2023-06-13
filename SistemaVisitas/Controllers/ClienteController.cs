using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVisitas.DTO;
using SistemaVisitas.Extensiones;
using SistemaVisitas.Models;

namespace SistemaVisitas.Controllers
{
	public class ClienteController:CustomController
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public ClienteController( ApplicationDbContext context, IMapper mapper)
        {
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<IActionResult> Index( PaginacionDTO paginacionDTO )
		{

			var queryable = context.Clientes.Where(x => !x.Anulado).OrderBy(x=> x.Nombres).AsQueryable();

			var modelo = new ClienteIndexDTO();

			modelo.ListaClientes = await queryable.Paginar(paginacionDTO).ToListAsync();
			modelo.CantidadRegistros = await queryable.CountAsync();
			modelo.paginaAnterior = paginacionDTO.Pagina - 1 > 0 ? paginacionDTO.Pagina - 1 : -1;
			modelo.paginaSiguiente = paginacionDTO.Pagina + 1 <= Math.Ceiling(modelo.CantidadRegistros * 1.00 / paginacionDTO.RegistrosPorPagina) 
									? paginacionDTO.Pagina + 1 : -1;
			return View(modelo);
		}

		public async Task<IActionResult> CrearActualizar( int? id )
		{
			var cliente = new Cliente();

			if( id != null)
			{
				cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
				if(cliente == null)
				{
					return RedirectToAction("NoEncontrado", "Home");
				}
			}

			return View(cliente);
		}
		[HttpPost]
		public async Task<IActionResult> CrearActualizar(Cliente clienteCreacion)
		{
			var cliente = new Cliente();

			if(clienteCreacion.Id> 0)
			{
				cliente = await context.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == clienteCreacion.Id);

				if (cliente == null)
				{
					return RedirectToAction("NoEncontrado", "Home");
				}

				//cliente = mapper.Map<Cliente>(clienteCreacion);
				context.Clientes.Update(clienteCreacion);
			}
			else
			{
				context.Clientes.Add(clienteCreacion);
			}

			await context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		// marca como anulado (no borra el registro) - invocado desde fetchApi
		[HttpDelete]
		public async Task<IActionResult> Borrar(int id)
		{
			var cliente = await context.Clientes.FirstOrDefaultAsync( x => x.Id == id);
			if(cliente == null)
			{
				return Json(new { ok=false, msgError ="Recurso no encontrado" });
			}
			cliente.Anulado = true;
			await context.SaveChangesAsync();
			return Ok();
		}


		[HttpGet]
		public async Task<IActionResult> BuscarCliente(string textSearch)
		{
		 
			var clientes = await (	from q in context.Clientes 
									where (q.Nombres + " " + q.Apellidos).Contains(textSearch) 
									select q ).ToListAsync();

			List<ResultadoModalBusquedaDTO> data = new List<ResultadoModalBusquedaDTO>();
			data = clientes.Select(x => new ResultadoModalBusquedaDTO()
			{
				Descripcion = x.NombreCompleto,
				Id = x.Id.ToString()
			}).ToList();

			if (clientes.Count == 0)
			{
				return Json(new { msgError = "No se encontraron coincidencias" });
			}
			else
			{
				return Json(new { data  });
			}
			
		}

	}
}
