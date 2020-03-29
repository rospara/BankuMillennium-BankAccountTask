using System.Collections.Generic;

namespace BankAccount.DataTransferObjects
{
    public class BankAccountHeaderDto
    {
        public string Status { get; set; }
        public string AccountNumber { get; set; }

        public IEnumerable<MoneyDto> CurrencyBalance { get; set; }
    }
}
