using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Helpers;
using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Models.MvcProyectoRentACar.Models;

namespace MvcProyectoRentACar.Repositories
{
    #region PROCEDURES
    /*  
     alter PROCEDURE SP_INSERT_COCHE
    (@MARCA NVARCHAR(50),@MODELO NVARCHAR(50),@MATRICULA NVARCHAR(50),@IMAGEN NVARCHAR(50),
    @ASIENTOS INT,@IDMARCHAS INT,@IDGAMA INT,@KILOMETRAJE INT,@PUERTAS INT,@IDCOMBUSTIBLE INT,
    @IDVENDEDOR INT,@PRECIOKILOMETROS DECIMAL(10,2),@PRECIOILIMITADO DECIMAL(10,2))
    AS
    BEGIN
        DECLARE @NEW_ID INT;

        -- Obtener el máximo ID de la tabla COCHE y sumarle 1
        SELECT @NEW_ID = ISNULL(MAX(ID), 0) + 1 FROM COCHE;

        -- Insertar el nuevo coche con el ID calculado
        INSERT INTO COCHE (ID, MARCA, MODELO, MATRICULA, IMAGEN, ASIENTOS, IDMARCHAS, IDGAMA, IDESTADO,
                           KILOMETRAJE, PUERTAS, IDCOMBUSTIBLE, IDVENDEDOR, PRECIOKILOMETROS, PRECIOILIMITADO)
        VALUES (@NEW_ID, @MARCA, @MODELO, @MATRICULA, @IMAGEN, @ASIENTOS, @IDMARCHAS, @IDGAMA, 1,
                @KILOMETRAJE, @PUERTAS, @IDCOMBUSTIBLE, @IDVENDEDOR, @PRECIOKILOMETROS, @PRECIOILIMITADO);
    END;
    GO
     */

    #endregion
    public class RepositoryVendedor
    {
        private RentACarContext context;
        private HelperPathProvider helperPath;
        public RepositoryVendedor(RentACarContext context, HelperPathProvider helperPath)
        {
            this.context = context;
            this.helperPath = helperPath;
        }

        public async Task<List<VistaCoche>> GetCochesAsync()
        {
            var consulta = from datos in this.context.VistaCoches
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Coche> DetailsCocheAsync(int idcoche)
        {
            var consulta = from datos in this.context.Coches
                           where datos.IdCoche == idcoche
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }
        public async Task InsertCocheAsync
            (string marca, string modelo, string matricula, IFormFile fichero, int asientos, int idmarchas,
            int idgama, int kilometraje, int puertas, int idcombustible, int idvendedor,
            decimal preciokilometros, decimal precioilimitado)
        {
            int maxId = await this.context.Coches
                        .Select(x => x.IdCoche)
                        .MaxAsync() + 1;
            string fileName = maxId.ToString() + fichero.FileName.ToLower();
            string path = this.helperPath.MapPath(fileName, Folders.Images);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }

            string sql = "SP_INSERT_COCHE @MARCA, @MODELO, @MATRICULA, @IMAGEN, @ASIENTOS, @IDMARCHAS, " +
                "@IDGAMA, @KILOMETRAJE, @PUERTAS, @IDCOMBUSTIBLE, @IDVENDEDOR, @PRECIOKILOMETROS, @PRECIOILIMITADO";
            var parametros = new[]
            {
                new SqlParameter("@MARCA", marca),
                new SqlParameter("@MODELO", modelo),
                new SqlParameter("@MATRICULA", matricula),
                new SqlParameter("@IMAGEN", fileName),
                new SqlParameter("@ASIENTOS", asientos),
                new SqlParameter("@IDMARCHAS", idmarchas),
                new SqlParameter("@IDGAMA", idgama),
                new SqlParameter("@KILOMETRAJE", kilometraje),
                new SqlParameter("@PUERTAS", puertas),
                new SqlParameter("@IDCOMBUSTIBLE", idcombustible),
                new SqlParameter("@IDVENDEDOR", idvendedor),
                new SqlParameter("@PRECIOKILOMETROS", preciokilometros),
                new SqlParameter("@PRECIOILIMITADO", precioilimitado)
            };
            await this.context.Database.ExecuteSqlRawAsync(sql, parametros);
        }

        public async Task UpdateCocheAsync
            (int idcoche, int idgama, decimal preciokilometros, decimal precioilimitado)
        {
            var consulta = from datos in this.context.Coches
                           where datos.IdCoche == idcoche
                           select datos;
            Coche coche = await consulta.FirstOrDefaultAsync();
            if (coche != null)
            {
                coche.IdGama = idgama;
                coche.PrecioKilometros = preciokilometros;
                coche.PrecioIlimitado = precioilimitado;
                await this.context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Estamos teniendo problemas al encontrar este coche");
            }
        }

        public async Task DeleteCocheAsync(int idcoche)
        {
            var consulta = from datos in this.context.Coches
                           where datos.IdCoche == idcoche
                           select datos;
            Coche coche = await consulta.FirstOrDefaultAsync();
            List<Reserva> reservas = await this.context.Reservas
                                .Where(x => x.IdCoche == idcoche).ToListAsync();
            if (coche != null)
            {
                if (reservas != null && reservas.Count > 0)
                {
                    this.context.Reservas.RemoveRange(reservas);
                }
                string imagePath = this.helperPath.MapPath(coche.Imagen, Folders.Images);

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                this.context.Coches.Remove(coche);
                await this.context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Estamos teniendo problemas al dar de baja a este coche");
            }
        }


        public async Task<List<Marchas>> GetMarchasAsync()
        {
            var consulta = from datos in this.context.Marchas
                           select datos;
            return await consulta.ToListAsync();
        }
        public async Task<List<Gama>> GetGamasAsync()
        {
            var consulta = from datos in this.context.Gamas
                           select datos;
            return await consulta.ToListAsync();
        }
        public async Task<List<Combustible>> GetCombustiblesAsync()
        {
            var consulta = from datos in this.context.Combustibles
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<List<EstadoReserva>> GetEstadoReservaAsync()
        {
            var consulta = from datos in this.context.EstadoReservas
                           select datos;
            return await consulta.ToListAsync();
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
        public async Task<List<Reserva>> GetReservasFilterAsync()
        {
            DateTime today = DateTime.Today;
            DateTime threeDaysAhead = today.AddDays(3);

            var consulta = from datos in this.context.Reservas
                           where datos.FechaFin >= today
                                 && datos.FechaFin <= threeDaysAhead
                                 && datos.IdEstadoReserva != 2
                           orderby datos.FechaFin ascending
                           select datos;
            return await consulta.ToListAsync();
        }
        public async Task<List<Reserva>> GetReservasKilometrajeAsync()
        {
            DateTime today = DateTime.Today;
            DateTime threeDaysAhead = today.AddDays(3);

            var consulta = from datos in this.context.Reservas
                           where datos.Kilometraje == true
                                 && datos.FechaFin >= today
                                 && datos.FechaFin <= threeDaysAhead
                                 && datos.IdEstadoReserva != 2
                           orderby datos.FechaFin ascending
                           select datos;
            return await consulta.ToListAsync();
        }


        public async Task<List<Reserva>> GetReservasIlimitadosAsync()
        {
            DateTime today = DateTime.Today;
            DateTime threeDaysAhead = today.AddDays(3);

            var consulta = from datos in this.context.Reservas
                           where datos.Kilometraje == false
                                 && datos.FechaFin >= today
                                 && datos.FechaFin <= threeDaysAhead
                                 && datos.IdEstadoReserva != 2
                           orderby datos.FechaFin ascending
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<List<Reserva>> GetReservasPorCocheAsync(int idcoche)
        {
            var consulta = from datos in this.context.Reservas
                           where datos.IdCoche == idcoche
                           select datos;
            return await consulta.ToListAsync();
        }
        public async Task<List<Reserva>> GetReservasNoFinalizadasPorCocheAsync(int idcoche)
        {
            var consulta = from datos in this.context.Reservas
                           where datos.IdCoche == idcoche && datos.IdEstadoReserva != 2
                           orderby datos.FechaInicio
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Reserva> GetReservaAsync(int idreserva)
        {
            var consulta = from datos in this.context.Reservas
                           where datos.IdReserva == idreserva
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task CambiarAEstadoActivoReservaAsync(int idreserva)
        {
            Reserva res = await this.GetReservaAsync(idreserva);
            if (res.FechaInicio != DateTime.Today)
            {
                throw new Exception("La reserva solo puede ser activada si la fecha de inicio es hoy.");
            }
            var activeReservations = await this.context.Reservas
                            .Where(r => r.IdCoche == res.IdCoche && r.IdEstadoReserva == 1)
                            .ToListAsync();

            if (activeReservations.Any())
            {
                throw new Exception("Ya existe una reserva activa para este coche");
            }

            res.IdEstadoReserva = 1;
            await this.context.SaveChangesAsync();
        }
        public async Task CambiarAEstadoFinalizadoReservaAsync(int idreserva)
        {
            Reserva res = await this.GetReservaAsync(idreserva);
            res.IdEstadoReserva = 2;
            await this.context.SaveChangesAsync();
        }

        public async Task ActualizarKilometraje(int idreserva, int newkilometraje)
        {
            var idCoche = await (from datos in this.context.Reservas
                                 where datos.IdReserva == idreserva
                                 select datos.IdCoche).FirstOrDefaultAsync();

            Coche coche = await this.DetailsCocheAsync(idCoche);
            int oldKilometraje = coche.Kilometraje;
            int kilometrosCalculados = oldKilometraje + newkilometraje;
            coche.Kilometraje = kilometrosCalculados;
            await this.context.SaveChangesAsync();
        }

        public async Task GetCargoExcedido(int idreserva, int newkilometraje)
        {
            

            double cargoAdicional = 0;

            if (newkilometraje > 1200)
            {
                int kilometrosExcedidos = newkilometraje - 1200;
                cargoAdicional = kilometrosExcedidos * 2.5;
            }

            // Obtener la reserva
            Reserva reserva = await this.GetReservaAsync(idreserva);
            if (reserva != null)
            {
                // Obtener el comprador asociado a la reserva
                var comprador = await (from datos in context.Compradores
                                      where datos.IdUsuario == reserva.IdUsuario
                                      select datos).FirstOrDefaultAsync();

                if (comprador != null)
                {
                    // Añadir el cargo adicional al monedero del comprador
                    comprador.Monedero += (decimal)cargoAdicional;
                    await this.context.SaveChangesAsync();
                }
            }
            await this.ActualizarKilometraje(idreserva, newkilometraje);
            await this.CambiarAEstadoFinalizadoReservaAsync(idreserva);
        }



    }
}
