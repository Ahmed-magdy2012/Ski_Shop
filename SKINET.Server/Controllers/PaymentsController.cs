using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using SKINET.Server.Entities.order;
using SKINET.Server.Entities.Specifictions;
using SKINET.Server.Extentions;
using SKINET.Server.NewFolder;
using Stripe;
using Order = SKINET.Server.Entities.order.Order;

namespace SKINET.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IPaymentService service,IUnitOfWork unit,ILogger<PaymentsController> logger,IConfiguration
         config,IHubContext<NotificationHub> hubContext) : ControllerBase
    {
        private readonly string _whSecret = config["StipreSettings:whSecret"]!;

        [Authorize]
        [HttpPost("{cardId}")]
        public async Task<ActionResult<ShopCart>> CreateOrUpdatePaymentAsync(string cardId)
        {
            var cart = await service.CreateorUodatePayment(cardId);
            if (cart == null) return BadRequest("problem with cart");
            return Ok(cart);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<Deliverymethod>>> GetDeliveryMethods()
        {
           
            return Ok(await unit.Repository<Deliverymethod>().ListAllsync());
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Stipewebhook()
        {
            Console.WriteLine("WEBHOOK HIT");

            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = ConstructStripe(json);

                if (stripeEvent.Data.Object is not PaymentIntent intent)
                {
                    return BadRequest("Invalid Data");
                }
                await HandelPayment(intent);
                return Ok();
            }
            catch(System.Exception)
            {
                throw;
            }
        }

        private async Task HandelPayment(PaymentIntent intent)
        {
            if (intent.Status == "succeeded")
            {
                var spec = new OrderSpecificaion(intent.Id, true);
                var order = await unit.Repository<Order>().GetEntityWithSpec(spec);
                if (order == null)
                {
                    Console.WriteLine("Order Not Found");
                    return;
                }
                if ((long)order.GetTotal() * 100 != intent.Amount)
                {
                    order.status = OrderStatus.PaymentMismatch;
                }
                else
                {

                    order.status = OrderStatus.PaymentReceived;
                }
                await unit.Complete();

                var id = NotificationHub.GetconnectionByemail(order.BuyerEmail);
                if (!string.IsNullOrEmpty(id))
                {
                    await hubContext.Clients.Client(id).SendAsync("OrderCompletNotification", order.ToDto());
                }
            }
          }

        private Event ConstructStripe(string json)
        {
            
            try
            {
                return EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);
                  
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "failed");
                throw new StripeException("ivalide");
            }
         }
    }
}
