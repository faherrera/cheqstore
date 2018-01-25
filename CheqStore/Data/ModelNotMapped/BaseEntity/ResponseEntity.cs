using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.ModelNotMapped.BaseEntity
{
    public class ResponseEntity
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}