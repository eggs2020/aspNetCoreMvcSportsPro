using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    // Signature class for UnitOfWork type class
    public interface ISportsProUnitOfWork
    {
        Repository<Customer> Customers { get; }
        Repository<Country> Countries { get; }
        Repository<Incident> Incidents { get; }
        Repository<Product> Products { get; }
        Repository<Registration> Registrations { get; }
        Repository<Technician> Technicians { get; }
    }
}
