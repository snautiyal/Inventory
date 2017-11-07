//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inventory.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreationTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public System.DateTime LastUpdationTime { get; set; }
        public string BillNo { get; set; }
        public Nullable<int> VendorID { get; set; }
        public Nullable<int> TenantId { get; set; }
    
        public virtual Item Item1 { get; set; }
        public virtual Item Item2 { get; set; }
        public virtual Product Product { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
