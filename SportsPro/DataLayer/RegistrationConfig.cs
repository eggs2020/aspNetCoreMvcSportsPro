using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    public class RegistrationConfig : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> entity)
        {
            //Creating a linking table called Registration in SQL DB
            //This is to accomodate many-to-many relationship between Customer and Product tables
           
            entity.HasKey(reg => new { reg.CustomerID, reg.ProductID });

            entity.HasOne(reg => reg.Customer)
            .WithMany(c => c.Registrations) // Need to add a List<> in Models/Customer.cs
            .HasForeignKey(reg => reg.CustomerID);

            entity.HasOne(reg => reg.Product)
            .WithMany(p => p.Registrations) // Need to add a List<> in Models/Customer.cs
            .HasForeignKey(reg => reg.ProductID);

        }
    }//class
}//namespace
