using CheqStore.DAL;
using CheqStore.Data.Repositories.OrderView;
using CheqStore.Filters;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Controllers
{
    [CustomAuthorize("Cliente")]
    public class OrderViewController : Controller
    {
        private CheqStoreContext ctx = new CheqStoreContext();
        // GET: OrderView
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(TempData["Message"] as string))
            {
                ViewBag.Message = TempData["Message"] as string;

                TempData.Remove("Message");
            }

            ViewBag.ListOrderView = null;
            if (Session["OrderView"] == null)
            {
                return View();
            }

            ViewBag.ListOrderView  = Session["OrderView"] as List<Data.ModelNotMapped.OrderView.OrderView>;
            
            return View();
        }


        [HttpPost]
        public ActionResult  AddProduct(Product product)
        {
            try
            {
                Product getProduct = ctx.Products.Find(product.ProductID);

                if (getProduct == null)
                {
                    TempData["Message"] = "No existe el producto que está intentando agregar";

                    return RedirectToAction("Index", "Product");
                }

                var res = OrderViewRepository.AddProductToCart(getProduct);

                if (!res.status)
                {
                    TempData["Message"] = res.message; 
                    return RedirectToAction("Index", "Products");
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                TempData["Message"] = "No pudo cargarse el producto, intente nuevamente por favor -> "+e.Message;

                return RedirectToAction("Index", "Product");
            }

          
        }

        [HttpPost]
        public ActionResult RemoveProduct()
        {
            var ProductID = int.Parse(Request["ProductID"]); 

            if (ProductID == 0)
            {
                TempData["Message"] = "Debe eliminar un producto valido";

                return RedirectToAction("Index");
            }

            try
            {
                OrderViewRepository.RemoveProductFromCart(ProductID);

            }
            catch (Exception e)
            {
                TempData["Message"] = "Por favor recargue e intente nuevamente ->   "+e.Message;

            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult CancelOrder()
        {
            try
            {
                OrderViewRepository.OrderViewSessionEmpty();
                TempData["Message"] = "Orden cancelada correctamente";
                return RedirectToAction("Index","Products");
            }
            catch (Exception e)
            {
                TempData["Message"] = "No pudimos cancelar la orden, el error fue,  ->   " + e.Message;

                return RedirectToAction("Index");
            }
        } 

    }
}