using BankAccount.ApiParameters;
using BankAccount.DataTransferObjects;
using System;

namespace Orchiestrators
{
    public interface IOrchiestrator
    {
        BankAccountHeaderDto GetBankAccountHeader(Guid accountId);
        BankAccountHeaderDto Deposite(Guid guid, MoneyUpdate amount);
        BankAccountHeaderDto Withdraw(Guid accountId, MoneyParams amount);
    }
}