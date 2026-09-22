using SKINET.Server.Entities.Interfaces;
using System.Linq.Expressions;

namespace SKINET.Server.Entities.Specifictions
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification() : this(null) { }

        private readonly Expression<Func<T, bool>>? _criteria;

        protected BaseSpecification(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }


        public Expression<Func<T, bool>>? Criteria { get; private set; }

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public bool? IsDistinct  {get;private set;}

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IspagingEnabeld { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; } = [];


        public IQueryable<T> ApplyDATA(IQueryable<T> query)
        {
            if(Criteria != null)
            {
                query= query.Where(Criteria);
            }
            return query;
        }

        protected void AddInclude(Expression<Func<T, object>> expression)
        {
            Includes.Add(expression);
        }

        
        protected void AddorderBy(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;
        }
        protected void AddorderByDescinding(Expression<Func<T, object>> OrderByDescending)
        {
            this.OrderByDescending = OrderByDescending;
        }

        protected void ApplyDitinct()
        {
            IsDistinct=true;
        }
        protected void ApplyPaging(int skip,int take)
        {
            Skip = skip;
            Take=take;
            IspagingEnabeld = true;   
        }


    }
    public class BaseSpecification<T, Tresult> : BaseSpecification<T>, ISpecification<T, Tresult>
    {
        protected BaseSpecification() { }
 
        public Expression<Func<T, Tresult>>? Select { get; private set; }

        protected void AddSelect(Expression<Func<T, Tresult>> criteria)
        {
            Select = criteria;
        }

    
    }

}
