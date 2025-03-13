using MvcProyectoRentACar.Models;
using MvcProyectoRentACar.Models.MvcProyectoRentACar.Models;

namespace MvcProyectoRentACar.Repositories
{
    public interface IRepositoryRentACar
    {
        #region repositoryFilter
        Task<Usuario> GetUsuarioAsync();
        Task<Vendedor> GetVendedorAsync(int idusuario);
        #endregion
        #region repositorysesion
        Task<int> GetMaxIdUser();
        Task<int> GetMaxIdComprador();
        Task<bool> CheckVendedor();
        Task<Usuario> LoginAsync(string email, string password);
        Task<List<Rol>> GetRolesAsync();
        Task<bool> RegisterUsuarioAsync
            (string nombre, string email, string password, int idrol, string telefono
            , string? apellidos, string? dni,  DateTime? fechanacimiento, string? direccion, string? passpecial
            , string? nombreempresa);
        #endregion
        #region repositoryVendedor
        Task<List<VistaCoche>> GetCochesAsync();
        Task<Coche> DetailsCocheAsync(int idcoche);
        Task<bool> InsertCocheAsync(string marca, string modelo, string matricula, IFormFile fichero, int asientos, int idmarchas,
        int idgama, int kilometraje, int puertas, int idcombustible, int idvendedor,
        decimal preciokilometros, decimal precioilimitado);

        Task UpdateCocheAsync(int idcoche, int idgama, decimal preciokilometros, decimal precioilimitado);
        Task DeleteCocheAsync(int idcoche);
        Task<List<Marchas>> GetMarchasAsync();
        Task<List<Gama>> GetGamasAsync();
        Task<List<Combustible>> GetCombustiblesAsync();
        Task<List<EstadoReserva>> GetEstadoReservaAsync();
        Task<List<VistaReserva>> GetVistaReservasAsync();
        Task<List<Reserva>> GetReservasFilterAsync();
        Task<List<Reserva>> GetReservasKilometrajeAsync();
        Task<List<Reserva>> GetReservasIlimitadosAsync();
        Task<List<Reserva>> GetReservasPorCocheAsync(int idcoche);
        Task<List<Reserva>> GetReservasNoFinalizadasPorCocheAsync(int idcoche);
        Task<Reserva> GetReservaAsync(int idreserva);
        Task<bool> CambiarAEstadoActivoReservaAsync(int idreserva);
        Task CambiarAEstadoFinalizadoReservaAsync(int idreserva);
        Task ActualizarKilometraje(int idreserva, int newkilometraje);
        Task GetCargoExcedido(int idreserva, int newkilometraje);
        #endregion
        #region repositoryComprador
        Task<List<VistaCoche>> FindAllCochesAsync();
        Task<List<VistaCoche>> FilterByPrecio(int valor);
        Task<VistaCoche> GetVistaCocheAsync(int idcoche);
        Task<Coche> GetCocheAsync(int idcoche);
        Task CompraCocheAsync(int idusuario, int idcoche, DateTime fechainicio, DateTime fechafin, double precio, bool kilometraje);
        Task<bool> ComprobarDisponibilidadCocheAsync(int idcoche, DateTime fechainicio, DateTime fechafin);
        //Task<List<EstadoReserva>> GetEstadoReservaAsync();
        Task<List<Compra>> GetComprasUsuarioAsync(int idusuario, string estadoReserva = null);
        //Task<List<VistaReserva>> GetVistaReservasAsync();
        #endregion
        
    }
}
