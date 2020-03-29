using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankAccount.BusinessLogic
{
    public class BankAccount : IBankAccount
    {
        private readonly Guid id;
        private readonly Guid userId;
        private Dictionary<Currency, Money> currencyBalance;
        private IState state;
        public IState State
        {
            get { return state; }
            internal set { state = value; }
        }

        public Guid GetAccountNumber()
        {
            return this.id;
        }

        public static string RemoveNamespaces(string typename)
        {
            return string.Join("",
                  Regex.Split(typename,
                             @"([^\w\.])").Select(p =>
                                             p.Substring(p.LastIndexOf('.') + 1)));
        }

        public string GetStatus()
        {
            var status = this.state.GetType().ToString();
            return RemoveNamespaces(status);
        }
        public BankAccount(Guid id, Guid userId, IState state)
        {
            this.id = id;
            this.userId = userId;
            this.currencyBalance = new Dictionary<Currency, Money>();
            state.BankAccount = this;
            this.state = state;
        }

        public BankAccount(Guid id, Guid userId, IDictionary<Currency, Money> currencyBalance, IState state)
        {
            this.id = id;
            this.userId = userId;
            this.currencyBalance = new Dictionary<Currency, Money>();
            foreach (var cb in currencyBalance)
            {
                this.currencyBalance.Add(cb.Key, cb.Value);
            }
            state.BankAccount = this;
            this.state = state;
        }

        public BankAccount(Guid id, Guid userId, IEnumerable<Currency> currencies, IState state)
        {
            this.id = id;
            this.userId = userId;
            this.currencyBalance = new Dictionary<Currency, Money>();
            if (currencies != null && currencies.Any())
            {
                foreach (var currency in currencies)
                {
                    this.currencyBalance.Add(currency, new Money(0m, currency));
                }
            }

            state.BankAccount = this;
            this.state = state;
        }

        public void Deposit(Currency currency, Money amount)
        {
            this.state.BeforeDeposit();
            this.currencyBalance[currency] += amount;
            this.state.AfterDeposit();
        }

        //@todo - to remove
        public Money Withdraw(Currency currency, Money amount)
        {
            this.state.BeforeWithdraw();

            var amountAfterWithdraw = this.currencyBalance[currency] - amount;
            if (amountAfterWithdraw.Amount < 0m)
            {
                throw new ArgumentException("insufficient funds");
            }
            this.currencyBalance[currency] -= amount;

            this.state.AfterWithdraw();

            return amount;
        }

        public Money Withdraw(Money money)
        {
            this.state.BeforeWithdraw();

            var amountAfterWithdraw = this.currencyBalance[money.Currency] - money.Amount;
            if (amountAfterWithdraw.Amount < 0m)
            {
                throw new ArgumentException("insufficient funds");
            }
            this.currencyBalance[money.Currency] -= money;

            this.state.AfterWithdraw();

            return money;
        }

        public void Deposit(Money money)
        {
            this.state.BeforeDeposit();
            this.currencyBalance[money.Currency] += money.Amount;
            this.state.AfterDeposit();
        }

        public Money GetBalance(Currency pln)
        {
            this.state.BeforeGetBalance();

            return this.currencyBalance[pln];
        }

        public Money GetBalance(string currencyISOCode)
        {
            this.state.BeforeGetBalance();
            var currency = new Currency(currencyISOCode);
            return this.currencyBalance[currency];
        }

        public void VerifyAccount()
        {
            this.state.BeforeVerify();
        }

        public void CloseAccount()
        {
            this.state.BeforeClose();
        }

        public void FreezeAccount()
        {
            this.state.BeforeFreeze();
        }

        public bool IsFreezed
        {
            get
            {
                return this.state.GetType() == typeof(FreezedState);
            }
        }

        public Guid Id => id;

        public Guid UserId => userId;

        public Dictionary<Currency, Money> CurrencyBalance { get => currencyBalance; }
    }
}
