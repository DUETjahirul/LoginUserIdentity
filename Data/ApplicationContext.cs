using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Importing necessary namespaces for IdentityDbContext
using Microsoft.EntityFrameworkCore; // Importing necessary namespaces for DbContext
namespace LoginUserIdentity.Data
{
    public class ApplicationContext: IdentityDbContext           // IdentityDbContext class responsible for database communication
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) // Constructor accepting DbContextOptions
        {
        }
        protected override void OnModelCreating(ModelBuilder builder) // Method to customize the model creation
        {
            base.OnModelCreating(builder); // Calling the base method to ensure IdentityDbContext configurations are applied
        }
    }
}
