using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Helpers;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Models.MvcProyectoRentACar.Models;

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

        public async Task<VistaCoche> GetVistaCocheAsync(int idcoche)
        {
            var consulta = from datos in this.context.VistaCoches
                           where datos.IdCoche == idcoche
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<Coche> GetCocheAsync(int idcoche)
        {
            var consulta = from datos in this.context.Coches
                           where datos.IdCoche == idcoche
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task CompraCocheAsync(int idusuario,int idcoche,DateTime fechainicio, DateTime fechafin, double precio,bool kilometraje)
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
            res.Kilometraje = kilometraje;
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

        public async Task<List<EstadoReserva>> GetEstadoReservaAsync()
        {
            var consulta = from datos in this.context.EstadoReservas
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<List<Compra>> GetComprasUsuarioAsync(int idusuario, string estadoReserva = null)
        {
            List<Reserva> reservas = await (from datos in this.context.Reservas
                                            where datos.IdUsuario == idusuario
                                            select datos).ToListAsync();
            List<Compra> compras = new List<Compra>();
            List<EstadoReserva> estadoreserva = await this.GetEstadoReservaAsync();
            foreach (Reserva reserva in reservas)
            {
                Coche coche = await this.GetCocheAsync(reserva.IdCoche);

                Compra compra = new Compra();
                compra.IdCoche = coche.IdCoche;
                compra.Marca = coche.Marca;
                compra.Modelo = coche.Modelo;
                compra.Imagen = coche.Imagen;
                compra.FechaInicio = reserva.FechaInicio;
                compra.FechaFin = reserva.FechaFin;
                compra.Precio = reserva.Precio;
                var estado = estadoreserva.FirstOrDefault(e => e.IdEstadoReserva == reserva.IdEstadoReserva);
                if (estado != null)
                {
                    compra.EstadoReserva = estado.EstadoDescripcion;
                }
                compras.Add(compra);
            }

            if (!string.IsNullOrEmpty(estadoReserva))
            {
                compras = compras.Where(c => c.EstadoReserva == estadoReserva).ToList();
            }

            return compras;
        }

        public async Task<List<VistaReserva>> GetVistaReservasAsync()
        {
            return await (from r in this.context.Reservas
                          join c in this.context.Coches on r.IdCoche equals c.IdCoche
                          select new VistaReserva
                          {
                              IdReserva = r.IdReserva,
                              IdCoche = r.IdCoche,
                              FechaInicio = r.FechaInicio,
                              FechaFin = r.FechaFin,
                              Marca = c.Marca,
                              Modelo = c.Modelo
                          }).ToListAsync();
        }

    }
}
