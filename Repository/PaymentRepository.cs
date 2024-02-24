using proeduedge.DAL;
using proeduedge.DAL.Entities;

namespace proeduedge.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDBContext _context;

        public PaymentRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<bool> Pay(Payment payment)
        {
            _context.Payment.Add(payment);
            await SaveAsync();
            return true;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
