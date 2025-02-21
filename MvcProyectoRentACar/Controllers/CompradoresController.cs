using Microsoft.AspNetCore.Mvc;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Repositories;

namespace MvcProyectoRentACar.Controllers
{
    public class CompradoresController : Controller
    {
        private RepositoryComprador repo;

        public CompradoresController(RepositoryComprador repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Coches()
        {
            List<VistaCoche> coches = await this.repo.FindAllCochesAsync();

            return View(coches);

        }

        [HttpPost]
        public async Task<IActionResult> Coches(int valor)
        {

            List<VistaCoche> coches = await this.repo.FilterByPrecio(valor);
            return View(coches);

        }

        public async Task<IActionResult> DetailsCoche(int idcoche)
        {
            VistaCoche coche = await this.repo.GetCocheAsync(idcoche);
            return View(coche);
        }
    }
}
