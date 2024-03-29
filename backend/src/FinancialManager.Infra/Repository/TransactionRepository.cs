﻿using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Repository;
using FinancialManager.Infra.Context;

namespace FinancialManager.Infra.Repository;
public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationContext context)
        : base(context) {}

    public void AddInstallment(Installment installment, CancellationToken cancellationToken)
    {
        _context.Set<Installment>().Add(installment);
    }
}

