using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// State pattern implementation based on https://www.dofactory.com/net/state-design-pattern
/// </summary>
namespace BankAccount.BusinessLogic
{
    internal abstract class State
    {
        internal State(BankAccount bankAccount) 
        {
            this.bankAccount = bankAccount;
        }

        protected BankAccount bankAccount;
        public BankAccount BankAccount
        {
            get { return bankAccount; }
            set { bankAccount = value; }
        }

        public virtual void BeforeGetBalance()
        {
        }

        public virtual void BeforeDeposit() 
        { 
        }

        public virtual void AfterDeposit() 
        { 
        }

        public virtual void BeforeWithdraw() 
        { 
        }

        public virtual void AfterWithdraw()
        {
        }

        public virtual void BeforeVerify()
        {
        }

        public virtual void BeforeFreeze()
        {
        }

        public virtual void BeforeClose()
        {
        }
    }
}
