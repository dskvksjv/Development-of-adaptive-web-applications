using System.Collections.Generic;
using System.Threading.Tasks;
using project7.Model;

namespace project7.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> AddOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(int id, Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
