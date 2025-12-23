using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using PaymentsAPI.Models;
using System.Data;

namespace PaymentsAPI.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task AddAsync(Payment payment)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_CreatePayment", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);
            cmd.Parameters.AddWithValue("@CustomerId", payment.CustomerId);
            cmd.Parameters.AddWithValue("@ServiceProvider", payment.ServiceProvider);
            cmd.Parameters.AddWithValue("@Amount", payment.Amount);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }


        public async Task<List<Payment>> GetByCustomerIdAsync(Guid customerId)
        {
            var result = new List<Payment>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetPaymentsByCustomer", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new Payment
                {
                    PaymentId = reader.GetGuid(0),
                    ServiceProvider = reader.GetString(1),
                    Amount = reader.GetDecimal(2),
                    Status = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4)
                });
            }

            return result;
        }
    }
}
