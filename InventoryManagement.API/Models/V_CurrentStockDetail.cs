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
    
    public partial class V_CurrentStockDetail
    {
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public decimal CatId { get; set; }
        public string CatName { get; set; }
        public string ProdId { get; set; }
        public string ProductName { get; set; }
        public string BatchCode { get; set; }
        public string Barcode { get; set; }
        public decimal PurchaseRate { get; set; }
        public System.DateTime MfgDate { get; set; }
        public System.DateTime ExpDate { get; set; }
        public decimal DP { get; set; }
        public decimal MRP { get; set; }
        public decimal RP { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> KitProdQty { get; set; }
        public Nullable<decimal> NSProdQty { get; set; }
        public Nullable<decimal> TotalQty { get; set; }
        public Nullable<decimal> RPValue { get; set; }
        public Nullable<decimal> StockValue { get; set; }
        public Nullable<decimal> DPStockValue { get; set; }
        public Nullable<decimal> MRPStockValue { get; set; }
    }
}
