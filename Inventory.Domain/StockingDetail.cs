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
    
    public partial class StockingDetail
    {
        public int StockingId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<decimal> StockInPrice { get; set; }
        public string BillNo { get; set; }
        public Nullable<int> VendorId { get; set; }
        public Nullable<System.DateTime> LastUpdationTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<int> TenantId { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}