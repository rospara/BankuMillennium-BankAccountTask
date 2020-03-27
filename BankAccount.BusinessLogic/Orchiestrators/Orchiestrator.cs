using BankAccount.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB = BankAccount.DBModel;
using BL = BankAccount.BusinessLogic;
using DTO = BankAccount.DataTransferObjects;

namespace Orchiestrators
{
    public class Orchiestrator : IOrchiestrator
    {
        private readonly IRepository repository;

        public Orchiestrator()
        {
            this.repository = new Repository();
        }

        DTO.BankAccountHeader GetBankAccountHeader(Guid accountId, Guid userId)
        {
            // get data from db
            DB.BankAccount EFBankAccount = this.repository.GetBankAccount(accountId, userId);

            // apply business logic
            BL.BankAccount BLBankAccount = new BL.BankAccount(
                EFBankAccount.Id,
                EFBankAccount.UserId,
                EFBankAccount.CurrencyBalance.Keys.Select(x => new BL.Currency(x)),
                EFBankAccount.State);

            // map business logic entity to dto
            DTO.BankAccountHeader DTOBankAccountHeader = new DTO.BankAccountHeader();
            DTOBankAccountHeader.Status = BLBankAccount.GetStatus();
            DTOBankAccountHeader.PLNBalance = BLBankAccount.GetBalance("PLN").Amount;
            DTOBankAccountHeader.AccountNumber = BLBankAccount.GetAccountNumber().ToString();
            return DTOBankAccountHeader;
        }

        void Deposit() { }
        void Withdraw() { }
    }
}
