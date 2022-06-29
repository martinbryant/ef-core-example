using ef_core_example.Logic;
using ef_core_example.Models;
using GoldMarketplace.ServerAPIService.Repositories;
using MockQueryable.Moq;
using Moq;
using Moq.AutoMock;

namespace ef_core_example.Tests
{
    public class DepotLogicTests
    {
        private readonly AutoMocker _mock;

        public DepotLogicTests()
        {
            _mock = new AutoMocker();
        }
        
        [Fact(DisplayName = "[DepotLogic] - Create depot was successful")]
        public async Task CreateSuccess()
        {
            //Arrange
            var mockProfile = _mock.GetMock<IProfileLogic>();
            var mockUnit    = _mock.GetMock<IUnitOfWork>();

            mockProfile.Setup(profile => profile.GetProfile(It.IsAny<string>()))
                        .ReturnsAsync(Profile.Create("valid"));

            mockUnit.Setup(unit => unit.Commit());
            mockUnit.Setup(unit => unit.Depots.Add(It.IsAny<Depot>()));
            
            var existingDepotDto = ValidDepotDto;
            existingDepotDto.DepotId = "B";
            
            var existingDepot = Depot.Create(existingDepotDto, Profile.Create("Valid").Value).Value;

            var depots = new List<Depot>()
            {
                existingDepot
            };

            var mockDepotQueryable = depots.BuildMock();

            mockUnit.Setup(unit => unit.Depots.AsQueryable())
                    .Returns(mockDepotQueryable);
            
            var logic = _mock.CreateInstance<DepotLogic>();

            var depotDto = ValidDepotDto;

            //Act
            var result = await logic.CreateDepot(depotDto);

            //Assert
            Assert.True(result.IsSuccess);
            mockProfile.Verify(profile => profile.GetProfile(It.IsAny<string>()),
                Times.Once);
            mockUnit.Verify(unit => unit.Depots.AsQueryable(), 
                Times.Once);
            mockUnit.Verify(unit => unit.Depots.Add(It.IsAny<Depot>()), 
                Times.Once);
            mockUnit.Verify(unit => unit.Commit(), 
                Times.Once);

        }

        [Fact(DisplayName = "[DepotLogic] - Create depot fails as already exists")]
        public async Task CreateDepotFailsAsAlreadyExists()
        {
            //Arrange
            var mockProfile = _mock.GetMock<IProfileLogic>();
            var mockUnit    = _mock.GetMock<IUnitOfWork>();

            mockProfile.Setup(profile => profile.GetProfile(It.IsAny<string>()))
                        .ReturnsAsync(Profile.Create("valid"));

            mockUnit.Setup(unit => unit.Commit());
            mockUnit.Setup(unit => unit.Depots.Add(It.IsAny<Depot>()));
            
            var existingDepotDto = ValidDepotDto;
            
            var existingDepot = Depot.Create(existingDepotDto, Profile.Create("Valid").Value).Value;

            var depots = new List<Depot>()
            {
                existingDepot
            };

            var mockDepotQueryable = depots.BuildMock();

            mockUnit.Setup(unit => unit.Depots.AsQueryable())
                    .Returns(mockDepotQueryable);
            
            var logic = _mock.CreateInstance<DepotLogic>();

            var depotDto = ValidDepotDto;

            //Act
            var result = await logic.CreateDepot(depotDto);

            //Assert
            Assert.Equal(Errors.Depot.Depot_Already_Exists, result.Error.Code);
            mockProfile.Verify(profile => profile.GetProfile(It.IsAny<string>()),
                Times.Once);
            mockUnit.Verify(unit => unit.Depots.AsQueryable(), 
                Times.Once);
            mockUnit.Verify(unit => unit.Depots.Add(It.IsAny<Depot>()), 
                Times.Never);
            mockUnit.Verify(unit => unit.Commit(), 
                Times.Never);

        }

        [Fact(DisplayName = "[DepotLogic] - Create depot profile not found")]
        public async Task CreateDepotProfileNotFound()
        {
            //Arrange
            var mockProfile = _mock.GetMock<IProfileLogic>();
            var mockUnit    = _mock.GetMock<IUnitOfWork>();

            mockProfile.Setup(profile => profile.GetProfile(It.IsAny<string>()))
                        .ReturnsAsync(Errors.General.NotFound("Profile", Guid.NewGuid()));

            mockUnit.Setup(unit => unit.Commit());
            mockUnit.Setup(unit => unit.Depots.Add(It.IsAny<Depot>()));
            
            var logic = _mock.CreateInstance<DepotLogic>();

            var depotDto = ValidDepotDto;

            //Act
            var result = await logic.CreateDepot(depotDto);

            //Assert
            Assert.Equal(Errors.General.Record_Not_Found, result.Error.Code);
            mockProfile.Verify(profile => profile.GetProfile(It.IsAny<string>()),
                Times.Once);
            mockUnit.Verify(unit => unit.Depots.AsQueryable(), 
                Times.Never);
            mockUnit.Verify(unit => unit.Depots.Add(It.IsAny<Depot>()), 
                Times.Never);
            mockUnit.Verify(unit => unit.Commit(), 
                Times.Never);

        }

        [Fact(DisplayName = "[DepotLogic] - Create depot invalid depot info")]
        public async Task CreateDepotInvalidDepotInfo()
        {
            //Arrange
            var mockProfile = _mock.GetMock<IProfileLogic>();
            var mockUnit    = _mock.GetMock<IUnitOfWork>();

            mockProfile.Setup(profile => profile.GetProfile(It.IsAny<string>()))
                        .ReturnsAsync(Profile.Create("valid"));

            mockUnit.Setup(unit => unit.Commit());
            mockUnit.Setup(unit => unit.Depots.Add(It.IsAny<Depot>()));
            
            var logic = _mock.CreateInstance<DepotLogic>();

            var depotDto = ValidDepotDto;
            depotDto.ContactEmail = null;

            //Act
            var result = await logic.CreateDepot(depotDto);

            //Assert
            Assert.Equal(Errors.General.Value_Is_Required, result.Error.Code);
            mockProfile.Verify(profile => profile.GetProfile(It.IsAny<string>()),
                Times.Once);
            mockUnit.Verify(unit => unit.Depots.AsQueryable(), 
                Times.Never);
            mockUnit.Verify(unit => unit.Depots.Add(It.IsAny<Depot>()), 
                Times.Never);
            mockUnit.Verify(unit => unit.Commit(), 
                Times.Never);

        }

        public AddressDto ValidAddress => 
            new AddressDto()
            {
                Address1 = "valid",
                Address2 = "valid",
                PostCode = "valid"
            };

        public DepotDto ValidDepotDto =>
            new DepotDto()
            {
                DisplayName = "Name",
                ContactName = "Name",
                DeliveryAddress = ValidAddress,
                BillingAddress = ValidAddress,
                ContactEmail = "mail@mail.net",
                DepotId = "A"
            };
    }
}