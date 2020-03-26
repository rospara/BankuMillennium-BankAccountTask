using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.BusinessLogic
{
    internal class ClosedState : State
    {
        public ClosedState(State state) : base(state.BankAccount)
        {
        }

        public override void BeforeDeposit()
        {
            throw new InvalidOperationException("forbiden operation");
        }

        public override void BeforeWithdraw()
        {
            throw new InvalidOperationException("forbiden operation");   
        }

        public override void BeforeVerify()
        {
            throw new InvalidOperationException("forbiden operation");
        }

        public override void BeforeFreeze()
        {
            throw new InvalidOperationException("forbiden operation");
        }

        public override void BeforeClose()
        {
            throw new InvalidOperationException("forbiden operation");
        }

        public override void BeforeGetBalance()
        {
            throw new InvalidOperationException("forbiden operation");
        }
    }
}
