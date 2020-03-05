using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Payment : AggregateRoot
    {
        public virtual Order Order { get; set; }

        public long OrderId { get; set; }

        public DateTime PaymentDate { get; set; }

        public Payment()
        {

        }

        public Payment(long orderId)
        {
            OrderId = orderId;
            PaymentDate = DateTime.Now;
        }

        public string IsValid()
        {
            var difference = (PaymentDate - Order.CreationDate).TotalDays;
            
            if (difference > 7)
                return "The order has expired and cannot be paid";

            return string.Empty;
        }
    }
}
