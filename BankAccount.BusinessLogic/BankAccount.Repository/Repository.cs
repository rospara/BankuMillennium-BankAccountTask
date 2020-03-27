using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB = BankAccount.DBModel;

namespace BankAccount.Repository
{
    public class Repository : IRepository
    {
        public DB.BankAccount GetBankAccount(Guid accountId, Guid userId)
        {
            var balance = new Dictionary<string, decimal>();
            balance.Add("PLN", 100m);
            return new DB.BankAccount(accountId, userId, balance, 1);
        }
    }
}
