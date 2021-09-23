using LandonHotel.Data;
using LandonHotel.Services;
using Xunit;
using Moq;
using LandonHotel.Repositories;


namespace LandonHotel.Tests
{
    public class BookingServiceTests
    {
        private Mock<IRoomsRepository> _roomsRepo;//using moq and repos
        public BookingServiceTests()
        {
            //before each
            _roomsRepo = new Mock<IRoomsRepository>();
            _roomsRepo.Setup(r => r.GetRoom(1)).Returns(new Room());//reason rom capacity test fail
        }

        private BookingService Subject()
        {
            return new BookingService(_roomsRepo.Object);
        }//from chapter 2 vid 2 begin

        [Fact]
        public void IsBookingValid_NonSmoker_Valid()
        {
            var service = Subject();//new BookingService(null, null);
            var isValid = service.IsBookingValid(1, new Booking() { IsSmoking = false });

            Assert.True(isValid);
        }

        [Fact]
        public void IsBookingValid_Smoker_Invalid()
        {
            var service = Subject();//new BookingService(null, null);
            var isValid = service.IsBookingValid(1, new Booking() { IsSmoking = true });

            Assert.False(isValid);
        }

        //obsoletre cause Theory
        //[Fact]
        //public void IsBookingValid_PetsNotAllowed_Invalid()
        //{
        //    var service = Subject();//new BookingService(null, null);
        //    _roomsRepo.Setup(r => r.GetRoom(1)).Returns(new Room() { ArePetsAllowed = false });
        //    var isValid = service.IsBookingValid(1, new Booking() { HasPets = true });

        //    Assert.False(isValid);
        //}
        ////from test_snippit.txt - if guest has pet and room disallow or allow pets, guest no pet and room disallows or allows pets
        //[Fact]
        //public void IsBookingValid_PetsAllowed_IsValid()
        //{
        //    var service = Subject();
        //    _roomsRepo.Setup(r => r.GetRoom(1)).Returns(new Room { ArePetsAllowed = true });

        //    var isValid = service.IsBookingValid(1, new Booking { HasPets = true });

        //    Assert.True(isValid);
        //}

        //[Fact]
        //public void IsBookingValid_NoPetsAllowed_IsValid()
        //{
        //    var service = Subject();
        //    _roomsRepo.Setup(r => r.GetRoom(1)).Returns(new Room { ArePetsAllowed = true });

        //    var isValid = service.IsBookingValid(1, new Booking { HasPets = false });

        //    Assert.True(isValid);
        //}

        //[Fact]
        //public void IsBookingValid_NoPetsNotAllowed_IsValid()
        //{
        //    var service = Subject();
        //    _roomsRepo.Setup(r => r.GetRoom(1)).Returns(new Room { ArePetsAllowed = false });

        //    var isValid = service.IsBookingValid(1, new Booking { HasPets = false });

        //    Assert.True(isValid);
        //}

        //refactoring - stop sthe duplication of the pets testing
        [Theory]
        //give data - will map to parameter
        [InlineData(false, true, false)]//for all pet has or no has and rooms allow or disallow
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        public void IsBookingValid_Pets(bool areAllowed, bool hasPets, bool result)
        {
            var service = Subject();
            _roomsRepo.Setup(r => r.GetRoom(1)).Returns(new Room { ArePetsAllowed = areAllowed });

            var isValid = service.IsBookingValid(1, new Booking { HasPets = hasPets });

            Assert.Equal(isValid, result);
        }

        [Fact]
        public void IsBookingValid_GuestsLessThanCapacity_Valid()
        {
            var service = Subject();
            _roomsRepo.Setup(r => r.GetRoom(1)).Returns(new Room { Capacity = 4 });

            var isValid = service.IsBookingValid(1, new Booking { NumberOfGuests = 1 });
            //Assert.False(isValid); to get broken test
            Assert.True(isValid);
        }

        [Fact]
        public void IsBookingValid_GuestsGreaterThanCapacity_InValid()
        {
            var service = Subject();
            _roomsRepo.Setup(r => r.GetRoom(1)).Returns(new Room { Capacity = 4 });

            var isValid = service.IsBookingValid(1, new Booking { NumberOfGuests = 15});
            //Assert.False(isValid); to get red test
            Assert.False(isValid);
        }
    }
}
