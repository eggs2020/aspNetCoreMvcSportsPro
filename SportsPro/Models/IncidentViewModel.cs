using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SportsPro.Models
{
    public class IncidentViewModel
    {
        public int ProductID { get; set; }
        public List<Product> Products { get; set; }

        // we don't have List<Technician> Technicians because we don't need properties of this objec
        // in the Views/List.cshtml

        public int CustomerID { get; set; }
        public List<Customer> Customers { get; set; }


        public int IncidentID { get; set; }

        //Not using auto-property becuase we want to modify the set of the property
        private List<Incident> incidents;
        public List<Incident> Incidents
        {
            get => incidents;

            set
            {
                incidents = value;
            }
        }

        // public variable
        public string filter;


    }
}
