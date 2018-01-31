using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Data.ModelNotMapped.OrderView
{
    public class OrderView
    {
        public int Quantity { get; set; }
        public Decimal Subtotal { get { return Quantity * Price; } }
        public Decimal Price { get; set; }

        public string ProductName { get; set; }
        public int ProductID { get; set; }
    }
}