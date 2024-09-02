using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Infrastructure.ORM.Repositories
{
    public class PortfolioRepository(DbContext context) : IPortfolioRepository
    {
        protected readonly DbContext _context = context;

        Portfolio? IPortfolioRepository.FindById(object id)
        {
            return _context.Set<Portfolio>().Find(id);
        }

        Portfolio? IPortfolioRepository.FindByName(string name)
        {
            return _context.Set<Portfolio>().FirstOrDefault(x => x.Name.Value == name);
        }

        public void Add(Portfolio entity)
        {
            _context.Set<Portfolio>().Add(entity);
            this.SaveChanges();
        }

        public void Remove(Portfolio entity)
        {
            _context.Set<Portfolio>().Remove(entity);
            this.SaveChanges();
        }

        public void Update(Portfolio entity)
        {
            _context.Set<Portfolio>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public int Count(bool enabled)
        {
            return _context.Set<Portfolio>()
                .Where(x => x.Enabled == enabled)
                .Count();
        }

        public IEnumerable<Portfolio> Page(string sort, int offset, int limit, bool enabled)
        {
            IQueryable<Portfolio> query = _context.Set<Portfolio>().Where(x => x.Enabled == enabled);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                var sortParts = sort.Split(',');
                var sortField = sortParts[0];
                var sortOrder = sortParts.Length > 1 ? sortParts[1] : "asc";

                switch (sortField.ToLower())
                {
                    case "id":
                        query = sortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase)
                            ? query.OrderByDescending(x => x.Id)
                            : query.OrderBy(x => x.Id);
                        break;
                    case "name":
                        query = sortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase)
                            ? query.OrderByDescending(x => x.Name.Value)
                            : query.OrderBy(x => x.Name.Value);
                        break;
                    default:
                        query = query.OrderByDescending(x => x.Id);
                        break;
                }
            }

            return query.Skip(offset).Take(limit).ToList();
        }

        private void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new TechnicalException("Portfolio save changes error" , ex);
            }
        }
    }
}
