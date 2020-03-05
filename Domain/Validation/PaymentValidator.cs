using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(payment => payment.Order.CreationDate)
                .Must(creationDate => (DateTime.Now - creationDate).TotalDays <= 7)
                .WithMessage("The order has expired and cannot be paid");
        }
    }
}
