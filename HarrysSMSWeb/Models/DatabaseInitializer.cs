using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarrysSMSWeb.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace HarrysSMSWeb.Models
{
    public static class DatabaseInitializer
    {
        public static void Initialize(HarrysDbContext context)
        {
            //context.Database.EnsureCreated();
            context.Database.Migrate();
            SeedData(context);
        }

        private static void SeedData(HarrysDbContext context)
        {
            if (!context.Customers.Any(r => r.PersonNummer == "1212129999"))
            {
                context.Customers.Add(new Customer
                {
                    Name = "Kajsa Anka",
                    PersonNummer =  "1212129999"
                });

                context.SaveChanges();
            }
        }
    }
}
