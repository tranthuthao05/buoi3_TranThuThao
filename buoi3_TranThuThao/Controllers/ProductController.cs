
using Microsoft.AspNetCore.Mvc;
using buoi3_TranThuThao.Models;
using System.Collections.Generic;
using System.Linq;

namespace buoi3_TranThuThao.Controllers
{
    public class ProductController : Controller
    {
        // Danh sách Product mẫu
        // Category được lấy từ CategoryController
        public static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 100,
                Description = "San pham so 1",
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Product 2",
                Price = 200,
                Description = "San pham so 2",
                CategoryId = 2
            },
            new Product
            {
                Id = 3,
                Name = "Product 3",
                Price = 300,
                Description = "San pham so 3",
                CategoryId = 1
            }
        };
        // Số sản phẩm hiển thị trên mỗi trang
        private const int PageSize = 3;
        // GET: Product/Index

        // GET: Product/Index

        // GET: Product/Index
        public ActionResult Index(string searchString, int page = 1)
        {
            // Liên kết Product với Category
            foreach (var p in products)
            {
                p.Category = CategoryController.categories
                    .FirstOrDefault(c => c.Id == p.CategoryId);
            }

            // Lấy danh sách sản phẩm
            var productList = products.AsQueryable();

            // Tìm kiếm sản phẩm
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                string keyword = searchString.Trim();

                productList = productList.Where(p =>
                    p.Id.ToString().Contains(keyword) ||
                    p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    (p.Description != null &&
                     p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Category != null &&
                     p.Category.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                );
            }

            // Tổng số sản phẩm sau khi tìm kiếm
            int totalProducts = productList.Count();

            // Tính tổng số trang
            int totalPages = (int)Math.Ceiling(
                (double)totalProducts / PageSize
            );

            // Luôn có ít nhất 1 trang để hiển thị
            totalPages = Math.Max(1, totalPages);

            // Kiểm tra số trang hợp lệ
            page = Math.Max(1, Math.Min(page, totalPages));

            // Lấy sản phẩm theo từng trang
            var pagedProducts = productList
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // Truyền dữ liệu sang View
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalProducts = totalProducts;

            return View(pagedProducts);
        }



        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            product.Category = CategoryController.categories
                .FirstOrDefault(c => c.Id == product.CategoryId);

            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.Categories = CategoryController.categories;

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Category");

            bool categoryExists = CategoryController.categories
                .Any(c => c.Id == product.CategoryId);

            if (!categoryExists)
            {
                ModelState.AddModelError(
                    "CategoryId",
                    "Vui lòng chọn danh mục hợp lệ"
                );
            }

            if (ModelState.IsValid)
            {
                int newId = products.Any()
                    ? products.Max(p => p.Id) + 1
                    : 1;

                product.Id = newId;

                products.Add(product);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = CategoryController.categories;

            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            ViewBag.Categories = CategoryController.categories;

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            ModelState.Remove("Category");

            bool categoryExists = CategoryController.categories
                .Any(c => c.Id == product.CategoryId);

            if (!categoryExists)
            {
                ModelState.AddModelError(
                    "CategoryId",
                    "Vui lòng chọn danh mục hợp lệ"
                );
            }

            if (ModelState.IsValid)
            {
                var existingProduct = products
                    .FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct == null)
                    return NotFound();

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = CategoryController.categories;

            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            product.Category = CategoryController.categories
                .FirstOrDefault(c => c.Id == product.CategoryId);

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
