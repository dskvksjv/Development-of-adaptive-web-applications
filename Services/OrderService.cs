using project7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project7.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new List<Order>();

        public OrderService()
        {
            _orders.Add(new Order { Id = 1, CustomerName = "Customer1", ProductName = "Product1", Quantity = 5 });
            _orders.Add(new Order { Id = 2, CustomerName = "Customer2", ProductName = "Product2", Quantity = 10 });
            _orders.Add(new Order { Id = 3, CustomerName = "Customer3", ProductName = "Product3", Quantity = 7 });
            _orders.Add(new Order { Id = 4, CustomerName = "Customer4", ProductName = "Product4", Quantity = 3 });
            _orders.Add(new Order { Id = 5, CustomerName = "Customer5", ProductName = "Product5", Quantity = 9 });
            _orders.Add(new Order { Id = 6, CustomerName = "Customer6", ProductName = "Product5", Quantity = 1 });
            _orders.Add(new Order { Id = 7, CustomerName = "Customer7", ProductName = "Product5", Quantity = 6 });
            _orders.Add(new Order { Id = 8, CustomerName = "Customer8", ProductName = "Product5", Quantity = 3 });
            _orders.Add(new Order { Id = 9, CustomerName = "Customer9", ProductName = "Product5", Quantity = 7 });
            _orders.Add(new Order { Id = 10, CustomerName = "Customer10", ProductName = "Product5", Quantity = 10 });
        }

        public Task<List<Order>> GetOrdersAsync()
        {
            return Task.FromResult(_orders);
        }

        public Task<Order> AddOrderAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            order.Id = _orders.Count + 1;
            _orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<Order> UpdateOrderAsync(int id, Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder != null)
            {
                existingOrder.CustomerName = order.CustomerName;
                existingOrder.ProductName = order.ProductName;
                existingOrder.Quantity = order.Quantity;
                return Task.FromResult(existingOrder);
            }
            return Task.FromResult<Order>(null);
        }

        public Task<bool> DeleteOrderAsync(int id)
        {
            var orderToRemove = _orders.FirstOrDefault(o => o.Id == id);
            if (orderToRemove != null)
            {
                _orders.Remove(orderToRemove);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
