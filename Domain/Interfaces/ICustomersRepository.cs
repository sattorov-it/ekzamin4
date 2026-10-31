using Domain.Models;
namespace Domain.Interfaces;

public interface ICustomersRepository
{
    Task AddCustomersAsync(Customers fullName);
    Task<Customers> GetCustomersByIdAsync(int id);
    Task<List<Customers>> GetCustomersAsync();
    Task DeleteCustomersAsync(int id);
}