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

        public IBankAccount CreateBankAccount(Guid userId, IEnumerable<Currency> currencies)
        {
            var bankAccount = new BankAccount(userId, currencies);
            bankAccounts.Add(bankAccount);
            return bankAccount;
        }

        //public AccountBase GetAccount(int ownerId)
        //{
        //    AccountBase account = bankAccounts.Where(x => x.owner == ownerId).FirstOrDefault();

        //    if (account == null)
        //    {
        //        throw new ApplicationException("no account exists with that id");
        //    }

        //    return account;
        //}

        //public bool TransferFunds(int fromAccountId, int toAccountId, decimal transferAmount)
        //{
        //    if (transferAmount <= 0)
        //    {
        //        throw new ApplicationException("transfer amount must be positive");
        //    }
        //    else if (transferAmount == 0)
        //    {
        //        throw new ApplicationException("invalid transfer amount");
        //    }

        //    AccountBase fromAccount = GetAccount(fromAccountId);
        //    AccountBase toAccount = GetAccount(toAccountId);

        //    if (fromAccount.balance < transferAmount)
        //    {
        //        throw new ApplicationException("insufficient funds");
        //    }

        //    fromAccount.Transfer(-1 * transferAmount, toAccountId);
        //    toAccount.Transfer(transferAmount, fromAccountId);

        //    return true;
        //}
    }
}
