using Microsoft.AspNetCore.Mvc;
using MvcProyectoRentACar.Filters;
using MvcProyectoRentACar.Helpers;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Models.MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Repositories;

namespace MvcProyectoRentACar.Controllers
{
    public class VendedoresController : Controller
    {
        IRepositoryRentACar repo;
        public VendedoresController(IRepositoryRentACar repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeUsers(Policy = "Admin")]
        public async Task<IActionResult> Coches()
        {
            TempData["WarningMessage"] = "¿Estas seguro de eliminar el coche?";
            TempData["WarningMessageBtn"] = "Eliminar";
            List<VistaCoche> coches = await this.repo.GetCochesAsync();
            return View(coches);
        }
        [AuthorizeUsers(Policy = "Admin")]
        public async Task<IActionResult> ManageCoche(int idcoche)
        {
            ViewData["coche"] = await this.repo.DetailsCocheAsync(idcoche);
            ViewData["estado"] = await this.repo.GetEstadoReservaAsync();
            List<Reserva> reservas = await this.repo.GetReservasNoFinalizadasPorCocheAsync(idcoche);
            return View(reservas);
        }
        [AuthorizeUsers(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ManageCoche(int idcoche, int? setactive, int? finalizado)
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
        [AuthorizeUsers(Policy = "Admin")]
        public async Task<IActionResult> ComprobarKilometraje(int? filtro)
        {
            List<Reserva> reservas = await this.repo.GetReservasFilterAsync();

            if (filtro.HasValue && filtro.Value != 0)
            {
                if (filtro.Value == 1)
                {
                    reservas = reservas.Where(r => r.Kilometraje == true).ToList();
                }
                else if (filtro.Value == 2)
                {
                    reservas = reservas.Where(r => r.Kilometraje == false).ToList();
                }
            }

            ViewData["Filtro"] = filtro ?? 0;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ComprobarKilometrajeTable", reservas);
            }

            return View(reservas);
        }
        [AuthorizeUsers(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ComprobarKilometraje(int idreserva, int newkilometraje)
        {
            await this.repo.GetCargoExcedido(idreserva, newkilometraje);

            TempData["SuccessMessage"] = "Kilometraje actualizado correctamente.";

            List<Reserva> reservas = await this.repo.GetReservasFilterAsync();

            int filtro = TempData["FiltroActual"] != null ? (int)TempData["FiltroActual"] : 0;
            ViewData["Filtro"] = filtro;

            if (filtro == 1)
            {
                reservas = reservas.Where(r => r.Kilometraje == true).ToList();
            }
            else if (filtro == 2)
            {
                reservas = reservas.Where(r => r.Kilometraje == false).ToList();
            }
            return View(reservas);
        }

        public async Task<IActionResult> ConfirmarKilometraje(int idreserva)
        {
            TempData["WarningMessage"] = "¿Estás seguro de actualizar el kilometraje?";
            TempData["WarningMessageBtn"] = "Actualizar";
            TempData["IdReservaKm"] = idreserva;
            return RedirectToAction("ComprobarKilometraje");
        }
        [HttpGet]
        public async Task<IActionResult> GetReservasConCoche()
        {
            List<VistaReserva> reservas = await this.repo.GetVistaReservasAsync();

            string[] colors = new[] { "#FF5733", "#33FF57", "#3357FF", "#F1C40F", "#9B59B6", "#1ABC9C" };
            Dictionary<int, string> carColorMap = new Dictionary<int, string>();
            int colorIndex = 0;

            var eventos = reservas.Select(r =>
            {
                if (!carColorMap.ContainsKey(r.IdCoche))
                {
                    carColorMap[r.IdCoche] = colors[colorIndex % colors.Length];
                    colorIndex++;
                }
                return new
                {
                    title = $"{r.Marca} {r.Modelo}", 
                    start = r.FechaInicio.ToString("yyyy-MM-dd"),
                    end = r.FechaFin.AddDays(1).ToString("yyyy-MM-dd"),
                    color = carColorMap[r.IdCoche]
                };
            }).ToList();

            return Json(eventos);
        }
    }
}
