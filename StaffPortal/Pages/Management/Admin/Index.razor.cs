﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace StaffPortal.Pages.Management.Admin
{
    public partial class Index
    {
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
        }

        async Task EditUser(IdentityUser _IdentityUser)
        {

        }

        async Task DeleteUser()
        {
        }

        void ClosePopup()
        {
            // Close the Popup
            ShowPopup = false;
        }
    }
}