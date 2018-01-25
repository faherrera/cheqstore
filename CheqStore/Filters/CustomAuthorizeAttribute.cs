using CheqStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheqStore.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private CheqStoreContext ctx = new CheqStoreContext();

        private string rol;
        private bool authorize = false;

        public CustomAuthorizeAttribute( string rol)
        {
            this.rol = rol; 
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] != null)
            {
                string username = filterContext.HttpContext.Session["Username"] as string;

                authorize = ctx.Users.Any(x => x.Username == username && x.Rol.Name == this.rol);
            }
            
            if (!authorize) //Si no esta autorizado
            {

                base.OnAuthorization(filterContext);
                
                
                filterContext.RequestContext.HttpContext.Response.Redirect("/Login", true);
            }
        }

    }
}