using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class ImportBill
    {
        public string BillID { get; set; }
        public string UserBillNo { get; set; }
        public string VchType { get; set; }
        public string VchRefNo { get; set; }
        public string Date { get; set; }
        public string Code { get; set; }
        public string PartyName { get; set; }
        public string Addres1 { get; set; }
        public string Addres2 { get; set; }
        public string StateNme { get; set; }
        public string Pincode { get; set; }
        public string TINNO { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string StateName { get; set; }
        public string Country { get; set; }
        public string ProductName { get; set; }
        public string ItemCode { get; set; }
        public string GoDownName { get; set; }
        public string Unit { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public string Tinno { get; set; }
        public decimal NetAmount { get; set; }
        public string TaxParent { get; set; }
        public string TaxType { get; set; }
        public decimal TaxRate { get; set; }
        public string Tax { get; set; }
        public string RoundOff { get; set; }
        public string OtherCharges { get; set; }
        public string BatchNo { get; set; }
        public string BatchWiseQty { get; set; }
        public string BatchWiseRate { get; set; }
        public string B1 { get; set; }
        public string B2 { get; set; }
        public string B3 { get; set; }
        public string IMPORTED { get; set; }
        public string HSNCODE { get; set; }
        public decimal ITEMMASTERIGST { get; set; }
        public string narration { get; set; }
        public string IGST { get; set; }
        public string ItemDescription { get; set; }
        public string VoucherNumber { get; set; }
        public string IsManualVoucherNo { get; set; }
        public string SalesType { get; set; }
        public string MultipleLedger { get; set; }
        public string SalesLedger { get; set; }
        public int OpeningStock { get; set; }
        public int OpeningRate { get; set; }
        public int OpeningValue { get; set; }
        public string OpeningUnit { get; set; }
        public string RegType { get; set; }
        public string RefType { get; set; }
        public string BillRefNo { get; set; }
        public string BillRefAmount { get; set; }
        public string BuyerName { get; set; }
        public string Buyer_Address { get; set; }
        public string Buyer_Address1 { get; set; }
        public string Buyer_StateName{ get; set; }
        public string Buyer_Country{ get; set; }
        public string Supply_State{ get; set; }
        public string Buyer_GstRegistrationType{ get; set; }
        public string Buyer_GSTINNO { get; set; }
        public string HSNDESC{ get; set; }
        public string PARTNO{ get; set; }
        public string ItemDiscountSlab{ get; set; }
        public string OriginalInvoiceDate{ get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}