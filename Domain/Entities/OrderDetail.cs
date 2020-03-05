using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OrderDetail : Entity
    {
        public string Product { get; set; }

        public decimal Price { get; set; }

        public uint Count { get; set; }

        public long OrderId { get; set; }

        public Order Order { get; set; }

        public OrderDetail()
        {

        }
        public OrderDetail(string product, decimal price, uint count)
        {
            if (price < 0)
                throw new InvalidOperationException("Price can't be negative");

            Product = product;
            Price = price;
            Count = count;
        }
    }
}
