using Microsoft.AspNetCore.Identity;

namespace Dealer.WebAPI.Infrastructure.Identity.Entities {
    public class ApplicationUser : IdentityUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
