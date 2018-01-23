using CheqStore.DAL;
using CheqStore.Data.ModelNotMapped.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.Repositories.Categories
{
    public class RepositoryCategories
    {
        private CheqStoreContext ctx = new CheqStoreContext();

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
    }
}