using ReservationApp.Models;

namespace ReservationApp.Services
{
    public class ReservationApiServices
    {
        private readonly HttpClient _httpClient;

        public ReservationApiServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReservationViewModel>> GetReservationsAsync()
        {
            var reservations = await _httpClient.GetAsync("reservations");
            reservations.EnsureSuccessStatusCode();

            return await reservations.Content.ReadFromJsonAsync<List<ReservationViewModel>>();

        }

        public async Task CreateReservationAsync(CreateReservationViewModel createReservationViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("reservations", createReservationViewModel);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error creating reservation: {errorMessage}");
            }
        }
        public async Task<ReservationViewModel> GetReservationByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"reservations/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ReservationViewModel>();
        }

        public async Task UpdateReservationAsync(UpdateReservationViewModel vm)
        {
            var dto = new
            {
                Id = vm.Id,
                ClientName = vm.Patient,
                Doctor = vm.Doctor,
                Specialty = vm.Specialty,
                Date = vm.Date.ToDateTime(TimeOnly.MinValue)
            };

            var response = await _httpClient.PutAsJsonAsync($"reservations/{vm.Id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error updating reservation: {errorMessage}");
            }
        }

        public async Task DeleteReservationAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"reservations/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error deleting reservation: {errorMessage}");
            }
        }
    }
}
