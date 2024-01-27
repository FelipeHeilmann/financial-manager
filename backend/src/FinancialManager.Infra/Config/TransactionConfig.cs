using FinancialManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialManager.Infra.Config;
public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Amount).HasColumnName("amount");
        builder.Property(t => t.Author).HasColumnName("author");
        builder.Property(t => t.Description).HasColumnName("description");
        builder.Property(t => t.Date).HasColumnName("date");
        builder.Property(t=> t.Type).HasColumnName("type");
        builder.Property(t => t.CreatedAt).HasColumnName("created_at");

        builder.HasMany(t => t.Installments)
            .WithOne()
            .HasForeignKey(installment => installment.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("transactions");
    }
}
