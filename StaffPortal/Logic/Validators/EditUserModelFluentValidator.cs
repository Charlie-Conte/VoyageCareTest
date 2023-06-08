using FluentValidation;
using Microsoft.AspNetCore.Identity;
using StaffPortal.Models;

namespace StaffPortal.Logic.Validators
{
    public class EditUserModelFluentValidator : AbstractValidator<UserModel>
    {
        public EditUserModelFluentValidator(UserManager<IdentityUser> userManager, string userId)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MustAsync(async (value, cancellationToken) =>
                {
                    IdentityUser identityUser = await userManager.FindByNameAsync(value);
                    return identityUser == null || identityUser.Id == userId;
                })
                .WithMessage("UserName must be unique and not assigned to another account")
                .Length(1, 100);

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (value, cancellationToken) =>
                {
                    IdentityUser userFound = await userManager.FindByEmailAsync(value);
                    return  userFound == null || userFound.Id == userId;
                })
                .WithMessage("Email address must be unique and not assigned to another account");

            RuleFor(x => x.EmailCopy)
                .NotEmpty()
                .EmailAddress()
                .Matches(x => x.Email)
                .WithMessage("Emails must match");

            RuleFor(x => x.PasswordHash).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.PasswordHash)
                    .NotEmpty().WithMessage("Password must not be empty.")
                    .MinimumLength(9).WithMessage("Password must be longer than 8 characters.")
                    .Matches("[A-Z]").WithMessage("Password must contain one or more capital letters.")
                    .Matches("[a-z]").WithMessage("Password must contain one or more lowercase letters.")
                    .Matches(@"\d").WithMessage("Password must contain one or more digits.")
                    .Matches("^[^£# “”]*$")
                    .WithMessage("Password must not contain the following characters £ # “” or spaces.")
                    .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]")
                    .WithMessage("Password must contain one or more special characters.");
            }).Unless(x=> x.PasswordHash == "*****");
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