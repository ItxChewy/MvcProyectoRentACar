using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
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

    }
}
