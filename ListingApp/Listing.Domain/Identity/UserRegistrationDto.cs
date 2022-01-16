using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Domain.Identity
{
    public class UserRegistrationDto
    {
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}
