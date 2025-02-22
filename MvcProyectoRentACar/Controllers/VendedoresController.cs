using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> InsertCoche()
        {
            ViewData["marchas"] = await this.repo.GetMarchasAsync();
            ViewData["gamas"] = await this.repo.GetGamasAsync();
            ViewData["combustibles"] = await this.repo.GetCombustiblesAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertCoche(string marca, string modelo, string matricula, string imagen, IFormFile fichero, int asientos, int idmarchas,
            int idgama, int kilometraje, int puertas, int idcombustible, int idvendedor,
            decimal preciokilometros, decimal precioilimitado)
        {
            return View();
        }

        public async Task<IActionResult>UpdateCoche(int idcoche)
        {
            VistaCoche coche = await this.repo.DetailsCocheAsync(idcoche);
            return View(coche);
        }
    }
}
