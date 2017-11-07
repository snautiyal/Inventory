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
    
    public partial class Category
    {
        public Category()
        {
            this.CategoryBrandMappings = new HashSet<CategoryBrandMapping>();
            this.Products = new HashSet<Product>();
        }
    
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreationTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public System.DateTime LastUpdationTime { get; set; }
        public Nullable<int> TenantId { get; set; }
    
        public virtual ICollection<CategoryBrandMapping> CategoryBrandMappings { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
