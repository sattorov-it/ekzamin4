namespace Domain.Models;

public class Customers
{
    public int Id { get; set; }
    public string Fullname { get; set; } = null!;
    public int OrdersCount { get; set; }
    public decimal TotalAmount { get; set; }

}
