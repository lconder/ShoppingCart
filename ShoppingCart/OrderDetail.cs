//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoppingCart
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }
    }
}
