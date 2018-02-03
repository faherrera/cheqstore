using CheqStore.DAL;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.Repositories.Orders
{
    public class OrdersRepository
    {
        private static CheqStoreContext ctx;

        public static void StoreOrder(Order order)
        {
            ctx = new CheqStoreContext();

            order.OrderDate = DateTime.Now;
            order.Status = false;
            order.CreatedAt = DateTime.Now;
            ctx.Orders.Add(order);
            ctx.SaveChanges();
        }

        public static void PutOrder(Order order)
        {
            //Aqui debo poner que reciba un cambio en el estado y un cambio en la fecha de confirmacion.
        }


    }
}