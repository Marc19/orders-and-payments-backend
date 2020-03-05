using Application.IServices;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRespository;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRespository = orderRepository;
        }
        
        public Result<Payment> CreatePayment(PaymentDTO paymentDTO)
        {
            Result<Order> orderResult = _orderRespository.GetOrderById(paymentDTO.OrderId);

            if (orderResult.IsFailure)
            {
                return Result.Fail<Payment>(orderResult.Error);
            }
            else
            {
                Order order = orderResult.Value;
                return _paymentRepository.CreatePayment(paymentDTO, order);
            }

        }
    }
}
