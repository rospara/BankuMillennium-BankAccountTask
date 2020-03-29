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
        public DB.BankAccount GetBankAccount(Guid accountId)
        {
            var balance = new Dictionary<string, decimal>();
            balance.Add("PLN", 1000m);
            var userId = Guid.Parse("4939209E-8CAA-4722-AC0D-31A1B15462DD");
            return new DB.BankAccount(accountId, userId, balance, 1);
        }
    }
}
