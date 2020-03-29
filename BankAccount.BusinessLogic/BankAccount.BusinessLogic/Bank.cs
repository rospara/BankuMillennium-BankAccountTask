using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.BusinessLogic
{
    public class Bank : IBank
    {
        private readonly Guid id = Guid.NewGuid();
        private IList<IBankAccount> bankAccounts = new List<IBankAccount>();

        public IBankAccount CreateBankAccount(Guid id, Guid userId, IEnumerable<Currency> currencies)
        {
            var bankAccount = new BankAccount(id, userId, currencies, new NewState());
            bankAccounts.Add(bankAccount);
            return bankAccount;
        }
    }
}
