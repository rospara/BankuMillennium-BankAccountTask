using System;
using System.Collections.Generic;
using BankAccount.BusinessLogic;
using BankAccount.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
