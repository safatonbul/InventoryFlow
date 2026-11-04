using System.ComponentModel.DataAnnotations;
namespace InventoryFlow.DTOs
{
    public class CreateProductRequest
    {
        [Key]
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@".*\S.*", ErrorMessage = "Name cannot be whitespace")]
        public string Name { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
