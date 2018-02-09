using CheqStore.DAL;
using CheqStore.Data.ModelNotMapped.Repositories;
using CheqStore.Data.Repositories.Miscellanious;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Data.Repositories.Products
{
    public class RepositoryProduct
    {
        private static CheqStoreContext ctx;

        public static List<Product> ListProductFromCategory(string Category)
        {
            ctx = new CheqStoreContext();

            if (!ctx.Products.Any(c=> c.Category.Name == Category) || string.IsNullOrEmpty(Category))
            {
                return ctx.Products.Where(p=> p.StatusLogic == true && p.Stock > 0).ToList();
            }

            return ctx.Products.Where(p => p.Category.Name == Category && p.StatusLogic == true && p.Stock > 0).ToList();

        }
        public static void StoreProduct(Product product, HttpPostedFileBase File)
        {
            ctx = new CheqStoreContext();

            product.PathPhoto = MultimediaRepository.uploadImage(File);
            product.CreatedAt = DateTime.Now;
            ctx.Products.Add(product);
            ctx.SaveChanges();
        }

        public static void UpdateProduct (Product product, HttpPostedFileBase File)
        {
            ctx = new CheqStoreContext();

            var OriginalProduct = ValidationProduct.getRecordFromID(product.ProductID);

            if (product.CreatedAt == null )
            {
                product.CreatedAt = OriginalProduct.CreatedAt;
            }

            if (File == null)
            {
                product.PathPhoto = OriginalProduct.PathPhoto;
            }else
            {
                product.PathPhoto = MultimediaRepository.uploadImage(File);
            }

            ctx.Entry(product).State = EntityState.Modified;
            ctx.SaveChanges();

        }

        /// <summary>
        /// Como producto tendrá un borrado logico, para no entorpecer los detalles de la orden y orden detail, esto lo que hará será actualizar el estado e indicar si está visible o no.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="product"></param>
        /// 
        public static void UpdateStatusLogic(CheqStoreContext ctx, Product product)
        {
            product.StatusLogic = !product.StatusLogic;
            ctx.Entry(product).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public static bool DeleteProduct(CheqStoreContext ctx,Product product) {

            bool AnyProductInOrder = ctx.OrderDetails.Any(x=> x.ProductID == product.ProductID);

            if (AnyProductInOrder) return false;

            ctx.Entry(product).State = EntityState.Deleted;
            ctx.SaveChanges();
            return true;
        }

        /// <summary>
        /// Descuento cantidad al producto, en caso de que exceda me devuelve false y no se efectuará la compra, de lo contrario devolverá true.
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public static bool SubstractStock(CheqStoreContext ctx, int ProductID,int Quantity) {
            
                Product product = ctx.Products.Find(ProductID);

                if (!IsThereStockToProcess(ProductID, Quantity))
                {
                    return false;
                }

                try
                {
                    product.Stock -= Quantity;
                    ctx.Entry(product).State = EntityState.Modified;
                    ctx.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("El error es ->" + e.Message);
                    return false;
                }
            

            
            
        }

        /// <summary>
        /// Pregunto si existe la cantidad suficiente como para procesar la solicitud.
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public static bool IsThereStockToProcess(int ProductID, int Quantity) {
            ctx = new CheqStoreContext();
            Product product = ctx.Products.Find(ProductID);

            if (product.Stock - Quantity >= 0)
            {
                return true;
            }

            return false;
        }
    }
}