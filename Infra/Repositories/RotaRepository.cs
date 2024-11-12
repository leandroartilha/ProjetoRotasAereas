using Domain.Entities;
using Domain.RepositoriesInterfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infra.Repositories
{
    public class RotaRepository : IRotaRepository
    {
        private readonly IMongoDatabase _db;

        public RotaRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public virtual IMongoCollection<Rota> GetCollection()
        {
            return _db.GetCollection<Rota>("Rota");
        }

        public async Task CreateAsync(Rota entity, CancellationToken token)
        {
            await GetCollection().InsertOneAsync(entity, cancellationToken: token);
        }

        public virtual async Task<IEnumerable<Rota>> GetAllAsync(CancellationToken token)
        {
            var filter = Builders<Rota>.Filter.Empty;

            return await GetCollection().Find(filter).ToListAsync(token);
        }

        public virtual async Task<Rota> GetAsync(ObjectId id, CancellationToken token)
        {
            var filter = Builders<Rota>.Filter.Eq(x => x._id, id);

            return await GetCollection().Find(filter).FirstOrDefaultAsync(token);
        }

        public virtual async Task<Rota> GetWithFilterAsync(Expression<Func<Rota, bool>> predicate, CancellationToken token)
        {
            FilterDefinition<Rota> filter = predicate;

            return await GetCollection().Find(filter).FirstOrDefaultAsync(token);
        }

        public virtual Task UpdateAsync(UpdateDefinition<Rota> update, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
