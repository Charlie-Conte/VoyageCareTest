#nullable disable
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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

        private NewUserModelFluentValidator _newUserModelValidator;
        public MudForm Form;
        public UserModel UserModel = new UserModel();
        [Parameter] public bool NoRedirect { get; set; }

        protected override Task OnInitializedAsync()
        {
            _newUserModelValidator = new NewUserModelFluentValidator(UserManager);
            return Task.CompletedTask;
        }

        private async Task Submit()
        {
            await Form.Validate();

            if (Form.IsValid)
            {
                var user = await AccountManager.RegisterUser(UserModel);

                if (user != null)
                {
                    if (!NoRedirect)
                    {
                        NavigationManager.NavigateTo("/Identity/Account/Login", true);
                    }

                    NavigationManager.NavigateTo(NavigationManager.Uri, true);
                }
            }
        }
    }
}