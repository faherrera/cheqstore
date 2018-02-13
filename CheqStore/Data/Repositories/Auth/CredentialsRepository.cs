using CheqStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CheqStore.Data.Repositories.Auth
{
    public class CredentialsRepository
    {
        static CheqStoreContext ctx = new CheqStoreContext();
        

        public static bool busyUsername(string username, int userID = 0)
        {
            if (userID == 0) //Si no paso ninguno es porque estoy creando
            {
                return ctx.Users.Any(u => u.Username == username);

            }
            return ctx.Users.Any(u => u.Username == username && u.UserID != userID);
        }

        public static bool busyEmail(string email, int userID = 0)
        {
            if (userID == 0) //Si no paso ninguno es porque estoy creando
            {
                return ctx.Users.Any(u => u.Email == email);

            }
            return ctx.Users.Any(u => u.Email == email && u.UserID != userID);
        }

        public static string EncryptingPassword(string password)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(password);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }

        public static void CreateSession(string Username)
        {
            HttpContext.Current.Session["Username"] = Username;
            HttpContext.Current.Session["UserID"] = ctx.Users.First(u => u.Username == Username).UserID;

        }

        /// <summary>
        /// Destruyo / Limpio los datos en sesion.
        /// </summary>
        /// <param name="Username"></param>
        public static void DestroySession()
        {
            HttpContext.Current.Session.Clear();
        }

        public static bool IsThereSession()
        {
            return (HttpContext.Current.Session.Count > 0) ? true : false;
        }
    }
}