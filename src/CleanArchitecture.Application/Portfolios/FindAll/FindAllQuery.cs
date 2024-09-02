using CleanArchitecture.Application.Shared.DTOs;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Portfolios.FindAll
{
    public class FindAllQuery : PageQueryDto, IRequest<PageDto<Portfolio>>
    {
        public bool Enabled { get; set; }
    }

    public class FindAllHandler(
        IPortfolioRepository repository,
        ILogger<FindAllQuery> logger) : IRequestHandler<FindAllQuery, PageDto<Portfolio>>
    {
        private readonly IPortfolioRepository repository = repository;
        private readonly ILogger<FindAllQuery> logger = logger;

        public Task<PageDto<Portfolio>> Handle(FindAllQuery query, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("call Portfolio FindAllQuery");

            var result = new PageDto<Portfolio>
            {
                Total = this.repository.Count(query.Enabled),
                Limit = query.Limit,
                Offset = query.Offset,
                Items = this.repository.Page(query.Sort, query.Offset, query.Limit, query.Enabled)
            };

            return Task.FromResult(result);
        }
    }
}
