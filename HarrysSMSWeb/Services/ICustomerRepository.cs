using System.Collections.Generic;
using HarrysSMSWeb.Models;

namespace HarrysSMSWeb.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        int Add(Customer customer);
        Customer GetByPersonNummer(string modelPersonNummer);
    }
}