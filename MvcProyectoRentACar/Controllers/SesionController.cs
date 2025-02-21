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
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["MENSAJE"] = "Nombre y/o contraseña ";
                return View();
            }
        }
    }
}
