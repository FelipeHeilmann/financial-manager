using FinancialManager.Domain.Entity;
using FinancialManager.Infra.Config;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Infra.Context
{
    public class ApplicationContext : DbContext
    {
 
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Installment> Installment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionConfig());
            modelBuilder.ApplyConfiguration(new InstallmentConfig());
            base.OnModelCreating(modelBuilder);
        }
}
    
}
