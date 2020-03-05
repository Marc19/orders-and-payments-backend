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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Result<Order> GetOrderById(long id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public Result<IEnumerable<Order>> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public Result<Order> CreateOrder(OrderDTO orderDTO)
        {
            return _orderRepository.CreateOrder(orderDTO);
        }

        public Result DeleteOrder(long id)
        {
            return _orderRepository.DeleteOrder(id);
        }
    }
}
