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
    
    public partial class TrnPickListDetail
    {
        public decimal AId { get; set; }
        public decimal FSessId { get; set; }
        public string PLBy { get; set; }
        public decimal SPLNo { get; set; }
        public string PLNo { get; set; }
        public System.DateTime PLDate { get; set; }
        public string OrderNo { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string IDNo { get; set; }
        public decimal FormNo { get; set; }
        public string MemName { get; set; }
        public string ProdId { get; set; }
        public string ProductName { get; set; }
        public string BatchNo { get; set; }
        public decimal OrdQty { get; set; }
        public decimal PickQty { get; set; }
        public decimal RemQty { get; set; }
        public string IsBill { get; set; }
        public string BillNo { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public string SoldBy { get; set; }
        public string Remarks { get; set; }
        public string ActiveStatus { get; set; }
        public decimal UserId { get; set; }
        public string Version { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
        public string IsKit { get; set; }
        public string ProdType { get; set; }
        public string DReason { get; set; }
        public decimal DUserId { get; set; }
        public Nullable<System.DateTime> DRecTimeStamp { get; set; }
        public int OfferUId { get; set; }
    }
}
