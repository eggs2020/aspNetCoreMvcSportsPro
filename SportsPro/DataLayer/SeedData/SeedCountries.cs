using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    public class SeedCountries : IEntityTypeConfiguration<Country>
    {
        // See data for Countries table
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.HasData(
                new Country { CountryID = "AU", Name = "Australia" },
                new Country { CountryID = "AT", Name = "Austria" },
                new Country { CountryID = "BE", Name = "Belgium" },
                new Country { CountryID = "BR", Name = "Brazil" },
                new Country { CountryID = "CA", Name = "Canada" },
                new Country { CountryID = "CN", Name = "China" },
                new Country { CountryID = "DK", Name = "Denmark" },
                new Country { CountryID = "FI", Name = "Finland" },
                new Country { CountryID = "FR", Name = "France" },
                new Country { CountryID = "GR", Name = "Greece" },
                new Country { CountryID = "GL", Name = "Greenland" },
                new Country { CountryID = "HK", Name = "Hong Kong" },
                new Country { CountryID = "IS", Name = "Iceland" },
                new Country { CountryID = "IN", Name = "India" },
                new Country { CountryID = "IE", Name = "Ireland" },
                new Country { CountryID = "IL", Name = "Israel" },
                new Country { CountryID = "IT", Name = "Italy" },
                new Country { CountryID = "JP", Name = "Japan" },
                new Country { CountryID = "LR", Name = "Liberia" },
                new Country { CountryID = "MY", Name = "Malaysia" },
                new Country { CountryID = "MX", Name = "Mexico" },
                new Country { CountryID = "NL", Name = "Netherlands" },
                new Country { CountryID = "NZ", Name = "New Zealand" },
                new Country { CountryID = "NG", Name = "Nigeria" },
                new Country { CountryID = "PH", Name = "Philippines" },
                new Country { CountryID = "PT", Name = "Portugal" },
                new Country { CountryID = "PR", Name = "Puerto Rico" },
                new Country { CountryID = "QA", Name = "Qatar" },
                new Country { CountryID = "SG", Name = "Singapore" },
                new Country { CountryID = "ES", Name = "Spain" },
                new Country { CountryID = "SE", Name = "Sweden" },
                new Country { CountryID = "CH", Name = "Switzerland" },
                new Country { CountryID = "TH", Name = "Thailand" },
                new Country { CountryID = "TR", Name = "Turkey" },
                new Country { CountryID = "UA", Name = "Ukraine" },
                new Country { CountryID = "AE", Name = "United Arab Emirates" },
                new Country { CountryID = "GB", Name = "United Kingdom" },
                new Country { CountryID = "US", Name = "United States" },
                new Country { CountryID = "VN", Name = "Vietnam" },
                new Country { CountryID = "ZW", Name = "Zimbabwe" }
            );
        }
    }
}
