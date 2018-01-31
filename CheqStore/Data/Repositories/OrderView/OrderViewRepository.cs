using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.Repositories.OrderView
{
    public class OrderViewRepository
    {
        public static void AddProductToCart(Product product)
        {

            //Saber si es nulo
            if (HttpContext.Current.Session["OrderView"]  == null)
            {
                CreateOrderViewInSession();
            }

            //Recupero el listado de la Orden en sesion, debo castearlo.
            List<ModelNotMapped.OrderView.OrderView> ListOrderView = HttpContext.Current.Session["OrderView"] as List<ModelNotMapped.OrderView.OrderView>;

            //Consulto si existe el producto ID ya en la lista.
            if (ListOrderView.Any(x => x.ProductID == product.ProductID))
            {
                var GetProduct = ListOrderView.Where(x => x.ProductID == product.ProductID).First();

                GetProduct.Quantity += 1; //Sumo una nueva cantidad.
            }else
            {
                var orderView = new ModelNotMapped.OrderView.OrderView() {
                    ProductID = product.ProductID,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                };

                ListOrderView.Add(orderView);
            }

            HttpContext.Current.Session["OrderView"] = ListOrderView;

        }

        /// <summary>
        /// Creo una variable de sesion con el nombre de OrderView;
        /// </summary>
        public static void CreateOrderViewInSession()
        {
            HttpContext.Current.Session["OrderView"] = new List<Data.ModelNotMapped.OrderView.OrderView>();

        }
        
        public static void RemoveProductFromCart(int ProductID)
        {
            //Listado de OrderView
            var ListOrderView = HttpContext.Current.Session["OrderView"] as List<ModelNotMapped.OrderView.OrderView>;

            if (!ListOrderView.Any(x=>x.ProductID == ProductID))
            {
                return;
            }


            var productInList = ListOrderView.Where(x => x.ProductID == ProductID).First();

            if (productInList.Quantity > 1)
            {
                productInList.Quantity -= 1;
            }else
            {
                ListOrderView.RemoveAll(x => x.ProductID == ProductID && x.Quantity == 1);

            }

            var NewOrder = ListOrderView;
            HttpContext.Current.Session["OrderView"] = NewOrder;

        }

    }
}