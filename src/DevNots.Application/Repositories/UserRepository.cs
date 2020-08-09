using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevNots.Domain;

namespace DevNots.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext dbContext;
        public UserRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<string> CreateAsync(User aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindOneAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> PaginateAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, User aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
