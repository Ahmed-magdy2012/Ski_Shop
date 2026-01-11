namespace SKINET.Server.Entities.Interfaces
{
    public interface ICartService
    {

        Task<ShopCart?> GetCart(String key);
        Task<ShopCart?> SetCart(ShopCart cart);
        Task<bool> DeletCart(String key);
    }
}
