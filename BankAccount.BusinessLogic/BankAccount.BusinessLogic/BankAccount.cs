using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.BusinessLogic
{
    public class BankAccount : IBankAccount
    {
        private readonly Guid id = Guid.NewGuid();
        private Guid userId;
        private bool verified = false;
        /// <summary>
        /// @todo - consider currencyBalance as concurrent hash map 
        /// </summary>
        private Dictionary<Currency, Money> currencyBalance = new Dictionary<Currency, Money>();

        public BankAccount(Guid userId, IEnumerable<Currency> currencies)
        {
            this.userId = userId;

            if (currencies != null && currencies.Any())
            {
                foreach (var currency in currencies)
                {
                    this.currencyBalance.Add(currency, new Money(0m, currency));
                }
            }
        }

        public Money Withdraw(Currency currency, Money amount)
        {
            var amountAfterWithdraw = this.currencyBalance[currency] - amount;
            if (amountAfterWithdraw.Amount < 0m)
            {
                throw new ArgumentException("insufficient funds");
            }

            this.currencyBalance[currency] -= amount;

            return amount;
        }

        public void Deposit(Currency currency, Money amount)
        {
            this.currencyBalance[currency] += amount;
        }

        public void VerifyAccount()
        {
            throw new NotImplementedException();
        }

        public Money GetBalance(Currency pln)
        {
            return this.currencyBalance[pln];
        }
    }
}
