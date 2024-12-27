using Domain.Orders;
using Microsoft.AspNetCore.Identity;

namespace Domain.User
{
    public class AppUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; } = string.Empty;

        public DateTime LastLogin { get; private set; }

        public void UpDateLastLogin()
        {
            LastLogin = DateTime.UtcNow;
        }
    }
}
