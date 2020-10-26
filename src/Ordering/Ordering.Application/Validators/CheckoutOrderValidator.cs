using FluentValidation;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Validators
{
    public class CheckoutOrderValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
