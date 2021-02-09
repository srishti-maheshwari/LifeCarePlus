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
    
    public partial class VisionOffer
    {
        public int AID { get; set; }
        public System.DateTime OfferFromDt { get; set; }
        public System.DateTime OfferToDt { get; set; }
        public string OfferDatePart { get; set; }
        public decimal OfferOnValue { get; set; }
        public string OfferExceptSubCat { get; set; }
        public string FreeProdIDs { get; set; }
        public decimal FreeProdQty { get; set; }
        public decimal OfferOnBV { get; set; }
        public string ConfFreeProdIDs { get; set; }
        public string ConfFreeProdQtys { get; set; }
        public string OfferBillType { get; set; }
        public Nullable<System.DateTime> IdDate { get; set; }
        public string IdStaus { get; set; }
        public string SortFirstBy { get; set; }
        public string ActiveStatus { get; set; }
        public Nullable<decimal> ContinueForMonth { get; set; }
        public string OfferType { get; set; }
        public string ForNewIds { get; set; }
        public string FreeProdQtys { get; set; }
        public Nullable<int> IdDays { get; set; }
        public int OfferId { get; set; }
        public Nullable<int> OfferStartDay { get; set; }
        public Nullable<int> OfferEndDay { get; set; }
        public string OfferName { get; set; }
        public string IsPVApplicable { get; set; }
        public Nullable<decimal> ExtraPV { get; set; }
        public string Remarks { get; set; }
        public string CombineWithOffer { get; set; }
        public string CheckFirstBillWith { get; set; }
        public Nullable<bool> CombineOffer { get; set; }
        public Nullable<decimal> CBAmount { get; set; }
        public Nullable<int> OfferFrequency { get; set; }
    }
}
