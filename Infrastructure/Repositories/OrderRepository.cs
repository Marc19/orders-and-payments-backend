using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.Validation;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersContext _context;
        private IEnumerable<Order> Orders => _context.Orders.Include(o => o.OrderDetails).Include(o => o.Payment);
        private DbSet<Order> OrdersDbSet => _context.Set<Order>();

        public OrderRepository(OrdersContext context)
        {
            _context = context;
        }
        public Result<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                if (Orders == null)
                    return Result.Fail<IEnumerable<Order>>("Something went wrong");

                return Result.Ok<IEnumerable<Order>>(Orders);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Result.Fail<IEnumerable<Order>>("Something went wrong");
            }
        }

        public Result<Order> GetOrderById(long id)
        {
            try
            {
                //IEnumerable<Order> orders = _context.Set<Order>();
            
                if(id < 0)
                    return Result.Fail<Order>("id value cannot be negative");
            

                Order order = Orders.SingleOrDefault(o => o.Id == id);

                if (order == null)
                    return Result.Fail<Order>("There is no order with this id");

                return Result.Ok<Order>(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Result.Fail<Order>(ex.Message);
            }
        }

        public Result<Order> CreateOrder(OrderDTO orderDTO)
        {
            ICollection<OrderDetail> orderDetails = orderDTO.OrderDetails.Select(od => new OrderDetail(od.Product, od.Price, od.Count)).ToList();
            Order order = new Order(orderDetails);

            //string failureMesasge = order.IsValid();

            OrderValidator orderValidator = new OrderValidator();
            var validationResult = orderValidator.Validate(order);

            //if (failureMesasge == string.Empty)
            if (validationResult.IsValid)
            {
                try
                {
                    OrdersDbSet.Add(order);
                    _context.SaveChanges();
                    return Result<Order>.Ok(order);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Result.Fail<Order>(ex.Message);
                }
            }

            else
                return Result.Fail<Order>(string.Join(" ", validationResult.Errors));
            //return Result.Fail<Order>(failureMesasge);

        }

        public Result DeleteOrder(long id)
        {
            try
            {
                Order orderToBeDeleted = OrdersDbSet.SingleOrDefault(o => o.Id == id);

                if(orderToBeDeleted == null)
                {
                    return Result.Fail("There is no order with this id: " + id);
                }

                OrdersDbSet.Remove(orderToBeDeleted);
                _context.SaveChanges();
                return Result.Ok();
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
