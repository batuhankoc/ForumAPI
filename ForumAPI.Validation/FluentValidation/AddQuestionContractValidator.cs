using FluentValidation;
using ForumAPI.Contract.QuestionContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Validation.FluentValidation
{
    public class AddQuestionContractValidator : AbstractValidator<AddQuestionContract>
    {
        public AddQuestionContractValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("{PropertyName} empty olmamalıdır").NotNull().WithMessage("{PropertyName} null olmamalıdır gereklidir");
            RuleFor(u => u.Category).NotEmpty().NotNull().MaximumLength(20);
            RuleFor(u => u.Title).NotEmpty().WithMessage("{PropertyName} empty olmamalıdır").NotNull().WithMessage("{PropertyName} null olmamalıdır gereklidir");
            RuleFor(u => u.Content).NotNull().NotEmpty().WithMessage("{PropertyName} null olmamalıdır gereklidir");
        }

    }
}
