using ef_core_example.Logic;
using ef_core_example.Models;
using GoldMarketplace.ServerAPIService.Repositories;
using Moq;
using Moq.AutoMock;

namespace ef_core_example.Tests
{
    public class ProfileLogicTests
    {
        private readonly AutoMocker _mock;
        public ProfileLogicTests()
        {
            _mock = new AutoMocker();
        }

        [Fact(DisplayName = "[ProfileLogic] - Profile finds a profile")]
        public async Task GetProfileFindsProfile()
        {
            //Arrange
            var mockProfile = _mock.GetMock<IProfileRepository>();
            var mockOrder = _mock.GetMock<IOrderRepository>();
            // var mockContext = _mock.Get<AppDbContext>();

            mockProfile.Setup(repo => repo.Get(It.IsAny<Guid>()))
                        .ReturnsAsync(Profile.Create("valid").Value);

            var logic = _mock.CreateInstance<ProfileLogic>();

            //Act
            var result = await logic.GetProfile(new Guid());

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "[ProfileLogic] - Profile finds nothing and errors")]
        public async Task GetProfileNotFoundReturnsError()
        {
            //Arrange
            var mockProfile = _mock.GetMock<IProfileRepository>();
            var mockOrder = _mock.GetMock<IOrderRepository>();
            // var mockContext = _mock.Get<AppDbContext>();

            mockProfile.Setup(repo => repo.Get(It.IsAny<Guid>()))
                        .ReturnsAsync(default(Profile));

            var logic = _mock.CreateInstance<ProfileLogic>();

            //Act
            var result = await logic.GetProfile(new Guid());

            //Assert
            Assert.Equal(Errors.General.Record_Not_Found, result.Error.Code);
        }
    }
}