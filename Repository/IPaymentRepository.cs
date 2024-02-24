using proeduedge.DAL.Entities;

namespace proeduedge.Repository
{
    public interface IPaymentRepository
    {
        Task<bool> Pay (Payment payment);
    }
}
