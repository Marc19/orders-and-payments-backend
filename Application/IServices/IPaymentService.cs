using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IPaymentService
    {
        Result<Payment> CreatePayment(PaymentDTO paymentDTO);
    }
}
