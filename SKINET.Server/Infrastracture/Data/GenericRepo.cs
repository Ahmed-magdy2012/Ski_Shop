using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;

namespace SKINET.Server.Infrastracture.Data
{
    public class GenericRepo<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public bool exists(int id)
        {
            return context.Set<T>().Any(x => x.Id == id);
        }

        public async Task<T> GetEntityPattern(ISpecification<T> SPEc)
        {
            throw new NotImplementedException();
        }

        public Task<Tresult> GetEntityPattern<Tresult>(ISpecification<T, Tresult> SPEc)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetProductByID(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> SPEc)
        {
            return await Applyspecification(SPEc).ToListAsync();
        }

        public async Task<IReadOnlyList<Tresult>> ListAsync<Tresult>(ISpecification<T, Tresult> SPEc)
        {
            return await Applyspecification(SPEc).ToListAsync();
        }

        public async Task<bool> Saveall()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        private IQueryable<T> Applyspecification(ISpecification<T> spec)
        {
            return Specification<T>.Get(context.Set<T>().AsQueryable(), spec);
        }
        private IQueryable<TResultt> Applyspecification<TResultt>(ISpecification<T, TResultt> spec)
        {
            return Specification<T>.Get<TResultt>(context.Set<T>().AsQueryable(), spec);
        }
    }
}
