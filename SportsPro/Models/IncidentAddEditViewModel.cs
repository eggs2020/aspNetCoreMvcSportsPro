using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SportsPro.Models
{
    public class IncidentAddEditViewModel
    {
        public int ProductID { get; set; }
        public List<Product> Products { get; set; }


        public int TechnicianID { get; set; }
        public List<Technician> Technicians { get; set; }


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

        public Incident CurrentIncident { get; set; }

        public Technician CurrentTechnician { get; set; }

        //public variable 

        public string operationType;


    }
}
