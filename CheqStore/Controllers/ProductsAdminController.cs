using CheqStore.DAL;
using CheqStore.Data.Repositories.Products;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Controllers
{
    public class ProductsAdminController : Controller
    {
        private CheqStoreContext ctx = new CheqStoreContext();

        // GET: ProductsAdmin
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(TempData["Message"] as string))
            {
                ViewBag.Message = TempData["Message"] as string;
                TempData["Message"] = null;

            }
            return View(ctx.Products.ToList());
        }

        // GET: ProductsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Debe ingresar un ID valido para la orden ";
                return RedirectToAction("Index");
            }

            var product = ctx.Products.Find(id);

            if (product == null)
            {
                TempData["Message"] = "No existe el producto que está solicitando";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: ProductsAdmin/Create
        public ActionResult Create()
        {
            if (!string.IsNullOrEmpty(TempData["Message"] as string))
            {
                ViewBag.Message = TempData["Message"] as string;
                TempData.Remove("Message"); 
            }

            ViewBag.CategoryID = ctx.Categories.ToList();
            return View();
        }

        // POST: ProductsAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                RepositoryProduct.StoreProduct(product, File);

                TempData["Message"] = string.Format("Creado {0} correctamente",product.Name);

                return RedirectToAction("Index");
            }

            TempData["Message"] = string.Format("Error al crear {0}, debe colocar correctamente los datos", product.Name);

            return View(product);
        }

        // GET: ProductsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {

                    TempData["Message"] = "Debe ingresar un ID valido de producto para editar";

                    return RedirectToAction("Index");
                }

                Product product = ctx.Products.Find(id);

                ViewBag.CategoryID = ctx.Categories.ToList();

                if (product == null)
                {
                    TempData["Message"] = "Debe ingresar un ID valido de producto existe para editar";

                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception e )
            {

                TempData["Message"] = "Ocurrio un problema con la edicion, intente nuevamente "+e.Message;

                return RedirectToAction("Index");
            }
           
        }
        // POST: ProductsAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                RepositoryProduct.UpdateProduct(product, file);

                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult ChangeStatusLogic(int? id)
        {
            try
            {
                if (id == null)
                {

                    TempData["Message"] = "Debe ingresar un ID valido de producto para cambiar su estado";

                    return RedirectToAction("Index");
                }

                Product product = ctx.Products.Find(id);

                RepositoryProduct.UpdateStatusLogic(ctx,product); //Actualizo el borrado logico

                TempData["Message"] = "Cambio de estado correcto";

                return RedirectToAction("Index");
            }

            catch (Exception e)
            {
                TempData["Message"] = "Problema al cambiar estado -> " + e.Message;

                return RedirectToAction("Index");
            }
          
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {

                    TempData["Message"] = "Debe ingresar un ID valido de producto para eliminar";

                    return RedirectToAction("Index");
                }

                Product product = ctx.Products.Find(id);

                if (!RepositoryProduct.DeleteProduct(ctx, product))
                {
                    TempData["Message"] = "No puede eliminar un producto que ya fue pedido";

                    return RedirectToAction("Index");
                }

                TempData["Message"] = "Eliminacion correcta";

                return RedirectToAction("Index");
            }

            catch (Exception e)
            {
                TempData["Message"] = "Problema al cambiar estado -> " + e.Message;

                return RedirectToAction("Index");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
