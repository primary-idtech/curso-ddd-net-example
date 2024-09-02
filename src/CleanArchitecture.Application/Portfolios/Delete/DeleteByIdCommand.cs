using CleanArchitecture.Application.Portfolios.FindOne;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Portfolios.Delete
{
    public record DeleteByIdCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteByNameHandler(
        IMediator mediator,
        IPortfolioRepository repository,
        ILogger<DeleteByIdCommand> logger) : IRequestHandler<DeleteByIdCommand>
    {
        private readonly IMediator mediator = mediator;
        private readonly IPortfolioRepository repository = repository;
        private readonly ILogger<DeleteByIdCommand> logger = logger;

        public async Task Handle(DeleteByIdCommand cmd, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("call Portfolio DeleteByIdCommand");

            if (cmd.Id <= 0)
                throw new ArgumentException("Id is required");

            var query = new FindOneByIdQuery { Id = cmd.Id};
            var portfolio = await this.mediator.Send(query, cancellationToken);
            if (portfolio == null)
                throw new UseCaseException("This Portfolio not exists.");

            this.repository.Remove(portfolio);
        }
    }
}
