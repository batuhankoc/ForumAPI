using FluentValidation;
using ForumAPI.Contract.QuestionContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Validation.FluentValidation
{
    public class PaginationContractValidator : AbstractValidator<PaginationContract>
    {
        public PaginationContractValidator()
        {
            RuleFor(p=> p.Page).NotEmpty().NotNull();
            RuleFor(p=> p.PageSize).NotEmpty().NotNull().LessThanOrEqualTo(100);

        }
    }
}
