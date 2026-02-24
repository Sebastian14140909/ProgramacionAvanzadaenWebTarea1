using Microsoft.AspNetCore.Mvc;
using ReservationApp.Services;
using ReservationApp.Models;

namespace ReservationApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationApiServices _reservationApiServices;

        public ReservationController(ReservationApiServices reservationApiServices)
        {
            _reservationApiServices = reservationApiServices;
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationApiServices.GetReservationsAsync();
            return View(reservations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationViewModel createReservationViewModel) {
            if (ModelState.IsValid)
            {
                try
                {
                    await _reservationApiServices.CreateReservationAsync(createReservationViewModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the reservation: {ex.Message}");
                    return View(createReservationViewModel);
                }

            }
            return View(createReservationViewModel);
        }

    public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _reservationApiServices.GetReservationByIdAsync(id);

            var vm = new UpdateReservationViewModel
            {
                Id = reservation.Id,
                Patient = reservation.Patient,
                Date = reservation.Date,
                Doctor = reservation.Doctor,
                Specialty = reservation.Specialty
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateReservationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _reservationApiServices.UpdateReservationAsync(vm);
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _reservationApiServices.DeleteReservationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}