using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using buoi3_TranThuThao.Models;

namespace buoi3_TranThuThao.Controllers
{
    public class ProductController : Controller
    {
        // Danh sách Category tĩnh (để làm nguồn chọn cho Product)
        private static List<Category> categories = new List<Category>()
        {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" },
            new Category { Id = 3, Name = "Category 3" }
        };

        // Danh sách Product mẫu có gán sẵn CategoryId
        private static List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Product 1", Price = 100, CategoryId = 1 },
            new Product { Id = 2, Name = "Product 2", Price = 200, CategoryId = 2 },
            new Product { Id = 3, Name = "Product 3", Price = 300, CategoryId = 1 }
        };

        // GET: Product/Index
        public ActionResult Index()
        {
            // Gán thông tin Category vào từng Product để hiển thị tên danh mục ra bảng
            foreach (var p in products)
            {
                p.Category = categories.FirstOrDefault(c => c.Id == p.CategoryId);
            }
            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            // Truyền danh sách Category sang View để làm thẻ <select> (Dropdown)
            ViewBag.Categories = categories;
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Product product)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                int newId = products.Any() ? products.Max(p => p.Id) + 1 : 1;
                product.Id = newId;

                products.Add(product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = categories; // Gán lại nếu có lỗi xảy ra để form không bị mất dữ liệu dropdown
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            ViewBag.Categories = categories;
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.CategoryId = product.CategoryId; // Cập nhật lại danh mục
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = categories;
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}