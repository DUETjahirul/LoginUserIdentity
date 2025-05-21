using LoginUserIdentity.Repository.Interface;
using LoginUserIdentity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginUserIdentity.Controllers.Authentication
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager; // UserManager for managing user-related operations
        private readonly SignInManager<IdentityUser> signInManager; // SignInManager for managing user sign-in operations
        private readonly IEmailSender emailSender;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender) // Constructor accepting UserManager
        {
            this.userManager = userManager; // Initializing UserManager in the constructor
            this.signInManager = signInManager; // Initializing SignInManager in the constructor
            this.emailSender = emailSender;
        }
        public IActionResult Index()    // This action method returns the default view for the Account controller
        {
            return View();
        }
        

        public IActionResult Register()     // This action method returns the view for user registration
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    var chkEmail = await userManager.FindByEmailAsync(model.Email); // Check if the email already exists
                    if (chkEmail != null) // If email exists, add error to ModelState
                    {
                        ModelState.AddModelError("", "Email already exists!");
                        return View(model);
                    }
                    var user = new IdentityUser() // Create a new user object
                    {
                        Email = model.Email, // Set the email property
                        UserName = model.Email // Set the username property
                    };
                    var result = await userManager.CreateAsync(user, model.Password); // Create the user with the provided password
                    if (result.Succeeded)
                    {
                        bool status = await emailSender.EmailSendAsync(model.Email, "Registration Successful", "Congratulation, You have successfully registered!"); // Send a success email
                        //await signInManager.SignInAsync(user, isPersistent: false); // Sign in the user
                        return RedirectToAction("Index", "Home"); // Redirect to the home page
                    }
                    if(result.Errors.Count()>0) // If there are errors during user creation
                    {
                        foreach (var error in result.Errors) // Loop through each error
                        {
                            ModelState.AddModelError("", error.Description); // Add the error to ModelState
                        }
                    }
                }
            }
            catch(Exception ex) // Catch any exceptions that occur during registration
            {
                ModelState.AddModelError("", ex.Message); // Add the exception message to ModelState
            }
            return View(model);
        }
        public IActionResult Login()        // This action method returns the view for user login
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model) // This action method handles user login
        {
            try
            {
                if (ModelState.IsValid) // Check if the model state is valid
                {
                    IdentityUser chkEmail = await userManager.FindByEmailAsync(model.Email); // Check if the email already exists
                    if (chkEmail == null) // If email exists, add error to ModelState
                    {
                        ModelState.AddModelError("", "Email are not registered!");
                        return View(model);
                    }
                    if(await userManager.CheckPasswordAsync(chkEmail, model.Password) == false) // Check if the password is correct
                    {
                        ModelState.AddModelError("", "Invalid Password!"); // Add error to ModelState for invalid password
                        return View(model);
                    }
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false); // Attempt to sign in the user
                    if (result.Succeeded) // If sign-in is successful
                    {
                        return RedirectToAction("Index", "Home"); // Redirect to the home page
                    }
                    ModelState.AddModelError("", "Invalid login password!"); // Add error to ModelState for invalid login
                }
            }
            catch(Exception ex) // Catch any exceptions that occur during login
            {
                ModelState.AddModelError("", ex.Message); // Add the exception message to ModelState
            }
            return View(model);
        }
        public async Task<IActionResult> Logout() // This action method handles user logout
        {
            await signInManager.SignOutAsync(); // Sign out the user
            return RedirectToAction("Login", "Account"); // Redirect to the home page
        }
    }
}
