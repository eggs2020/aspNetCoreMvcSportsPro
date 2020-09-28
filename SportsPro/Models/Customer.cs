using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; // manually added

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }


		[Required(ErrorMessage = "First Name is required.")]
		[StringLength(51, ErrorMessage = "First Name must be between 1 and 51 characters.")]
		public string FirstName { get; set; }


		[Required(ErrorMessage = "Last Name is required.")]
		[StringLength(51, ErrorMessage = "Last Name must be between 1 and 51 characters.")]
		public string LastName { get; set; }


		[Required(ErrorMessage = "Address is required.")]
		[StringLength(51, ErrorMessage = "Address must be between 1 and 51 characters.")]
		public string Address { get; set; }


		[Required(ErrorMessage = "City is required.")]
		[StringLength(51, ErrorMessage = "City must be between 1 and 51 characters.")]
		public string City { get; set; }


		[Required(ErrorMessage = "State is required.")]
		[StringLength(51, ErrorMessage = "State must be between 1 and 51 characters.")]
		public string State { get; set; }


		[Required(ErrorMessage = "Postal Code is required.")]
		[StringLength(21, ErrorMessage = "Postal Code must be between 1 and 21 characters.")]
		public string PostalCode { get; set; }


		[Required(ErrorMessage = "Country is required.")]
		public string CountryID { get; set; }
		public Country Country { get; set; }


		[RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Phone number must be in (999) 999-9999 format.")]
		public string Phone { get; set; }


		[Required(ErrorMessage = "Email is required.")]
		[StringLength(51, ErrorMessage = "Must must be between 1 and 51 characters.")]
		[DataType(DataType.EmailAddress)]
		[Remote("CheckEmail", "Customer", AdditionalFields = "CustomerID")]
		public string Email { get; set; }


		public string FullName => FirstName + " " + LastName;   // read-only property

		// Add this to facilitate many-to-many relationship between Customer and Product tables
		public ICollection<Registration> Registrations { get; set; }
	}
}