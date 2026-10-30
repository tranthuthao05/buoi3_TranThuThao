using System.ComponentModel.DataAnnotations;

namespace buoi3_TranThuThao.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = string.Empty;

        // Thêm dòng này để 1 Category có thể chứa nhiều Product
        public List<Product>? Products { get; set; }
    }
}