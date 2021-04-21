using Microsoft.AspNetCore.Identity;

namespace CoffeeAndSnackVendingMachine.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
