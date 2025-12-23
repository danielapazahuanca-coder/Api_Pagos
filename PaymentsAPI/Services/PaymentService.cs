using PaymentsAPI.DTOs;
using PaymentsAPI.Models;
using PaymentsAPI.Repository;

namespace PaymentsAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task CreatePaymentAsync(CreatePaymentRequest request)
        {
            if (request.Amount > 1500)
                throw new ArgumentException("El monto no puede superar los 1500 Bs.");

            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                ServiceProvider = request.ServiceProvider,
                Amount = request.Amount,
                Status = "pendiente"
            };

            await _repository.AddAsync(payment);
        }


        public async Task<List<PaymentResponse>> GetPaymentsAsync(Guid customerId)
        {
            var payments = await _repository.GetByCustomerIdAsync(customerId);

            return payments.Select(p => new PaymentResponse
            {
                PaymentId = p.PaymentId,
                ServiceProvider = p.ServiceProvider,
                Amount = p.Amount,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            }).ToList();
        }
    }
}
