using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.DBModel
{
    public class BankAccount
    {
        public BankAccount(Guid id, Guid userId, Dictionary<string, decimal> currencyBalance, int state)
        {
            Id = id;
            UserId = userId;
            CurrencyBalance = currencyBalance;
            State = state;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Dictionary<string, decimal> CurrencyBalance { get; set; }
        public int State { get; set; }
    }
}
