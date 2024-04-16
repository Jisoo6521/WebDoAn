﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebDoAn.Models;
using WebDoAn.Models.EF;

namespace WebDoAn.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //GET: Products
        public ActionResult Index(string Searchtext, int? giaTu, int? giaDen)
        {

            var items = db.Products.Take(150).ToList();
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(s => s.Title.Contains(Searchtext)).ToList();
            }
            if (giaTu != null)
                items = items.Where(s => s.Price > giaTu).ToList();
            if (giaDen != null)
                items = items.Where(s => s.Price < giaDen).ToList();
            ViewBag.Searchtext = Searchtext;
            return View(items);
        }
        
        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }

            return View(item);
        }
        public ActionResult ProductCategory(string alias, int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }

            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(150).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(150).ToList();
            return PartialView(items);
        }
    }
}