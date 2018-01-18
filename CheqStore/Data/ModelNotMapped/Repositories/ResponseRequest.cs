using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.ModelNotMapped.Repositories
{
    public class ResponseRequest
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Object Obj { get; set; }
    }
}