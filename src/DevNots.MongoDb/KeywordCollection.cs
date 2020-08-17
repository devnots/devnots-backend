using DevNots.Domain;
using DevNots.Domain.Keyword;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DevNots.MongoDb
{
    public class KeywordCollection:IDbCollection<Keyword>
    {
        private IMongoCollection<Keyword> collection;

        public KeywordCollection(IMongoCollection<Keyword> collection)
        {
            this.collection = collection;
        }

        public async Task<Keyword> GetByIdAsync(string id)
        {
            var _keyword = await collection.FindAsync(filter => filter.Id == id).ConfigureAwait(false);
            return _keyword.SingleOrDefault();
        }

        public async Task<IEnumerable<Keyword>> PaginateAsync(int page, int pageSize)
        {
            var keywords = collection.Aggregate()
                .Match(FilterDefinition<Keyword>.Empty)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize);

            return await keywords.ToListAsync();
        }

        public async Task<string> CreateAsync(Keyword aggregate)
        {
            await collection.InsertOneAsync(aggregate).ConfigureAwait(false);
            return aggregate.Id;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var isValidId = ObjectId.TryParse(id, out _);

            if (!isValidId)
                return false;

            var result = await collection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> UpdateAsync(string id, Keyword aggregate)
        {
            var _keyword = await collection.ReplaceOneAsync(x => x.Id == id, aggregate);
            return _keyword.IsModifiedCountAvailable;
        }

        public async Task<IEnumerable<Keyword>> FindAsync(Expression<Func<Keyword, bool>> predicate)
        {
            var keywords = await collection.FindAsync(predicate).ConfigureAwait(false);
            return keywords.ToList();
        }

        public async Task<Keyword> FindOneAsync(Expression<Func<Keyword, bool>> predicate)
        {
            var keyword = await collection.FindAsync(predicate).ConfigureAwait(false);
            return keyword.SingleOrDefault();
        }
    }
}
