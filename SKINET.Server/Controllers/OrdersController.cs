using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKINET.Server.DTOS;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using SKINET.Server.Entities.order;
using SKINET.Server.Entities.Specifictions;
using SKINET.Server.Extentions;
using SKINET.Server.NewFolder;
using System.Text.Json;

namespace SKINET.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ICartService cartService,IUnitOfWork unit) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDTO createOrder)
        {
            var email =User.GetEmail();
            var cart=await cartService.GetCart(createOrder.CartId);
            if (cart == null) return BadRequest("Cart not found");

            if (cart.PaymentmethodId == null) return BadRequest("No Payment intent");

            var items = new List<OrderItem>();

            foreach (var item in cart.Items)
            {
                var productItem=await unit.Repository<Product>().GetByID(item.ProductId);

                if (productItem == null) return BadRequest("wha");

                var OrderItem = new ProductItem
                {
                    PictureUrl = item.PictureUrl,
                    ProductName=item.PropductName,
                    ProductId=item.ProductId,
                };
                var odrerItem = new OrderItem
                {
                    Item = OrderItem,
                    Price = productItem.Price,
                    Quantity = item.Quantity
                };
                items.Add(odrerItem);
            }
            var delivery=await unit.Repository<Deliverymethod>().GetByID(createOrder.DeliveryMethodId);
            if (delivery == null) return BadRequest("No delivery method");

            var order = new Order
            {
                orderItems=items,
                Deliverymethod=delivery,
                shippingAddress=createOrder.address,
                subtotal=items.Sum(x=>x.Price*x.Quantity),
                Payment=createOrder.paymentSummary,
                PaymentId=cart.PaymentmethodId,
                BuyerEmail = email
            };
            unit.Repository<Order>().Add(order);    

            if(await unit.Complete())
            {
                return order;
            }
            else
            {
                return BadRequest("Problema");
            }
        }


        [HttpGet]
            public async Task<ActionResult<IReadOnlyList<OrderDTO>>> getOrdersForUser()
        {
            var spec = new OrderSpecificaion(User.GetEmail());

            var orders=await unit.Repository<Order>().ListAsync(spec);

            if (orders == null) return NotFound();

            var MyordersDto = orders.Select(O => O.ToDto()).ToList();

            return Ok(MyordersDto);
        }


        [HttpGet("{id:int}")]
         public async Task<ActionResult<OrderDTO>> GetorderById(int id)
      
        {
            var spec= new OrderSpecificaion(User.GetEmail(),id);

            var order=await unit.Repository<Order>().GetEntityWithSpec(spec);


            if (order == null) return NotFound();
            return Ok(order.ToDto());   

        }



    }
}
