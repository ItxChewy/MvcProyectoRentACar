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
            VistaCoche coche = await this.repo.GetVistaCocheAsync(idcoche);
            return View(coche);
        }

        [HttpPost]
        public async Task<IActionResult>DetailsCoche(int idcoche
            , DateTime fechainicio, DateTime fechafin,double valor)
        {
            int idusuario = (int)HttpContext.Session.GetInt32("usuarioactual");
            bool disponible = await this.repo.ComprobarDisponibilidadCocheAsync(idcoche, fechainicio, fechafin);
            if (disponible)
            {
                await this.repo.CompraCocheAsync(idusuario,idcoche,fechainicio,fechafin,valor);
                return RedirectToAction("Coches");
            }
            else
            {
                VistaCoche coche = await this.repo.GetVistaCocheAsync(idcoche);
                Console.WriteLine("fecha ocupada");
                return View(coche);
            }
        }

        public async Task<IActionResult> Compras()
        {
            int idusuario = (int)HttpContext.Session.GetInt32("usuarioactual");
            List<Compra> compras = await this.repo.GetComprasUsuarioAsync(idusuario);
            return View(compras);
        }
    }
}
