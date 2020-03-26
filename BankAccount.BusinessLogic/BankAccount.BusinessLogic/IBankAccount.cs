namespace BankAccount.BusinessLogic
{
    public interface IBankAccount
    {
       
        Money Withdraw(Currency currency, Money amount);
        void Deposit(Currency currency, Money amount);
        Money GetBalance(Currency pln);
        void CloseAccount();
        void FreezeAccount();
        void VerifyAccount();
        bool IsFreezed { get; }
    }
}