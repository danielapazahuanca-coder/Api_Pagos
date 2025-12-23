using PaymentsAPI.DTOs;

namespace PaymentsAPI.Services
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(CreatePaymentRequest request);
        Task<List<PaymentResponse>> GetPaymentsAsync(Guid customerId);
    }
}
