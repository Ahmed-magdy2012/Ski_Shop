using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using Stripe;
using Product = SKINET.Server.Entities.Product;

namespace SKINET.Server.Infrastracture.Services
{
    public class PaymentService(IConfiguration config,ICartService cartService, IGenericRepository<Product> Productrepo,IGenericRepository<Deliverymethod> dmRepo) : IPaymentService
    {
        public async Task<ShopCart> CreateorUodatePayment(string cardId)
        {
            StripeConfiguration.ApiKey = config["StipreSettings:Secretkey"];

            var cart = await cartService.GetCart(cardId);


            if (cart == null) return null;

            var shippingPrice = 0m;
            if (cart.DeliverymethodId.HasValue)
            {
                var deliveryMethod = await dmRepo.GetByID(cart.DeliverymethodId.Value);

                if (cart.DeliverymethodId.HasValue)
                {
                    if (deliveryMethod != null)
                        shippingPrice = deliveryMethod.price;
                }

            }
             foreach (var item in cart.Items)
            {
                var productItem = await Productrepo.GetByID(item.ProductId);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }



            var service = new PaymentIntentService();


            PaymentIntent? intent = null;
            if (string.IsNullOrEmpty(cart.PaymentmethodId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };
                intent = await service.CreateAsync(options);
                cart.PaymentmethodId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var existing = await service.GetAsync(cart.PaymentmethodId);

                if (existing.Status == "succeeded")
                {
                    return cart; 
                }
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100,
                   
                };
                intent = await service.UpdateAsync(cart.PaymentmethodId, options);
            }
            await cartService.SetCart(cart);
            
            return cart;
        }

    }
}
