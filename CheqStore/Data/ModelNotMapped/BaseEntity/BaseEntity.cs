using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheqStore.Data.ModelNotMapped.BaseEntity
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}