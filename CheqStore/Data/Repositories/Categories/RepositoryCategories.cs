using CheqStore.DAL;
using CheqStore.Data.ModelNotMapped.Categories;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CheqStore.Data.Repositories.Categories
{
    public class RepositoryCategories
    {
        static private CheqStoreContext ctx = new CheqStoreContext();


        public List<CategoryAutocomplete> CategoryAutocomplete()
        {
            var Categories = ctx.Categories.ToList();

            List<CategoryAutocomplete> ListAutocomplete = new List<ModelNotMapped.Categories.CategoryAutocomplete>();


            foreach (var item in Categories)
            {
                var CategoryAut = new CategoryAutocomplete()
                {
                    CategoryID = item.CategoryID,
                    Name = item.Name,
                };

                ListAutocomplete.Add(CategoryAut);
            }

            return ListAutocomplete;

        }

        public static void UpdateStatusLogic( Category category )
        {
            using (ctx = new CheqStoreContext())
            {
                category.StatusLogic = !category.StatusLogic;
                ctx.Entry(category).State = EntityState.Modified;
                ctx.SaveChanges();
            }
           
        }

        public static bool DeleteCategory(Category category)
        {
            ctx = new CheqStoreContext();
            bool AnyProductInCategory = ctx.Products.Any(x => x.CategoryID == category.CategoryID);

            if (AnyProductInCategory) return false;

            ctx.Entry(category).State = EntityState.Deleted;
            ctx.SaveChanges();
            return true;
        }
    }
}