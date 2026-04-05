using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace SKINET.Server.Infrastracture.NewFolder
{
    public class CartService(IConnectionMultiplexer redis) : ICartService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        public async Task<bool> DeletCart(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<ShopCart?> GetCart(string key)
        {
            var data = await _database.StringGetAsync(key);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShopCart>(data!);
        }

        public async Task<ShopCart?> SetCart(ShopCart cart)
        {
            var created = await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetCart(cart.Id);

        }
    }
}
