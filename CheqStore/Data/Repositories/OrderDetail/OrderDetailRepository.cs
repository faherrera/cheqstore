using CheqStore.DAL;
using CheqStore.Data.ModelNotMapped.BaseEntity;
using CheqStore.Data.Repositories.Orders;
using CheqStore.Data.Repositories.OrderView;
using CheqStore.Data.Repositories.Products;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace CheqStore.Data.Repositories
{
    public class OrderDetailRepository
    {
        private static CheqStoreContext ctx = new CheqStoreContext();
        private static ResponseEntity res;
        public static void StoreOrderDetail(OrderDetail orderDetail)
        {
            orderDetail.CreatedAt = DateTime.Now;
            ctx.OrderDetails.Add(orderDetail);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Genera tanto el detalle de la orden como la orden y el descuento del stock. En caso de efectuarse la transaccion devuelve true, en caso contrario false y un mensaje.
        /// </summary>
        /// <returns></returns>
        public static ResponseEntity GenerateOrderDetailWithOrder()
        {
            res = new ResponseEntity();

            using (var trans = ctx.Database.BeginTransaction())
            {

               try
                {
               
                    var ListOrderView = OrderViewRepository.GetListOrderView();

                    Order order = new Order()
                    {
                        UserID = Convert.ToInt32(HttpContext.Current.Session["UserID"]),
                        OrderDate = DateTime.Now,
                        Status = false,
                        CreatedAt = DateTime.Now

                    };

                    
                    OrdersRepository.StoreOrder(ctx,order); //Creando el Order.

                    decimal Total = 0;

                    foreach (var item in ListOrderView)
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            OrderID = order.ID,
                            Price = item.Price,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            Subtotal = item.Subtotal,

                        };

                        if (!RepositoryProduct.SubstractStock(ctx,item.ProductID,item.Quantity) )
                        {
                            trans.Rollback();
                            return res = new ResponseEntity() { status = false, message = "La cantidad que quiere exige el stock del producto, por favor cambie su orden"};
                        }


                        StoreOrderDetail(orderDetail);
                        Total += orderDetail.Subtotal;
                    }
                    OrdersRepository.UpdateTotal(ctx, order.ID, Total); //Actualizo el total.
                    trans.Commit();
                    OrderViewRepository.OrderViewSessionEmpty(); //Dejo vacia la orden
                    return res = new ResponseEntity() { status = true, message = "" };

                }
                catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        trans.Rollback();
                        return res = new ResponseEntity() { status = false, message = "El error fue -> "+e.Message };
                    }


            }

            }

    }


    
}