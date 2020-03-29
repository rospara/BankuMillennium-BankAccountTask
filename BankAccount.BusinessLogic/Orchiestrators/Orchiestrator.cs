using BankAccount.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB = BankAccount.DBModel;
using BL = BankAccount.BusinessLogic;
using DTO = BankAccount.DataTransferObjects;
using BankAccount.BusinessLogic;

namespace Orchiestrators
{
    public class Orchiestrator : IOrchiestrator
    {
        private readonly IRepository repository;

        public Orchiestrator()
        {
            this.repository = new Repository();
        }

        public DTO.BankAccountHeader GetBankAccountHeader(Guid accountId)
        {
            // get data from db
            DB.BankAccount EFBankAccount = this.repository.GetBankAccount(accountId);

            var currencyBalance = new Dictionary<Currency, Money>();
            foreach (var cb in EFBankAccount.CurrencyBalance)
            {
                var currency = new BL.Currency(cb.Key);
                currencyBalance.Add(currency, new Money(cb.Value, currency));
            }

            // apply business logic
            BL.BankAccount BLBankAccount = new BL.BankAccount(
                EFBankAccount.Id,
                EFBankAccount.UserId,
                currencyBalance,
                MapDBStatusBLStatus(EFBankAccount.State));

            // map business logic entity to dto
            DTO.BankAccountHeader DTOBankAccountHeader = new DTO.BankAccountHeader();
            DTOBankAccountHeader.Status = BLBankAccount.GetStatus();
            DTOBankAccountHeader.PLNBalance = BLBankAccount.GetBalance("PLN").Amount;
            DTOBankAccountHeader.AccountNumber = BLBankAccount.GetAccountNumber().ToString();
            return DTOBankAccountHeader;
        }

        private IState MapDBStatusBLStatus(int statusId)
        {
            switch (statusId)
            {
                case 0:
                    {
                        return new NewState();
                    }
                case 1:
                    {
                        return new VerifiedState();
                    }
                case 2:
                    {
                        return new FreezedState();
                    }
                case 3:
                    {
                        return new ClosedState();
                    }
                default: throw new Exception("status unknow");
            }
        }

        void Deposit() { }
        void Withdraw() { }
    }
}
