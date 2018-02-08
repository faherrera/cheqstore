using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CheqStore.DAL;
using CheqStore.Models;
using CheqStore.Data.Repositories.Products;
using CheqStore.Filters;

namespace CheqStore.Controllers
{
    public class ProductsController : Controller
    {
        private CheqStoreContext db = new CheqStoreContext();

        // GET: Products
        public ActionResult Index(string category)
        {
            ViewBag.Categories = db.Categories.ToList();

            if (!string.IsNullOrEmpty(TempData["Message"] as string))
            {
                ViewBag.Message = TempData["Message"] as string;
                TempData.Remove("Message"); //Limpio el TempData Message.
            }
            var Products = RepositoryProduct.ListProductFromCategory(category);

            return View(Products);
        }



        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
