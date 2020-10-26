using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    //Seed data for Products table
    public class SeedProducts : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasData(
                new Product
                {
                    ProductID = 1,
                    ProductCode = "DRAFT10",
                    Name = "Draft Manager 1.0",
                    YearlyPrice = 4.99M,
                    ReleaseDate = DateTime.Parse("2017-02-01")
                },
                new Product
                {
                    ProductID = 2,
                    ProductCode = "DRAFT20",
                    Name = "Draft Manager 2.0",
                    YearlyPrice = 5.99M,
                    ReleaseDate = DateTime.Parse("2019-07-15 00:00:00.000")
                },
                new Product
                {
                    ProductID = 3,
                    ProductCode = "LEAG10",
                    Name = "League Scheduler 1.0",
                    YearlyPrice = 4.99M,
                    ReleaseDate = DateTime.Parse("2016-05-01 00:00:00.000")
                },
                new Product
                {
                    ProductID = 4,
                    ProductCode = "LEAGD10",
                    Name = "League Scheduler Deluxe 1.0",
                    YearlyPrice = 7.99M,
                    ReleaseDate = DateTime.Parse("2016-08-01 00:00:00.000")
                },
                new Product
                {
                    ProductID = 5,
                    ProductCode = "TEAM10",
                    Name = "Team Manager 1.0",
                    YearlyPrice = 4.99M,
                    ReleaseDate = DateTime.Parse("2017-05-01 00:00:00.000")
                },
                new Product
                {
                    ProductID = 6,
                    ProductCode = "TRNY10",
                    Name = "Tournament Master 1.0",
                    YearlyPrice = 4.99M,
                    ReleaseDate = DateTime.Parse("2015-12-01 00:00:00.000")
                },
                new Product
                {
                    ProductID = 7,
                    ProductCode = "TRNY20",
                    Name = "Tournament Master 2.0",
                    YearlyPrice = 5.99M,
                    ReleaseDate = DateTime.Parse("2018-02-15 00:00:00.000")
                }
            );
        }

    }//class
}//namespace
