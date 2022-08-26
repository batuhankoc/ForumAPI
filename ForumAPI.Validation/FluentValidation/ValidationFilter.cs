using ForumAPI.Contract.ResponseContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Validation.FluentValidation
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.
                    Where(p => p.Value.ValidationState == ModelValidationState.Invalid).
                    ToDictionary(p => p.Key, p => p.Value.Errors.Select(e => e.ErrorMessage)).ToArray();

                context.Result = new BadRequestObjectResult(CustomResponseContract.Fail(errors, HttpStatusCode.NotAcceptable));
                return;

            }
            await next();

        }
    }
}
