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
    
    public partial class StockingReport_Result
    {
        public string ProductName { get; set; }
        public Nullable<int> OpeningStock { get; set; }
        public Nullable<int> StockIn { get; set; }
        public Nullable<int> TotalTransaction { get; set; }
        public Nullable<int> ClosingStock { get; set; }
    }
}
