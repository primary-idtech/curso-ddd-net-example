using CleanArchitecture.Application.Portfolios.FindOne;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Portfolios.Update
{
    public record UpdateCommand : IRequest<Portfolio>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }

    public class UpdateHandler(
        IMediator mediator,
        IPortfolioRepository repository,
        ILogger<UpdateCommand> logger) : IRequestHandler<UpdateCommand, Portfolio>
    {
        private readonly IMediator mediator = mediator;
        private readonly IPortfolioRepository repository = repository;
        private readonly ILogger<UpdateCommand> logger = logger;

        public async Task<Portfolio> Handle(UpdateCommand cmd, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("call Portfolio UpdateCommand");

            if (cmd.Id <= 0)
                throw new ArgumentException("Id is required");

            var query = new FindOneByIdQuery { Id = cmd.Id };
            var portfolio = await this.mediator.Send(query, cancellationToken);
            if (portfolio == null)
                throw new UseCaseException("This Portfolio not exists.");

            if (!string.IsNullOrEmpty(cmd.Name))
            {
                var queryName = new FindOneByNameQuery { Name = cmd.Name };
                var portfolioExists = await this.mediator.Send(queryName, cancellationToken);
                if (portfolioExists != null && portfolioExists.Id != cmd.Id)
                    throw new UseCaseException("The Portfolio rename not working because already exists another portfolio with this name.");
            } else {
                cmd.Name = portfolio.Name.Value;
            }

            portfolio.Update(cmd.Name, cmd.Enabled);
            this.repository.Update(portfolio);

            return portfolio;
        }
    }
}
