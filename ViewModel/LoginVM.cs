using System.ComponentModel.DataAnnotations;

namespace LoginUserIdentity.ViewModel
{
    public class LoginVM
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        [Required(ErrorMessage = "Please Enter EMail!")]
        public string Email { get; set; } = default!; // Property for the username

        [Required]
        [DataType(DataType.Password)] // Specifies that this property is a password 
        public string Password { get; set; } = default!; // Property for the password
        public bool RememberMe { get; set; } // Property to remember the user for future logins
    }
}
