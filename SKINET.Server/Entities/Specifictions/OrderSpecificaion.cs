using SKINET.Server.Entities.order;

namespace SKINET.Server.Entities.Specifictions
{
    public class OrderSpecificaion : BaseSpecification<Order>
    {

        public OrderSpecificaion( string email) :base (x=>x.BuyerEmail==email){
        
        AddInclude(x=>x.orderItems);
            AddInclude(x => x.Deliverymethod);
            AddorderByDescinding(x=>x.OrderDate);
        }
        public OrderSpecificaion(string email,int id) : base(x => x.BuyerEmail == email&&x.Id==id) {
            
            AddInclude(x => x.orderItems);
            AddInclude(x => x.Deliverymethod);
        }

        public OrderSpecificaion(string paymentIntentId,bool ispaymentIntent) : base(x => x.PaymentId == paymentIntentId)
        {
            AddInclude(x => x.orderItems);
            AddInclude(x => x.Deliverymethod);
        }
    }
}
