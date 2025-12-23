using System.ComponentModel.DataAnnotations;

namespace PaymentsAPI.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(150)]
        public string ServiceProvider { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; } = "pendiente";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
