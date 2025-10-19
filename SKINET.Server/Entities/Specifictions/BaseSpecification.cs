using SKINET.Server.Entities.Interfaces;
using System.Linq.Expressions;

namespace SKINET.Server.Entities.Specifictions
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? ctor) : ISpecification<T>
    {
        protected BaseSpecification():this(null) { }
        public Expression<Func<T, bool>>? WhereBrandAndType => ctor;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public bool? IsDistinct  {get;private set;}

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public int IspagingEnabeld { get; private set; }

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


    }
    public class BaseSpecification<T, Tresult> : BaseSpecification<T>, ISpecification<T, Tresult>
    {
        protected BaseSpecification() { }
 
        public Expression<Func<T, Tresult>>? Select { get; private set; }

        protected void AddSelect(Expression<Func<T, Tresult>> ctor)
        {
            Select = ctor;
        }

    
    }

}
