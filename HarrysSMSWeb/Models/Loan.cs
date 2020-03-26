using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarrysSMSWeb.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int CustomerId { get; set; }

        public string LoanNo { get; set; }
        public int Belopp { get; set; }
        public DateTime FromWhen { get; set; }
        public decimal InterestRate { get; set; }
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
