using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Portfolios.FindOne
{
    public record FindOneByNameQuery : IRequest<Portfolio>
    {
        public string Name { get; set; }
    }

    public class FindOneByNameHandler(
        IPortfolioRepository repository,
        ILogger<FindOneByIdQuery> logger): IRequestHandler<FindOneByNameQuery, Portfolio>
    {
        private readonly IPortfolioRepository repository = repository;
        private readonly ILogger<FindOneByIdQuery> logger = logger;

        public Task<Portfolio> Handle(FindOneByNameQuery query, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("call Portfolio FindOneQuery by Name");

            if (string.IsNullOrEmpty(query.Name))
                throw new ArgumentException("Name is required");

            return Task.FromResult(this.repository.FindByName(query.Name));
        }
    }
}
