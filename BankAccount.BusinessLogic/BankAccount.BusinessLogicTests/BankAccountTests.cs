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
        public void Money_Can_Be_Deposited_To_BankAccount_Any_Time()
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
            bankAccount.Deposit(pln, m100);

            Assert.AreEqual(bankAccount.GetBalance(pln), new Money(200m, "PLN")); 
        }

        [TestMethod]
        public void Money_Can_Be_Withdrawn_Only_If_Was_Verified_Negative_Test()
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

            try
            {
                bankAccount.Withdraw(pln, m100);
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void Money_Can_Be_Withdrawn_Only_If_Was_Verified_Positive_Test()
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
            bankAccount.VerifyAccount();

            try
            {
                bankAccount.Withdraw(pln, m100);
            }
            catch
            {
                Assert.Fail();
                return;
            }

            Assert.AreEqual(bankAccount.GetBalance(pln), new Money(0m, "PLN"));
        }

        // Kiedy konto zostanie zamknięte wszystkie dalsze akcje na tym koncie są niedozwolone.
        [TestMethod]
        public void When_Account_Is_Closed_Any_Actions_Are_Forbidden_Test()
        {
            IBank bank = new Bank();
            Currency pln = new Currency("PLN");
            var currencies = new List<Currency>();
            currencies.Add(pln);
            Guid userId = Guid.NewGuid();
            IBankAccount bankAccount = bank.CreateBankAccount(userId, currencies);

            Money m0 = new Money(0m, "PLN");
            bankAccount.Deposit(pln, );
            bankAccount.Deposit(pln, new Money(200m, "PLN"));
            bankAccount.Deposit(pln, new Money(300m, "PLN"));
            bankAccount.Deposit(pln, new Money(500m, "PLN"));
            bankAccount.VerifyAccount();
            bankAccount.Deposit(pln, new Money(1000m, "PLN"));
            bankAccount.Deposit(pln, new Money(2000m, "PLN"));
            bankAccount.Withdraw(pln, new Money(100m, "PLN"));
            bankAccount.Deposit(pln, new Money(3000m, "PLN"));
            bankAccount.Withdraw(pln, new Money(100m, "PLN"));
            bankAccount.Deposit(pln, new Money(5000m, "PLN"));
            bankAccount.Withdraw(pln, new Money(100m, "PLN"));

            bankAccount.CloseAccount();

            try
            {
                
            }
            catch
            {
                Assert.Fail();
                return;
            }

            Assert.AreEqual(bankAccount.GetBalance(pln), new Money(0m, "PLN"));
        }
    }
}
