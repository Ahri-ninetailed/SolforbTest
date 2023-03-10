using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Database
{
    public class SolforbDbContext : DbContext
    {
        protected readonly IConfiguration configuration;
        public SolforbDbContext(IConfiguration configuration,DbContextOptions<SolforbDbContext> options) : base(options)
        {
            this.configuration = configuration;
        }
        public DbSet<Provider> Providers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
            }
        }
    }
}
