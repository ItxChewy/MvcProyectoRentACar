using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("ID")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("PASSWORD")]
        public string Password { get; set; }
        [Column("IDROL")]
        public int IdRolUsuario { get; set; }

        [Column("SALT")]
        public string Salt { get; set; }
        [Column("PASS")]
        public byte[] PassBytes { get; set; }

        public virtual Vendedor Vendedor { get; set; }

        public virtual Comprador Comprador { get; set; }

    }
}
