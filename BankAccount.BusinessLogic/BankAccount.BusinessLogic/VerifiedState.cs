using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.BusinessLogic
{
    internal class VerifiedState : State
    {
        public VerifiedState(State state) : base(state.BankAccount)
        {
        }

        public override void BeforeVerify() 
        {
            throw new InvalidOperationException("account has been verified already");
        }

        public override void BeforeFreeze() 
        {
            this.bankAccount.State = new FreezedState(this);
        }

        public override void BeforeClose()
        {
            this.bankAccount.State = new ClosedState(this);
        }
    }
}
