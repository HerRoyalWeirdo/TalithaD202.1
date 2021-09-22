using System;
using LandonHotel.Data;
using LandonHotel.Services;
using Xunit;


namespace LandonHotels.Tests
{
    public class BookingServiceTests
    {
        [Fact]
        public void IsBookingValid_NonSmoker_Valid()
        {
            var service = new BookingService(null, null);
            var isValid = service.IsBookingValid(1, new Booking() { IsSmoking = false });

            Assert.True(isValid);
        }
        [Fact]
        public void IsBookingValid_Smoker_Invalid()
        {
            var service = new BookingService(null, null);
            var isValid = service.IsBookingValid(1, new Booking() { IsSmoking = true });

            Assert.False(isValid);
        }
    }
}
