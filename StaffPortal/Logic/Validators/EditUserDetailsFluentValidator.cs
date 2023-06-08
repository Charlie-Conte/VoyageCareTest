using FluentValidation;
using Microsoft.AspNetCore.Components;
using StaffPortal.DataAccess.Core.Models;
using StaffPortal.Services;

namespace StaffPortal.Logic.Validators
{
    public class EditUserDetailsFluentValidator : AbstractValidator<UserDetail>
    {
        [Inject] private StaffService staffService { get; set; }
        public EditUserDetailsFluentValidator(string userId)
        {
            RuleFor(x => x.Forename)
                .NotEmpty()
                .Matches("^[a-zA-Z]*$")
                .WithMessage("Only a-Z allowed")
                .Length(1, 100);           
            
            RuleFor(x => x.Surname)
                .NotEmpty()
                .Matches("^[a-zA-Z]*$")
                .WithMessage("Only a-Z allowed")
                .Length(1, 100);            
            
            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .Matches("^[a-zA-Z]*$")
                .WithMessage("Only a-Z allowed")
                .Length(1, 100);

            RuleFor(x => x.DateOfBirth)
                .NotNull();    
            
            RuleFor(x => x.AnnualSalary)
                .NotNull();
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result =
                await ValidateAsync(ValidationContext<UserDetail>.CreateWithOptions((UserDetail)model,
                    x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}