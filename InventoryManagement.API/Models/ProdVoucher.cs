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
    
    public partial class ProdVoucher
    {
        public decimal VoucherId { get; set; }
        public decimal VoucherNo { get; set; }
        public System.DateTime VoucherDate { get; set; }
        public string DrTo { get; set; }
        public string CrTo { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public string RefNo { get; set; }
        public string AcType { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
        public string VType { get; set; }
        public decimal SessID { get; set; }
        public decimal OrderNo { get; set; }
        public string EntryType { get; set; }
        public System.DateTime LastDate { get; set; }
    }
}
