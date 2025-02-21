using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Models;

namespace MvcProyectoRentACar.Repositories
{
    public class RepositorySesion
    {
        private RentACarContext context;
        public RepositorySesion(RentACarContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            return await this.context.Usuarios
                .FirstOrDefaultAsync(i => i.Email == email && i.Password == password);
        }
    }
}
