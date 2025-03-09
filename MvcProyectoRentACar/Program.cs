using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Filters;
using MvcProyectoRentACar.Helpers;
using MvcProyectoRentACar.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ViewDataFilter>(); // Registrar el filtro globalmente
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<HelperPathProvider>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddTransient<RepositorySesion>();
builder.Services.AddTransient<RepositoryComprador>();
builder.Services.AddTransient<RepositoryVendedor>();
builder.Services.AddTransient<RepositoryFilter>();

builder.Services.AddTransient<ViewDataFilter>();


string connectionString = builder.Configuration.GetConnectionString("SqlRentACarCasa");
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

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
