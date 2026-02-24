using Microsoft.AspNetCore.Mvc;
using ReservationApp.Api.DTOs;
using ReservationApp.Api.Services;

namespace ReservationApp.Api.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations()
            => Ok(await _service.GetReservationsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(int id)
        {
            var r = await _service.GetReservationByIdAsync(id);
            if (r == null) return NotFound();
            return Ok(r);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDTO dto)
        {
            await _service.CreateReservationAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, UpdateReservationDTO dto)
        {
            dto.Id = id;
            await _service.UpdateReservationAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _service.DeleteReservationAsync(id);
            return Ok();
        }

    }
}