namespace SKINET.Server.Entities.Interfaces
{
    public interface IPaymentService
    {
        Task<ShopCart?> CreateorUodatePayment(string cardId);
    }
}
