using System.Linq.Expressions;

namespace SKINET.Server.Entities.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        List<Expression<Func<T, object>>> Includes {  get; }
        Expression<Func<T, object>>? OrderByDescending { get; }
        IQueryable<T> ApplyDATA(IQueryable<T> query);
        bool? IsDistinct { get; }
        int Take { get; }
        int Skip {  get; }
        bool IspagingEnabeld { get; } 

    }
    public interface ISpecification<T, Tresult> : ISpecification<T>
    {
        Expression<Func<T, Tresult>>? Select { get; }
    }
}