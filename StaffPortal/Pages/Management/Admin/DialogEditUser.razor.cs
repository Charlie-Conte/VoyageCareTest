using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using StaffPortal.Logic;
using StaffPortal.Logic.Validators;
using StaffPortal.Models;

namespace StaffPortal.Pages.Management.Admin
{
    public partial class DialogEditUser
    {
        
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = null!;
        [Parameter] public IdentityUser User { get; set; } = new IdentityUser();
        [Parameter] public bool DisableForm { get; set; }
        [Inject] private UserManager<IdentityUser> UserManager { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ILogInAccountManager<IdentityUser> AccountManager { get; set; }


        string ADMINISTRATION_ROLE = "Administrators";


        public MudForm FormTab1;
        private bool unknownError = false;
        public UserModel UserModel;
        private bool setUserAsAdmin;


        void Close() => MudDialog.Close(DialogResult.Ok(true));

        private EditUserModelFluentValidator _editUserModelValidator;

        protected override async Task OnInitializedAsync()
        {
            _editUserModelValidator = new EditUserModelFluentValidator(UserManager, User.Id);

            UserModel = new UserModel(User);
            setUserAsAdmin = await AccountManager.GetIsUserAdminById(User.Id);
        }
        private async Task Submit()
        {
            await FormTab1.Validate();

            if (FormTab1.IsValid)
            {

                var userData = await AccountManager.GetUserById(UserModel.Id);
                bool success;

                userData.UserName = UserModel.UserName;
                userData.Email = UserModel.Email;
                if (UserModel.PasswordHash != "*****")
                {
                    success = await AccountManager.UpdateUserPassword(userData, UserModel.PasswordHash);
                }
                else
                {
                    success = await AccountManager.UpdateUser(userData);
                }

                

                if (success)
                {
                    await AccountManager.SetUserAdminStatusById(userData.Id, setUserAsAdmin);
                    NavigationManager.NavigateTo(NavigationManager.Uri, true);
                }

                unknownError = true;

            }
        }
    }
}
