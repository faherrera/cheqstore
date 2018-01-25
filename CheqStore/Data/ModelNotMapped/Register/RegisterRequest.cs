using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.ModelNotMapped.Register
{
    public class RegisterRequest
    {

        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}