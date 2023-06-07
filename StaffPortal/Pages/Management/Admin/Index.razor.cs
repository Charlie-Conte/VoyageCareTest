#nullable disable
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using StaffPortal.Logic;

namespace StaffPortal.Pages.Management.Admin
{
    public partial class Index
    {
        [Inject] private ILogInAccountManager<IdentityUser> AccountManager { get; set; }

        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
        string ADMINISTRATION_ROLE = "Administrators";
        System.Security.Claims.ClaimsPrincipal CurrentUser;

        protected override async Task OnInitializedAsync()
        {
            // ensure there is a ADMINISTRATION_ROLE
            var RoleResult = await _RoleManager.FindByNameAsync(ADMINISTRATION_ROLE);
            if (RoleResult == null)
            {
                // Create ADMINISTRATION_ROLE Role
                await _RoleManager.CreateAsync(new IdentityRole(ADMINISTRATION_ROLE));
            }

            // Ensure a user named admin@voyagecare.com is an Administrator
            var user = await _UserManager.FindByNameAsync("admin@voyagecare.com");
            if (user != null)
            {
                // Is admin@voyagecare.com in administrator role?
                var UserResult = await _UserManager.IsInRoleAsync(user, ADMINISTRATION_ROLE);
                if (!UserResult)
                {
                    // Put admin in Administrator role
                    await _UserManager.AddToRoleAsync(user, ADMINISTRATION_ROLE);
                }
            }

            // Get the current logged in user
            CurrentUser = (await authenticationStateTask).User;

            GetUsers();
        }

        // Property used to add or edit the currently selected user
        IdentityUser objUser = new IdentityUser();

        // Tracks the selected role for the currently selected user
        string CurrentUserRole { get; set; } = "Users";

        // Collection to display the existing users
        List<IdentityUser> ColUsers = new List<IdentityUser>();

        // Options to display in the roles dropdown when editing a user
        List<string> Options = new List<string>() { "Users", "Administrators" };

        // To hold any possible errors
        string strError = "";

        // To enable showing the Popup
        bool ShowPopup = false;


        void AddNewUser()
        {
            // Make new user
            objUser = new IdentityUser();
            objUser.PasswordHash = "*****";
            // Set Id to blank so we know it is a new record
            objUser.Id = "";
            // Open the Popup
            ShowPopup = true;
        }

        public void GetUsers()
        {
            // clear any error messages
            strError = "";
            // Collection to hold users
            ColUsers = new List<IdentityUser>();
            // get users from _UserManager
            var user = _UserManager.Users.Select(x => new IdentityUser
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PasswordHash = "*****"
            });
            foreach (var item in user)
            {
                ColUsers.Add(item);
            }
        }

        async Task SaveUser()
        {
            try
            {
                // Is this an existing user?
                if (objUser.Id != "")
                {
                    // Get the user
                    var user = await _UserManager.FindByIdAsync(objUser.Id);
                    // Update Email
                    user.Email = objUser.Email;
                    // Update the user
                    await _UserManager.UpdateAsync(user);
                    // Only update password if the current value
                    // is not the default value
                    if (objUser.PasswordHash != "*****")
                    {
                        var resetToken =
                            await _UserManager.GeneratePasswordResetTokenAsync(user);
                        var passworduser =
                            await _UserManager.ResetPasswordAsync(
                                user,
                                resetToken,
                                objUser.PasswordHash);
                        if (!passworduser.Succeeded)
                        {
                            if (passworduser.Errors.FirstOrDefault() != null)
                            {
                                strError =
                                    passworduser
                                        .Errors
                                        .FirstOrDefault()
                                        .Description;
                            }
                            else
                            {
                                strError = "Pasword error";
                            }

                            // Keep the popup opened
                            return;
                        }
                    }

                    // Handle Roles
                    // Is user in administrator role?
                    var UserResult =
                        await _UserManager
                            .IsInRoleAsync(user, ADMINISTRATION_ROLE);
                    // Is Administrator role selected 
                    // but user is not an Administrator?
                    if (
                        (CurrentUserRole == ADMINISTRATION_ROLE)
                        &
                        (!UserResult))
                    {
                        // Put admin in Administrator role
                        await _UserManager
                            .AddToRoleAsync(user, ADMINISTRATION_ROLE);
                    }
                    else
                    {
                        // Is Administrator role not selected 
                        // but user is an Administrator?
                        if ((CurrentUserRole != ADMINISTRATION_ROLE)
                            &
                            (UserResult))
                        {
                            // Remove user from Administrator role
                            await _UserManager
                                .RemoveFromRoleAsync(user, ADMINISTRATION_ROLE);
                        }
                    }
                }
                else
                {
                    // Insert new user
                    var newUser =
                        new IdentityUser
                        {
                            UserName = objUser.UserName,
                            Email = objUser.Email,
                            PasswordHash = objUser.PasswordHash
                        };
                    var createdUser =
                        await AccountManager.RegisterUser(newUser);
                    if (createdUser == null)
                    {
                        strError = "Create error";

                        // Keep the popup opened
                        return;
                    }
                    else
                    {
                        // Handle Roles
                        if (CurrentUserRole == ADMINISTRATION_ROLE)
                        {
                            // Put admin in Administrator role
                            await _UserManager
                                .AddToRoleAsync(newUser, ADMINISTRATION_ROLE);
                        }
                    }
                }

                // Close the Popup
                ShowPopup = false;
                // Refresh Users
                GetUsers();
            }
            catch (Exception ex)
            {
                strError = ex.GetBaseException().Message;
            }
        }

        async Task EditUser(IdentityUser _IdentityUser)
        {
            // Set the selected user
            // as the current user
            objUser = _IdentityUser;
            // Get the user
            var user = await _UserManager.FindByIdAsync(objUser.Id);
            if (user != null)
            {
                // Is user in administrator role?
                var UserResult =
                    await _UserManager
                        .IsInRoleAsync(user, ADMINISTRATION_ROLE);
                if (UserResult)
                {
                    CurrentUserRole = ADMINISTRATION_ROLE;
                }
                else
                {
                    CurrentUserRole = "Users";
                }
            }

            // Open the Popup
            ShowPopup = true;
        }

        async Task DeleteUser()
        {
            // Close the Popup
            ShowPopup = false;
            // Get the user
            var user = await _UserManager.FindByIdAsync(objUser.Id);
            if (user != null)
            {
                // Delete the user
                await _UserManager.DeleteAsync(user);
            }
            // Refresh Users
            GetUsers();
        }

        void ClosePopup()
        {
            // Close the Popup
            ShowPopup = false;
        }
    }
}