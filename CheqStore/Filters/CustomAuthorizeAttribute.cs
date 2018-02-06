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

        private Models.Rol rol;
        private bool authorize = false;

        public CustomAuthorizeAttribute( string rol)
        {
            this.rol = (Models.Rol) Enum.Parse(typeof(Models.Rol),rol); //Convierto en rol 
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] != null)
            {
                string username = filterContext.HttpContext.Session["Username"] as string;

                authorize = ctx.Users.Any(x => x.Username == username && x.Rol == rol);
            }
            
            if (!authorize) //Si no esta autorizado
            {

                base.OnAuthorization(filterContext);
                
                //hacer un Switch, en caso de que se solicite admin enviar a un Admin/login en caso de cliente a /login al igual que default
                filterContext.RequestContext.HttpContext.Response.Redirect("/Login", true);
            }
        }

    }
}