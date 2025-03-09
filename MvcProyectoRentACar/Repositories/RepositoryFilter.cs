using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Models;

namespace MvcProyectoRentACar.Repositories
{
    public class RepositoryFilter
    {
        private RentACarContext context;
        public RepositoryFilter(RentACarContext context)
        {
            this.context = context;
        }

        public async Task<Usuario>GetUsuarioAsync()
        {
            return await this.context.Usuarios
                .Where(x => x.IdRolUsuario == 1).FirstOrDefaultAsync();
        }
        public async Task<Vendedor> GetVendedorAsync(int idusuario)
        {
            return await this.context.Vendedor
                .Where(x => x.IdUsuario == idusuario).FirstOrDefaultAsync();
        }
    }
}
