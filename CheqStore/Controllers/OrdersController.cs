using CheqStore.DAL;
using CheqStore.Data.Repositories.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Controllers
{
    public class OrdersController : Controller
    {
        private CheqStoreContext ctx = new CheqStoreContext();


        // GET: Orders
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(TempData["Message"] as string))
            {
                ViewBag.Message = TempData["Message"] as string;
                TempData["Message"] = null;

            }
            return View(ctx.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var order = OrdersRepository.GetByID(id);
            if (order == null)
            {
                TempData["Message"] = "Debe ingresar un ID valido para la orden ";
                return RedirectToAction("Index");
            }

            ViewBag.OrderDetails = ctx.OrderDetails.Where(x => x.OrderID == id).ToList();
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Delete: Orders/Delete/5
        [HttpPost]
        public void Delete(int id)
        {
            try
            {

                OrdersRepository.DeleteOrder(id);

                
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id)
        {
            try
            {
                OrdersRepository.UpdateStatus(id);
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["Message"] = "Ocurrio un error al intentar cambiar el estado, intente nuevamente -> "+ e.Message;
                return RedirectToAction("Index");

            }

        }
    
    }
}
