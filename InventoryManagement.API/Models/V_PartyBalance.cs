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
    
    public partial class V_PartyBalance
    {
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public decimal GroupID { get; set; }
        public string CityName { get; set; }
        public string UserPartyCode { get; set; }
        public decimal StateCode { get; set; }
        public decimal CrAmt { get; set; }
        public decimal DrAmt { get; set; }
        public Nullable<decimal> Balance { get; set; }
    }
}