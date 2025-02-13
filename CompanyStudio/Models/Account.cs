﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStudio.Models
{
    public class Account
    {
        public long AccountID { get; set; }
        public long CompanyID { get; set; }
        public long? CategoryID { get; set; }
        public string Description { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
