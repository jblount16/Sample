//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_Webapi_Mvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inventory
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public int Category { get; set; }
    
        public virtual Category Category1 { get; set; }
    }
}
