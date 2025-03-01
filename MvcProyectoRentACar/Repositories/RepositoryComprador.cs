using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Helpers;
using MvcProyectoRentACar.Models;

namespace MvcProyectoRentACar.Repositories
{
    public class RepositoryComprador
    {
        private RentACarContext context;
        
        public RepositoryComprador(RentACarContext context)
        {
            this.context = context;
            
        }

        public async Task<List<VistaCoche>> FindAllCochesAsync()
        {
            var consulta = from datos in this.context.VistaCoches
                           orderby datos.PrecioKilometros descending
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<List<VistaCoche>>FilterByPrecio(int valor)
        {
            if(valor == 0)
            {
                var consulta = from datos in this.context.VistaCoches
                               orderby datos.PrecioKilometros ascending
                               select datos;
                return await consulta.ToListAsync();
            }
            else
            {
                var consulta = from datos in this.context.VistaCoches
                               orderby datos.PrecioKilometros descending
                               select datos;
                return await consulta.ToListAsync();
            }

        }

        public async Task<VistaCoche> GetCocheAsync(int idcoche)
        {
            var consulta = from datos in this.context.VistaCoches
                           where datos.IdCoche == idcoche
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task CompraCocheAsync(int idusuario,int idcoche,DateTime fechainicio, DateTime fechafin, double precio)
        {
            //se comprueba en el controller si ese coche esta disponible 
            int idReserva = 1;
            if (this.context.Reservas.Any())
            {
                idReserva = (from datos in this.context.Reservas
                             select datos.IdReserva).Max() + 1;
            }
            Reserva res = new Reserva();
            res.IdReserva = idReserva;
            res.IdCoche = idcoche;
            res.IdUsuario = idusuario;
            res.FechaInicio = fechainicio;
            res.FechaFin = fechafin;
            res.Precio = precio;
            if(DateTime.Now.Date == fechainicio.Date)
            {
                res.IdEstadoReserva = 1;
            }
            else
            {
                res.IdEstadoReserva = 3;
            }
            await this.context.Reservas.AddAsync(res);
            await this.context.SaveChangesAsync();
            
        }

        public async Task<bool> ComprobarDisponibilidadCocheAsync(int idcoche, DateTime fechainicio, DateTime fechafin)
        {
            var reservas = await (from reserva in this.context.Reservas
                                  where reserva.IdCoche == idcoche &&
                                        ((fechainicio >= reserva.FechaInicio && fechainicio <= reserva.FechaFin) ||
                                         (fechafin >= reserva.FechaInicio && fechafin <= reserva.FechaFin) ||
                                         (fechainicio <= reserva.FechaInicio && fechafin >= reserva.FechaFin))
                                  select reserva).ToListAsync();

            return !reservas.Any();
        }

    }
}
