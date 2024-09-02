using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FindAll = CleanArchitecture.Application.Portfolios.FindAll;


namespace UnitTests.Application.Portfolios
{
    public class FindAllTests
    {
        const string NAME = "PortfolioName";
        const ushort LIMIT = 200;
        const ushort OFFSET = 0;

        [Fact]
        public async Task GetAll_Filter_ReturnEmpty()
        {
            //Arrange
            var query = CreateQuery(true);
            var stubPortfolioRepo = A.Fake<IPortfolioRepository>();
            var stubLogger = A.Fake<ILogger<FindAll.FindAllQuery>>();

            A.CallTo(() => stubPortfolioRepo.Count(A<bool>._)).Returns(0);
            A.CallTo(() => stubPortfolioRepo.Page(A<string>._, A<int>._, LIMIT, A<bool>._)).Returns([]);

            var handler = new FindAll.FindAllHandler(stubPortfolioRepo, stubLogger);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(0, result.Total);
        }

        [Fact]
        public async Task GetAll_Filter_ReturnOneResult()
        {
            //Arrange
            var query = CreateQuery(true);
            var portfolios = new List<Portfolio> { new(NAME) };
            var stubPortfolioRepo = A.Fake<IPortfolioRepository>();
            var stubLogger = A.Fake<ILogger<FindAll.FindAllQuery>>();

            A.CallTo(() => stubPortfolioRepo.Count(A<bool>._)).Returns(portfolios.Count);
            A.CallTo(() => stubPortfolioRepo.Page(A<string>._, A<int>._, LIMIT, A<bool>._)).Returns(portfolios);

            var handler = new FindAll.FindAllHandler(stubPortfolioRepo, stubLogger);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(1, result.Total);
        }

        private static FindAll.FindAllQuery CreateQuery(bool enabled)
        {
            return new  FindAll.FindAllQuery()
            {
                Enabled = enabled,
                Limit = LIMIT,
                Offset = OFFSET,
                Sort = string.Empty
            };
        }
    }
}
