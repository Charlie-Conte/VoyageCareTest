#nullable disable
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.AspNetCore.Identity;
using StaffPortal.Logic;
using StaffPortal.Logic.Validators;
using StaffPortal.Models;

namespace StaffPortal.Pages.Account
{
    public partial class UserRegister
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ILogInAccountManager<IdentityUser> AccountManager { get; set; }
        [Inject] private UserManager<IdentityUser> UserManager { get; set; } = null!;

        private UserModelFluentValidator _userModelValidator;
        public MudForm Form;
        public UserModel UserModel = new UserModel();


        protected override Task OnInitializedAsync()
        {
            _userModelValidator = new UserModelFluentValidator(UserManager);
            return Task.CompletedTask;
        }

        private async Task Submit()
        {
            await Form.Validate();

            if (Form.IsValid)
            {
                var user = await RegisterAccount();

                if (user != null)
                {
                    NavigationManager.NavigateTo("/Identity/Account/Login", true);
                }
            }
        }

        public async Task<IdentityUser> RegisterAccount()
        {
            return await AccountManager.RegisterUser(UserModel);
        }
    }
}