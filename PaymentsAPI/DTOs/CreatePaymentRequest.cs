using System.ComponentModel.DataAnnotations;

namespace PaymentsAPI.DTOs
{
    public class CreatePaymentRequest
    {
        [Required(ErrorMessage = "El customerId es obligatorio.")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "El proveedor del servicio es obligatorio.")]
        [StringLength(255, ErrorMessage = "El nombre del proveedor no puede exceder 255 caracteres.")]
        public string ServiceProvider { get; set; } = string.Empty;

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, 1500.00, ErrorMessage = "El monto debe estar entre 0.01 y 1500.00 Bs.")]
        public decimal Amount { get; set; }
    }
}
