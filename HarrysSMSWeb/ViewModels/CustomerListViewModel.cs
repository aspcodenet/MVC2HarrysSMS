using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarrysSMSWeb.ViewModels
{
    public class CustomerListViewModel
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public class Customer
        {
            public int Id{ get; set; }
            public string PersonNummer { get; set; }
            public string Namn { get; set; }
        }
    }
}
