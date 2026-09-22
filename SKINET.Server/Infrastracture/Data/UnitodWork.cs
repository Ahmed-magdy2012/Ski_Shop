using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using System.Collections.Concurrent;

namespace SKINET.Server.Infrastracture.Data
{
    public class UnitodWork(StoreContext context) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string, object> _repo = new();
        public async Task<bool> Complete()
        {
         return await context.SaveChangesAsync()>0;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
          var type= typeof(TEntity).Name;
            return (IGenericRepository<TEntity>)_repo.GetOrAdd(type, t =>
            {
                var repositoryType = typeof(GenericRepo<>).MakeGenericType(typeof(TEntity));
                return Activator.CreateInstance(repositoryType, context) ?? throw new InvalidOperationException();
            });
         

        }
    }
}
