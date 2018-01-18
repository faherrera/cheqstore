using CheqStore.DAL;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.Repositories.Products
{
    public class ValidationProduct
    {
        private static CheqStoreContext ctx = new CheqStoreContext();

        public static bool bussyName(string name)
        {
            return ctx.Products.Any(x => x.Name == name);

        }

        public static bool thereIsRecord(int ID)
        {
            return ctx.Products.Any(x => x.ProductID == ID);
        }

        public static Product getRecordFromID(int id)
        {
            Product product = null;

            if (!thereIsRecord(id))
            {
                return product;
            }

            return  product = ctx.Products.Find(id);
            
        }
    }
}