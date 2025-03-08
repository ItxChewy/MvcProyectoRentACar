using Microsoft.AspNetCore.Mvc;
using MvcProyectoRentACar.Helpers;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Models.MvcProyectoRentACar.Models;
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
            string preciokilometros, string precioilimitado)
        {
            int idvendedor = (int)HttpContext.Session.GetInt32("usuarioactual");

            decimal kilometrossanize = HelperInputSanitizer.SanitizeDecimalInput(preciokilometros);
            decimal ilimitadosanized = HelperInputSanitizer.SanitizeDecimalInput(precioilimitado);
             

            await this.repo.InsertCocheAsync(marca, modelo, matricula, fichero, asientos, idmarchas, idgama, kilometraje
                , puertas, idcombustible, idvendedor, kilometrossanize, ilimitadosanized);

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

        [HttpPost]
        public async Task<IActionResult> ComprobarKilometraje(int idreserva,int newkilometraje)
        {
            await this.repo.GetCargoExcedido(idreserva, newkilometraje);
            List<Reserva> reservas = await this.repo.GetReservasFilterAsync();
            return View(reservas);
        }
        [HttpGet]
        public async Task<IActionResult> GetReservasConCoche()
        {
            // Retrieve reservations with car details.
            List<VistaReserva> reservas = await this.repo.GetVistaReservasAsync();

            // Predefine a set of colors.
            string[] colors = new[] { "#FF5733", "#33FF57", "#3357FF", "#F1C40F", "#9B59B6", "#1ABC9C" };
            // Dictionary to store mapping: car id -> assigned color.
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
                    title = $"{r.Marca} {r.Modelo}",  // Display car brand and model.
                    start = r.FechaInicio.ToString("yyyy-MM-dd"),
                    // FullCalendar's end date is exclusive, so you may wish to add one day (if needed):
                    end = r.FechaFin.AddDays(1).ToString("yyyy-MM-dd"),
                    color = carColorMap[r.IdCoche]
                };
            }).ToList();

            return Json(eventos);
        }
    }
}
