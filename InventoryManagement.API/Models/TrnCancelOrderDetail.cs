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
    
    public partial class TrnCancelOrderDetail
    {
        public decimal TID { get; set; }
        public decimal ID { get; set; }
        public decimal OrderNo { get; set; }
        public decimal FormNo { get; set; }
        public decimal ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal MRP { get; set; }
        public decimal Rate { get; set; }
        public decimal DP { get; set; }
        public decimal DP1 { get; set; }
        public decimal CVP { get; set; }
        public decimal QVP { get; set; }
        public decimal Qty { get; set; }
        public decimal NetAmount { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
        public Nullable<System.DateTime> DispDate { get; set; }
        public string DispStatus { get; set; }
        public decimal DispQty { get; set; }
        public decimal RemQty { get; set; }
        public decimal DispAmt { get; set; }
        public string BillNo { get; set; }
        public string SoldBy { get; set; }
        public string IsProduct { get; set; }
        public decimal PickQty { get; set; }
        public string PLNo { get; set; }
        public Nullable<System.DateTime> PLDate { get; set; }
        public string PLGen { get; set; }
        public decimal BV { get; set; }
        public decimal RP { get; set; }
        public decimal DiscPer { get; set; }
        public decimal Discount { get; set; }
        public string IsKit { get; set; }
        public string ProdType { get; set; }
        public int SpclOfferId { get; set; }
        public int OfferUId { get; set; }
    }
}
