//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demo.Jumper
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductSale
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> SaleDate { get; set; }
        public Nullable<int> ProductQuantity { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> AgentId { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Product Product { get; set; }
    }
}
