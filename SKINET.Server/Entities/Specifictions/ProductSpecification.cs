namespace SKINET.Server.Entities.Specifictions
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification( ProductParams param):base(x =>
            (string.IsNullOrEmpty(param.Search )||x.Name.ToLower().Contains(param.Search))&&
            (param.Brand.Count==0||param.Brand.Contains(x.Brand))&&
            (param.Types.Count == 0 || param.Types.Contains(x.Type))  
            )
        {
            ApplyPaging(param.Pagesize * (param.Pageindex - 1), param.Pagesize);

            switch(param.Sort){
                case "PriceAsc":
                    AddorderBy(x => x.Price);
                       break;
                    case "PriceDesc":
                    AddorderByDescinding(x => x.Price);
                    break;
                default:
                    AddorderBy(x => x.Name);
                    break;
                    
            }
        }
    }
}
