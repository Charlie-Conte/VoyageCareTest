#nullable disable
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace StaffPortal.Logic
{
    public class AspAccountManager : ILogInAccountManager<IdentityUser>
    {
        public AspAccountManager(ILogger<AspAccountManager> logger, 
            UserManager<IdentityUser> userManager)
        {
            Logger = logger;
            UserManager = userManager;
        }

        private ILogger<AspAccountManager> Logger { get; set; }
        private UserManager<IdentityUser> UserManager { get; set; }


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
