using System.ComponentModel.DataAnnotations;

namespace LoginUserIdentity.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please Enter Username or Email!")]
        public string UserNameOrEmail { get; set; } = default!; // Property for username or email
        [Required]
        [DataType(DataType.Password)] // Specifies that this property is a password 
        public string Password { get; set; } = default!; // Property for the password

        public bool RememberMe { get; set; } // Property to remember the user for future logins
    }
}
