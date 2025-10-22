namespace SKINET.Server.Entities.Interfaces
{
    public interface IGenericRepository<T> where T :  BaseEntity
    {
        
        Task<T> GetProductByID(int id);

        Task<IReadOnlyList<T>> ListAllsync();
        Task<T> GetEntityPattern(ISpecification<T> SPEc);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> SPEc);
        Task<Tresult> GetEntityPattern<Tresult>(ISpecification<T, Tresult> SPEc);
        Task<IReadOnlyList<Tresult>> ListAsync<Tresult>(ISpecification<T, Tresult> SPEc);

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<bool> Saveall();
        bool exists(int  id);
        Task<int> count(ISpecification<T> SPEc);
    }

}
