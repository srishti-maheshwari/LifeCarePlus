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
    
    public partial class M_CouponSalesDetail
    {
        public int AId { get; set; }
        public int FSessId { get; set; }
        public decimal SessId { get; set; }
        public decimal FormNo { get; set; }
        public decimal Amount { get; set; }
        public string RefNo { get; set; }
        public System.DateTime BillDate { get; set; }
        public string FCode { get; set; }
        public string Ftype { get; set; }
        public string Remarks { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
        public string AType { get; set; }
        public decimal UserId { get; set; }
    }
}