using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace CheqStore.Data.Repositories.Miscellanious
{
    public class MultimediaRepository
    {


        public static string uploadImage(HttpPostedFileBase file)
        {

            if (file == null) return "";

            string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();

            file.SaveAs(HostingEnvironment.MapPath("~/Uploads/" + archivo));

            return archivo;
        }
    }
}