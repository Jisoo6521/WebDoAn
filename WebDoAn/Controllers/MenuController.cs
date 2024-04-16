using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Models;

namespace WebDoAn.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = db.Categories.OrderBy(x=>x.Position).ToList();
            return PartialView("_MenuTop",items);
        }
        public ActionResult MenuProductCategory()
        {
            var item = db.ProductCategories.Take(3).ToList();
            return PartialView("_MenuProductCategory",item);
        }
        public ActionResult MenuLeft(int? id)
        {
            if(id != null)
            {
                ViewBag.CateId = id;
            }
            var item = db.ProductCategories.ToList();
            return PartialView("_MenuLeft", item);
        }
        public ActionResult MenuArrivals()
        {
            var item = db.ProductCategories.Take(20).ToList();
            return PartialView("_MenuArrivals", item);
        }
    }
}