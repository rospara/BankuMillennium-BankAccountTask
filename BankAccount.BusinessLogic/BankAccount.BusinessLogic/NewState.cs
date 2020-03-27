using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.BusinessLogic
{
    internal class NewState : State
    {
        internal NewState() 
        {
        }

        public override void BeforeWithdraw()
        {
            throw new InvalidOperationException("account not verified");
        }

        public override void BeforeVerify()
        {
            bankAccount.State = new VerifiedState(this);
        }

        public override void BeforeFreeze()
        {
            throw new InvalidOperationException("account not verified");
        }

        public override void BeforeClose()
        {
            throw new InvalidOperationException("account not verified");
        }
    }
}
