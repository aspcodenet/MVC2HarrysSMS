using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarrysSMSWeb.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public Loan Loan { get; set; }
        public DateTime InvoiceDate { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime DueDate { get; set; }

        public int Belopp { get; set; }

        public List<Payment> Payments { get; set; }

        public int LatePayment()
        {
            return Payments.Where(r => r.PaymentDate.Date > DueDate.Date).Sum(r => r.Belopp);

        }

    }
}
