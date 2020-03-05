using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DB
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var orderEntity = modelBuilder.Entity<Order>();
            orderEntity.ToTable("Order");
            orderEntity.HasKey(o => o.Id);

            orderEntity.Ignore(o => o.TotalAmount);
            orderEntity.Ignore(o => o.TotalNumberOfItems);
            orderEntity.Ignore(o => o.IsPaid);

            //orderEntity
                //.HasOne(o => o.Payment)
                //.WithOne(p => p.Order)
                //.HasForeignKey<Payment>(p => p.OrderId)
                //.OnDelete(DeleteBehavior.Cascade);

            var orderDetailEntity = modelBuilder.Entity<OrderDetail>();
            orderDetailEntity.ToTable("OrderDetail");
            orderDetailEntity.HasKey(o => o.Id);

            orderEntity
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            var paymentEntity = modelBuilder.Entity<Payment>();
            paymentEntity.ToTable("Payment");
            paymentEntity.HasKey(p => p.Id);
        }
    }
}
