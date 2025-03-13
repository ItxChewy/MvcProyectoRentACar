using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Filters;
using MvcProyectoRentACar.Helpers;
using MvcProyectoRentACar.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<HelperPathProvider>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(
   options =>
    {
        options.DefaultSignInScheme =
        CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme =
        CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme =
        CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ViewDataFilter>(); // Registrar el filtro globalmente
    options.EnableEndpointRouting = false;
}).AddSessionStateTempDataProvider();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        policy => policy.RequireRole("1"));
});

builder.Services.AddTransient<IRepositoryRentACar,RepositoryRentACar>();


builder.Services.AddTransient<ViewDataFilter>();


string connectionString = builder.Configuration.GetConnectionString("SqlRentACar");
builder.Services.AddDbContext<RentACarContext>
    (options => options.UseSqlServer(connectionString));
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

//app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapStaticAssets();

app.UseSession();
app.UseMvc(routes =>
{
    routes.MapRoute(
      name: "Default",
      template: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
