using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;

namespace SKINET.Server.Infrastracture.Data
{
    public class Specification<T>where T : BaseEntity
    {
        public static IQueryable<T> Get(IQueryable <T> query,ISpecification<T> specification)        
        {
            if (specification.WhereBrandAndType != null)
            {
                query=query.Where(specification.WhereBrandAndType);
            }
            if (specification.OrderBy != null)
            {
                query=query.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDescending!=null)
            {
            query=query.OrderByDescending(specification.OrderByDescending);
            }
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

            return select??query.Cast<Tresult>() ;
        }
    }
}
