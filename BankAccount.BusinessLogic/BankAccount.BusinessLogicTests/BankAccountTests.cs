using System;
using System.Collections.Generic;
using BankAccount.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankAccount.BusinessLogic.Tests
{
    [TestClass]
    public class BankAccountTests
    {
        // 1. add async code 
        // 2. use parallel processing where possible
        // 3. use locks and thread safe techniques 
        [TestMethod]
        public void Money_Can_Be_Deposit_To_BankAccount_Any_Time()
        {
            IBank bank = new Bank();
            Currency pln = new Currency("PLN");
            var currencies = new List<Currency>();
            currencies.Add(pln);
            Guid userId = Guid.NewGuid();
            IBankAccount bankAccount = bank.CreateBankAccount(userId, currencies);
            
            Money m100 = new Money(100m, "PLN");
            Money m0 = new Money(0m, "PLN");
            bankAccount.Deposit(pln, m100);
            bankAccount.Withdraw(pln, m100);

            Assert.AreEqual(bankAccount.GetBalance(pln), m0); 
        }
    }
}
