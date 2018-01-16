using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheqStore.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public string PathPhoto { get; set; }
        public int CategoryID { get; set; } //FK. Relacionada con Categories.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}