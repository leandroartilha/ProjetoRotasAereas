using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Domain.RepositoriesInterfaces
{
    public interface IRotaRepository
    {
        public Task<Rota> GetAsync(ObjectId id, CancellationToken token);
        public Task<Rota> GetWithFilterAsync(Expression<Func<Rota, bool>> predicate, CancellationToken token);
        public Task<IEnumerable<Rota>> GetAllAsync(CancellationToken token);
        public Task CreateAsync(Rota entity, CancellationToken token);
        public Task UpdateAsync(UpdateDefinition<Rota> update, CancellationToken token);
    }
}
