namespace ReservationApp.Api.DTOs
{
    public class UpdateReservationDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Doctor { get; set; }
        public DateTime Date { get; set; }
        public string Specialty { get; set; }
    }
}   