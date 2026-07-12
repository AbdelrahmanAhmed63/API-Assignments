namespace E_Commerce.Domain.Entities.Baskets;

public class CustomerBasket
{
    public string Id { get; set; } = default!; // Created From Frontend Side [GUID]
    public ICollection<BasketItem> Items { get; set; } = [];
}