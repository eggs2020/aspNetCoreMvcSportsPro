using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SportsPro.Models
{
    public class RegistrationViewModel
    {
        public List<Registration> Registrations { get; set; }

        public int ProductID { get; set; }
        public List<Product> Products { get; set; }

        public int CustomerID { get; set; }
        public List<Customer> Customers { get; set; }
        public Customer ActiveCustomer { get; set; }
    }
}
