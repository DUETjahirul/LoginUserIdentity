using System.ComponentModel.DataAnnotations;

namespace LoginUserIdentity.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Please Enter UserName!")] // Validation attribute to ensure the field is required
        [Display(Name = "User Name")] // Display name for the username field
        public string UserName { get; set; } = default!; // Property for the username

        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        [Required(ErrorMessage ="Please Enter EMail!")]
        public string Email { get; set; } = default!; // Property for the username

        [Required]
        [DataType(DataType.Password)] // Specifies that this property is a password 
        public string Password { get; set; } = default!; // Property for the password

        [Display(Name = "Confirm Password")] // Display name for the confirm password field
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match!")] // Validation attribute to compare password and confirm password
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = default!; // Property for confirming the password
    }
}
