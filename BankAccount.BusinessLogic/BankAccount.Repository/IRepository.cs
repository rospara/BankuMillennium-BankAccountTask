using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB = BankAccount.DBModel;

namespace BankAccount.Repository
{
    public interface IRepository
    {
        DB.BankAccount GetBankAccount(Guid accountId, Guid userId);
    }
}
