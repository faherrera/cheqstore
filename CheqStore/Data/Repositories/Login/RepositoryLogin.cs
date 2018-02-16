using CheqStore.DAL;
using CheqStore.Data.ModelNotMapped.BaseEntity;
using CheqStore.Data.ModelNotMapped.Login;
using CheqStore.Data.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.Repositories.Login
{
    public class RepositoryLogin
    {
        private static CheqStoreContext ctx = new CheqStoreContext();
        private static ResponseEntity responseEntity;
        /// <summary>
        /// Procesa el Login para poder comparar las credenciales y dar o no acceso cargandolo en la sesion.
        /// Here below process the login to comparing the credentials and give or not access in the current session
        /// </summary>
        /// <param name="loginClass"></param>
        /// <returns> string</returns>
        public static ResponseEntity ProccessingLogin( LoginClass loginClass)
        {
            if (loginClass == null) return responseEntity = new ResponseEntity() { status = false,message = "El login no puede ser nulo" };

            //Here I compare the credentials with encrypting password
            string EncryptingPassword = CredentialsRepository.EncryptingPassword(loginClass.Password); 
            var credentials = ctx.Users.FirstOrDefault(x => x.Username == loginClass.Username && x.Password == EncryptingPassword);

            if (credentials == null) return responseEntity = new ResponseEntity() { status = false, message = "Los datos no coinciden, por favor revisar" };

            CredentialsRepository.CreateSession(credentials.Username);

            return responseEntity = new ResponseEntity() { status = true, message = ""};

        }
    }
}