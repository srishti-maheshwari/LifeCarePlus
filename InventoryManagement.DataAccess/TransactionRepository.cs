using InventoryManagement.API.Controllers;
using InventoryManagement.DataAccess.Contract;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagement.DataAccess
{
    public class TransactionRepository : ITransactionRepository
    {
        TransactionAPIController objTransacAPI = new TransactionAPIController();
        public List<string> GetAutocompleteProductNames(bool RestrictedproductsAlso,  string ProdFor, bool forFrOrder, bool forDrOrder)
        {
            return (objTransacAPI.GetAutocompleteProductNames(RestrictedproductsAlso,    ProdFor,  forFrOrder,  forDrOrder));
        }
        public List<string> GetAutocompProductsOnly(bool AllowFreeProds,string ProdFor, bool forFrOrder, bool forDrOrder)
        {
            return (objTransacAPI.GetAutocompProductsOnly( AllowFreeProds,    ProdFor,  forFrOrder,  forDrOrder));
        }
        public List<ProductModel> GetproductInfo(string SearchType, string data, bool isCForm, string BillType, decimal CurrentStateCode, string CurrentPartyCode, bool IsBillOnMrp)
        {
            return (objTransacAPI.GetproductInfo(SearchType, data, isCForm, BillType, CurrentStateCode, CurrentPartyCode, IsBillOnMrp));
        }
        public List<ProductModel> GetproductInfoOfferWise(string SearchType, string data, bool isCForm, string BillType, decimal CurrentStateCode, string CurrentPartyCode, bool IsBillOnMrp,int OfferID)
        {
            return (objTransacAPI.GetproductInfoOfferWiseRate(SearchType, data, isCForm, BillType, CurrentStateCode, CurrentPartyCode, IsBillOnMrp, OfferID));
        }

        public List<KitDetail> GetRateOfferList()
        {
            return (objTransacAPI.GetRateOfferList());
        }
        public CustomerDetail GetCustInfo(string IdNo)
        {
            return (objTransacAPI.GetCustInfo(IdNo));
        }
        public CustomerDetail GetCustName(string IdNo)
        {
            return (objTransacAPI.GetCustName(IdNo));
        }
        public ResponseDetail SaveDistributorBill(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = objTransacAPI.SaveDistributorBill(objModel);
            return objResponse;
        }

        public ResponseDetail ChangeOrderAddress(string orderno, string address)
        {
            ResponseDetail objResponse = objTransacAPI.ChangeOrderAddress(orderno,  address);
            return objResponse;
        }
        public List<BankModel> GetBankList()
        {
            List<BankModel> objListBank = objTransacAPI.GetBankList();
            return (objListBank);
        }
        public ResponseDetail SaveBillDetail(DistributorBillModel model)
        {
            ResponseDetail objResponse = objTransacAPI.SaveBillDetail(model);
            return objResponse;
        }
        public Task<ResponseDetail> SendOTP(string MobileNo, string TotalBillAmount)
        {
            Task<ResponseDetail> objResponse = objTransacAPI.SendOTP(MobileNo, TotalBillAmount);
            return objResponse;
        }

        public Task<ResponseDetail> SendLoginOTP(string MobileNo, string Email,int UserId)
        {
            Task<ResponseDetail> objResponse = objTransacAPI.SendLoginOTP(MobileNo, Email,UserId);
            return objResponse;
        }

        public Task<ResponseDetail> VerifyOTP(int UserId, string OTPEntered)
        {
            Task<ResponseDetail> objResponse = objTransacAPI.VerifyOTP(OTPEntered, UserId);
            return objResponse;
        }

        public DistributorBillModel getInvoice(string BillNo, string CurrentPartyCode, string id)
        {
            DistributorBillModel objResponse = objTransacAPI.getInvoice(BillNo, CurrentPartyCode,  id);
            return objResponse;
        }
        public List<PartyModel> GetAllParty(string LoginPartyCode, decimal LoginStateCode, bool NeedWallet)
        {
            return (objTransacAPI.GetAllParty(LoginPartyCode, LoginStateCode,  NeedWallet));
        }


        public List<GroupModel> GetGroupList()
        {
            List<GroupModel> objGroupList = new List<GroupModel>();
            objGroupList = objTransacAPI.GetGroupList();
            return objGroupList;
        }

        public List<PartyModel> GetPartyList()
        {
            List<PartyModel> objPartyList = new List<PartyModel>();
            objPartyList = objTransacAPI.GetPartyList();
            return objPartyList;
        }
        public ResponseDetail SaveStockJv(StockJv objModel)
        {
            return (objTransacAPI.SaveStockJv(objModel));
        }
        public ResponseDetail SavePurchaseInvoice(DistributorBillModel objModel)
        {
            return (objTransacAPI.SavePurchaseInvoice(objModel));
        }
        public List<PartyModel> GetAllSupplierList(string LoginPartyCode, decimal LoginStateCode)
        {
            return (objTransacAPI.GetAllSupplierList(LoginPartyCode, LoginStateCode));
        }
        public List<PurchaseReport> GetPurchaseInvoice(string InvoiceNo)
        {
            return (objTransacAPI.GetPurchaseInvoice(InvoiceNo));
        }
        public ResponseDetail SavePartyOrderDetails(PartyOrderModel objPartyOrderModel)
        {
            return (objTransacAPI.SavePartyOrderDetails(objPartyOrderModel));
        }
        public decimal GetPartyWalletBalance(string LoginPartyCode)
        {
            return (objTransacAPI.GetPartyWalletBalance(LoginPartyCode));
        }
        public string GetOrderNo(string LoginPartyCode)
        {
            return (objTransacAPI.GetOrderNo(LoginPartyCode));
        }
        
             public List<PartyModel> GetOrderToParties(string LoginPartyCode)
        {
            return (objTransacAPI.GetOrderToParties(LoginPartyCode));
        }
        public List<ProductModel> GetOrderProductList(string OrderNo, string OrderBy, bool isPending)
        {
            return (objTransacAPI.GetOrderProductList(OrderNo, OrderBy,  isPending));
        }

        public List<ProductModel> GetDistOrderProductList(string OrderNo)
        {
            return (objTransacAPI.GetDistOrderProductList(OrderNo));
        }
        public List<PartyOrderModel> GetOrderList(string OrderBy, string OrderTo,string status)

        {
            return (objTransacAPI.GetOrderList(OrderBy, OrderTo, status));
        }
        public ResponseDetail SaveDispatchOrder(PartyOrderModel objPartyDispatchOrder)
        {
            return (objTransacAPI.SaveDispatchOrder(objPartyDispatchOrder));
        }
        public List<DisptachOrderModel> GetDispatchOrderList(string FromDate, string ToDate, string PartyCode, string ViewType, string IdNo, string OrderNo, string DispMode, bool notOfferOrder)
        {
            return (objTransacAPI.GetDispatchOrderList(FromDate, ToDate, PartyCode, ViewType, IdNo, OrderNo, DispMode,  notOfferOrder));
        }
        public ResponseDetail RejectOrder(string OrderNo,string FormNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacAPI.RejectOrder(OrderNo, FormNo, RejectReason, RejectedByUserId,"Y"));
        }
        public List<ProductModel> GetOrderProduct(string OrderNo, string CurrentPartyCode)
        {
            return (objTransacAPI.GetOrderProduct(OrderNo, CurrentPartyCode));
        }
        public ResponseDetail SaveDispatchOrderdetails(List<DisptachOrderModel> objModel)
        {
            return (objTransacAPI.SaveDispatchOrderdetails(objModel));
        }

        public ResponseDetail RejectFranchiseOrder(string OrderNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacAPI.RejectFranchiseOrder(OrderNo, RejectReason, RejectedByUserId, true,""));
        }
        public ResponseDetail TransferFranchiseOrder(string OrderNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacAPI.TransferFranchiseOrder(OrderNo, RejectReason, RejectedByUserId));
        }
        public ResponseDetail SaveOrderReturn(SalesReturnModel objPartyDispatchOrder)
        {
            return (objTransacAPI.SaveOrderReturn(objPartyDispatchOrder));
        }
        public List<OldBills> GetOldBills(string FromDate, string ToDate, string IdNo, string OrderNo, string PartyCode)
        {
            return (objTransacAPI.GetOldBills(FromDate, ToDate, IdNo, OrderNo, PartyCode));
        }
        public List<ProductModel> GetBillProducts(string billNo)
        {
            return (objTransacAPI.GetOldBillProducts(billNo));
        }
        public ResponseDetail DeleteBills(string BillNo, string FsessId, decimal UserId, string Reason)
        {
            return (objTransacAPI.DeleteBills(BillNo, FsessId, UserId, Reason));
        }
        public List<KitDetail> GetKitIdList()
        {
            return (objTransacAPI.GetKitIdList());
        }
        public KitDescriptionModel GetKitDescription(decimal KitId)
        {
            return (objTransacAPI.GetKitDescription(KitId));
        }
        //public ConfigDetails GetConfigDetails()
        //{
        //    ConfigDetails objConfigDetails = objTransacAPI.GetConfigDetails();
        //    return (objConfigDetails);
        //}
        //public decimal GetWalletBalance(string CustCode)
        //{
        //    return (objTransacAPI.GetWalletBalance(CustCode));
        //}
        //public ReferenceModel CheckReferenceId(string CustCode)
        //{
        //    return (objTransacAPI.CheckReferenceId(CustCode));
        //}
        //public Task<ResponseDetail> IssueCard(string CardNo, string IdNo, string ContactNo, string CustomerType)
        //{
        //    return (objTransacAPI.IssueCard(CardNo, IdNo,ContactNo, CustomerType));
        //}

        public ResponseDetail DeletePurchaseInvoice(string InwardNo, decimal FsessId, decimal UserId, string Reason)
        {
            return (objTransacAPI.DeletePurchaseInvoice(InwardNo, FsessId, UserId, Reason));
        }
        public string GetSalesReturnNumber(string Loggedinparty)
        {
            return (objTransacAPI.GetSalesReturnNumber(Loggedinparty));
        }
        public List<PartyBill> GetBillList(string partyType, string Fcode)
        {
            return (objTransacAPI.GetBillList(partyType, Fcode));
        }

        public List<PartyBill> GetListOfSupplierBills(string supplier)
        {
            return (objTransacAPI.GetListOfSupplierBills(supplier));
        }

        public PartyBill GetBillDetail(string BillNo,string partycode)
        {
            return (objTransacAPI.GetBillDetail(BillNo, partycode));
        }
        public ResponseDetail SavePartyTargetDetails(PartyTargetMaster objModel)
        {
            return (objTransacAPI.SavePartyTargetDetails(objModel));
        }

        public List<SalesReport> GetRecordToUpdateDelDetails(string FromDate, string ToDate, string PartyCode, string Fcode, string status, string ordtype)
        {
            return (objTransacAPI.GetRecordToUpdateDelDetails(FromDate, ToDate, PartyCode, Fcode, status, ordtype));
        }

        public ResponseDetail UpdateDeliveryDetails(UpdateDeliveryDetails obj)
        {
            return (objTransacAPI.UpdateDeliveryDetails(obj));
        }

        public List<kit> GetKitList()
        {
            return (objTransacAPI.GetKitList());
        }

        public List<PackUnpackProduct> GetPackUnpackProducts(string PackUnpack, decimal KitId, string prodID, string LoginPartyCode)
        {
            return (objTransacAPI.GetPackUnpackProducts(PackUnpack, KitId, prodID, LoginPartyCode));
        }

        public ResponseDetail SavePackUnpack(PackUnpack obj)
        {
            return (objTransacAPI.SavePackUnpack(obj));
        }

        public UpgradeID GetCustomerKitDetail(string obj)
        {
            return (objTransacAPI.GetCustomerKitDetail(obj));
        }
        public UpgradeID GetKitProductList(string kitId)
        {
            return (objTransacAPI.GetKitProductList(kitId));
        }
        public ResponseDetail CheckForOffer(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = objTransacAPI.CheckForOffer(objModel);
            return objResponse;
        }
        public List<PaymodeModel> GetMPaymodes()
        {
            List<PaymodeModel> objResponse = objTransacAPI.GetMPaymodes();
            return objResponse;
        }


        public List<BankModel> GetMBanks()
        {
            List<BankModel> objResponse = objTransacAPI.GetMBanks();
            return objResponse;
        }

        public string SaveWalletRequest(WalletRequest objWallet)
        {
            return objTransacAPI.SaveWalletRequest(objWallet); 
        }
        public ResponseDetail IsDuplicateCourierName(string Name)
        {
            ResponseDetail objResponse = objTransacAPI.IsDuplicateCourierName(Name);
            return objResponse;
        }
        public ResponseDetail SaveCourierDetails(Courier objModel)
        {
            ResponseDetail objResponse = objTransacAPI.SaveCourierDetails(objModel);
            return objResponse;
        }

        public ResponseDetail SaveCourierAmount(Courier objModel)
        {
            ResponseDetail objResponse = objTransacAPI.SaveCourierAmount(objModel);
            return objResponse;
        }

        public List<Courier> GetCouierDetailList(decimal id)
        {
            List<Courier> objResponse = objTransacAPI.GetCouierDetailList(id);
            return objResponse;
        }

        public List<Courier> GetCouierList(int CourierID)
        {
            return objTransacAPI.GetCouierList(CourierID);
        }
        public List<WalletRequest> GetAllWalletRequest(string PartyCode, string dateType, string FromDate, string ToDate, string IsApproved)
        {
            return objTransacAPI.GetAllWalletRequest( PartyCode,  dateType,  FromDate,  ToDate,  IsApproved);
        }
        public ResponseDetail SaveApproveWaletRequest(List<WalletRequest> objModelList)
        {
            ResponseDetail objResponse = objTransacAPI.SaveApproveWaletRequest(objModelList);
            return objResponse;
        }
        public ResponseDetail RejectWalletRequest(string ReqNo, string RejectReason, int UserID)
        {
            ResponseDetail objResponse = objTransacAPI.RejectWalletRequest(ReqNo, RejectReason, UserID);
            return objResponse;
        }
        public Courier CourierDetailByweight(int CourierId, int Weight)
        {
            return objTransacAPI.CourierDetailByweight(CourierId, Weight);
        }
        public ResponseDetail UpdateBillDate(List<SalesReport> objModel, int UserID)
        {
            return objTransacAPI.UpdateBillDate(objModel, UserID);
        }

        public ResponseDetail saveOfferDynamic(Offer offerDetail)
        {
            return objTransacAPI.saveOfferDynamic(offerDetail);
        }
        public List<Offer> GetAllExtraPVOfferList()
        {
            return objTransacAPI.GetAllExtraPVOfferList();
        }
        public ResponseDetail SaveExtraPVOffer(Offer ObjOffer)
        {
            return objTransacAPI.SaveExtraPVOffer(ObjOffer);
        }
        public List<Offer> GetAllOfferOnValueList()
        {
            return objTransacAPI.GetAllOfferOnValueList();
        }
        public Offer getOfferDetail(int id)
        {
            return null;//objTransacAPI.getOfferDetail(id);
        }
        public List<OfferProduct> getfreeproducts(int id)
        {
            return objTransacAPI.getfreeproducts(id);
        }
        
        public List<OfferProduct> CheckForExtraPVOffer(string OfferID)
        {
            return (objTransacAPI.CheckForExtraPVOffer(OfferID));
        }
        public ResponseDetail DebitCreditWallet(Wallet objWallet)
        {
            return (objTransacAPI.DebitCreditWallet(objWallet));
        }
        public List<PartyModel> GetPartyBalance()
        {
            return (objTransacAPI.GetPartyBalance());
        }
        public List<Offer> GetAllBuyThisGetThatOfferList(string IsActive,bool InDateRange)
        {
            return objTransacAPI.GetAllBuyThisGetThatOfferList(IsActive,InDateRange);
        }
        public ResponseDetail SaveBuyThisGetThatOffer(Offer ObjOffer)
        {
            return objTransacAPI.SaveBuyThisGetThatOffer(ObjOffer);
        }
        public List<Offer> GetValidOfferList(string Doj, string UpgradeDate, string IsFirstBill, string ActiveStatus)
        {
            return objTransacAPI.GetValidOfferList(Doj, UpgradeDate, IsFirstBill, ActiveStatus);
        }

        public ResponseDetail SelectedOfferDetail(string OfferID)
        {
            return objTransacAPI.SelectedOfferDetail(OfferID);
        }
        public ResponseDetail SaveOffer(Offer ObjOffer)
        {
            return objTransacAPI.saveOfferDynamicNew(ObjOffer);
        }
        public List<Offer> GetAllOffers()
        {
            return objTransacAPI.GetAllOffers();
        }
        public ResponseDetail SaveCourierReturn(string BillNo, string ReturnDate, string Reason)
        {
            return objTransacAPI.SaveCourierReturn(BillNo, ReturnDate, Reason);
        }
        public ResponseDetail SaveToWishList(PartyOrderModel objmodel)
        {
            return objTransacAPI.SaveToWishList(objmodel);
        }
        public List<ProductModel> GetFromWishList(string id)
        {
            return objTransacAPI.GetFromWishList(id);
        }
		public ResponseDetail SaveIssueSampleProducts(DistributorBillModel products)
        {
            ResponseDetail objResponse = objTransacAPI.SaveIssueSampleProducts(products);
            return objResponse;
        }
               public ResponseDetail ActivateIdWithPackage(UpgradeID objModel)
        {
            return (objTransacAPI.ActivateIdWithPackage(objModel));
        }
       
    }        
}