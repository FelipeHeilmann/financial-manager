using FinancialManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialManager.Infra.Config;
public class InstallmentConfig : IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("id");
        builder.Property(i => i.Amount).HasColumnName("amount");
        builder.Property(i => i.CreatedAt).HasColumnName("created_at");
        builder.Property(i => i.Date).HasColumnName("data");
        builder.Property(i => i.TransactionId).HasColumnName("transaction_id");

        builder.ToTable("installments");
    }
}