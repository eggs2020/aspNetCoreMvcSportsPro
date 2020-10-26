using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    // Class to implement methods in the corresponding interface class
    public class SportsProUnitOfWork : ISportsProUnitOfWork
    {
        // use this class if we need to access multiple tables in DB
        private SportsProContext context { get; set; }
        public SportsProUnitOfWork(SportsProContext ctx) => context = ctx;


        private Repository<Country> countryData;
        public Repository<Country> Countries
        {
            get
            {
                if (countryData == null)
                    countryData = new Repository<Country>(context);
                return countryData;
            }
        }

        private Repository<Customer> customerData;
        public Repository<Customer> Customers
        {
            get
            {
                if (customerData == null)
                    customerData = new Repository<Customer>(context);
                return customerData;
            }
        }

        private Repository<Incident> incidentData;
        public Repository<Incident> Incidents
        {
            get
            {
                if (incidentData == null)
                    incidentData = new Repository<Incident>(context);
                return incidentData;
            }
        }

        private Repository<Product> productData;
        public Repository<Product> Products
        {
            get
            {
                if (productData == null)
                    productData = new Repository<Product>(context);
                return productData;
            }
        }

        private Repository<Registration> registrationData;
        public Repository<Registration> Registrations
        {
            get
            {
                if (registrationData == null)
                    registrationData = new Repository<Registration>(context);
                return registrationData;
            }
        }

        private Repository<Technician> technicianData;
        public Repository<Technician> Technicians
        {
            get
            {
                if (technicianData == null)
                    technicianData = new Repository<Technician>(context);
                return technicianData;
            }
        }

        //Method to save changes/update
        public void Save() => context.SaveChanges();


    }
}
