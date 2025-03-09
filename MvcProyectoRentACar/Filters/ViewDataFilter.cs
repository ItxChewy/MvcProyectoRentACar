using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Repositories;

namespace MvcProyectoRentACar.Filters
{
    public class ViewDataFilter:IAsyncActionFilter
    {
        private RepositoryFilter repo;
        public ViewDataFilter(RepositoryFilter repo)
        {
            this.repo = repo;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                Usuario usuario = await repo.GetUsuarioAsync();
                Vendedor vendedor = await repo.GetVendedorAsync(usuario.IdUsuario);

                controller.ViewData["nombreempresa"] = vendedor?.NombreEmpresa ?? "Empresa por defecto";
                controller.ViewData["direccion"] = vendedor?.Direccion ?? "Dirección no disponible";
                controller.ViewData["telefono"] = vendedor?.Telefono ?? "Teléfono no disponible";
                controller.ViewData["email"] = usuario?.Email ?? "Email no disponible";
                controller.ViewData["nombre"] = usuario?.Nombre ?? "Usuario desconocido";
            }

            await next(); // Continúa con la ejecución de la acción
        }
    }
}
