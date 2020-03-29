using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.ApiParameters
{
    public class MoneyParams
    {
        private string currencyISOCode;
        private decimal amount;

        public MoneyParams(string currencyISOCode, decimal amount)
        {
            this.currencyISOCode = currencyISOCode;
            this.amount = amount;
        }
    }
}
