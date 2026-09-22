using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;

namespace SKINET.Server.Infrastracture.Data
{
    public class Specification<T>where T : BaseEntity
    {
        public static IQueryable<T> Get(IQueryable <T> query,ISpecification<T> specification)        
        {
            if (specification.Criteria != null)
            {
                query=query.Where(specification.Criteria);
            }
            if (specification.OrderBy != null)
            {
                query=query.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDescending!=null)
            {
            query=query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.IspagingEnabeld)
            {
                query=query.Skip(specification.Skip).Take(specification.Take);
            }
            query=specification.Includes.Aggregate(query,(current,include)=>current.Include(include));

            return query;
        }
        public static IQueryable<Tresult> Get<Tresult>(IQueryable<T> query, ISpecification<T,Tresult> specification)
        {
           
            var select=query as IQueryable<Tresult>;
            if (specification.Select != null)
            {
                select = query.Select(specification.Select);
            }
            if (specification.IsDistinct==true)
            {
               select= select?.Distinct();
            }
            if (specification.IspagingEnabeld)
            {
                select = select?.Skip(specification.Skip).Take(specification.Take);
            }

            return select??query.Cast<Tresult>() ;
        }
    }
}
