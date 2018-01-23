using CheqStore.DAL;
using CheqStore.Data.ModelNotMapped.Repositories;
using CheqStore.Data.Repositories.Miscellanious;
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
        static CheqStoreContext ctx;

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
    }
}