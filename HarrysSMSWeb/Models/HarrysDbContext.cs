using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HarrysSMSWeb.Models
{
    public class HarrysDbContext : DbContext
    {
        public HarrysDbContext(DbContextOptions<HarrysDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payment { get; set; }
    }
}
