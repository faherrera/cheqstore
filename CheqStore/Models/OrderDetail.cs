using CheqStore.Data.ModelNotMapped.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Models
{
    public class OrderDetail : BaseEntity
    {
        public int Quantity { get; set; }
        public Decimal Subtotal { get; set; }
        public int ProductID { get; set; } //ProductID is relationship with Product Table
        public int OrderID { get; set; } //OrderDetail belongs to one Order.

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}