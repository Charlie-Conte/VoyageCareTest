#nullable disable
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using StaffPortal.Areas.Identity.Pages.Account;
using StaffPortal.Logic.Validators;
using StaffPortal.Models;

namespace StaffPortal.Pages.Account
{
    public partial class UserRegister
    {
        [Inject] private ILogger<RegisterModel> Logger { get; set; }
        [Inject] private IEmailSender EmailSender { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private UserManager<IdentityUser> UserManager { get; set; }


        private UserModelFluentValidator _userModelValidator;
        public MudForm Form;
        public UserModel UserModel = new UserModel();


        protected override Task OnInitializedAsync()
        {
            _userModelValidator = new UserModelFluentValidator(UserManager);
            return Task.CompletedTask;
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

        private async Task Submit()
        {
            await Form.Validate();

            if (Form.IsValid)
            {
                var user = await RegisterAccount();

                if (user != null)
                {
                    NavigationManager.NavigateTo("/Accounts/Login", true);
                }
            }
        }

        public async Task<IdentityUser> RegisterAccount()
        {
            var user = CreateUser();

            await UserManager.SetUserNameAsync(user, UserModel.UserName);
            await UserManager.SetEmailAsync(user, UserModel.Email);
            var result = await UserManager.CreateAsync(user, UserModel.PasswordHash);

            if (!result.Succeeded) return null;
            Logger.LogInformation("User created a new account with password.");
            return user;
        }
    }
}