﻿using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Domain.Exception;
public static class InstallmentErrors
{
    public static readonly Error NotFound = Error.NotFound("Not.Found", "Installment was not found");
    public static readonly Error InvalidAmount = Error.Validation("Amount.Less.Equal.Zero", "The amount of the installment should be greatter than zero");
}
