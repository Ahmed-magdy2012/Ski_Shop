namespace SKINET.Server.Entities.Specifictions
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(string? brand,string? type,string? sort):base(x =>

            (string.IsNullOrWhiteSpace(brand)|| x.Brand == brand)&&
            (string.IsNullOrWhiteSpace(type)|| x.Type == type)
            )
        {

            switch(sort){
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
