using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Models;

namespace MvcProyectoRentACar.Data
{
    public class RentACarContext :DbContext
    {
        public RentACarContext (DbContextOptions<RentACarContext> options)
            : base(options) { }


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<Comprador> Compradores { get; set; }
        public DbSet<Coche> Coches { get; set; }
        public DbSet<VistaCoche>VistaCoches { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Marchas> Marchas { get; set; }
        public DbSet<Combustible> Combustibles { get; set; }
        public DbSet<Gama> Gamas { get; set; }
        public DbSet<EstadoReserva> EstadoReservas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }


    }
}
