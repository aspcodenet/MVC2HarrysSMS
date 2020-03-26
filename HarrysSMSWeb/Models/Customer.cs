﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarrysSMSWeb.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string PersonNummer { get; set; }

        public string Name { get; set; }
        public List<Loan> Loans { get; set; } = new List<Loan>();

        public int Total()
        {
            return Loans.Sum(l => l.Belopp);
        }

        public bool HasEverBeenLatePaying
        {
            get
            {
                foreach (var loan in Loans)
                    foreach (var i in loan.Invoices)
                        if (i.LatePayment() > 0) return true;
                return false;
            }
        }

    }
}
