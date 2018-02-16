using CheqStore.Data.ModelNotMapped.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheqStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        public bool StatusLogic { get; set; }
        public ICollection<Product> Products { get; set; } 
    }
}