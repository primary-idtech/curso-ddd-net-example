using CleanArchitecture.Application.Portfolios.FindOne;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Portfolios.Create
{
    public record CreateCommand : IRequest<Portfolio>
    {
        public string Name { get; set; }
    }

    public class CreateHandler(
        IMediator mediator,
        IPortfolioRepository repository,
        ILogger<CreateCommand> logger) : IRequestHandler<CreateCommand, Portfolio>
    {
        private readonly IMediator mediator = mediator;
        private readonly IPortfolioRepository repository = repository;
        private readonly ILogger<CreateCommand> logger = logger;

        public async Task<Portfolio> Handle(CreateCommand cmd, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("call Portfolio CreateCommand");

            var portfolio = new Portfolio(cmd.Name);
            
            var queryName = new FindOneByNameQuery { Name = cmd.Name};
            var portfolioExists = await this.mediator.Send(queryName, cancellationToken);
            if (portfolioExists != null)
                throw new UseCaseException("This Portfolio exists.");

            this.repository.Add(portfolio);

            return portfolio;
        }
    }
}
