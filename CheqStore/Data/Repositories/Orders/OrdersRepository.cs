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

        public static void StoreOrder(CheqStoreContext ctx,Order order)
        {
            ctx.Orders.Add(order);
            ctx.SaveChanges();
        }

        public static void UpdateOrder(Order order)
        {
            //Aqui debo poner que reciba un cambio en el estado y un cambio en la fecha de confirmacion.
        }

        public static void UpdateTotal(CheqStoreContext ctx,int OrderID,decimal Quantity) {
           

            Order order = ctx.Orders.Find(OrderID);

            order.Total = Quantity;

            ctx.Entry(order).State = System.Data.Entity.EntityState.Modified;

            ctx.SaveChanges();
        }

        public static void DeleteOrder(int OrderID)
        {
            using (ctx = new CheqStoreContext())
            {
                Order order = ctx.Orders.Find(OrderID);

                ctx.Entry(order).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();

            }
        }
    }
}