namespace BankAccount.BusinessLogic
{
    public interface IState
    {
        BankAccount BankAccount { get; set; }

        void AfterDeposit();
        void AfterWithdraw();
        void BeforeClose();
        void BeforeDeposit();
        void BeforeFreeze();
        void BeforeGetBalance();
        void BeforeVerify();
        void BeforeWithdraw();
    }
}