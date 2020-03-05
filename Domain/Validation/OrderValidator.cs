using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.TotalNumberOfItems).GreaterThanOrEqualTo(2);
            RuleFor(order => order.TotalAmount).GreaterThanOrEqualTo(100).LessThanOrEqualTo(3000);
            RuleFor(order => order.OrderDetails).NotNull();
        }
    }
}
