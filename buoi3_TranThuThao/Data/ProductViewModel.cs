using buoi3_TranThuThao.Models;

namespace buoi3_TranThuThao.Data
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;


        //navigation property to Category
        public String? Category { get; set; }
    }
}
