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
            DB.BankAccount dbBankAccount = this.AssertBankAccount(accountId);

            // map db entities to bl entities
            // offcourse automapper can be used
            Dictionary<Currency, Money> blCurrencyBalance = MapCurrencyBalance(dbBankAccount);
            IState blState = MapState(dbBankAccount.State);

            // apply business logic
            BL.BankAccount blBankAccount = LoadBankAccount(dbBankAccount.Id, dbBankAccount.UserId, blCurrencyBalance, blState);

            // map business logic entity to dto
            // offcourse automapper can be used
            BankAccountHeaderDto bankAccountHeaderDto = MapBankAccount(blBankAccount);
            return bankAccountHeaderDto;
        }

        public BankAccountHeaderDto Deposite(Guid accountId, MoneyUpdate amountUpdate)
        {
            // get data from db
            DB.BankAccount dbBankAccount = this.AssertBankAccount(accountId);

            // map db entities to bl entities
            // offcourse automapper can be used
            Dictionary<Currency, Money> blCurrencyBalance = MapCurrencyBalance(dbBankAccount);
            IState blState = MapState(dbBankAccount.State);

            // apply business logic
            BL.BankAccount blBankAccount = LoadBankAccount(dbBankAccount.Id, dbBankAccount.UserId, blCurrencyBalance, blState);
            Money amount = new Money(amountUpdate.Amount, new Currency(amountUpdate.CurrencyISOCode));
            blBankAccount.Deposit(amount);

            BankAccountHeaderDto bankAccountHeaderDto = MapBankAccount(blBankAccount);
            return bankAccountHeaderDto;
        }

        public BankAccountHeaderDto Withdraw(Guid accountId, MoneyParams moneyParams)
        {
            // get data from db
            DB.BankAccount dbBankAccount = this.AssertBankAccount(accountId);

            // map db entities to bl entities
            // offcourse automapper can be used
            Dictionary<Currency, Money> blCurrencyBalance = MapCurrencyBalance(dbBankAccount);
            IState blState = MapState(dbBankAccount.State);

            // apply business logic
            BL.BankAccount blBankAccount = LoadBankAccount(dbBankAccount.Id, dbBankAccount.UserId, blCurrencyBalance, blState);
            Money amount = new Money(moneyParams.Amount, new Currency(moneyParams.CurrencyISOCode));
            var money = blBankAccount.Withdraw(amount);
            BankAccountHeaderDto bankAccountHeaderDto = MapBankAccount(blBankAccount);
            return bankAccountHeaderDto;
        }

        private static MoneyDto MapMoney(Money money)
        {
            MoneyDto moneyDto = new MoneyDto();
            moneyDto.Amount = money.Amount;
            moneyDto.CurrencyISOCode = money.Currency.IsoCode;
            return moneyDto;
        }

        private static BankAccountHeaderDto MapBankAccount(BL.BankAccount blBankAccount)
        {          
            BankAccountHeaderDto bankAccountHeaderDto = new BankAccountHeaderDto();
            bankAccountHeaderDto.Status = blBankAccount.GetStatus();
            var currencyBalanceDto = new List<MoneyDto>();
            foreach (var cb in blBankAccount.CurrencyBalance)
            {
                currencyBalanceDto.Add(MapMoney(cb.Value));
            }
            bankAccountHeaderDto.Balances = currencyBalanceDto;
            bankAccountHeaderDto.AccountNumber = blBankAccount.GetAccountNumber().ToString();
            return bankAccountHeaderDto;
        }

        private static BL.BankAccount LoadBankAccount(Guid id, Guid userId, IDictionary<Currency, Money> blCurrencyBalance, IState blState)
        {
            return new BL.BankAccount(
                id,
                userId,
                blCurrencyBalance,
                blState);
        }

        private static BL.BankAccount LoadBankAccount(Guid id, Guid userId, IState blState)
        {
            return new BL.BankAccount(
                id,
                userId,
                blState);
        }

        private static Dictionary<Currency, Money> MapCurrencyBalance(DB.BankAccount dbBankAccount)
        {
            // map db entities to bl entities
            var currencyBalance = new Dictionary<Currency, Money>();
            foreach (var cb in dbBankAccount.CurrencyBalance)
            {
                var currency = new BL.Currency(cb.Key);
                currencyBalance.Add(currency, new Money(cb.Value, currency));
            }

            return currencyBalance;
        }

        private DB.BankAccount AssertBankAccount(Guid accountId)
        {
            var bankAccount = this.repository.GetBankAccount(accountId);
            ObjectRetrievalFailureException.ThrowIfNull(bankAccount, accountId);
            return bankAccount;
        }

        private IState MapState(int statusId)
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
    }
}
