﻿using InventoryManagement.Business.Contract;
using InventoryManagement.DataAccess;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagement.Business
{
	public class TransactionManager:ITransactionManager
	{
        TransactionRepository objTransacRepo = new TransactionRepository();
        public List<string> GetAutocompleteProductNames(bool RestrictedproductsAlso, string ProdFor, bool forFrOrder, bool forDrOrder)
        {
            return (objTransacRepo.GetAutocompleteProductNames( RestrictedproductsAlso,  ProdFor,  forFrOrder,  forDrOrder));
        }
        public List<string> GetAutocompProductsOnly(bool AllowFreeProds, string ProdFor, bool forFrOrder, bool forDrOrder)
        {
            return (objTransacRepo.GetAutocompProductsOnly(AllowFreeProds,  ProdFor,  forFrOrder,  forDrOrder));
        }
        public List<ProductModel> GetproductInfo(string SearchType, string data, bool isCForm, string BillType,decimal CurrentStateCode, string CurrentPartyCode, bool IsBillOnMrp)
        {
            return (objTransacRepo.GetproductInfo(SearchType, data, isCForm,BillType, CurrentStateCode, CurrentPartyCode,IsBillOnMrp));
        }
        public List<ProductModel> GetproductInfoOfferWise(string SearchType, string data, bool isCForm, string BillType, decimal CurrentStateCode, string CurrentPartyCode, bool IsBillOnMrp,int OfferID)
        {
            return (objTransacRepo.GetproductInfoOfferWise(SearchType, data, isCForm, BillType, CurrentStateCode, CurrentPartyCode, IsBillOnMrp,OfferID));
        }
        public List<KitDetail> GetRateOfferList()
        {
            return (objTransacRepo.GetRateOfferList());
        }
        public CustomerDetail GetCustName(string IdNo)
        {
            return (objTransacRepo.GetCustName(IdNo));
        }
        public CustomerDetail GetCustInfo(string IdNo)
        {
            return (objTransacRepo.GetCustInfo(IdNo));
        }
        public ResponseDetail SaveDistributorBill(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = objTransacRepo.SaveDistributorBill(objModel);
            return objResponse;
        }
        public ResponseDetail ChangeOrderAddress(string orderno, string address)
        {
            ResponseDetail objResponse = objTransacRepo.ChangeOrderAddress(orderno, address);
            return objResponse;
        }
        public List<BankModel> GetBankList()
        {
            List<BankModel> objListBank = objTransacRepo.GetBankList();
            return (objListBank);
        }
        public Task<ResponseDetail> SendOTP(string MobileNo, string TotalBillAmount)
        {
            Task<ResponseDetail> objResponse = objTransacRepo.SendOTP(MobileNo, TotalBillAmount);
            return objResponse;
        }
        public Task<ResponseDetail> SendLoginOTP(string MobileNo, string Email,int UserId)
        {
            Task<ResponseDetail> objResponse = objTransacRepo.SendLoginOTP(MobileNo, Email, UserId);
            return objResponse;
        }

       
        public Task<ResponseDetail> VerifyOTP(int UserId, string OTPEntered)
        {
            Task<ResponseDetail> objResponse = objTransacRepo.VerifyOTP(UserId, OTPEntered);
            return objResponse;
        }

        public ResponseDetail SaveBillDetail(DistributorBillModel model)
        {
            ResponseDetail objResponse = objTransacRepo.SaveBillDetail(model);
            return objResponse;
        }

        public DistributorBillModel getInvoice(string BillNo, string CurrentPartyCode,string id)
        {
            DistributorBillModel objResponse = objTransacRepo.getInvoice(BillNo,CurrentPartyCode,id);
            return objResponse;
        }
        public List<PartyModel> GetAllParty(string LoginPartyCode, decimal LoginStateCode, bool NeedWallet)
        {
            return (objTransacRepo.GetAllParty(LoginPartyCode, LoginStateCode,  NeedWallet));
        }

        public List<GroupModel> GetGroupList()
        {
            List<GroupModel> objGroupList = new List<GroupModel>();
            objGroupList = objTransacRepo.GetGroupList();
            return objGroupList;
        }

        public List<PartyModel> GetPartyList()
        {
            List<PartyModel> objPartyList = new List<PartyModel>();
            objPartyList = objTransacRepo.GetPartyList();
            return objPartyList;
        }
        public ResponseDetail SaveStockJv(StockJv objModel)
        {
            return (objTransacRepo.SaveStockJv(objModel));
        }
        public ResponseDetail SavePurchaseInvoice(DistributorBillModel objModel)
        {
            return (objTransacRepo.SavePurchaseInvoice(objModel));
        }
        public List<PartyModel> GetAllSupplierList(string LoginPartyCode, decimal LoginStateCode)
        {
            return (objTransacRepo.GetAllSupplierList(LoginPartyCode, LoginStateCode));
        }
        public List<PurchaseReport> GetPurchaseInvoice(string InvoiceNo)
        {
            return (objTransacRepo.GetPurchaseInvoice(InvoiceNo));
        }
        public ResponseDetail SavePartyOrderDetails(PartyOrderModel objPartyOrderModel)
        {
            return (objTransacRepo.SavePartyOrderDetails(objPartyOrderModel));
        }
        public decimal GetPartyWalletBalance(string LoginPartyCode)
        {
            return (objTransacRepo.GetPartyWalletBalance(LoginPartyCode));
        }
        public string GetOrderNo(string LoginPartyCode)
        {
            return (objTransacRepo.GetOrderNo(LoginPartyCode));
        }
        public List<PartyModel> GetOrderToParties(string LoginPartyCode)
        {
            return (objTransacRepo.GetOrderToParties(LoginPartyCode));
        }
        
        public List<ProductModel> GetOrderProductList(string OrderNo, string OrderBy, bool isPending)
        {
            return (objTransacRepo.GetOrderProductList(OrderNo, OrderBy,  isPending));
        }
        public List<ProductModel> GetDistOrderProductList(string OrderNo)
        {
            return (objTransacRepo.GetDistOrderProductList(OrderNo));
        }
        public List<PartyOrderModel> GetOrderList(string OrderBy,string OrderTo,string Status)
        {
            return (objTransacRepo.GetOrderList(OrderBy,  OrderTo,  Status));
        }
        public ResponseDetail SaveDispatchOrder(PartyOrderModel objPartyDispatchOrder)
        {
            return (objTransacRepo.SaveDispatchOrder(objPartyDispatchOrder));
        }
        public List<DisptachOrderModel> GetDispatchOrderList(string FromDate, string ToDate, string PartyCode, string ViewType, string IdNo, string OrderNo, string DispMode, bool notOfferOrder)
        {
            return (objTransacRepo.GetDispatchOrderList(FromDate, ToDate, PartyCode, ViewType, IdNo, OrderNo, DispMode,  notOfferOrder));
        }
        public List<OldBills> GetOldBills(string FromDate, string ToDate, string IdNo, string OrderNo, string PartyCode)
        {
            return (objTransacRepo.GetOldBills(FromDate, ToDate, IdNo, OrderNo, PartyCode));
        }
        public List<ProductModel> GetBillProducts(string billNo)
        {
            return (objTransacRepo.GetBillProducts(billNo));
        }
        public ResponseDetail RejectOrder(string OrderNo,string FormNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacRepo.RejectOrder(OrderNo, FormNo, RejectReason, RejectedByUserId));
        }
        public List<ProductModel> GetOrderProduct(string OrderNo, string CurrentPartyCode)
        {
            return (objTransacRepo.GetOrderProduct(OrderNo, CurrentPartyCode));
        }
        public ResponseDetail SaveDispatchOrderdetails(List<DisptachOrderModel> objModel)
        {
            return (objTransacRepo.SaveDispatchOrderdetails(objModel));
        }
        public ResponseDetail RejectFranchiseOrder(string OrderNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacRepo.RejectFranchiseOrder(OrderNo, RejectReason, RejectedByUserId));
        }
        public ResponseDetail TransferFranchiseOrder(string OrderNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacRepo.TransferFranchiseOrder(OrderNo, RejectReason, RejectedByUserId));
        }
        public ResponseDetail SaveOrderReturn(SalesReturnModel objPartyDispatchOrder)
        {
            return (objTransacRepo.SaveOrderReturn(objPartyDispatchOrder));
        }
        public ResponseDetail DeleteBills(string BillNo, string FsessId, decimal UserId, string Reason)
        {
            return (objTransacRepo.DeleteBills(BillNo, FsessId, UserId, Reason));
        }
        public string GetSalesReturnNumber(string Loggedinparty)
        {
            return (objTransacRepo.GetSalesReturnNumber(Loggedinparty));
        }
        
        public List<PartyBill> GetBillList(string partyType, string Fcode)
        {
            return (objTransacRepo.GetBillList(partyType, Fcode));
        }
       public List<PartyBill> GetListOfSupplierBills(string supplier)
        {
            return (objTransacRepo.GetListOfSupplierBills(supplier));
        }
		
		
		
		
		
		
		
		
		
		public List<KitDetail> GetKitIdList()
        {
            return (objTransacRepo.GetKitIdList());
        }
		
		 public PartyBill GetBillDetail(string BillNo,string partycode)
        {
            return (objTransacRepo.GetBillDetail(BillNo, partycode));
        }
		
        public KitDescriptionModel GetKitDescription(decimal KitId)
        {
            return (objTransacRepo.GetKitDescription(KitId));
        }
        //public ConfigDetails GetConfigDetails()
        //{
        //    ConfigDetails objConfigDetails = objTransacRepo.GetConfigDetails();
        //    return (objConfigDetails);
        //}
        //public decimal GetWalletBalance(string CustCode)
        //{
        //    return (objTransacRepo.GetWalletBalance(CustCode));
        //}
        //public ReferenceModel CheckReferenceId(string CustCode)
        //{
        //    return (objTransacRepo.CheckReferenceId(CustCode));
        //}
        //public Task<ResponseDetail> IssueCard(string CardNo, string IdNo, string ContactNo, string CustomerType)
        //{
        //    return (objTransacRepo.IssueCard(CardNo, IdNo,ContactNo, CustomerType));
        //}
        public ResponseDetail DeletePurchaseInvoice(string InwardNo, decimal FsessId, decimal UserId, string Reason)
        {
            return (objTransacRepo.DeletePurchaseInvoice(InwardNo, FsessId, UserId, Reason));
        }

        public ResponseDetail SavePartyTargetDetails(PartyTargetMaster objModel)
        {
            return (objTransacRepo.SavePartyTargetDetails(objModel));
        }

        public List<SalesReport> GetRecordToUpdateDelDetails(string FromDate, string ToDate, string PartyCode, string Fcode, string status,string ordtype)
        {
            return (objTransacRepo.GetRecordToUpdateDelDetails(FromDate, ToDate, PartyCode, Fcode, status, ordtype));
        }

        public ResponseDetail UpdateDeliveryDetails(UpdateDeliveryDetails obj)
        {
            return (objTransacRepo.UpdateDeliveryDetails(obj));
        }

        public List<kit> GetKitList()
        {
            return (objTransacRepo.GetKitList());
        }

        public List<PackUnpackProduct> GetPackUnpackProducts(string PackUnpack, decimal KitId, string prodID, string LoginPartyCode)
        {
            return (objTransacRepo.GetPackUnpackProducts(PackUnpack, KitId, prodID, LoginPartyCode));
        }

        public ResponseDetail SavePackUnpack(PackUnpack obj)
        {
            return (objTransacRepo.SavePackUnpack(obj));
        }

        public UpgradeID GetCustomerKitDetail(string obj)
        {
            return (objTransacRepo.GetCustomerKitDetail(obj));
        }
        public UpgradeID GetKitProductList(string kitId)
        {
            return (objTransacRepo.GetKitProductList(kitId));
        }
        public ResponseDetail CheckForOffer(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = objTransacRepo.CheckForOffer(objModel);
            return objResponse;
        }
        public List<PaymodeModel> GetMPaymodes()
        {
            List<PaymodeModel> objResponse = objTransacRepo.GetMPaymodes();
            return objResponse;
        }
        public List<BankModel> GetMBanks()
        {
            List<BankModel> objResponse = objTransacRepo.GetMBanks();
            return objResponse;
        }

        public string SaveWalletRequest(WalletRequest objWallet)
        {
            return objTransacRepo.SaveWalletRequest( objWallet); ;
        }
 
        public ResponseDetail IsDuplicateCouirerName(string Name)
        {
            ResponseDetail objResponse = objTransacRepo.IsDuplicateCourierName(Name);
            return objResponse;
        }
        public ResponseDetail SaveCourierDetails(Courier objModel)
        {
            ResponseDetail objResponse = objTransacRepo.SaveCourierDetails(objModel);
            return objResponse;
        }

        public ResponseDetail SaveCourierAmount(Courier objModel)
        {
            ResponseDetail objResponse = objTransacRepo.SaveCourierAmount(objModel);
            return objResponse;
        }

        public List<Courier> GetCouierDetailList(decimal id)
        {
            List<Courier> objResponse = objTransacRepo.GetCouierDetailList(id);
            return objResponse;
        }

        public ResponseDetail SaveApproveWaletRequest(List<WalletRequest> objModelList)
        {
            ResponseDetail objResponse = objTransacRepo.SaveApproveWaletRequest(objModelList);
            return objResponse;
        }

        public ResponseDetail RejectWalletRequest(string ReqNo, string RejectReason, int UserID)
        {
            ResponseDetail objResponse = objTransacRepo.RejectWalletRequest(ReqNo, RejectReason, UserID);
            return objResponse;
        }
        public List<Courier> GetCouierList(int CourierID)
        {
            return objTransacRepo.GetCouierList(CourierID);            
        }
        public List<WalletRequest> GetAllWalletRequest(string PartyCode,string dateType, string FromDate, string ToDate, string IsApproved)
        {
            return objTransacRepo.GetAllWalletRequest( PartyCode,  dateType,  FromDate,  ToDate,  IsApproved);
        }

        public Courier CourierDetailByweight(int CourierId, int Weight)
        {
            return objTransacRepo.CourierDetailByweight(CourierId, Weight);
        }
        public ResponseDetail UpdateBillDate(List<SalesReport> objModel,int UserID)
        {
            return objTransacRepo.UpdateBillDate(objModel, UserID);
        }
        public ResponseDetail saveOfferDynamic(Offer offerDetail)
        {
            return objTransacRepo.saveOfferDynamic(offerDetail);
        }
        public List<Offer> GetAllExtraPVOfferList()
        {
            return objTransacRepo.GetAllExtraPVOfferList();
        }

        public List<Offer> GetAllBuyThisGetThatOfferList(string IsActive,bool InDateRange)
        {
            return objTransacRepo.GetAllBuyThisGetThatOfferList(IsActive, InDateRange);
        }
        
        public ResponseDetail SaveExtraPVOffer(Offer ObjOffer)
        {
            return objTransacRepo.SaveExtraPVOffer(ObjOffer);
        }
        public ResponseDetail SaveBuyThisGetThatOffer(Offer ObjOffer)
        {
            return objTransacRepo.SaveBuyThisGetThatOffer(ObjOffer);
        }
        
        public List<Offer> GetAllOfferOnValueList()
        {
            return objTransacRepo.GetAllOfferOnValueList();
        }
        public Offer getOfferDetail(int id)
        {
            return objTransacRepo.getOfferDetail(id);
        }
        public List<OfferProduct> getfreeproducts(int id)
        {
            return objTransacRepo.getfreeproducts(id);
        }
        
        
        public List<OfferProduct> CheckForExtraPVOffer(string OfferID)
        {
            return (objTransacRepo.CheckForExtraPVOffer(OfferID));
        }
        public ResponseDetail DebitCreditWallet(Wallet objWallet)
        {
            return (objTransacRepo.DebitCreditWallet(objWallet));
        }
        public List<PartyModel> GetPartyBalance()
        {
            return (objTransacRepo.GetPartyBalance());
        }
        public List<Offer> GetValidOfferList(string Doj, string UpgradeDate, string IsFirstBill, string ActiveStatus)
        {
            return objTransacRepo.GetValidOfferList(Doj,UpgradeDate,IsFirstBill,ActiveStatus);
        }
        
        public ResponseDetail SelectedOfferDetail(string OfferID)
        {
            return objTransacRepo.SelectedOfferDetail(OfferID);
        }

        public ResponseDetail SaveOffer(Offer ObjOffer)
        {
            return objTransacRepo.SaveOffer(ObjOffer);
        }
        public List<Offer> GetAllOffers()
        {
            return objTransacRepo.GetAllOffers();
        }

        public ResponseDetail SaveCourierReturn(string BillNo, string ReturnDate, string Reason)
        {
            return objTransacRepo.SaveCourierReturn(BillNo,ReturnDate,Reason);
        }
        
        public ResponseDetail SaveToWishList(PartyOrderModel objmodel)
        {
            return objTransacRepo.SaveToWishList(objmodel);
        }
        public List<ProductModel> GetFromWishList(string id)
        {
            return objTransacRepo.GetFromWishList(id);
        }
		public ResponseDetail SaveIssueSampleProducts(DistributorBillModel products)
        {
            ResponseDetail objResponse = objTransacRepo.SaveIssueSampleProducts(products);
            return objResponse;
        }
     
        public ResponseDetail ActivateIdWithPackage(UpgradeID objModel)
        {
            return (objTransacRepo.ActivateIdWithPackage(objModel));
        }
       
    }
}