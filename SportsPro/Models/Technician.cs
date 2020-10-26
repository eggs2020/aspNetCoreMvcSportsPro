using System;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Technician
    {
		public int TechnicianID { get; set; }	   

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone mumber is required.")]
		[RegularExpression(@"^(\d{3}-)\d{3}-\d{4}", ErrorMessage = "Phone number must be in 999-999-9999 format.")]
		public string Phone { get; set; }
	}
}
