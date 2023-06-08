using Microsoft.AspNetCore.Identity;

namespace StaffPortal.Models
{
    public class UserModel : IdentityUser
    {
        public UserModel()
        {
        }

        public UserModel(IdentityUser User)
        {
            Id = User.Id;
            UserName = User.UserName;
            NormalizedUserName = User.NormalizedUserName;
            Email = User.Email;
            EmailCopy = User.Email;
            NormalizedEmail = User.NormalizedEmail;
            EmailConfirmed = User.EmailConfirmed;
            PasswordHash = User.PasswordHash;
            SecurityStamp = User.SecurityStamp;
            ConcurrencyStamp = User.ConcurrencyStamp;
            PhoneNumber = User.PhoneNumber;
            PhoneNumberConfirmed = User.PhoneNumberConfirmed;
            TwoFactorEnabled = User.TwoFactorEnabled;
            LockoutEnd = User.LockoutEnd;
            LockoutEnabled = User.LockoutEnabled;
            AccessFailedCount = User.AccessFailedCount;

        }

        public string EmailCopy { get; set; } = null!;
    }
}
