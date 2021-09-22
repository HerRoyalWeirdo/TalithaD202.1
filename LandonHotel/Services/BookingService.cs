using LandonHotel.Data;
using LandonHotel.Repositories;

namespace LandonHotel.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRoomsRepository _roomRepo;
        private readonly IBookingsRepository _bookingRepo;

        public BookingService(IBookingsRepository bookingRepo, IRoomsRepository roomRepo)
        {
            _roomRepo = roomRepo;
            _bookingRepo = bookingRepo;
        }
//int CalculateBookingCost
        public bool IsBookingValid(int roomId, Booking booking)
        {
            var guestIsSmoking = booking.IsSmoking;
            var guestIsBringingPets = booking.HasPets;
            var numberOfGuests = booking.NumberOfGuests;
            return !guestIsSmoking;
            //return 0;
        }
    }
}
