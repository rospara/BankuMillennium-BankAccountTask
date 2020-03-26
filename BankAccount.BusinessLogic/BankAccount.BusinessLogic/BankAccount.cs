﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.BusinessLogic
{
    public class BankAccount : IBankAccount
    {
        private readonly Guid id;
        private Guid userId;
        private bool isVerified;
        /// <summary>
        /// @todo - consider currencyBalance as concurrent hash map 
        /// </summary>
        private Dictionary<Currency, Money> currencyBalance;
        private bool isClosed;
        private bool isFreezed;

        public BankAccount(Guid userId, IEnumerable<Currency> currencies)
        {
            this.id= id = Guid.NewGuid();
            this.userId = userId;
            this.isVerified = false;
            this.currencyBalance = new Dictionary<Currency, Money>();
            this.isClosed = false;
            this.isFreezed = false;

            if (currencies != null && currencies.Any())
            {
                foreach (var currency in currencies)
                {
                    this.currencyBalance.Add(currency, new Money(0m, currency));
                }
            }
        }

        public Money Withdraw(Currency currency, Money amount)
        {
            if (this.isClosed)
            {
                throw new InvalidOperationException("forbiden operation");
            }

            if (!this.isVerified)
            {
                throw new InvalidOperationException("account not verified");
            }

            var amountAfterWithdraw = this.currencyBalance[currency] - amount;
            if (amountAfterWithdraw.Amount < 0m)
            {
                throw new ArgumentException("insufficient funds");
            }

            this.currencyBalance[currency] -= amount;

            if (this.isFreezed)
            {
                this.isFreezed = false;
            }

            return amount;
        }

        public void Deposit(Currency currency, Money amount)
        {
            if (this.isClosed)
            {
                throw new InvalidOperationException("forbiden operation");
            }

            this.currencyBalance[currency] += amount;

            if (this.isFreezed)
            {
                this.isFreezed = false;
            }
        }

        public void VerifyAccount()
        {
            if (this.isClosed)
            {
                throw new InvalidOperationException("forbiden operation");
            }

            if (this.isVerified)
            {
                throw new InvalidOperationException("account has been verified already");
            }

            this.isVerified = true;
        }

        public Money GetBalance(Currency pln)
        {
            if (this.isClosed)
            {
                throw new InvalidOperationException("forbiden operation");
            }

            return this.currencyBalance[pln];
        }

        public void CloseAccount()
        {
            if (this.isClosed)
            {
                throw new InvalidOperationException("forbiden operation");
            }

            if (!this.isVerified)
            {
                throw new InvalidOperationException("forbiden operation");
            }

            this.isClosed = true;
        }

        public void FreezeAccount()
        {
            if (this.isClosed)
            {
                throw new InvalidOperationException("forbiden operation");
            }

            if (!this.isVerified)
            {
                throw new InvalidOperationException("forbiden operation");
            }

            if (this.isFreezed)
            {
                throw new InvalidOperationException("forbiden operation");
            }

            this.isFreezed = true;
        }

        public bool IsFreezed
        {
            get
            {
                return this.isFreezed;
            }
        }
    }
}
