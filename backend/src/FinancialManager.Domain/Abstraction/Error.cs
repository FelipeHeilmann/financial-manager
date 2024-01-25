namespace FinancialManager.Domain.Abstraction
{
    public record Error(string Code, string? Detail = null);
}
