using Microsoft.EntityFrameworkCore;
using PaymentsAPI.Models;

namespace PaymentsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }
    }
}
