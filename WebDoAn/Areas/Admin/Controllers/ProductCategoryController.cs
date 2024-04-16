using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Models;
using WebDoAn.Models.EF;

namespace WebDoAn.Areas.Admin.Controllers
{

    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = db.ProductCategories;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebDoAn.Models.Common.Filter.FilterChar(model.Title);
                db.ProductCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.ProductCategories.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebDoAn.Models.Common.Filter.FilterChar(model.Title);
                db.ProductCategories.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }



        // xóa sản phẩm
        //public ActionResult DeleteCategory(int id)
        //{
        //    var item = db.ProductCategories.Find(id);
        //    return View(item);
        //}
        public ActionResult DeleteCategory(int id)
        {
            // Tìm danh mục theo ID
            var category = db.ProductCategories.Find(id);

            if (category != null)
            {
                // Kiểm tra xem danh mục có sản phẩm liên quan không
                var hasProducts = category.Products.Any();

                if (hasProducts)
                {
                    // Hiển thị thông báo lỗi nếu danh mục có sản phẩm
                    TempData["ErrorMessage"] = "Không thể xóa danh mục có chứa sản phẩm";
                    return View(category); // Quay lại trang chi tiết danh mục với thông báo lỗi
                }

                // Xóa danh mục
                db.ProductCategories.Remove(category);
                db.SaveChanges();

                // Hiển thị thông báo thành công
                TempData["SuccessMessage"] = "Xóa danh mục thành công";
            }
            else
            {
                // Hiển thị thông báo lỗi nếu danh mục không tồn tại
                TempData["ErrorMessage"] = "Danh mục không tồn tại";
            }

            // Chuyển hướng về trang Index
            return RedirectToAction("Index");
        }


    }
}