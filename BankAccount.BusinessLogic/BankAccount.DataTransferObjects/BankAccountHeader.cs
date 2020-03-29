using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.DataTransferObjects
{
    public class BankAccountHeaderDto
    {
        public string Status { get; set; }
        public decimal PLNBalance { get; set; }
        public string AccountNumber { get; set; }
    }
}
