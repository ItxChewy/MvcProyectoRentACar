using Microsoft.AspNetCore.Mvc;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Repositories;

namespace MvcProyectoRentACar.Controllers
{
    public class SesionController : Controller
    {
        private RepositorySesion repo;
        public SesionController(RepositorySesion repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario usuario = await this.repo.LoginAsync(email, password);

            if (usuario != null)
            {
                HttpContext.Session.SetInt32("rol", usuario.IdRolUsuario);
                HttpContext.Session.SetInt32("usuarioactual", usuario.IdUsuario);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["MENSAJE"] = "Nombre y/o contraseña ";
                return View();
            }
        }

        public async Task<IActionResult> Register()
        {
            ViewData["roles"] = await this.repo.GetRolesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register
            (string nombre, string email, string password, int idrol, string telefono
            , string? apellidos = null, string? dni = null, string? carnet = null
             , DateTime? fechanacimiento = null, string? direccion = null, string? passpecial = null
            , string? nombreempresa = null)
        {
            bool isRegistered = await this.repo.RegisterUsuarioAsync(nombre,email, password, idrol, telefono, apellidos, dni, carnet, fechanacimiento, direccion, passpecial,nombreempresa);
            if (isRegistered)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewData["MENSAJE"] = "Error al registrar el usuario. Por favor, intente nuevamente.";
                ViewData["roles"] = await this.repo.GetRolesAsync();
                return View();
            }
        }
    }
}
