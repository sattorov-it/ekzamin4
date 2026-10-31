using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastructure.Repositories;

public class CustomerRepositories : ICustomersRepository
{
    public async Task AddCustomersAsync(Customers customer)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into customers (fullname) values (@fullname)";
        await conn.ExecuteAsync(query, new { fullname = customer.Fullname });
    }

    public async Task<Customers> GetCustomersByIdAsync(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select id, fullname from customers where id = @id";
        var customer = await conn.QuerySingleOrDefaultAsync<Customers>(query, new { id = id });
        return customer;
    }

    public async Task<List<Customers>> GetCustomersAsync()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select id, fullname from customers order by id";
        var customers = await conn.QueryAsync<Customers>(query);
        return customers.ToList();
    }

    public async Task DeleteCustomersAsync(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from customers where id = @id";
        await conn.ExecuteAsync(query, new { id = id });
    }
}