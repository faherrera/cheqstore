using CheqStore.DAL;
using CheqStore.Data.Repositories.Orders;
using CheqStore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Controllers
{
    [CustomAuthorize("Cliente")]
    public class AccountController : Controller
    {
        private CheqStoreContext ctx = new CheqStoreContext();
        // GET: Account
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(TempData["Message"] as string))
            {
                ViewBag.Message = TempData["Message"] as string;
                TempData.Remove("Message");

            }
            int UserID = Convert.ToInt32(Session["UserID"]);
            return View(ctx.Orders.Where(x=> x.UserID == UserID).ToList());
        }

        [HttpPost]
        public ActionResult CancelOrder(int id)
        {
            try
            {
                OrdersRepository.DeleteOrder(id);

                TempData["Message"] = "Orden eliminada correctamente";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Message"] = "Problema en Orden al intentar eliminar -> "+e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}