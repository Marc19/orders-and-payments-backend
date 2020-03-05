using Domain.Common;
using NullGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Order : AggregateRoot
    {
        public DateTime CreationDate { get; private set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; private set; }
        
        [AllowNull]
        public virtual Payment Payment { get; private set; }
        
        public bool IsPaid => Payment == null ? false : true;

        public decimal TotalAmount => OrderDetails.Select(od => od.Price * od.Count).Sum();

        public int TotalNumberOfItems => OrderDetails.Select(od => (int) od.Count).Sum();

        public Order()
        {

        }
        public Order(ICollection<OrderDetail> orderDetails)
        {
            CreationDate = DateTime.Now;
            OrderDetails = orderDetails;
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            if(orderDetail != null)
            {
                OrderDetails.Add(orderDetail);
            }
        }

        public void EditOrderDetail(int id, OrderDetail orderDetail)
        {
            OrderDetail orderDetailToEdit = OrderDetails.SingleOrDefault(od => od.Id == id);

            if (orderDetailToEdit != null)
            {
                orderDetailToEdit.Product = orderDetail.Product;
                orderDetailToEdit.Price = orderDetail.Price;
                orderDetail.Count = orderDetail.Count;
            }
        }

        public void RemoveOrderDetail(int id)
        {
            OrderDetail orderDetailToRemove = OrderDetails.SingleOrDefault(od => od.Id == id);

            if(orderDetailToRemove != null)
            {
                OrderDetails.Remove(orderDetailToRemove);
            }
        }

        public string IsValid()
        {
            if (TotalAmount < 100)
                return "Total amount paid can't be less than 100";

            if (TotalAmount > 3000)
                return "Total amount paid can't be greater than 3000";

            if (TotalNumberOfItems < 2)
                return "Total number of items has to be at least 2";

            return string.Empty;
        }
    }
}
