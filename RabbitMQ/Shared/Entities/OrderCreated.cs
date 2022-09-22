namespace Shared.Entities;

public interface OrderCreated
{
    int id { get; set; }
    string ProductName { get; set; }
    decimal Price { get; set; }
    int Quantity { get; set; }
}