using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastructure.Repositories;

public class OrderRepositories : IOrdersRepository
{
    public async Task AddOrdersAsync(Orders order)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into orders (customer_id, amount) values (@customerId, @amount)";
        await conn.ExecuteAsync(query, new { customerId = order.Customerid, amount = order.Amount });
    }

    public async Task<Orders> GetOrdersByIdAsync(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select id, customer_id as CustomerId, amount, created_at as CreatedAt from orders where id = @id";
        var order = await conn.QuerySingleOrDefaultAsync<Orders>(query, new { id = id });
        return order;
    }

    public async Task<List<Orders>> GetOrdersByCustomersIdAsync(int customerId)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select id, customer_id as CustomerId, amount, created_at as CreatedAt from orders where customer_id = @customerId ORDER BY id";
        var orders = (await conn.QueryAsync<Orders>(query, new { customerId = customerId })).ToList();
        return orders;
    }

    public async Task DeleteOrdersAsync(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from orders where id = @id";
        await conn.ExecuteAsync(query, new { id = id });
    }

    public async Task<List<Orders>> GetOrdersWithCustomersAsync()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = @"select o.id as Id, o.customer_id as CustomerId, c.fullname as CustomerName, o.amount as Amount, o.created_at AS CreatedAt
        from orders o
        join customers c on c.id = o.customer_id
        order by o.id";
        var list = await conn.QueryAsync<Orders>(query);
        return list.ToList();
    }

    public async Task<decimal> GetTotalAmountAsync()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select sum(amount) from orders";
        var totals = await conn.QuerySingleOrDefaultAsync<decimal>(query);
        return totals;
    }

    public async Task<List<Customers>> GetOrdersCountByCustomersAsync()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = @"SELECT c.id AS Id, c.fullname as Fullname, count(o.id)::int as OrdersCount
        from customers c
        left join orders o on o.customer_id = c.id
        group by c.id, c.fullname
        order by c.id";
        var list = await conn.QueryAsync<Customers>(query);
        return list.ToList();
    }

    public async Task<List<Customers>> GetTopThreeCustomersAsync()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = @"select c.id as Id, c.fullname as Fullname, sum(o.amount) as TotalAmount
        from customers c
        join orders o ON o.customer_id = c.id
        group by c.id, c.fullname
        order by TotalAmount desc
        limit 3";
        var list = await conn.QueryAsync<Customers>(query);
        return list.ToList();
    }
}