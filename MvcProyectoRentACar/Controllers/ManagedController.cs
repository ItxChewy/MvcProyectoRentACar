﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Repositories;

namespace MvcProyectoRentACar.Controllers
{
    public class ManagedController : Controller
    {
        private IRepositoryRentACar repo;
        public ManagedController(IRepositoryRentACar repo)
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
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role
                        );
                Claim claimUserName =
                new Claim(ClaimTypes.Name, usuario.Nombre);
                identity.AddClaim(claimUserName);
                Claim claimRole =
                    new Claim(ClaimTypes.Role, usuario.IdRolUsuario.ToString());
                identity.AddClaim(claimRole);
                Claim claimId =
                    new Claim("id", usuario.IdUsuario.ToString());
                identity.AddClaim(claimId);
                ClaimsPrincipal userPrincipal =
                new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme,userPrincipal);
                string controller = TempData["controller"]?.ToString();
                string action = TempData["action"]?.ToString();

                if (string.IsNullOrEmpty(controller)  || usuario.IdRolUsuario == 1)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (TempData["id"] != null)
                {
                    string id =
                        TempData["id"].ToString();
                    return RedirectToAction(action, controller
                        , new { id = id });
                }
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["MENSAJE"] = "Nombre y/o contraseña erroneas";
                return View();
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            TempData.Clear();
           

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Register()
        {
            ViewData["roles"] = await this.repo.GetRolesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register
            (string nombre, string email, string password, int idrol, string telefono
            , string? apellidos, string? dni, string? carnet
             , DateTime? fechanacimiento, string? direccion, string? passpecial
            , string? nombreempresa)
        {
            bool isRegistered = await this.repo.RegisterUsuarioAsync(nombre, email, password, idrol, telefono, apellidos, dni, carnet, fechanacimiento, direccion, passpecial, nombreempresa);
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
        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
