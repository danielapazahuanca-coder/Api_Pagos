using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PaymentsAPI.DTOs;
using PaymentsAPI.Services;

namespace PaymentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService service, ILogger<PaymentsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentRequest request)
        {
            _logger.LogInformation("Iniciando registro de pago para CustomerId {CustomerId}", request.CustomerId);

            try
            {
                await _service.CreatePaymentAsync(request);
                _logger.LogInformation("Pago registrado correctamente para CustomerId {CustomerId}", request.CustomerId);

                return Ok(new { message = "Pago registrado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar pago para CustomerId {CustomerId}", request.CustomerId);
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetPayments([FromQuery] Guid customerId)
        {
            var result = await _service.GetPaymentsAsync(customerId);
            return Ok(result);
        }
    }
}
