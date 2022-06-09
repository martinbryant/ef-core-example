using ef_core_example.Logic;
using GoldMarketplace.ServerAPIService.Repositories;
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
        
        public async Task CreateSuccess()
        {
            //Arrange
            _mock.GetMock<IUnitOfWork>()
                    .Setup(unit => unit.Commit());
            
            var logic = _mock.CreateInstance<DepotLogic>();
            
            // var result = await logic.CreateDepot();
        }
    }
}