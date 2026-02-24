using Microsoft.Data.SqlClient;
using ReservationApp.Api.DTOs;

namespace ReservationApp.Api.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;

        public ReservationRepository(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationsAsync()
        {
            var reservations = new List<ReservationDTO>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(
                "SELECT Id, Paciente, Medico, Especialidad, Fecha FROM Reservas",
                connection);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reservations.Add(new ReservationDTO
                {
                    Id = reader.GetInt32(0), 
                    Patient = reader.GetString(1),
                    Doctor = reader.GetString(2),
                    Specialty = reader.GetString(3),
                    Date = DateOnly.FromDateTime(reader.GetDateTime(4))
                });
            }

            return reservations;
        }

        public async Task<ReservationDTO?> GetReservationByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(
                "SELECT Id, Paciente, Medico, Especialidad, Fecha FROM Reservas WHERE Id = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ReservationDTO
                {
                    Id = reader.GetInt32(0),
                    Patient = reader.GetString(1),
                    Doctor = reader.GetString(2),
                    Specialty = reader.GetString(3),
                    Date = DateOnly.FromDateTime(reader.GetDateTime(4))
                };
            }

            return null;
        }

        public async Task CreateReservationAsync(CreateReservationDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(
                @"INSERT INTO Reservas 
                  (Paciente, Medico, Especialidad, Fecha, FechaCreacion) 
                  VALUES (@Paciente, @Medico, @Especialidad, @Fecha, @FechaCreacion)",
                connection);

            command.Parameters.AddWithValue("@Paciente", dto.Patient);
            command.Parameters.AddWithValue("@Medico", dto.Doctor);
            command.Parameters.AddWithValue("@Especialidad", dto.Specialty);
            command.Parameters.AddWithValue("@Fecha", dto.Date.ToDateTime(new TimeOnly(0, 0)));
            command.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateReservationAsync(UpdateReservationDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(
            @"UPDATE Reservas 
      SET Paciente = @Paciente, 
          Medico = @Medico, 
          Especialidad = @Especialidad,
          Fecha = @Fecha
      WHERE Id = @Id",
            connection);

            command.Parameters.AddWithValue("@Id", dto.Id);
            command.Parameters.AddWithValue("@Paciente", dto.ClientName);
            command.Parameters.AddWithValue("@Medico", dto.Doctor);
            command.Parameters.AddWithValue("@Especialidad", dto.Specialty);
            command.Parameters.AddWithValue("@Fecha", dto.Date);


            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteReservationAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(
                "DELETE FROM Reservas WHERE Id = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}