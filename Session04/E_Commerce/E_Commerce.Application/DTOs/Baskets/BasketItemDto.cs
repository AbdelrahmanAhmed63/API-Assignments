using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.Baskets;

public class BasketItemDto
{
    [Required(ErrorMessage = "Product Id is required")]
    public string Id { get; set; } = default!;
    [Required(ErrorMessage = "Product Name is required")]
    public string ProductName { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    [Range(1, 50, ErrorMessage = "Quantity must be between 1 and 50")]
    public int Quantity { get; set; }
    [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
}