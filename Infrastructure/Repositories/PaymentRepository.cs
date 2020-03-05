using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.Validation;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OrdersContext _context;
        //private IEnumerable<Payment> Payments => _context.Payments.Include(p => p.Order);
        private DbSet<Payment> PaymentsDbSet => _context.Set<Payment>();

        public PaymentRepository(OrdersContext context)
        {
            _context = context;
        }
        
        public Result<Payment> CreatePayment(PaymentDTO paymentDTO, Order order)
        {
            Payment payment = new Payment(paymentDTO.OrderId);
            payment.Order = order;

            //string failureMessage = payment.IsValid();
            PaymentValidator paymentValidator = new PaymentValidator();
            var validationResult = paymentValidator.Validate(payment);


            //if (failureMessage == string.Empty)
            if (validationResult.IsValid)
            {
                try
                {
                    PaymentsDbSet.Add(payment);
                    _context.SaveChanges();
                    return Result<Payment>.Ok(payment);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Result.Fail<Payment>(ex.Message + " " + ex.InnerException);
                }
            }
            else
                return Result.Fail<Payment>(string.Join(" ", validationResult.Errors));
                //return Result.Fail<Payment>(failureMessage);
        }
    }
}
