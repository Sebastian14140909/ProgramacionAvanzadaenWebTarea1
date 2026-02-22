using ReservationApp.Api.DTOs;

namespace ReservationApp.Api.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetReservationsAsync();
        Task<ReservationDTO?> GetReservationByIdAsync(int id);
        Task CreateReservationAsync(CreateReservationDTO dto);
        Task UpdateReservationAsync(UpdateReservationDTO dto);
        Task DeleteReservationAsync(int id);
    }
}