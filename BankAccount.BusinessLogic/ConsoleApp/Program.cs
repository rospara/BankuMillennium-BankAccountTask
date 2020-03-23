using BankAccount.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            

            IBank bank = new Bank();
            Currency pln = new Currency("PLN");
            var currencies = new List<Currency>();
            currencies.Add(pln);
            Guid userId = Guid.NewGuid();
            IBankAccount bankAccount = bank.CreateBankAccount(userId, currencies);
            //bankAccount.VerifyAccount();
            Money m1 = new Money(100m, "PLN");
            bankAccount.Deposit(pln, m1);
            //bankAccount.Withdraw(m1);
            //bankAccount.GetBalance(); 

        }
    }
}
