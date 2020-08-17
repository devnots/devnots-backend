using DevNots.Domain;
using DevNots.Domain.Keyword;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevNots.Application.Repositories
{
    public class KeywordRepository:IKeywordRepository
    {
        private readonly DbContext context;

        public KeywordRepository(DbContext context)
        {
            this.context = context;
        }

        public Task<Keyword> GetByIdAsync(string id)
        {
            return context.Keywords.GetByIdAsync(id);
        }

        public Task<IEnumerable<Keyword>> PaginateAsync(int page, int pageSize)
        {
            return context.Keywords.PaginateAsync(page, pageSize);
        }

        public Task<string> CreateAsync(Keyword aggregate)
        {
            return context.Keywords.CreateAsync(aggregate);
        }

        public Task<bool> RemoveAsync(string id)
        {
            return context.Keywords.RemoveAsync(id);
        }

        public Task<bool> UpdateAsync(string id, Keyword aggregate)
        {
            return context.Keywords.UpdateAsync(id, aggregate);
        }

        public Task<IEnumerable<Keyword>> FindAsync(Expression<Func<Keyword, bool>> predicate)
        {
            return context.Keywords.FindAsync(predicate);
        }

        public Task<Keyword> FindOneAsync(Expression<Func<Keyword, bool>> predicate)
        {
            return context.Keywords.FindOneAsync(predicate);
        }
    }
}
