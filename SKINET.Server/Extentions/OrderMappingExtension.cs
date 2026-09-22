using SKINET.Server.DTOS;
using SKINET.Server.Entities.order;
using System.Linq;

namespace SKINET.Server.Extentions
{
    public static class OrderMappingExtension
    {
        public static OrderDTO ToDto(this Order order)
        {

            return new OrderDTO
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                shippingAddress = order.shippingAddress,
                shippingPrice = order.Deliverymethod.price,
                Payment = order.Payment,
                Deliverymethod = order.Deliverymethod.Description,
                orderItems = order.orderItems.Select(x => x.ToDto()).ToList(),
                subtotal = order.subtotal,
                total = order.GetTotal(),
                status = order.status.ToString(),
                PaymentId = order.PaymentId


            };
        }
        public static OrderItemDto ToDto(this OrderItem orderItem)
        {

            return new OrderItemDto
            {


                ProductId = orderItem.Item.ProductId,
                ProductName = orderItem.Item.ProductName,
                PictureUrl = orderItem.Item.PictureUrl,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,







            };
        }

    }
}
