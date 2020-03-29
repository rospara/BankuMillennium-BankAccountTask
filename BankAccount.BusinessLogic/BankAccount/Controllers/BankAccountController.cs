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
        public BankAccountHeader GetBankAccountHeader(Guid accountId)
        {
            var bankAccountHeader = this.orchiestrator.GetBankAccountHeader(accountId);
            return bankAccountHeader;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
