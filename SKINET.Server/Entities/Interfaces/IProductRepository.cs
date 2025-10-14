namespace SKINET.Server.Entities.Interfaces
{
    public interface IProductRepository
    {

        Task<IReadOnlyList<Product>> GetProducts(string? brand,string?type,string? sort);

        Task<IReadOnlyList<string>> GetBrands();

        Task<IReadOnlyList<string>> GetTyps();

        Task<Product> GetProductById(int id);
        void addProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

        bool ProductExists(int id);
        Task<bool> saveChanges();  

    }
}
 