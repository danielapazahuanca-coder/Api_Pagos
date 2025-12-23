using PaymentsAPI.Models;

namespace PaymentsAPI.Repository
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task<List<Payment>> GetByCustomerIdAsync(Guid customerId);
    }
}
