namespace SKINET.Server.Entities.Specifictions
{
    public class ListofTypeSpecification : BaseSpecification<Product, string>
    {
        public ListofTypeSpecification() {
            AddSelect(x => x.Type);
            ApplyDitinct();
        }
    
    }
}
