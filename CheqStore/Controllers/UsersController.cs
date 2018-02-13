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
using CheqStore.Data.Repositories.Users;
using CheqStore.Data.Repositories.Auth;

namespace CheqStore.Controllers
{
    [CustomAuthorize("Admin","SuperAdmin")]
    public class UsersController : Controller
    {
        private CheqStoreContext db = new CheqStoreContext();

        // GET: Users
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(TempData["Message"] as string))
            {
                ViewBag.Message = TempData["Message"] as string;
                TempData.Remove("Message");
            }

            int userID =Convert.ToInt32(Session["UserID"]);
            User user = db.Users.FirstOrDefault(u => u.UserID ==  userID);

            if (user != null)
            {
                if (user.Rol.ToString() == "Admin")
                {
                    return View(db.Users.Where(x => x.Rol.ToString() == "Cliente").ToList());

                }

                return View(db.Users.Where(x => x.Rol.ToString() != "SuperAdmin").ToList());

            }

            return RedirectToAction("Login", "Login");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user.Rol.ToString() == "Cliente")
            {
                ViewBag.Orders = db.Orders.ToList();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( User user)
        {
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                if (CredentialsRepository.busyUsername(user.Username))
                {
                    ModelState.AddModelError("Username", "El usuario ya está ocupado, intente otro por favor.");
                    return View(user);

                }

                if (CredentialsRepository.busyEmail(user.Email))
                {
                    ModelState.AddModelError("Email", "El Email ya está ocupado, intente otro por favor.");
                                        return View(user);

                }

              
                UserRepository.StoreUser(user);
                TempData["Message"] = string.Format(" {0} creado correctamente ", user.Username);
                return RedirectToAction("Index");


            }
            catch (Exception e)
            {
                TempData["Message"] = string.Format(" Ocurrio el error -> {0} || intente nuevamente", e.Message);
                return RedirectToAction("Index");
            }          

        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                if (CredentialsRepository.busyUsername(user.Username,user.UserID))
                {
                    ModelState.AddModelError("Username", "El usuario ya está ocupado, intente otro por favor.");
                    return View(user);
                }

                if (CredentialsRepository.busyEmail(user.Email, user.UserID))
                {
                    ModelState.AddModelError("Email", "El Email ya está ocupado, intente otro por favor.");
                    return View(user);

                }

                UserRepository.UpdateUser(user);
                TempData["Message"] = string.Format(" {0} editado correctamente ", user.Username);
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["Message"] = string.Format(" Ocurrio el error -> {0} || intente nuevamente", e.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult ChangeStatusLogic(int? id)
        {
            CheqStoreContext ctx = new CheqStoreContext();

            try
            {
                if (id == null)
                {

                    TempData["Message"] = "Debe ingresar un ID valido de User para cambiar su estado";

                    return RedirectToAction("Index");
                }

                User user = ctx.Users.Find(id);

                UserRepository.UpdateStatusLogic(user); //Actualizo el borrado logico

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
            CheqStoreContext ctx = new CheqStoreContext();

            try
            {
                if (id == null)
                {

                    TempData["Message"] = "Debe ingresar un ID valido de user para eliminar";

                    return RedirectToAction("Index");
                }

                User user = ctx.Users.Find(id);

                if (!UserRepository.DeleteUser( user))
                {
                    TempData["Message"] = "No puede eliminar un user que ya tenga ordenes pedidas.";

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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
