using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using SistemaVisitas.DTO;
using SistemaVisitas.Enums;
using SistemaVisitas.Migrations;
using SistemaVisitas.Models;

namespace SistemaVisitas.Controllers
{
	public class UsuarioController : CustomController
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly IMapper mapper;

		public UsuarioController(ApplicationDbContext context,
									UserManager<IdentityUser> userManager,
									SignInManager<IdentityUser> signInManager,
									IMapper mapper)
		{
			this.context = context;
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.mapper = mapper;
		}

		//
		public async Task<IActionResult> Index()
		{
			var usuarios = await context.Usuarios.Where(x => x.EstadoUsuario == EstadosEntidad.Activo).ToListAsync();
			return View(usuarios);
		}

		public async Task<IActionResult> CrearActualizar(string? id)
		{
			var modelo = new UsuarioCreacionDTO();
			if (id == null)
			{
				return View(modelo);
			}

			var usuarioDB = await context.Usuarios

							.FirstOrDefaultAsync(x => x.Id == id);
			if (usuarioDB == null)
			{
				return RedirectToAction("NoEncontrado", "Home");
			}

			modelo = mapper.Map(usuarioDB, modelo);
			modelo.EsUsuarioAdmin = await (from ur in context.UserRoles
										   join r in context.Roles on ur.RoleId equals r.Id
										   where ur.UserId == modelo.Id
										   select r.Name == "admin" ? true : false).FirstOrDefaultAsync();

			return View(modelo);
		}
		[HttpPost]
		public async Task<IActionResult> CrearActualizar( UsuarioCreacionDTO usuarioCreacion )
		{


			if (!ModelState.IsValid)
			{
				return View(usuarioCreacion);
			}
			var usuario = new Usuario();

			if (usuarioCreacion.UserName == null)
			{
				usuario.Email = usuarioCreacion.Email;
				usuario.UserName = usuarioCreacion.Email;
				usuario.Nombres = usuarioCreacion.Nombres;
				usuario.Apellidos = usuarioCreacion.Apellidos;
				usuario.CodUsuario = usuarioCreacion.CodUsuario;
				usuario.EsUsuarioAtencion = usuarioCreacion.EsUsuarioAtencion;

				var resultado = await userManager.CreateAsync(usuario, password: usuarioCreacion.Password);
				if (!resultado.Succeeded)
				{
					foreach (var error in resultado.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}

					return View(usuarioCreacion);
				}
				// agrega el rol

			}
			else
			{
				var usuarioDB = await context.Users.FirstOrDefaultAsync(x => x.Id == usuarioCreacion.Id);
				if (usuarioDB == null)
				{
					return RedirectToAction("NoEncontrado", "Home");
				}

				usuario = mapper.Map(usuarioDB, usuario);
				usuario.Email = usuarioCreacion.Email;
				usuario.UserName = usuarioCreacion.Email;
				usuario.Nombres = usuarioCreacion.Nombres;
				usuario.Apellidos = usuarioCreacion.Apellidos;
				usuario.CodUsuario = usuarioCreacion.CodUsuario;
				usuario.EsUsuarioAtencion = usuarioCreacion.EsUsuarioAtencion;
				//cambia password
				var token = await userManager.GeneratePasswordResetTokenAsync(usuario);
				var resultado = await userManager.ResetPasswordAsync(usuario, token, usuarioCreacion.Password );
				if (!resultado.Succeeded)
				{
					foreach (var error in resultado.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}

					return View(usuarioCreacion);
				}
			}

			var esAdmin = await (from ur in context.UserRoles
								 join r in context.Roles on ur.RoleId equals r.Id
								 where ur.UserId == usuario.Id
								 select r.Name == "admin" ? true : false).FirstOrDefaultAsync();

			if (usuarioCreacion.EsUsuarioAdmin && !esAdmin)
			{
				await userManager.AddToRoleAsync(usuario, "admin");
			}


			await context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		//apis
		[HttpGet]
		public async Task<IActionResult> BuscarUsuario(string textSearch)
		{
			var usuarios = await context.Usuarios
							.Where(x => x.EstadoUsuario == EstadosEntidad.Activo
									&& (x.Nombres + " " + x.Apellidos).Contains(textSearch.Trim()))
							.Select(x => new ResultadoModalBusquedaDTO()
							{
								Id = x.Id.ToString(),
								Descripcion = x.Nombres + " " + x.Apellidos
							})
							.ToListAsync();

			if (usuarios.Count == 0)
			{
				return Json(new { msgError = "No se encontraron coincidencias" });
			}

			return Json(new { data = usuarios });

		}
	}
}
