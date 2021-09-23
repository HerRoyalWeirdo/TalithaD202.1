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
                //NumberOfGuests = 1
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(BookingViewModel model)
        {
            if (!ModelState.IsValid)//needed the ! otherwise it was always going through as not valid
            {
                model.Rooms = roomService.GetAllRooms();
                ViewBag.ErrorMessage = "Booking was not valid";

                return View("Index", model);
               
                //if (bookingService.IsBookingValid(model.RoomId, booking))
               // {
                //return View("Success");
               // }
            }

            var booking = new Booking()
            {
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                RoomId = model.RoomId,
                CouponCode = model.CouponCode//this made the test work
                //HasPets = model.BringingPets,
                // IsSmoking = model.IsSmoking,
            };
            //model.Rooms = roomService.GetAllRooms();
            //ViewBag.ErrorMessage = "Booking was not valid";

            //return View("Index", model);
            return View("Success",
                new BookingSuccessViewModel
                {
                    Price = bookingService.CalculateBookingPrice(booking)
                });
        }
    }
}
