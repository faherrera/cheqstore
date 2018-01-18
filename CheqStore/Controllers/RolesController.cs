using CheqStore.DAL;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Controllers
{
    public class RolesController : Controller
    {
        private CheqStoreContext ctx = new CheqStoreContext();
        // GET: Roles
        public ActionResult Index()
        {
            return View(ctx.Roles.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                rol.CreatedAt = DateTime.Now;
                ctx.Roles.Add(rol);
                ctx.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(rol);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rol rol = ctx.Roles.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }

            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Rol rol)
        {
            if (rol == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            if (ModelState.IsValid) {

                ctx.Entry(rol).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(rol);
        }
    }
}