using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using StaffPortal.Areas.Identity.Pages.Account;
using StaffPortal.Logic;


namespace StaffPortal.Pages.Account
{
    public partial class UserRegister
    {
        [Inject] private ILogger<RegisterModel> Logger { get; set; }
        [Inject] private IEmailSender EmailSender { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }


        protected override Task OnInitializedAsync()
        {
            userModelValidator = new UserModelFluentValidator(UserManager);
            return Task.CompletedTask;
        }

        public MudForm form;
        public UserModel userModel = new UserModel();

        private UserModelFluentValidator userModelValidator;


        public class UserModel : IdentityUser
        {
            public string EmailCopy { get; set; } = null!;
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
            await form.Validate();

            if (form.IsValid)
            {
                await RegisterAccount();
            }
        }

        public async Task RegisterAccount()
        {
            var returnUrl = NavigationManager.Uri;
            var user = CreateUser();

            await UserManager.SetUserNameAsync(user, userModel.UserName);
            await UserManager.SetEmailAsync(user, userModel.Email);
            var result = await UserManager.CreateAsync(user, userModel.PasswordHash);

            if (result.Succeeded)
            {
                Logger.LogInformation("User created a new account with password.");

                var userId = await UserManager.GetUserIdAsync(user);
                var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                //    protocol: Request.Scheme);
                //var queryString = new Dictionary<string, string>
                //{
                //    { "area", "Identity" },
                //    { "userId", userId },
                //    { "code", code },
                //    { "returnUrl", returnUrl }
                //};
                //var callbackUrl = QueryHelpers.AddQueryString(NavigationManager.BaseUri + "Account/ConfirmEmail",
                //    queryString!);

                //await EmailSender.SendEmailAsync(user.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (UserManager.Options.SignIn.RequireConfirmedAccount)
                {
                    //queryString = new Dictionary<string, string>
                    //{
                    //    { "email", user.Email },
                    //    { "returnUrl", returnUrl }
                    //};
                    //var registerUri =
                    //    QueryHelpers.AddQueryString(NavigationManager.BaseUri + "Account/RegisterConfirmation",
                    //        queryString!);
                    //NavigationManager.NavigateTo(registerUri);
                }
                else
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    NavigationManager.NavigateTo(returnUrl);
                }
            }

            NavigationManager.NavigateTo(returnUrl);
        }

        public class UserModelFluentValidator : AbstractValidator<UserModel>
        {
            public UserModelFluentValidator(UserManager<IdentityUser> userManager)
            {
                RuleFor(x => x.UserName)
                    .NotEmpty()
                    .Length(1, 100);

                RuleFor(x => x.Email)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .EmailAddress()
                    .MustAsync(async (value, cancellationToken) => await userManager.FindByEmailAsync(value) == null);

                RuleFor(x => x.EmailCopy)
                    .NotEmpty()
                    .EmailAddress()
                    .Matches(x => x.Email);

                RuleFor(x => x.PasswordHash)
                    .NotEmpty().WithMessage("Password must not be empty.")
                    .MinimumLength(9).WithMessage("Password must be longer than 8 characters.")
                    .Matches("[A-Z]").WithMessage("Password must contain one or more capital letters.")
                    .Matches("[a-z]").WithMessage("Password must contain one or more lowercase letters.")
                    .Matches(@"\d").WithMessage("Password must contain one or more digits.")
                    .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Password must contain one or more special characters.")
                    .Matches("^[^£# “”]*$").WithMessage("Password must not contain the following characters £ # “” or spaces.");

            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result =
                    await ValidateAsync(ValidationContext<UserModel>.CreateWithOptions((UserModel)model,
                        x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }
    }
}