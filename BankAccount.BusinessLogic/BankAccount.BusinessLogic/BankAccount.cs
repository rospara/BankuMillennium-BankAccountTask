using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.BusinessLogic
{
    public class BankAccount : IBankAccount
    {
        private readonly Guid id;
        private Guid userId;
        private Dictionary<Currency, Money> currencyBalance;
        private IState state;
        internal IState State
        {
            get { return state; }
            set { state = value; }
        }

        public BankAccount(Guid userId, IEnumerable<Currency> currencies, IState state)
        {
            this.id = Guid.NewGuid();
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

        public Money GetBalance(Currency pln)
        {
            this.state.BeforeGetBalance();

            return this.currencyBalance[pln];
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
    }
}
