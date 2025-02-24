using Microsoft.EntityFrameworkCore;
using MvcProyectoRentACar.Data;
using MvcProyectoRentACar.Helpers;
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
        private async Task<int> GetMaxIdUser()
        {
            if(this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Usuarios.MaxAsync
                    (x => x.IdUsuario) + 1;
            }
        }
        private async Task<int> GetMaxIdComprador()
        {
            if (this.context.Compradores.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Compradores.MaxAsync
                    (x => x.IdComprador) + 1;
            }
        }

        private async Task<bool> CheckVendedor()
        {
            return await this.context.Vendedor.AnyAsync();
        }
        public async Task<Usuario> LoginAsync(string email, string password)
        {
            return await this.context.Usuarios
                .FirstOrDefaultAsync(i => i.Email == email && i.Password == password);
        }

        public async Task<List<Rol>> GetRolesAsync()
        {
            var consulta = from datos in this.context.Roles
                           //orderby datos.Id
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<bool> RegisterUsuarioAsync
            (string nombre, string email,string password, int idrol,string telefono
            , string ?  apellidos = null ,string ? dni = null, string ? carnet=null
             ,DateTime ? fechanacimiento = null,string ? direccion = null,string ? passpecial = null
            ,string ? nombreempresa = null)
        {
            Usuario usuario = new Usuario();
            usuario.IdUsuario = await this.GetMaxIdUser();
            usuario.Nombre = nombre;
            usuario.Email = email;
            usuario.Password = password;
            usuario.IdRolUsuario = idrol;
            usuario.Salt = HelperCryptography.GenerateSalt();
            usuario.PassBytes = HelperCryptography.EncryptPassword(password, usuario.Salt);
            this.context.Usuarios.Add(usuario);

            if(idrol == 2)
            {
                Comprador comprador = new Comprador();
                comprador.IdComprador = await this.GetMaxIdComprador();
                comprador.IdUsuario = usuario.IdUsuario;
                comprador.Nombre =usuario.Nombre;
                comprador.Apellidos = apellidos;
                comprador.Dni = dni;
                comprador.Carnet = carnet;
                comprador.Telefono = telefono;
                comprador.FechaNacimiento = (DateTime)fechanacimiento;
                this.context.Compradores.Add(comprador);
                await this.context.SaveChangesAsync();
                return true;
            }
            else
            {
                bool check = await this.CheckVendedor();
                if (!check && passpecial=="v123") {
                    Vendedor vendedor = new Vendedor();
                    vendedor.IdVendedor = 1;
                    vendedor.IdUsuario = usuario.IdUsuario;
                    vendedor.NombreEmpresa = nombreempresa;
                    vendedor.Direccion = direccion;
                    vendedor.Telefono = telefono;
                    this.context.Vendedor.Add(vendedor);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    System.Console.WriteLine("ya existe vendedor");
                    return false;
                }
               
            }
          
           
        }
    }
}
