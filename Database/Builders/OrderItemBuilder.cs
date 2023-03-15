using Database.Interfaces;
using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Builders
{
    public class OrderItemBuilder : IDatabaseModelBuilder<OrderItem>
    {
        OrderItem orderItem;
        public OrderItemBuilder()
        {
            orderItem = new();
        }
        public OrderItemBuilder(OrderItem orderItem)
        {
            this.orderItem = orderItem;
        }
        public OrderItem Build()
        {
            var orderItem = this.orderItem;
            this.orderItem = new();
            return orderItem;
        }
        public OrderItemBuilder SetId(int id)
        {
            this.orderItem.Id = id;
            return this;
        }
        public OrderItemBuilder SetName(string name)
        {
            this.orderItem.Name = name;
            return this;
        }
        public OrderItemBuilder SetQuantity(decimal quantity)
        {
            this.orderItem.Quantity = quantity;
            return this;
        }
        public OrderItemBuilder SetOrderNumber(string orderNumber)
        {
            this.orderItem.OrderNumber = orderNumber;
            return this;
        }
        public OrderItemBuilder SetUnit(string unit)
        {
            this.orderItem.Unit = unit;
            return this;
        }
        public OrderItemBuilder SetOrderId(int orderId)
        {
            this.orderItem.OrderId = orderId;
            return this;
        }
    }
}
