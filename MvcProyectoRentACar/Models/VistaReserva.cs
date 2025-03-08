namespace MvcProyectoRentACar.Models
{
    namespace MvcProyectoRentACar.Models
    {
        public class VistaReserva
        {
            public int IdReserva { get; set; }
            public int IdCoche { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFin { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
        }
    }

}
