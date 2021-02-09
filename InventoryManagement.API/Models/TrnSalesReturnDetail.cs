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
    
    public partial class TrnSalesReturnDetail
    {
        public decimal SrId { get; set; }
        public string SReturnNo { get; set; }
        public decimal SRNo { get; set; }
        public string ReturnBy { get; set; }
        public string ReturnTo { get; set; }
        public string Ftype { get; set; }
        public System.DateTime ReturnDate { get; set; }
        public string ProdId { get; set; }
        public string BatchNo { get; set; }
        public string ProductName { get; set; }
        public decimal ReturnQty { get; set; }
        public decimal FreeQty { get; set; }
        public decimal AcceptQty { get; set; }
        public decimal RemainingQty { get; set; }
        public decimal Rate { get; set; }
        public string TaxType { get; set; }
        public decimal Tax { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
        public string Status { get; set; }
        public string ActiveStatus { get; set; }
        public decimal UserID { get; set; }
        public string Version { get; set; }
        public decimal BV { get; set; }
        public decimal BVValue { get; set; }
        public decimal RP { get; set; }
        public decimal RPValue { get; set; }
        public string IsKit { get; set; }
        public string ProdType { get; set; }
        public decimal MRP { get; set; }
        public decimal DP { get; set; }
        public string ROn { get; set; }
        public int OfferUId { get; set; }
        public int FSessId { get; set; }
        public string EntryBy { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
    }
}
