namespace ReservationApp.Api.DTOs
{
    public class UpdateReservationDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int RoomNumber { get; set; }
        public DateTime Date { get; set; }
        public int Nights { get; set; }
    }
}   