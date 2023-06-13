using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using SistemaVisitas;
using SistemaVisitas.Servicios;

var builder = WebApplication.CreateBuilder(args);

var politicaUsuariosAutentacados = new AuthorizationPolicyBuilder()
							.RequireAuthenticatedUser()
							.Build();

// Add services to the container.
builder.Services.AddControllersWithViews(opciones =>
{
	opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutentacados));
});

//builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>( opciones =>
{
	opciones.UseSqlServer( "name=DefaultConnection"  );
} );

builder.Services.AddAutoMapper(typeof(Program));

//identity
builder.Services.AddAuthentication();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
	opciones.SignIn.RequireConfirmedAccount = false;

	opciones.Password.RequireDigit = false;
	opciones.Password.RequireLowercase = false;
	opciones.Password.RequireUppercase = false;
	opciones.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
	opciones =>
	{
		opciones.LoginPath = "/autenticacion/login";
		opciones.AccessDeniedPath = "/autenticacion/login";
	});

//fin identity
builder.Services.AddTransient<IServicioUsuario, ServicioUsuario>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
