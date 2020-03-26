namespace BankAccount.BusinessLogic
{
    public interface IBankAccount
    {
        void VerifyAccount();
        Money Withdraw(Currency currency, Money amount);
        void Deposit(Currency currency, Money amount);
        Money GetBalance(Currency pln);
        void CloseAccount();
    }
}