#nullable disable
using Microsoft.AspNetCore.Identity;

namespace StaffPortal.Logic;

public interface ILogInAccountManager<TUser> where TUser : class
{
    Task<TUser> RegisterUser(TUser userToCreate);
    Task<bool> UpdateUser(IdentityUser user);
    Task<bool> UpdateUserPassword(IdentityUser user, string newPassword);
    Task<bool> DeleteUser(IdentityUser user);
    Task<IdentityUser> GetUserById(string userId);
    Task<bool> GetIsUserAdminById(string userId);
    Task SetUserAdminStatusById(string userId, bool setAsAdmin);
}