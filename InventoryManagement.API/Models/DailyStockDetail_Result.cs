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
    
    public partial class DailyStockDetail_Result
    {
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public string ProdID { get; set; }
        public string ProductName { get; set; }
        public System.DateTime StockDate { get; set; }
        public string StockDateStr { get; set; }
        public decimal OpStock { get; set; }
        public decimal InStock { get; set; }
        public decimal StockOut { get; set; }
        public decimal ClsStock { get; set; }
        public decimal MRP { get; set; }
        public decimal DP { get; set; }
        public decimal OpStockValue { get; set; }
        public decimal InStockValue { get; set; }
        public decimal StockOutValue { get; set; }
        public decimal ClsStockValue { get; set; }
    }
}