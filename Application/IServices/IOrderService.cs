using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IOrderService
    {
        Result<Order> GetOrderById(long id);

        Result<IEnumerable<Order>> GetAllOrders();

        Result<Order> CreateOrder(OrderDTO orderDTO);

        Result DeleteOrder(long id);
    }
}
