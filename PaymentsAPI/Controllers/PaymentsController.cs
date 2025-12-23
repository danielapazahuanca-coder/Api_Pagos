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

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentRequest request)
        {
            try
            {
                await _service.CreatePaymentAsync(request);
                return Ok(new { message = "Pago registrado correctamente" });
            }
            catch (SqlException ex)
            {
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
