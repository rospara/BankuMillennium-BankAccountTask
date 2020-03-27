using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.BusinessLogic
{
    internal class FreezedState : State
    {
        internal FreezedState(State state) : base(state.BankAccount)
        {
        }

        internal FreezedState(BankAccount bankAccount) : base(bankAccount)
        {
        }

        public override void AfterDeposit()
        {
            bankAccount.State = new VerifiedState(this);
        }

        public override void AfterWithdraw()
        {
            bankAccount.State = new VerifiedState(this);
        }

        public override void BeforeVerify()
        {
            throw new InvalidOperationException("account has been verified already");
        }

        public override void BeforeFreeze()
        {
            throw new InvalidOperationException("forbiden operation");
        }
    }
}
