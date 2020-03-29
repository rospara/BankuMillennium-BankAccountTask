using System;
using DTO = BankAccount.DataTransferObjects;

namespace Orchiestrators
{
    public interface IOrchiestrator
    {
        DTO.BankAccountHeader GetBankAccountHeader(Guid accountId);
    }
}