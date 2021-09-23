using LandonHotel.Data;
using LandonHotel.Repositories;

namespace LandonHotel.Services
{
    public class BookingService : IBookingService
    {
        //private readonly IBookingsRepository _bookingRepo;
        private readonly IRoomsRepository _roomsRepo;

        public BookingService(IRoomsRepository roomsRepo)//IBookingsRepository bookingRepo,
        {
            //_bookingRepo = bookingRepo;
            _roomsRepo = roomsRepo;
            
        }
//int CalculateBookingCost
        public bool IsBookingValid(int roomId, Booking booking)
        {
            var guestIsSmoking = booking.IsSmoking;
            var guestIsBringingPets = booking.HasPets;
            var numberOfGuests = booking.NumberOfGuests;
            //smoking
            if (guestIsSmoking)
            {
                return false;
            }
            //for the pets test
            var room = _roomsRepo.GetRoom(roomId);
            if (guestIsBringingPets && !room.ArePetsAllowed)//booking.HasPets
            {
                return false;
            }
            //for guest greater than capacity
            if (numberOfGuests > room.Capacity)
            {
                return false;
            }

            return true; //!guestIsSmoking;//both tests work
            //false is booking valid non smoke valid test breaks - bookingvalid smoke invalid test works, true booking valid non smoke valid test works - bookingvalid smoke invalid test breaks
            //return 0;
        }
    }
}
