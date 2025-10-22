using System.Linq.Expressions;

namespace SKINET.Server.Entities.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? WhereBrandAndType { get; }
        Expression<Func<T, object>>? OrderBy { get; }

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