using CheqStore.Data.ModelNotMapped.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheqStore.Models
{
    public class Rol : BaseEntity
    {
     
        public string Name { get; set; }
    }
}