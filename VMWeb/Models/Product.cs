using System.ComponentModel.DataAnnotations;

namespace VMWeb.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }

    }
}
