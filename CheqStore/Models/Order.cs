using CheqStore.Data.ModelNotMapped.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheqStore.Models
{
    public class Order:BaseEntity
    {
        public bool Status { get; set; } //Estado de la orden => [true => Confirmado, false => Pendiente]
        public DateTime OrderDate { get; set; } //Fecha en la que fue creada.
        public Nullable<DateTime> ConfirmedDate { get; set; } //Fecha en la que el estado de Status cambio de false a true.

        public Decimal Total { get; set; } //Total de cobro, sumatoria de todos los OrderDetails con OrderID igual a este ID.

        public int UserID { get; set; } //Order have conecction with a User.
        public virtual User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}