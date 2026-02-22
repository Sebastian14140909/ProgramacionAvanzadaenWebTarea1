using ReservationApp.Api.DTOs;

namespace ReservationApp.Api.Repository
{
    public interface IReservationRepository
    {
        Task<IEnumerable<ReservationDTO>> GetReservationsAsync();
        Task<ReservationDTO?> GetReservationByIdAsync(int id);
        Task CreateReservationAsync(CreateReservationDTO dto);
        Task UpdateReservationAsync(UpdateReservationDTO dto);
        Task DeleteReservationAsync(int id);
    }
}