using CheqStore.Data.ModelNotMapped.Login;
using CheqStore.Data.Repositories.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
            
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login( LoginClass loginClass)
        {
            if (!string.IsNullOrEmpty(Session["Username"] as string))
            {
                return RedirectToAction("GoToRoot");
            }
            var repositoryLogin = RepositoryLogin.ProccessingLogin(loginClass);

            if (!repositoryLogin.status)
            {
                ViewBag.Message = repositoryLogin.message;
            }
            TempData["Message"] = "Logueo Correcto";
            return RedirectToAction("Index", "Products");
        }

        public ActionResult GoToRoot()
        {
            return RedirectToAction("Index","Products");
        }
    }
}