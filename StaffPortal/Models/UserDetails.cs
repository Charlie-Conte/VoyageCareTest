using Microsoft.AspNetCore.Identity;

namespace StaffPortal.Models
{
    public class UserDetailsModel : IdentityUser
    {
        public UserDetailsModel(string emailCopy)
        {
            EmailCopy = emailCopy;
        }

        public string EmailCopy { get; set; } = null!;
    }
}
