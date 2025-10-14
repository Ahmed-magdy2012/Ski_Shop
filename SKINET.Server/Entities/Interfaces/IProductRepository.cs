namespace SKINET.Server.Entities.Interfaces
{
    public interface IProductRepository
    {

        Task<IReadOnlyList<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        void addProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

        bool ProductExists(int id);
        Task<bool> saveChanges();  

    }
}
 