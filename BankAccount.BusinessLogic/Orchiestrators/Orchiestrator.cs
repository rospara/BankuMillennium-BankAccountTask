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
using BankAccount.ApiParameters;
using BankAccount.DataTransferObjects;

namespace Orchiestrators
{
    /// <summary>
    /// Orchiestrator aranges all request handling between layers: retrieves data from db, receives parameters from
    /// controller, loads buisness logic entites and run buisness logic, returning some results optionally.
    /// So main responsibility is:
    /// 1. mapping between layers
    /// 2. runing business logic
    /// </summary>
    public class Orchiestrator : IOrchiestrator
    {
        private readonly IRepository repository;

        public Orchiestrator()
        {
            this.repository = new Repository();
        }

        public DTO.BankAccountHeaderDto GetBankAccountHeader(Guid accountId)
        {
            // get data from db
            DB.BankAccount dbBankAccount = this.repository.GetBankAccount(accountId);

            var currencyBalance = new Dictionary<Currency, Money>();
            foreach (var cb in dbBankAccount.CurrencyBalance)
            {
                var currency = new BL.Currency(cb.Key);
                currencyBalance.Add(currency, new Money(cb.Value, currency));
            }

            // apply business logic
            BL.BankAccount blBankAccount = new BL.BankAccount(
                dbBankAccount.Id,
                dbBankAccount.UserId,
                currencyBalance,
                MapDBStatusBLStatus(dbBankAccount.State));

            // map business logic entity to dto
            BankAccountHeaderDto bankAccountHeaderDto = new BankAccountHeaderDto();
            bankAccountHeaderDto.Status = blBankAccount.GetStatus();
            bankAccountHeaderDto.PLNBalance = blBankAccount.GetBalance("PLN").Amount;
            bankAccountHeaderDto.AccountNumber = blBankAccount.GetAccountNumber().ToString();
            return bankAccountHeaderDto;
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

        public void Deposite(Guid accountId, MoneyParams amount)
        {

        }
        public MoneyDto Withdraw(Guid accountId, MoneyParams amount) 
        {
            throw new NotImplementedException();
        }
    }
}
