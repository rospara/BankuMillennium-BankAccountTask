using BankAccount.ApiParameters;
using BankAccount.DataTransferObjects;
using System;

namespace Orchiestrators
{
    public interface IOrchiestrator
    {
        BankAccountHeaderDto GetBankAccountHeader(Guid accountId);
        void Deposite(Guid guid, MoneyUpdate amount);
        MoneyDto Withdraw(Guid accountId, MoneyParams amount);
    }
}