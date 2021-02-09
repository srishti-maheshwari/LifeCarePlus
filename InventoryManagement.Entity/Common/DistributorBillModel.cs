﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class DistributorBillModel
    {
        public CustomerDetail objCustomer { get; set; }
        public ProductModel objProduct { get; set; }
        public List<ProductModel> objListProduct { get; set; }
        public List<TrnPayModeDetail11> objDTListPayMode { get; set; }
        public string objProductListStr { get; set; }
        public ConfigDetails objConfigDetails { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAdd { get; set; }
        public string CompanyTel { get; set; }
        public string CompanyMail { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string GSTNo { get; set; }
        public string SoldBy { get; set; }
        public string SoldByName { get; set; }
        public string SoldByAddress { get; set; }
        public string SoldByCity { get; set; }
        public string SoldByTel { get; set; }
        public string SoldByEmail { get; set; }
        public string CinNo { get; set; }
        public string PanNo { get; set; }
        public List<TaxSummary> objTaxSummary { get; set; }
        public string BillDateStr { get; set; }
        public decimal OldTaxAmount { get; set; }
        public string BillType { get; set; }
        public int offerId { get; set; }
        public string IsCouponWallet { get; set; }
        public string StateGSTName { get; set; }
        public string OrderNo { get; set; }
        public string CompCity { get; set; }
        public string FreightType { get; set; }
        public decimal FreightAmt { get; set; }

        public string ShippedTo { get; set; }
        public string DelvAddress { get; set; }
        public string TransporterName { get; set; }
        public string VehicleNo { get; set; }
        public string Station { get; set; }
        public string EWayBillNo { get; set; }
        public decimal? SoapandToothpastepv { get; set; }
        public decimal? OtherProductpv { get; set; }
        public decimal CashReward { get; set; }
        public string TaxORStock { get; set; }
        public string DispatchDateStr { get; set; }
        public string CompanyId { get; set; }
        public string WalletType { get; set; }

    }
    public class TaxSummary
    {
        //public decimal TaxPer { get; set; }
        //public decimal CGSTPer { get; set; }
        //public decimal SGSTPer { get; set; }
        //public decimal TaxAmt { get; set; }
        //public decimal CGSTAmt { get; set; }
        //public decimal SGSTAmt { get; set; }
        //public decimal Amount { get;set;
        public string HSNCode { get; set; }
        public decimal SumTaxPer { get; set; }
        public decimal SumTaxAmt { get; set; }
        public decimal SumCGSTPer { get; set; }
        public decimal SumCGSTAmt { get; set; }
        public decimal SumSGSTPer { get; set; }
        public decimal SumSGSTAmt { get; set; }
        public decimal SumAmount { get; set; }
        public decimal SumNetPayableAmount { get; set; }
    }
    public class CustomerDetail {
        public string Aadhar { get; set; }
        public string Doj { get; set; }
        public string IdNo { get; set; }
        public string UpgradeDate { get; set; }
        public bool IsCustomerBill { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ReferenceIdNo { get; set; }
        public string ReferenceName { get; set; }
        public string Remarks { get; set; }
        public int KitId { get; set; }
        public decimal KitAmount { get; set; }
        public decimal CouponWalletAmount { get; set; }
        public decimal MinBillAmt { get; set; }
        public List<string> InvoiceType { get; set; }
        public string SelectedInvoiceType { get; set; }
        public User UserDetails { get; set; }
        public decimal FormNo { get; set; }
        public bool IsActive { get; set; }
        public decimal StateCode { get; set; }
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public bool IsBlock { get; set; }

        public bool IsBillOnMrp { get; set; }
        public bool IsFirstBill { get; set; }
        public bool IsFirstBillOffer { get; set; }
        public decimal MinRepurch { get; set; }
        public decimal MinPV { get; set; }
        public string CustId { get; set; }
        public decimal WalletBalance { get; set; }
        public decimal MainWalletBalance { get; set; }
        public bool IsRegisteredCustomer { get; set; }
        public string CustomerType { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string PANNo { get; set; }
        public string Password { get; set; }
        public string GSTNo { get; set; }
        public int NumberOfBill { get; set; }
        public int MonthBill { get; set; }
        public string ShowInvType { get; set; }
        public string ShowOffers { get; set; }
        public string FirstIDUpgradeMandatory { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string ShowRepurchBV { get; set; }
        public string ShowActiveBV { get; set; }
        public string ShowRepurchPV { get; set; }
        public string ShowActivePV { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string WalletType { get; set; }
    }
    public class ReferenceModel
    {
        public ResponseDetail objresponse { get; set; }
        public string RefId { get; set; }
        public string RefName { get; set; }
    }
    public class ProductModel
    {
        public int? SpecialOfferId { get; set; }
        public int? freeQty { get; set; }
        public string ProdForBill { get; set; }
        public string buyerTin { get; set; }
        public decimal CourierId { get; set; }
        public decimal CourierCharges { get; set; }
        public string DocketNo { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? DocketDate { get; set; }
        public string DocketDateStr { get; set; }
        public string QtyStr { get; set; }
        public string TotalAmountString { get; set; }
        public string CourierName { get; set; }
        public string BillSoldBy { get; set; }
        public decimal OrderedOty { get; set; }
        public string PartyName { get; set; }
        public string OrderType { get; set; }
        public decimal ProdStateCode { get; set; }
        public string IdNo { get; set; }
        public string Mobileno { get; set; }
        public decimal CatId { get; set; }
        public decimal SubCatId { get; set; }
        public string CatName { get; set; }
        //public string ProductCodeStr { get; set; }
        public int IsCommissionAdd { get; set; }
        public int IsDiscountAdd { get; set; }
        public string ProductName { get; set; }
        public string ProductCodeStr { get; set; }
        public string Barcode { get; set; }
        public string BatchNo { get; set; }
        public decimal? BV { get; set; }
        public decimal? CV { get; set; }
        public decimal? PV { get; set; }
        public decimal? CommissionPer { get; set; }
        public decimal? CommissionAmt { get; set; }
        public decimal Weight { get; set; }
        public decimal? FundPoint { get; set; }
        public string ProductTye { get; set; }
        public decimal? BVValue { get; set; }
        public decimal? CVValue { get; set; }
        public decimal? PVValue { get; set; }
        public decimal WeightVal { get; set; }
        public decimal? FundPointValue { get; set; }
        public decimal Quantity { get; set; }
        public decimal StockAvailable { get; set; }
        public decimal? DP { get; set; }
        public decimal? DiscPer { get; set; }
        public decimal? DiscAmt { get; set; }
        public decimal Amount { get; set; }
        public decimal? TaxPer { get; set; }
        public decimal? TaxAmt { get; set; }
        public decimal OldTaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? Rate { get; set; }
        public decimal? MRP { get; set; }
        public decimal? RP { get; set; }
        public decimal? RPValue { get; set; }
        public int ProdCode { get; set; }
        public string TaxType { get; set; }
        public string ProductType { get; set; }
        public decimal TotalNetPayable { get; set; }
        public decimal TotalRP { get; set; }
        public decimal TotalPayAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalTaxPer { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalBV { get; set; }
        public decimal TotalCV { get; set; }
        public decimal TotalPV { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalTotalAmount { get; set; }
        public decimal Roundoff { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CashDiscPer { get; set; }
        public decimal CashDiscAmount { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal TotalDiscPer { get; set; }
        public decimal TotalCommsonAmt { get; set; }
        public string DeliveryPlace { get; set; }
        public PayDetails PayDetails { get; set; }
        public bool IsExpirable { get; set; }
        public DateTime ExpDate { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal TotalCGSTPer { get; set; }
        public decimal TotalSGSTPer { get; set; }
        public decimal TotalCGSTAmt { get; set; }
        public decimal TotalSGSTAmt { get; set; }
        public decimal TotalTaxableAmt { get; set; }
        public DateTime BillDate { get; set; }
        public string UID { get; set; }
        public decimal OfferUID { get; set; }
        public decimal SpclOfferId { get; set; }
        public string OfferName { get; set; }

        public decimal DispQty { get; set; }
        public decimal GSTPer { get; set; }
        public decimal ReturnQty { get; set; }
        public string DispStatus { get; set; }
        public string HSNCode { get; set; }
        public int Row_number { get; set; }
        public decimal TotalWeight { get; set; }
        public string IsCardIssue { get; set; }
        public string BillType { get; set; }
        public string TransporterName { get; set; }
        public string VehicleNo { get; set; }
        public string Station { get; set; }
        public string EWayBillNo { get; set; }
        public string Remark { get; set; }
        public string FreightType { get; set; }
        public decimal FreightAmt { get; set; }
        public decimal PReturnQuantity { get; set; }
        public decimal Remaining { get; set; }
        
    }
    public class ProductSearchModel
    {
        public List<BarcodeDetails> objBarcodeList { get; set; }
        public List<ProductModel> objProductList { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public decimal ProdId { get; set; }
    }
    public class PayDetails
    {
        public string PayMode { get; set; }
        public string PayPrefix { get; set; }
        public bool IsBD { get; set; }
        public bool IsQ { get; set; }
        public bool IsD { get; set; }
        public bool IsCC { get; set; }
        public bool IsV { get; set; }
        public bool IsW { get; set; }
        public bool IsMW { get; set; }
        public bool IsT { get; set; }
        public bool IsP { get; set; }
        public decimal AmountByBD { get; set; }
        public string BDBankName { get; set; }
        public string AccNo { get; set; }
        public string IFSCCode { get; set; }
        public decimal AmountByCheque { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string ChequeDateStr { get; set; }
        public string CHBankName { get; set; }
        public decimal AmountByDD { get; set; }
        public string DDBankName { get; set; }
        public string DDNo { get; set; }
        public DateTime DDDate { get; set; }
        public string DDDateStr { get; set; }
        public decimal AmountByCard { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public decimal AmountByCredit { get; set; }
        public string Narration { get; set; }
        public decimal AvailBal { get; set; }
        public decimal AvailMainBal { get; set; }
        public decimal RemBal { get; set; }
        public decimal RemMainBal { get; set; }
        public decimal TotalPayDisc { get; set; }
        public decimal UsedDisc { get; set; }
        public decimal RemainDisc { get; set; }
        public decimal AmountByWallet { get; set; }
        public decimal AmountByMainWallet { get; set; }
        public decimal AmountByVoucher { get; set; }
        public decimal AmountByPaytm { get; set; }
        public string PaytmTransactionId { get; set; }
        public decimal BankCode { get; set; }
    }
     public  class TrnPayModeDetail11
    {
        public decimal AId { get; set; }
        public decimal FSessId { get; set; }
        public decimal SBillNo { get; set; }
        public string BillNo { get; set; }
        public string SoldBy { get; set; }
        public System.DateTime BillDate { get; set; }
        public string PayPrefix { get; set; }
        public string PayMode { get; set; }
        public string ChqDDNo { get; set; }
        public Nullable<System.DateTime> ChqDDDate { get; set; }
        public string CardNo { get; set; }
        public decimal BankCode { get; set; }
        public string BankName { get; set; }
        public decimal Amount { get; set; }
        public decimal BillAmt { get; set; }
        public string Narration { get; set; }
        public string ActiveStatus { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
        public string Version { get; set; }
        public decimal UserId { get; set; }
        public string UserName { get; set; }
        public string BillType { get; set; }
        public decimal DUserId { get; set; }
        public Nullable<System.DateTime> DRecTimeStamp { get; set; }
        public string AcNo { get; set; }
        public string IFSCode { get; set; }
    }

}