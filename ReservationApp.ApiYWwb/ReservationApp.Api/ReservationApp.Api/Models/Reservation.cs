using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationApp.Api.Models
{
    [Table("Reservas")]
    public class Reservation
    {
        public int Id { get; set; }

        [Column("Paciente")]
        public string Patient { get; set; } = string.Empty;

        [Column("Medico")]
        public string Doctor { get; set; } = string.Empty;

        [Column("Especialidad")]
        public string Specialty { get; set; } = string.Empty;

        [Column("Fecha")]
        public DateOnly Date { get; set; }

        [Column("FechaCreacion")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}