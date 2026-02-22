using ReservationApp.Api.DTOs;
using ReservationApp.Api.Repository;

namespace ReservationApp.Api.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;

        public ReservationService(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationsAsync()
            => await _repository.GetReservationsAsync();

        public async Task<ReservationDTO?> GetReservationByIdAsync(int id)
            => await _repository.GetReservationByIdAsync(id);

        public async Task CreateReservationAsync(CreateReservationDTO dto)
            => await _repository.CreateReservationAsync(dto);

        public async Task UpdateReservationAsync(UpdateReservationDTO dto)
            => await _repository.UpdateReservationAsync(dto);

        public async Task DeleteReservationAsync(int id)
            => await _repository.DeleteReservationAsync(id);
    }
}