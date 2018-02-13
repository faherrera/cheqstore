using CheqStore.DAL;
using CheqStore.Data.ModelNotMapped.BaseEntity;
using CheqStore.Data.ModelNotMapped.Register;
using CheqStore.Data.Repositories.Auth;
using CheqStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.Repositories.Register
{
    public class RegisterRepository
    {
        private static CheqStoreContext ctx = new CheqStoreContext();
        public static ResponseEntity responseEntity;


        public static ResponseEntity StoreRegister(RegisterRequest registerRequest){

            if (CredentialsRepository.busyUsername(registerRequest.Username)) return responseEntity = new ResponseEntity() { status = false,message = "El usuario ya está ocupado, pruebe con otro por favor"};

            try
            {
                User userEntity = new User()
                {
                    Name = registerRequest.Name,
                    Username = registerRequest.Username,
                    Password = CredentialsRepository.EncryptingPassword(registerRequest.Password),
                };

                ctx.Users.Add(userEntity);
                ctx.SaveChanges();

                return responseEntity = new ResponseEntity() { status = true, message = ""};
            }
            catch (Exception e)
            {
                return responseEntity = new ResponseEntity() { status = false, message = "Error al grabar el usuario desde register, -> "+e.Message };

            }
        }
    }
}