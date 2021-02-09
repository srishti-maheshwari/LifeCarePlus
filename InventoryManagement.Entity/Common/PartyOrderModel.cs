﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class PartyOrderModel
    {
        public DateTime OrderDate { get; set; }
        public string OrderDateStr { get; set; }
        public string OrderNo { get; set; }
        public string OrderBy { get; set; }
        public string OrderTo { get; set; }
        public string Remarks { get; set; }
        public ProductModel objProduct { get; set; }
        public List<ProductModel> objListProduct { get; set; }
        public string objProductListStr { get; set; }
        public List<TaxSummary> objTaxSummary { get; set; }
        public User LoginUser { get; set; }
        public decimal PartyWalletBalance { get; set; }
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public decimal OrderAmt { get; set; }
        public string ChNo { get; set; }
        public DateTime ChDate { get; set; }
        public decimal ChAmt { get; set; }
        public string BankName { get; set; }
        public decimal WalletAmt { get; set; }
        public string DispStatus { get; set; }
        public decimal TotalOrdQty { get; set; }
        public decimal TotalDispQty { get; set; }
        public string PLStatus { get; set; }
        public string OrderType { get; set; }
        public string FType { get; set; }
        public decimal FormNo { get; set; }
        public string SaveTo { get; set; }
        public bool Processwishlist { get; set; }
        public string FreightType { get; set; }
        public decimal FreightAmt { get; set; }

        public string ShippedTo { get; set; }
        public string DelvAddress { get; set; }
        public string TransporterName { get; set; }
        public string VehicleNo { get; set; }
        public string Station { get; set; }
        public string EWayBillNo { get; set; }
    }
  
}