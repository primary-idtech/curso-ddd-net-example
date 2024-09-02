using CleanArchitecture.Application;
using CleanArchitecture.Application.Portfolios.Create;
using CleanArchitecture.Application.Portfolios.FindOne;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Models;
using FakeItEasy;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application.Portfolios
{
    public class CreateTests
    {
        private const string PORTFOLIO_NAME = "PortfolioName";

        [Fact]
        public async Task CreateCommand_PortfolioExists_ReturnError()
        {
            //Arrange
            var exceptionMsg = $"This Portfolio exists.";
            var cmd = new CreateCommand() { Name = PORTFOLIO_NAME };
            var stubPortfolioRepo = A.Fake<IPortfolioRepository>();
            var stubMediator = A.Fake<IMediator>();
            var stubLogger = A.Fake<ILogger<CreateCommand>>();

            A.CallTo(() => stubMediator.Send(A<FindOneByNameQuery>._, default)).Returns(Task.FromResult<Portfolio>(new(PORTFOLIO_NAME)));

            var handler = new CreateHandler(stubMediator, stubPortfolioRepo, stubLogger);

            //Act
            var exception = await Assert.ThrowsAsync<UseCaseException>(() => handler.Handle(cmd, CancellationToken.None));

            //Assert
            Assert.Equal(exceptionMsg, exception.Message[..exceptionMsg.Length]);
        }

        [Fact]
        public async Task CreateCommand_PortfolioNotExists_ReturnOk()
        {
            //Arrange
            var cmd = new CreateCommand() { Name = PORTFOLIO_NAME };
            var stubPortfolioRepo = A.Fake<IPortfolioRepository>();
            var stubMediator = A.Fake<IMediator>();
            var stubLogger = A.Fake<ILogger<CreateCommand>>();

            A.CallTo(() => stubMediator.Send(A<FindOneByNameQuery>._, default)).Returns(Task.FromResult<Portfolio>(null));
            A.CallTo(() => stubPortfolioRepo.Add(A<Portfolio>._)).DoesNothing();

            var handler = new CreateHandler(stubMediator, stubPortfolioRepo, stubLogger);

            //Act
            var result = await handler.Handle(cmd, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("#&%&$")]
        public async Task CreateCommand_InvalidArguments_ReturnError(string name)
        {
            //Arrange
            var cmd = new CreateCommand() { Name = name};
            var stubPortfolioRepo = A.Fake<IPortfolioRepository>();
            var stubMediator = A.Fake<IMediator>();
            var stubLogger = A.Fake<ILogger<CreateCommand>>();

            var handler = new CreateHandler(stubMediator, stubPortfolioRepo, stubLogger);

            //Act
            var exception = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(cmd, CancellationToken.None));

            //Assert
            Assert.NotNull(exception);
        }
    }
}
