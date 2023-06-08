using Microsoft.AspNetCore.Identity;

namespace StaffPortal.Models
{
    public class UserIdentityModel : IdentityUser
    {
        public UserIdentityModel(string emailCopy)
        {
            EmailCopy = emailCopy;
        }

        public string EmailCopy { get; set; } = null!;
    }
}
