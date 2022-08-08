using FluentValidation;
using ForumAPI.Contract.LoginContract;

namespace ForumAPI.Validation.FluentValidation
{
    public class LoginContractValidator : AbstractValidator<UserLoginContract>
    {
        public LoginContractValidator()
        {
            RuleFor(u => u.Email).NotNull().EmailAddress();
            RuleFor(u => u.Password).NotEmpty().NotNull().MinimumLength(6).MaximumLength(50);
        
        }
    }
}
