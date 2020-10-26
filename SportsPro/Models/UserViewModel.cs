using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SportsPro.Models
{
    // ViewModel to pass data from the UserController to the Views/User/Index.cshtml
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
