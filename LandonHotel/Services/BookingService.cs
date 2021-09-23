using LandonHotel.Data;
using LandonHotel.Repositories;


namespace LandonHotel.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRoomsRepository _roomRepo;
        private readonly ICouponRepository _couponRepo;//to use in tests

        public BookingService(IRoomsRepository roomRepo, ICouponRepository couponRepo)
        {
            _roomRepo = roomRepo;
            _couponRepo = couponRepo;
        }

        public decimal CalculateBookingPrice(Booking booking)
        {
            //var roomRepo = new RoomsRepository();//up there ^
            var room = _roomRepo.GetRoom(booking.RoomId);

            var numberOfNights = (booking.CheckOutDate - booking.CheckInDate).Days;
            var price =  room.Rate * numberOfNights;

            //if(booking.CouponCode != null)//works after this is added
            //{
            //var discount = _couponRepo.GetCoupon(booking.CouponCode).PercentageDiscount;
            //price = (int)(price - (price * discount / 100));//wont work with out the int
            //cannot implicitly convert typre decimal to int. and explicit conversion exists - missing cast?
            //}
            //resharper invert if - i copy
            //if (booking.CouponCode == null) return price;//this broke
            if (string.IsNullOrEmpty(booking.CouponCode)) return price;//this wokr
            var discount = _couponRepo.GetCoupon(booking.CouponCode).PercentageDiscount;
            price = (int)(price - (price * discount / 100));
            return price;
        }
        //private readonly IBookingsRepository _bookingRepo;
        //private readonly IRoomsRepository _roomsRepo;

        // public BookingService(IRoomsRepository roomsRepo)//IBookingsRepository bookingRepo,
        //{
        //_bookingRepo = bookingRepo;
        //_roomsRepo = roomsRepo;

        //}
        //int CalculateBookingCost
        //public bool IsBookingValid(int roomId, Booking booking)
        //{
        //var guestIsSmoking = booking.IsSmoking;
        //var guestIsBringingPets = booking.HasPets;
        //var numberOfGuests = booking.NumberOfGuests;
        //smoking
        //if (guestIsSmoking)
        // {
        //return false;
        //}
        //for the pets test
        //var room = _roomsRepo.GetRoom(roomId);
        //if (guestIsBringingPets && !room.ArePetsAllowed)//booking.HasPets
        //{
        //return false;
        //}
        //for guest greater than capacity
        //if (numberOfGuests > room.Capacity)
        //{
        // return false;
        //{

        //return true; //!guestIsSmoking;//both tests work
        //false is booking valid non smoke valid test breaks - bookingvalid smoke invalid test works, true booking valid non smoke valid test works - bookingvalid smoke invalid test breaks
        //return 0;
        //}
    }
}