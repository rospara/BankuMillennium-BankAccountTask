﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.DataTransferObjects
{
    public class MoneyDto
    {
        public string CurrencyISOCode { get; set; }
        public decimal Amount { get; set; }
    }
}
