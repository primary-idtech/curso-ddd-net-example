using CleanArchitecture.Domain.Models;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Abstractions
{
    public interface IPortfolioRepository
    {
        Portfolio? FindById(object id);
        Portfolio? FindByName(string name);
        void Add(Portfolio entity);
        void Remove(Portfolio entity);
        void Update(Portfolio entity);
        int Count(bool enabled);
        IEnumerable<Portfolio> Page(string sort, int offset, int limit, bool enabled);
    }
}
