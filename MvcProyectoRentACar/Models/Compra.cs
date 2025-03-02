namespace MvcProyectoRentACar.Models
{
    public class Compra
    {
        public int IdCoche { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double Precio { get; set; }
        public string EstadoReserva { get; set; }
        //public int IdUsuario { get; set; }

    }
}
