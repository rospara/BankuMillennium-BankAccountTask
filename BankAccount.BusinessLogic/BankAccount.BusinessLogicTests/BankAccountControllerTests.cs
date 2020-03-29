using System;
using System.Collections.Generic;
using BankAccount.ApiParameters;
using BankAccount.BusinessLogic;
using BankAccount.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccount.DataTransferObjects;

namespace BankAccount.Controllers.Tests
{
    [TestClass]
    public class BankAccountControllerTests
    {
        [TestMethod]
        public void Check_Status_And_Balance()
        {
            var bankAccountController = new BankAccountController();
            var bankAccountHeader = bankAccountController.GetBankAccountHeader(Guid.Parse("4939209E-8CAA-4722-AC0D-31A1B15462DD"));
            Assert.AreEqual(bankAccountHeader.Status, "VerifiedState");
            Assert.AreEqual(bankAccountHeader.PLNBalance, 1000m);
        }

        [TestMethod]
        public void Deposite()
        {
            var bankAccountController = new BankAccountController();
            MoneyUpdate amount = new MoneyUpdate("PLN", 1000m);
            bankAccountController.Deposite(Guid.Parse("4939209E-8CAA-4722-AC0D-31A1B15462DD"), amount);
            var bankAccountHeader = bankAccountController.GetBankAccountHeader(Guid.Parse("4939209E-8CAA-4722-AC0D-31A1B15462DD"));
            Assert.AreEqual(bankAccountHeader.PLNBalance, 2000m);
        }

        [TestMethod]
        public void Withdraw()
        {
            var bankAccountController = new BankAccountController();
            MoneyParams amount = new MoneyParams("PLN", 1000m);
            bankAccountController.Withdraw(Guid.Parse("4939209E-8CAA-4722-AC0D-31A1B15462DD"), amount);
            var bankAccountHeader = bankAccountController.GetBankAccountHeader(Guid.Parse("4939209E-8CAA-4722-AC0D-31A1B15462DD"));
            Assert.AreEqual(bankAccountHeader.PLNBalance, 0m);
        }
    }
}
