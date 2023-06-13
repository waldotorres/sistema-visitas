using System.Security.Claims;

namespace SistemaVisitas.Servicios
{


	public interface IServicioUsuario
	{
		string ObtenerIdUsuario();
	}


	public class ServicioUsuario : IServicioUsuario
	{
		private readonly ApplicationDbContext context;
		private readonly HttpContext httpContext;

		public ServicioUsuario( ApplicationDbContext context, IHttpContextAccessor contextAccessor )
        {
			this.context = context;
			this.httpContext = contextAccessor.HttpContext;
		}

		public string ObtenerIdUsuario()
		{
			if (httpContext.User.Identity.IsAuthenticated)
			{
				var usuarioid = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
				if(usuarioid == null)
				{
					throw new Exception("Error de authenticacion.");
				}
				return usuarioid.Value.ToString();	
			}

			return "";

		}
	}


}
