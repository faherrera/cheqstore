using CheqStore.DAL;
using CheqStore.Models;
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
        private Nullable<Rol> rol2 = null;
        private bool authorize = false;

        public CustomAuthorizeAttribute( string rol1,string rol2 = "")
        {
            this.rol = (Models.Rol) Enum.Parse(typeof(Models.Rol),rol1); //Convierto en rol 
            if (!string.IsNullOrEmpty(rol2))
            {
                this.rol2 = (Models.Rol)Enum.Parse(typeof(Models.Rol), rol2); //Convierto en rol 

            }
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] != null)
            {
                string username = filterContext.HttpContext.Session["Username"] as string;

                if (this.rol2 == null)
                {
                    authorize = ctx.Users.Any(x => x.Username == username && x.Rol == rol);

                }else
                {
                    if (ctx.Users.Any(x => x.Username == username && x.Rol == rol) || ctx.Users.Any(x => x.Username == username && x.Rol == rol2))
                    {
                        authorize = true;
                    }
                }


            }
            
            if (!authorize) //Si no esta autorizado
            {

                base.OnAuthorization(filterContext);

                //hacer un Switch, en caso de que se solicite admin enviar a un Admin/login en caso de cliente a /login al igual que default

                if (this.rol.ToString().ToUpper() != "CLIENTE")
                {
                    filterContext.RequestContext.HttpContext.Response.Redirect("/Admin/Login", true);

                }else
                {

                    filterContext.RequestContext.HttpContext.Response.Redirect("/Login", true);
                }
            }
        }

    }
}