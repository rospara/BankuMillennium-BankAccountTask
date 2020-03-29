using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.ApiParameters
{
    public class MoneyUpdate : MoneyParams
    {
        public MoneyUpdate(string currencyISOCode, decimal amount) : base(currencyISOCode, amount)
        { }
    }

    public class MoneyParams
    {
        private string currencyISOCode;
        private decimal amount;

        public MoneyParams(string currencyISOCode, decimal amount)
        {
            this.CurrencyISOCode = currencyISOCode;
            this.Amount = amount;
        }

        public string CurrencyISOCode { get => currencyISOCode; set => currencyISOCode = value; }
        public decimal Amount { get => amount; set => amount = value; }
    }
}
