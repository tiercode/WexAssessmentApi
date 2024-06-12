using System.ComponentModel.DataAnnotations;
using Domain.Primitives;

namespace Domain.Products
{
    public class Product : Entity
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Range(0, 9999999999999999.99, ErrorMessage = "Price can only be a positive amount.")]
        public decimal Price { get; set; }

        [Required]
        public string? Category { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative number.")]

        public int? StockQuantity { get; set; }
    }
}
