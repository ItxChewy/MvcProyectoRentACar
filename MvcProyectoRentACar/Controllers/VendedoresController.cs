﻿using Microsoft.AspNetCore.Mvc;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Repositories;

namespace MvcProyectoRentACar.Controllers
{
    public class VendedoresController : Controller
    {
        RepositoryVendedor repo;
        public VendedoresController(RepositoryVendedor repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Coches()
        {
            List<VistaCoche> coches = await this.repo.GetCochesAsync();
            return View(coches);
        }

        public async Task<IActionResult> ManageCoche(int idcoche)
        {
            ViewData["coche"] = await this.repo.DetailsCocheAsync(idcoche);
            ViewData["estado"] = await this.repo.GetEstadoReservaAsync();
            List<Reserva> reservas = await this.repo.GetReservasNoFinalizadasPorCocheAsync(idcoche);
            return View(reservas);
        }

        [HttpPost]
        public async Task<IActionResult> ManageCoche(int idcoche,int? setactive , int? finalizado)
        {
            ViewData["coche"] = await this.repo.DetailsCocheAsync(idcoche);
            ViewData["estado"] = await this.repo.GetEstadoReservaAsync();
            
            if (setactive != null)
            {
                await this.repo.CambiarAEstadoActivoReservaAsync((int)setactive);
                List<Reserva> reservas = await this.repo.GetReservasNoFinalizadasPorCocheAsync(idcoche);
                return View(reservas);
            }
            else
            {
                await this.repo.CambiarAEstadoFinalizadoReservaAsync((int)finalizado);
                List<Reserva> reservas = await this.repo.GetReservasNoFinalizadasPorCocheAsync(idcoche);
                return View(reservas);
            }
        }

        public async Task<IActionResult> InsertCoche()
        {
            ViewData["marchas"] = await this.repo.GetMarchasAsync();
            ViewData["gamas"] = await this.repo.GetGamasAsync();
            ViewData["combustibles"] = await this.repo.GetCombustiblesAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertCoche(string marca, string modelo, string matricula,IFormFile fichero, int asientos, int idmarchas,
            int idgama, int kilometraje, int puertas, int idcombustible,
            decimal preciokilometros, decimal precioilimitado)
        {
            int idvendedor = (int)HttpContext.Session.GetInt32("usuarioactual");

            await this.repo.InsertCocheAsync(marca, modelo, matricula, fichero, asientos, idmarchas, idgama, kilometraje
                , puertas, idcombustible, idvendedor, preciokilometros, precioilimitado);

            return RedirectToAction("Coches");
        }

        public async Task<IActionResult>UpdateCoche(int idcoche)
        {
            ViewData["gamas"] = await this.repo.GetGamasAsync();
            Coche coche = await this.repo.DetailsCocheAsync(idcoche);
            return View(coche);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCoche(int idcoche,string preciokilometros
            , string precioilimitado, int idgama)
        {
            if (decimal.TryParse(preciokilometros, out decimal parsedPrecioKilometros) &&
        decimal.TryParse(precioilimitado, out decimal parsedPrecioIlimitado))
            {
                await this.repo.UpdateCocheAsync(idcoche, idgama, parsedPrecioKilometros, parsedPrecioIlimitado);
                return RedirectToAction("Coches");
            }
            else
            {
                // Handle parsing error, e.g., return an error message to the view
                ModelState.AddModelError("", "Error parsing decimal values.");
                ViewData["gamas"] = await this.repo.GetGamasAsync();
                Coche coche = await this.repo.DetailsCocheAsync(idcoche);
                return View(coche);
            }
        }

        public async Task<IActionResult> DeleteCoche(int idcoche)
        {
            await this.repo.DeleteCocheAsync(idcoche);
            return RedirectToAction("Coches");
        }

        public async Task<IActionResult> ComprobarKilometraje()
        {
            List<Reserva> reservas = await this.repo.GetReservasKilometrajeAsync();
            return View(reservas);
        }
    }
}
