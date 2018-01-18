using CheqStore.DAL;
using CheqStore.Data.ModelNotMapped.Repositories;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Data.Repositories.Products
{
    public class RepositoryProduct
    {
        static CheqStoreContext ctx = new CheqStoreContext();

        public static void StoreProduct(Product product)
        {
            product.CreatedAt = DateTime.Now;
            ctx.Products.Add(product);
            ctx.SaveChanges();
        }

        public static void UpdateProduct (Product product)
        {
            var OriginalProduct = ValidationProduct.getRecordFromID(product.ProductID);

            if (product.CreatedAt == null )
            {
                product.CreatedAt = OriginalProduct.CreatedAt;
            }

            if (product.PathPhoto == null)
            {
                product.PathPhoto = OriginalProduct.PathPhoto;
            }

            ctx.Entry(product).State = EntityState.Modified;
            ctx.SaveChanges();

        }
    }
}