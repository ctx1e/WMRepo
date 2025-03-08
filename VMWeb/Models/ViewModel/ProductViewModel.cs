using System.ComponentModel.DataAnnotations;

namespace VMWeb.Models.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string Image { get; set; }

        public IFormFile IFFImage { get; set; }
    }
}
