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
    [RoutePrefix("api/bank-account")]
    public class BankAccountController : ApiController
    {
        private readonly IOrchiestrator orchiestrator;

        public BankAccountController()
        {
            this.orchiestrator = new Orchiestrator();
        }

        [HttpGet, Route("{accountId:Guid}")]
        public BankAccountHeaderDto GetBankAccountHeader(Guid accountId)
        {
            var bankAccountHeader = this.orchiestrator.GetBankAccountHeader(accountId);
            return bankAccountHeader;
        }

        [HttpPut, Route("deposite/{id:Guid}")]
        public BankAccountHeaderDto Deposite(Guid id, [FromBody]MoneyUpdate amount)
        {
            var bankAccountHeader = this.orchiestrator.Deposite(id, amount);
            return bankAccountHeader;
        }

        [HttpPut, Route("withdraw/{id:Guid}")]
        public BankAccountHeaderDto Withdraw(Guid id, [FromBody]MoneyParams amount)
        {
            var bankAccountHeader = this.orchiestrator.Withdraw(id, amount);
            return bankAccountHeader;
        }
    }
}
