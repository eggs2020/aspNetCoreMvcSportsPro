using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Registration
    {
		public int ProductID { get; set; }

		public Product Product { get; set; }

		public List<Product> Products { get; set; } // may not need this


		public int CustomerID { get; set; }

		public Customer Customer { get; set; }

	

		

		
	}
}
