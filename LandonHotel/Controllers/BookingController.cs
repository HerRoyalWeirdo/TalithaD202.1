using System;
using LandonHotel.Data;
using LandonHotel.Models;
using LandonHotel.Services;
using Microsoft.AspNetCore.Mvc;

namespace LandonHotel.Controllers
{
    public class BookingController : Controller
    {
        private readonly IRoomService roomService;
        private readonly IBookingService bookingService;

        public BookingController(IRoomService roomService, IBookingService bookingService)
        {
            this.roomService = roomService;
            this.bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new BookingViewModel()
            {
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                Rooms = roomService.GetAllRooms(),
                NumberOfGuests = 1
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var booking = new Booking()
                {
                    CheckInDate = model.CheckInDate,
                    CheckOutDate = model.CheckOutDate,
                    HasPets = model.BringingPets,
                    IsSmoking = model.IsSmoking,
                };

                if (bookingService.IsBookingValid(model.RoomId, booking))
                {
                return View("Success");
                }
            }
            model.Rooms = roomService.GetAllRooms();
            ViewBag.ErrorMessage = "Booking was not valid";

            return View("Index", model);
        }
    }
}
