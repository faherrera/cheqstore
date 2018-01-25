using CheqStore.Data.ModelNotMapped.Register;
using CheqStore.Data.Repositories.Auth;
using CheqStore.Data.Repositories.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult Index(RegisterRequest registerRequest)
        {
            if (registerRequest == null) { ViewBag.Message = "No puede ser nulo el registro"; return View(); } //Devuelvo la vista.

            var StoreRegister = RegisterRepository.StoreRegister(registerRequest);

            if (StoreRegister.status)
            {
                CredentialsRepository.CreateSession(registerRequest.Username);

                return RedirectToAction("Index","Products");
            }

            ViewBag.Message = StoreRegister.message;
             
            return View();
        }
    }
}
