using CheqStore.Data.Repositories.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Controllers
{
    public class ApiController : Controller
    {
        RepositoryCategories _repoCat;
        public ApiController()
        {
            _repoCat = new RepositoryCategories();
        }

        // GET: ApiAutocomplete
        public ActionResult GetCategories()
        {
            return Json(_repoCat.CategoryAutocomplete(),JsonRequestBehavior.AllowGet);
        }
    }
}