using System;
using System.Collections.Generic;

namespace BankAccount.BusinessLogic
{
    public interface IBank
    {
        IBankAccount CreateBankAccount(Guid userId, IEnumerable<Currency> currencies);
    }
}