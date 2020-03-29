using BankAccount.ApiParameters;
using BankAccount.DataTransferObjects;
using Orchiestrators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankAccount.Controllers
{
    public class BankAccountController : ApiController
    {
        private readonly IOrchiestrator orchiestrator;

        public BankAccountController()
        {
            this.orchiestrator = new Orchiestrator();
        }

        // GET api/values/5
        public BankAccountHeaderDto GetBankAccountHeader(Guid accountId)
        {
            var bankAccountHeader = this.orchiestrator.GetBankAccountHeader(accountId);
            return bankAccountHeader;
        }

        [HttpPut]
        // PUT api/values/5
        public IHttpActionResult Deposite(Guid guid, MoneyUpdate amount)
        {
            this.orchiestrator.Deposite(guid, amount);
            return Ok();
        }

        [HttpPut]
        // PUT api/values/5
        public MoneyDto Withdraw(Guid guid, MoneyParams amount)
        {
            var moneyDto = this.orchiestrator.Withdraw(guid, amount);
            return moneyDto;
        }
    }
}
