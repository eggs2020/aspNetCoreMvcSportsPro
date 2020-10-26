using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    //Seed data for Customers table
    public class SeedCustomers : IEntityTypeConfiguration<Customer>
    {        
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.HasData(
                new Customer
                {
                    CustomerID = 1002,
                    FirstName = "Ania",
                    LastName = "Irvin",
                    Address = "PO Box 96621",
                    City = "Washington",
                    State = "DC",
                    PostalCode = "20090",
                    CountryID = "US",
                    Phone = "(301) 555-8950",
                    Email = "ania@mma.nidc.com"
                },
                new Customer
                {
                    CustomerID = 1004,
                    FirstName = "Kenzie",
                    LastName = "Quinn",
                    Address = "1990 Westwood Blvd Ste 260",
                    City = "Los Angeles",
                    State = "CA",
                    PostalCode = "90025",
                    CountryID = "US",
                    Phone = "(800) 555-8725",
                    Email = "kenzie@mma.jobtrak.com"
                },
                new Customer
                {
                    CustomerID = 1006,
                    FirstName = "Anton",
                    LastName = "Mauro",
                    Address = "3255 Ramos Cir",
                    City = "Sacramento",
                    State = "CA",
                    PostalCode = "95827",
                    CountryID = "US",
                    Phone = "(916) 555-6670",
                    Email = "amauro@yahoo.org"
                },
                new Customer
                {
                    CustomerID = 1008,
                    FirstName = "Kaitlyn",
                    LastName = "Anthoni",
                    Address = "Box 52001",
                    City = "San Francisco",
                    State = "CA",
                    PostalCode = "94152",
                    CountryID = "US",
                    Phone = "(800) 555-6081",
                    Email = "kanthoni@pge.com"
                },
                new Customer
                {
                    CustomerID = 1010,
                    FirstName = "Kendall",
                    LastName = "Mayte",
                    Address = "PO Box 2069",
                    City = "Fresno",
                    State = "CA",
                    PostalCode = "93718",
                    CountryID = "US",
                    Phone = "(559) 555-9999",
                    Email = "kmayte@fresno.ca.gov"
                },
                new Customer
                {
                    CustomerID = 1012,
                    FirstName = "Marvin",
                    LastName = "Quintin",
                    Address = "4420 N. First Street, Suite 108",
                    City = "Fresno",
                    State = "CA",
                    PostalCode = "93726",
                    CountryID = "US",
                    Phone = "(559) 555-9586",
                    Email = "marvin@expedata.com"
                },
                new Customer
                {
                    CustomerID = 1015,
                    FirstName = "Gonzalo",
                    LastName = "Keeton",
                    Address = "27371 Valderas",
                    City = "Mission Viejo",
                    State = "CA",
                    PostalCode = "92691",
                    CountryID = "US",
                    Phone = "(214) 555-3647",
                    Email = ""
                }
            );
        } //configure
    } //class
}// namespace
