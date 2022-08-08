using FluentValidation;
using ForumAPI.Contract.AnswerContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Validation.FluentValidation
{
    public class AddAnswerContractValidator : AbstractValidator<AddAnswerContract>
    {
        public AddAnswerContractValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("{PropertyName} empty olmamalıdır").NotNull().WithMessage("{PropertyName} null olmamalıdır gereklidir");
            RuleFor(u => u.QuestionId).NotEmpty().WithMessage("{PropertyName} empty olmamalıdır").NotNull().WithMessage("{PropertyName} empty olmamalıdır");
            RuleFor(u => u.Content).NotNull().NotEmpty().WithMessage("{PropertyName} null olmamalıdır gereklidir");

        }
        

    }
}
