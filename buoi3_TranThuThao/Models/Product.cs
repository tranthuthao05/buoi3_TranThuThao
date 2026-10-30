using System.ComponentModel.DataAnnotations;

namespace buoi3_TranThuThao.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        // --- THÊM PHẦN LIÊN KẾT VỚI CATEGORY ---
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; } // Khóa ngoại trỏ tới Id của Category

        public Category? Category { get; set; } // Thuộc tính điều hướng
    }
}