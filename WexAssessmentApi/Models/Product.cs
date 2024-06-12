using System.ComponentModel.DataAnnotations;

namespace WexAssessmentApi.Models
{
    public class Product
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Range(typeof(decimal?), "0.00", "79228162514264337593543950335.00", ErrorMessage = "Price can only be a positive amount.")]
        public decimal? Price { get; set; }
        [Required]
        public string? Category { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative number.")]
        public int? StockQuantity { get; set; }
    }
}
