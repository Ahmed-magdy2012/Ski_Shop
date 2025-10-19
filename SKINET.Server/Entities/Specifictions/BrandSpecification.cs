namespace SKINET.Server.Entities.Specifictions
{
    public class BrandSpecification : BaseSpecification<Product, string>
    {
        public BrandSpecification()
        {
            AddSelect(x => x.Brand);
            ApplyDitinct();
        }
    }
    
}
