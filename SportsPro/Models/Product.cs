using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Product
    {
		public int ProductID { get; set; }

		[Required(ErrorMessage="Code is required.")]
		public string ProductCode { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }

		[Range(0, 1000000)]
		[Column(TypeName = "decimal(8,2)")]
		public decimal YearlyPrice { get; set; }

		public DateTime ReleaseDate { get; set; } = DateTime.Today;

		public string Slug => Name?.Replace(' ', '-').ToLower() + '-' + ProductID.ToString();

		// Add this to facilitate many-to-many relationship between Customer and Product tables
		public ICollection<Registration> Registrations { get; set; }
	}
}
