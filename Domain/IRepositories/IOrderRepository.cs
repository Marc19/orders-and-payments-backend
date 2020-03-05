using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        Result<IEnumerable<Order>> GetAllOrders();
        
        Result<Order> GetOrderById(long id);

        Result<Order> CreateOrder(OrderDTO orderDTO);

        Result DeleteOrder(long id);
    }
}
