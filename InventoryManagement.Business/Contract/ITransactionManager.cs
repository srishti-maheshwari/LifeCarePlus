﻿using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Business.Contract
{
    public interface ITransactionManager
    {
        List<string> GetAutocompleteProductNames(bool RestrictedproductsAlso, string ProdFor, bool forFrOrder, bool forDrOrder);
        List<string> GetAutocompProductsOnly(bool AllowFreeProds, string ProdFor, bool forFrOrder, bool forDrOrder);
        List<ProductModel> GetproductInfo(string SearchType, string data, bool isCForm, string BillType, decimal CurrentStateCode, string CurrentPartyCode,bool IsBillOnMrp);
        CustomerDetail GetCustInfo(string IdNo);
        ResponseDetail SaveDistributorBill(DistributorBillModel objModel);
        Task<ResponseDetail> SendOTP(string MobileNo, string TotalBillAmount);
        DistributorBillModel getInvoice(string BillNo, string CurrentPartyCode,string id);
        List<PartyModel> GetAllParty(string LoginPartyCode, decimal LoginStateCode, bool NeedWallet);
        List<GroupModel> GetGroupList();
        List<PartyModel> GetPartyList();
        List<PartyModel> GetAllSupplierList(string LoginPartyCode, decimal LoginStateCode);
        ResponseDetail SaveStockJv(StockJv objModel);
        ResponseDetail SavePurchaseInvoice(DistributorBillModel objModel);
        List<PurchaseReport> GetPurchaseInvoice(string InvoiceNo);
        ResponseDetail SavePartyOrderDetails(PartyOrderModel objPartyOrderModel);
        decimal GetPartyWalletBalance(string LoginPartyCode);
        string GetOrderNo(string LoginPartyCode);
        List<ProductModel> GetOrderProductList(string OrderNo, string OrderBy, bool isPending);
        List<PartyOrderModel> GetOrderList(string OrderBy, string OrderTo, string Status);
        ResponseDetail SaveDispatchOrder(PartyOrderModel objPartyDispatchOrder);
        List<DisptachOrderModel> GetDispatchOrderList(string FromDate, string ToDate, string PartyCode, string ViewType, string IdNo, string OrderNo, string DispMode, bool notOfferOrder);
        ResponseDetail RejectOrder(string OrderNo, string FormNo, string RejectReason, decimal RejectedByUserId);
        List<ProductModel> GetOrderProduct(string OrderNo, string CurrentPartyCode);
        ResponseDetail SaveDispatchOrderdetails(List<DisptachOrderModel> objModel);
        ResponseDetail DeleteBills(string BillNo, string FsessId, decimal UserId, string Reason);
    }
}
