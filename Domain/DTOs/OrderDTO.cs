using NullGuard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class OrderDetailDTO
    {
        public string Product { get; set; }

        public decimal Price { get; set; }

        public uint Count { get; set; }
    }
    public class OrderDTO
    {
        [AllowNull]
        public ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}
