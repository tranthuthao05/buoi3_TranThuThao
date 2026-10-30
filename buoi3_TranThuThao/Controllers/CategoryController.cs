using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using buoi3_TranThuThao.Models;

namespace buoi3_TranThuThao.Controllers
{
    public class CategoryController : Controller
    {
        private static List<Category> categories = new List<Category>()
        {
            new Models.Category { Id = 1, Name = "Category 1" },
            new Models.Category { Id = 2, Name = "Category 2" },
            new Models.Category { Id = 3, Name = "Category 3" }
        };

        // GET: Category/Index
        public ActionResult Index()
        {
            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Category/Create (Mở giao diện form tạo mới - Không có [HttpPost])
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create (Xử lý lưu dữ liệu, tự sinh ID và thêm vào danh sách)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Category category)
        {
            // Xóa lỗi validate của Id trước vì Id do hệ thống tự sinh
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                // Tạo id mới = max id hiện tại + 1 (nếu danh sách trống thì bắt đầu từ 1)
                int newId = categories.Any() ? categories.Max(c => c.Id) + 1 : 1;
                category.Id = newId;

                // Thêm category vào danh sách
                categories.Add(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit([FromRoute] int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            ModelState.Remove("Id"); // Bỏ qua validate Id khi chỉnh sửa

            if (ModelState.IsValid)
            {
                var existingCategory = categories.FirstOrDefault(c => c.Id == category.Id);
                if (existingCategory != null)
                {
                    existingCategory.Name = category.Name; // Cập nhật tên mới
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                categories.Remove(category); // Xóa khỏi danh sách
            }
            return RedirectToAction(nameof(Index));
        }
    }
}