using Microsoft.AspNetCore.Mvc;

namespace SistemaVisitas.Controllers
{
	public class CustomController:Controller
	{
		protected string ObtenerUsuario()
		{
			return "idUsuario";
		}
	}
}
