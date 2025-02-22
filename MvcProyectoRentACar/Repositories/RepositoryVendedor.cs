using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Helpers;
using MvcProyectoRentACar.Models;

namespace MvcProyectoRentACar.Repositories
{
    #region PROCEDURES
    /*  
     CREATE PROCEDURE SP_INSERT_COCHE
    (@MARCA NVARCHAR(50),@MODELO NVARCHAR(50),@MATRICULA NVARCHAR(50),@IMAGEN NVARCHAR(50)
    ,@ASIENTOS INT,@IDMARCHAS INT,@IDGAMA INT,@KILOMETRAJE INT,@PUERTAS INT,@IDCOMBUSTIBLE INT
    ,@IDVENDEDOR INT,@PRECIOKILOMETROS DECIMAL(10,2),@PRECIOILIMITADO DECIMAL(10,2))
    AS
	    INSERT INTO COCHE VALUES(@MARCA,@MODELO,@MATRICULA,@IMAGEN,@ASIENTOS,@IDMARCHAS,@IDGAMA,1
	    ,@KILOMETRAJE,@PUERTAS,@IDCOMBUSTIBLE,@IDVENDEDOR,@PRECIOKILOMETROS,@PREKCIOILIMITADO);

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
            (string marca, string modelo, string matricula,IFormFile fichero, int asientos, int idmarchas,
            int idgama, int kilometraje, int puertas, int idcombustible, int idvendedor,
            decimal preciokilometros, decimal precioilimitado)
        {
            string fileName = fichero.FileName.ToLower();
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
            if (coche != null)
            {
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
        public async Task <List<Combustible>> GetCombustiblesAsync()
        {
            var consulta = from datos in this.context.Combustibles
                           select datos;
            return await consulta.ToListAsync();
        }
    }
}
