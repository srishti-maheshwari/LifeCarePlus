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
    
    public partial class RegisterUser
    {
        public int Id { get; set; }
        public string Fax { get; set; }
        public string ActiveStatus { get; set; }
        public string FormNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Nullable<int> CityCode { get; set; }
        public Nullable<int> DistrictCode { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public Nullable<int> StateCode { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string PinCode { get; set; }
        public string EMail { get; set; }
        public string Mobl { get; set; }
        public string MemFirstName { get; set; }
        public string MemLastName { get; set; }
        public string IdNo { get; set; }
        public string passw { get; set; }
        public string randomId { get; set; }
        public string CountryName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<decimal> ReferalAmount { get; set; }
    }
}
