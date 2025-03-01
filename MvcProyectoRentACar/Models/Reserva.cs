using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("RESERVA")]
    public class Reserva
    {
        [Key]
        [Column("ID")]
        public int IdReserva { get; set; }

        [ForeignKey("Coche")]
        [Column("IDCOCHE")]
        public int IdCoche { get; set; }
        public virtual Coche Coche { get; set; }

        [ForeignKey("Usuario")]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Column("FECHAINICIO")]
        public DateTime FechaInicio { get; set; }

        [Column("FECHAFIN")]
        public DateTime FechaFin { get; set; }

        [ForeignKey("EstadoReserva")]
        [Column("IDESTADORESERVA")]
        public int IdEstadoReserva { get; set; }

        [Column("PRECIO", TypeName = "decimal(10,2)")]
        public double Precio { get; set; }
        public virtual EstadoReserva EstadoReserva { get; set; }
    }
}
