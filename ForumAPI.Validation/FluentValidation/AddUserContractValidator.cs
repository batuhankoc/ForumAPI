using FluentValidation;
using ForumAPI.Contract.UserContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Validation.FluentValidation
{
    public class AddUserContractValidator : AbstractValidator<AddUserContract>  
    {
        public AddUserContractValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("{PropertyName} empty olmamalıdır").NotNull().WithMessage("{PropertyName} null olmamalıdır gereklidir");
            RuleFor(u => u.Password).NotEmpty().NotNull().MinimumLength(6).MaximumLength(50);
            RuleFor(u => u.ConfirmPassword).NotEmpty().NotNull().Equal(u => u.Password).WithMessage("Girdiğiniz şifreler uyuşmuyor.");
            RuleFor(u => u.Email).NotNull().EmailAddress();
        }

    }
}
