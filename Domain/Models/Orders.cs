namespace Domain.Models;

public class Orders
{
    public int Id { get; set; }
    public int Customerid { get; set; }
    public decimal Amount { get; set; }
    public DateTime Created_at { get; set; }
    public string CustomerName { get; set; } = null!;
}
