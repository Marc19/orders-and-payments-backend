using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Application.IServices;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger; 
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(long id)
        {
            Result<Order> orderResult = _orderService.GetOrderById(id);

            if (orderResult.IsFailure)
            {
                _logger.LogError(orderResult.Error);
                return BadRequest(orderResult.Error);
            }

            _logger.LogInformation("Got order with id: {Id}", orderResult.Value.Id);
            return Ok(orderResult.Value);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(long id)
        {
            Result result = _orderService.DeleteOrder(id);

            if (result.IsFailure)
            {
                _logger.LogError(result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Deleted order with id: {Id}", id);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            Result<IEnumerable<Order>> ordersResult = _orderService.GetAllOrders();


            if (ordersResult.IsFailure)
            {
                _logger.LogError(ordersResult.Error);
                return Problem(ordersResult.Error, null, 500);
            }

            _logger.LogInformation("Number of returned orders: {Count}", ordersResult.Value.Count());
            return Ok(ordersResult.Value.ToList());
        }
    
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDTO orderDTO)
        {
            Result<Order> result = _orderService.CreateOrder(orderDTO);

            if(result.IsFailure)
            {
                _logger.LogError("Couldn't create order. Error message: {Error}", result.Error);
                return Problem(null, null, 500, result.Error);
            }
            else
            {
                _logger.LogInformation("Created new order with id: {Id}", result.Value.Id);

                string url = this.GetUrl("/api/orders/" + result.Value.Id);

                return Created(url, result.Value);
            }
        }

    }
}