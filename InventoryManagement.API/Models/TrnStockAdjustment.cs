//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryManagement.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TrnStockAdjustment
    {
        public decimal SId { get; set; }
        public decimal RefNo { get; set; }
        public System.DateTime SDate { get; set; }
        public string ProdId { get; set; }
        public string ProductName { get; set; }
        public string BatchNo { get; set; }
        public string SType { get; set; }
        public decimal Qty { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
    }
}