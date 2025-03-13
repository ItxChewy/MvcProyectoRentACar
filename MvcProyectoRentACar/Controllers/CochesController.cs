using Microsoft.AspNetCore.Mvc;
using MvcProyectoRentACar.Filters;
using MvcProyectoRentACar.Helpers;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Repositories;

namespace MvcProyectoRentACar.Controllers
{
    public class CochesController : Controller
    {
        private IRepositoryRentACar repo;
        public CochesController(IRepositoryRentACar repo)
        {
            this.repo = repo;
        }

        [AuthorizeUsers(Policy = "Admin")]
        public async Task<IActionResult> InsertCoche()
        {
            ViewData["marchas"] = await this.repo.GetMarchasAsync();
            ViewData["gamas"] = await this.repo.GetGamasAsync();
            ViewData["combustibles"] = await this.repo.GetCombustiblesAsync();

            return View();
        }
        [AuthorizeUsers(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InsertCoche(string marca, string modelo, string matricula, IFormFile fichero, int asientos, int idmarchas,
            int idgama, int kilometraje, int puertas, int idcombustible,
            string preciokilometros, string precioilimitado)
        {
            try
            {
                ViewData["marchas"] = await this.repo.GetMarchasAsync();
                ViewData["gamas"] = await this.repo.GetGamasAsync();
                ViewData["combustibles"] = await this.repo.GetCombustiblesAsync();

                int idvendedor = int.Parse(HttpContext.User.FindFirst("id").Value);

                decimal kilometrossanize = HelperInputSanitizer.SanitizeDecimalInput(preciokilometros);
                decimal ilimitadosanized = HelperInputSanitizer.SanitizeDecimalInput(precioilimitado);

                bool result = await this.repo.InsertCocheAsync(marca, modelo, matricula, fichero, asientos, idmarchas, idgama, kilometraje
                    , puertas, idcombustible, idvendedor, kilometrossanize, ilimitadosanized);

                if (result)
                {
                    TempData["SuccessMessage"] = "Coche insertado correctamente.";
                    return RedirectToAction("Coches", "Vendedores");
                }
                else
                {
                    TempData["ErrorMessage"] = "Ha ocurrido un error al insertar el coche.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        [AuthorizeUsers(Policy = "Admin")]
        public async Task<IActionResult> UpdateCoche(int idcoche)
        {
            ViewData["gamas"] = await this.repo.GetGamasAsync();
            TempData["WarningMessage"] = "¿Estas seguro de modificar el coche?";
            TempData["WarningMessageBtn"] = "Modificar";
            Coche coche = await this.repo.DetailsCocheAsync(idcoche);
            return View(coche);
        }

        [AuthorizeUsers(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateCoche(int idcoche, string preciokilometros, string precioilimitado, int idgama)
        {
            decimal parsedPrecioKilometros = HelperInputSanitizer.SanitizeDecimalInput(preciokilometros);
            decimal parsedPrecioIlimitado = HelperInputSanitizer.SanitizeDecimalInput(precioilimitado);

            await this.repo.UpdateCocheAsync(idcoche, idgama, parsedPrecioKilometros, parsedPrecioIlimitado);
            TempData["SuccessMessage"] = "Coche actualizado correctamente.";
            ViewData["gamas"] = await this.repo.GetGamasAsync();
            Coche coche = await this.repo.DetailsCocheAsync(idcoche);
            return RedirectToAction("Coches", "Vendedores");
            //return View(coche);
        }

        [AuthorizeUsers(Policy = "Admin")]
        public async Task<IActionResult> DeleteCoche(int idcoche)
        {
            TempData["SuccessMessage"] = "Coche eliminado correctamente.";
            await this.repo.DeleteCocheAsync(idcoche);
            return RedirectToAction("Coches", "Vendedores");
        }


    }
}
