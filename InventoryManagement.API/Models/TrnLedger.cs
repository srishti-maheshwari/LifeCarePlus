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
    
    public partial class TrnLedger
    {
        public decimal AID { get; set; }
        public decimal SessId { get; set; }
        public decimal FormNo { get; set; }
        public decimal Amount { get; set; }
        public string CrDr { get; set; }
        public string VType { get; set; }
        public System.DateTime VDate { get; set; }
        public string PayMode { get; set; }
        public decimal BankId { get; set; }
        public string BankNm { get; set; }
        public string ChNo { get; set; }
        public decimal RefNo { get; set; }
        public Nullable<System.DateTime> ChDate { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> CardNo { get; set; }
        public decimal CourierID { get; set; }
        public string CourierNm { get; set; }
        public string DocketNo { get; set; }
        public string CrTo { get; set; }
        public string IFSCode { get; set; }
        public string AcNo { get; set; }
        public string Reason { get; set; }
    }
}
