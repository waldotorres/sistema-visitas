using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVisitas.DTO;
using SistemaVisitas.Models;

namespace SistemaVisitas.Controllers
{
	public class AutenticacionController:Controller
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public AutenticacionController(ApplicationDbContext context,
									UserManager<IdentityUser> userManager,
									SignInManager<IdentityUser> signInManager)
        {
			this.context = context;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
        //metodos para registro y login
        [AllowAnonymous]
		public IActionResult Registro()
		{
			//var modelo = new RegistroViewModel();
			return View();
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Registro(RegistroViewModel registro)
		{
			if (!ModelState.IsValid)
			{
				return View(registro);
			}

			var usuario = new Usuario()
			{
				Email = registro.Email,
				UserName = registro.Email
			};

			var resultado = await userManager.CreateAsync(usuario, password: registro.Password);
			if (!resultado.Succeeded)
			{
				foreach (var error in resultado.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}

				return View(registro);
			}

			await signInManager.SignInAsync(usuario, isPersistent: true);
			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public IActionResult Login()
		{
			//var modelo = new LoginViewModel();
			return View();
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel login)
		{
			if (!ModelState.IsValid)
			{
				return View(login);
			}

			var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.CodUsuario == login.CodUsuario.ToLower());
			if(usuario== null)
			{
				ModelState.AddModelError(string.Empty, "El usuario o contraseña es incorrecto");
				return View(login);
			}

			var resultado = await signInManager.PasswordSignInAsync(usuario.Email, login.Password, login.Recuerdame, lockoutOnFailure: false);
			if (!resultado.Succeeded)
			{
				ModelState.AddModelError(string.Empty, "El usuario o contraseña es incorrecto");
				return View(login);
			}


			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}
