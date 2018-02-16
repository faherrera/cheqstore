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
using CheqStore.Filters;
using CheqStore.Data.Repositories.Categories;

namespace CheqStore.Controllers
{
    [CustomAuthorize("Admin", "SuperAdmin")]
    public class CategoriesController : Controller
    {
        private CheqStoreContext db = new CheqStoreContext();

        // GET: Categories
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(TempData["Message"] as string))
            {
                ViewBag.Message = TempData["Message"] as string;
                TempData["Message"] = null;

            }
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Debe ingresar un ID valido de categoria para editar";

                return RedirectToAction("Index");
            }
            Category category = db.Categories.Find(id);

            if (category == null)
            {
                TempData["Message"] = "Debe ingresar un ID valido de categoria existe para editar";

                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                
                if (db.Categories.Any(x=> x.Name == category.Name && x.CategoryID != category.CategoryID))
                {
                    TempData["Message"] = "No puede repetir el nombre en cheqstore";

                    return RedirectToAction("Index");
                }
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpPost]
        public ActionResult ChangeStatusLogic(int? id)
        {
            try
            {
                if (id == null)
                {

                    TempData["Message"] = "Debe ingresar un ID valido de categoria para cambiar su estado";

                    return RedirectToAction("Index");
                }

                Category category = db.Categories.Find(id);

                RepositoryCategories.UpdateStatusLogic(category); //Actualizo el borrado logico

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
            using (db = new CheqStoreContext())
            {

                try
                {
                    if (id == null)
                    {

                        TempData["Message"] = "Debe ingresar un ID valido de Categoria para eliminar";

                        return RedirectToAction("Index");
                    }

                    Category category = db.Categories.Find(id);

                    if (!RepositoryCategories.DeleteCategory(category))
                    {
                        TempData["Message"] = "No puede eliminar una categoria con productos, debe eliminar o cambiar de categoria antes los productos dependientes";

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
