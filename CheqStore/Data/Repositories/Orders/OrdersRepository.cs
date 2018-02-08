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

        public static void UpdateOrder(CheqStoreContext ctx,Order order)
        {
            using (ctx)
            {
                order.Status = !order.Status;

                if (order.Status)
                {
                    order.ConfirmedDate = DateTime.Now;

                }else
                {
                    order.ConfirmedDate = null;
                }

                ctx.Entry(order).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

            }
        }

        public static void UpdateTotal(CheqStoreContext ctx,int OrderID,decimal Total) {
           

            Order order = ctx.Orders.Find(OrderID);

            order.Total = Total;

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

        public static void UpdateStatus(int OrderID)
        {
            ctx = new CheqStoreContext();
            var Order = ctx.Orders.Find(OrderID);

            UpdateOrder(ctx,Order);


        }

        public static Order GetByID(int id) {
            ctx = new CheqStoreContext();

            return ctx.Orders.FirstOrDefault(x=> x.ID == id);
        }
    }
}