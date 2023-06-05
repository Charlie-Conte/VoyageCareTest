using Microsoft.AspNetCore.Identity;

namespace StaffPortal.Models
{
    public class UserModel : IdentityUser
    {
        public string EmailCopy { get; set; } = null!;
    }
}
