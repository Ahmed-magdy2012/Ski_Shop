namespace SKINET.Server.Entities.Interfaces
{
    public interface IGenericRepository<T> where T :  BaseEntity
    {
        
        Task<T> GetByID(int id);

        Task<IReadOnlyList<T>> ListAllsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> SPEc);

        Task<IReadOnlyList<Tresult>> ListAsync<Tresult>(ISpecification<T, Tresult> SPEc);

        Task<T?> GetEntityWithSpec(ISpecification<T> SPEc);
        Task<Tresult?> GetEntityWithSpec<Tresult>(ISpecification<T, Tresult> SPEc);

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        bool exists(int  id);
        Task<int> count(ISpecification<T> SPEc);
    }

}
