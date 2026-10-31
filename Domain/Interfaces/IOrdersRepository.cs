using Domain.Models;
namespace Domain.Interfaces;

public interface IOrdersRepository
{
    Task AddOrdersAsync(Orders order);
    Task<Orders> GetOrdersByIdAsync(int id);
    Task<List<Orders>> GetOrdersByCustomersIdAsync(int Customer_id);
    Task DeleteOrdersAsync(int id);
    Task<List<Orders>> GetOrdersWithCustomersAsync();
    Task<decimal> GetTotalAmountAsync();
    Task<List<Customers>> GetOrdersCountByCustomersAsync();
    Task<List<Customers>> GetTopThreeCustomersAsync();
}