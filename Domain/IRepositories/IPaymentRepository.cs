using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IPaymentRepository
    {
        Result<Payment> CreatePayment(PaymentDTO paymentDTO, Order order);
    }
}
