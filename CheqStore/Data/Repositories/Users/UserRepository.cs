using CheqStore.DAL;
using CheqStore.Data.Repositories.Auth;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CheqStore.Data.Repositories.Users
{
    public class UserRepository
    {
        private static CheqStoreContext ctx;

        public static User GetByID(int ID)
        {
            using (ctx = new CheqStoreContext())
            {
                return ctx.Users.Find(ID);
            }
        }
        public static void StoreUser(User user)
        {
            ctx = new CheqStoreContext();

            user.Password = CredentialsRepository.EncryptingPassword(user.Password);
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        public static void UpdateUser(User user )
        {
            User originalUser = GetByID(user.UserID);
            string Password = originalUser.Password;
            using (ctx = new CheqStoreContext())
            {
                user.Password = (string.IsNullOrWhiteSpace(user.Password)) ? Password: CredentialsRepository.EncryptingPassword(user.Password);

                ctx.Entry(user).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }

        }

        /// <summary>
        /// Como User tendrá un borrado logico, para no entorpecer los detalles de la orden , esto lo que hará será actualizar el estado e indicar si está visible o no.
        /// </summary>
        /// 
        public static void UpdateStatusLogic(User user)
        {
            ctx = new CheqStoreContext();

            user.StatusLogic = !user.StatusLogic;
            ctx.Entry(user).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public static bool DeleteUser( User user)
        {
            try
            {
                ctx = new CheqStoreContext();

                bool hadOrders = ctx.Orders.Any(x => x.UserID == user.UserID);

                if (hadOrders) return false;

                ctx.Entry(user).State = EntityState.Deleted;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
         
        }


    }
}