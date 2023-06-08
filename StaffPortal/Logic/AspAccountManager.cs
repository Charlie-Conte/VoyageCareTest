#nullable disable
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace StaffPortal.Logic
{
    public class AspAccountManager : ILogInAccountManager<IdentityUser>
    {
        const string AdministrationRole = "Administrators";

        public AspAccountManager(ILogger<AspAccountManager> logger, 
            UserManager<IdentityUser> userManager)
        {
            Logger = logger;
            UserManager = userManager;
        }

        private ILogger<AspAccountManager> Logger { get; set; }
        private UserManager<IdentityUser> UserManager { get; set; }

        public async Task<IdentityUser> GetUserById(string userId)
        {
            var result = await UserManager.FindByIdAsync(userId);

            return result;
        }

        public async Task<bool> GetIsUserAdminById(string userId)
        {
            var user = await GetUserById(userId);
            var result = await UserManager.IsInRoleAsync(user,AdministrationRole);
            return result;
        }

        public async Task SetUserAdminStatusById(string userId, bool setAsAdmin)
        {
            var user = await GetUserById(userId);
            if (setAsAdmin)
                await UserManager.AddToRoleAsync(user, AdministrationRole);
            else await UserManager.RemoveFromRoleAsync(user, AdministrationRole);
        }

        public async Task<bool> UpdateUser(IdentityUser user)
        {
            var result = await UserManager.UpdateAsync(user);
            
            if (!result.Succeeded)
            {
                Logger.LogError("User failed to update account.");

                return false;
            }
            Logger.LogInformation("User updated.");
            return true;
        }  
        
        public async Task<bool> UpdateUserPassword(IdentityUser user, string newPassword)
        {

            user.PasswordHash = UserManager.PasswordHasher.HashPassword(user, newPassword);
            var result = await UserManager.UpdateAsync(user);
            
            if (!result.Succeeded)
            {
                Logger.LogError("User failed to update account password.");

                return false;
            }
            Logger.LogInformation("User password updated.");
            return true;
        } 
        
        public async Task<bool> DeleteUser(IdentityUser user)
        {
            var result = await UserManager.DeleteAsync(user);
            
            if (!result.Succeeded)
            {
                Logger.LogError("User failed to delete a account.");

                return false;
            }
            Logger.LogInformation("User deleted.");
            return true;
        }

        public async Task<IdentityUser> RegisterUser(IdentityUser userToCreate)
        {
            var newUser = CreateUser();

            await UserManager.SetUserNameAsync(newUser, userToCreate.UserName);
            await UserManager.SetEmailAsync(newUser, userToCreate.Email);
            var result = await UserManager.CreateAsync(newUser, userToCreate.PasswordHash);

            if (!result.Succeeded)
            {
                Logger.LogError("User failed to create a new account with password.");

                return null;
            }
            Logger.LogInformation("User created a new account with password.");
            return newUser;
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                                                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }


    }
}
