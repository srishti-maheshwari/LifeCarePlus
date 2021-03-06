﻿
using InventoryManagement.API.Common;
using InventoryManagement.API.Models;
using InventoryManagement.Entity.Common;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using static InventoryManagement.Entity.Common.EnumCalculation;

namespace InventoryManagement.API.Controllers
{
    public class TransactionAPIController : ApiController
    {
        GenConnection objGetConn = new GenConnection();
        private static string _numbers = "0123456789";
        private static string dbName, invDbName, enttConstr, AppConstr, InvConstr, CompName, WRPartyCode;
        Random random = new Random();
        //According to new schema Inventory DB
        //public ProductModel GetproductInfo(string SearchType,string data,bool isCForm,decimal CurrentStateCode,string CurrentPartyCode)
        //{
        //    ProductModel objProductModel = new ProductModel();
        //    bool searchByProductFlag = true;
        //    if(SearchType=="B")
        //    {
        //        searchByProductFlag = false;
        //    }

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(data))
        //        {
        //            using (var entity = new InventoryEntities(enttConstr))
        //            {

        //                if (searchByProductFlag)
        //                {

        //                        objProductModel = (from product in entity.ProductMasters
        //                                      where product.ProductName.ToLower().Equals(data.ToLower()) && product.IsActive == true
        //                                           join barcode in entity.BarcodeMasters on product.ProductCode equals barcode.ProductCode
        //                                      join tax in entity.TaxMasters on product.ProductCode equals tax.ProductId
        //                                      select new ProductModel
        //                                      {
        //                                          ProductName = product.ProductName,
        //                                          Barcode = barcode.Barcode,
        //                                          BatchNo=barcode.BatchCode,
        //                                          DP = barcode.DP,
        //                                          RP = product.RP,
        //                                          DiscPer = product.DiscountPer,
        //                                          DiscAmt = product.DiscountInRs,
        //                                          ProdCode = (int)product.ProductCode,
        //                                          TaxPer = tax.VatTax,
        //                                          MRP=barcode.MRP,
        //                                          BV = product.BV,
        //                                          PV = product.PV,
        //                                          CV = product.CV,

        //                                           TaxType ="VAT",
        //                                           CommissionPer=product.ProductCommission
        //                                      }).FirstOrDefault();


        //                }
        //                else
        //                {
        //                    decimal? BarCodeData = decimal.Parse(data);


        //                        objProductModel = (from product in entity.ProductMasters
        //                                           where product.IsActive == true
        //                                           join barcode in entity.BarcodeMasters on product.ProductCode equals barcode.ProductCode
        //                                   where barcode.Barcode==BarCodeData
        //                                   join tax in entity.TaxMasters on product.ProductCode equals tax.ProductId
        //                                   select new ProductModel
        //                                   {
        //                                       ProductName = product.ProductName,
        //                                       Barcode = barcode.Barcode,
        //                                       BatchNo = barcode.BatchCode,
        //                                       DP = barcode.DP,
        //                                       RP = product.RP,
        //                                       DiscPer = product.DiscountPer,
        //                                       DiscAmt = product.DiscountInRs,
        //                                       ProdCode = (int)product.ProductCode,
        //                                       TaxPer = tax.VatTax,
        //                                       MRP = barcode.MRP,
        //                                       BV = product.BV,
        //                                       PV = product.PV,
        //                                       CV = product.CV,
        //                                       TaxType = "VAT",
        //                                       CommissionPer = product.ProductCommission
        //                                   }).FirstOrDefault();


        //                }
        //                objProductModel.StockAvailable = (from stockAvail in entity.Im_CurrentStock
        //                                                  where stockAvail.BatchCode == objProductModel.Barcode.ToString() && stockAvail.ProdId == objProductModel.ProdCode.ToString() && stockAvail.FCode.Equals(CurrentPartyCode)
        //                                                  select stockAvail.Qty
        //                                         ).Sum();
        //            }

        //        }
        //        //calculations
        //        //objProductModel = DoCalculation(objProductModel, Qty);
        //        object valueIsCommissonAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsCommissonAdd.ToString());
        //        object valueIsDiscountAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsDiscountAdd.ToString());
        //        int IsCommission = Convert.ToInt32(valueIsCommissonAdd);
        //        int IsDiscount = Convert.ToInt32(valueIsDiscountAdd);
        //        objProductModel.IsCommissionAdd = IsCommission;
        //        objProductModel.IsDiscountAdd = IsDiscount;

        //    }
        //    catch(Exception e)
        //    {

        //    }

        //    return objProductModel;
        //}

        //public CustomerDetail GetCustInfo(string IdNo)
        //{
        //    CustomerDetail objCustomerDetail = new CustomerDetail();
        //    if (!(string.IsNullOrEmpty(IdNo)))
        //    {
        //        try {
        //            SqlConnection SC = new SqlConnection("Data Source=144.217.216.195;Initial Catalog=SjLabs;Integrated Security=False;User Id=usrsjl;Password=S90$#usr02J;");



        //            string query = "select a.FormNo,a.MemFirstName+' '+ a.MemLastName as Name,a.IDno as IDno,a.Address1+','+a.Address2+','+a.City as Address,a.StateCode as StateCode,b.idno as RefId,b.MemFirstName+' '+ b.MemLastName as RefName FROM M_MemberMaster a,M_MemberMaster b WHERE a.RefFormNo=b.FormNo AND a.IDno=@IdNo";
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = query;
        //            cmd.Parameters.AddWithValue("@IdNo", IdNo);
        //            cmd.Connection = SC;
        //            SC.Close();
        //            SC.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {

        //                    objCustomerDetail.IdNo = reader["IDno"] != null ? reader["IDno"].ToString() : "";
        //                    objCustomerDetail.ReferenceIdNo = reader["RefId"] != null ? reader["RefId"].ToString() : "";
        //                    objCustomerDetail.ReferenceName = reader["RefName"] != null ? reader["RefName"].ToString() : "";
        //                    objCustomerDetail.Name = reader["Name"] != null ? reader["Name"].ToString() : "";
        //                    objCustomerDetail.Address = reader["Address"] != null ? reader["Address"].ToString() : "";
        //                    objCustomerDetail.FormNo= reader["FormNo"] != null ? decimal.Parse(reader["FormNo"].ToString()) : 0;
        //                }
        //                else
        //                {
        //                    objCustomerDetail = null;
        //                }
        //            }
        //            SC.Close();
        //        }
        //        catch(Exception e)
        //        {

        //        }
        //      }

        //    return objCustomerDetail;
        //}

        //public List<string> GetAutocompleteProductNames()
        //{
        //    List<string> objProductNames = new List<string>();
        //    try
        //    {
        //        using(var entity=new InventoryEntities(enttConstr))
        //        {
        //            objProductNames = (from result in entity.ProductMasters
        //                               where result.IsActive == true
        //                               select result.ProductName).ToList();
        //        }
        //    }
        //    catch(Exception e){

        //    }

        //    return objProductNames;
        //}

        //public ResponseDetail SaveDistributorBill(DistributorBillModel objModel)
        //{
        //    ResponseDetail objResponse = new ResponseDetail();
        //    try
        //    {
        //        using (var entity = new InventoryEntities(enttConstr))
        //        {
        //            decimal maxSbillNo = (from result in entity.TrnBillMains select result.SBillNo).Max();
        //            maxSbillNo = maxSbillNo + 1;
        //            decimal? FsessId = (from result in entity.FiscalMasters where result.IsActive==true select result.FSessionId).FirstOrDefault();
        //            decimal? SessId = (from result in entity.M_SessnMaster select result.SessID).Max();
        //            SessId = SessId + 1;
        //            string billPrefix = (from result in entity.M_ConfigMaster select result.BillPrefix).FirstOrDefault();
        //            string version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();
        //            TrnPayModeDetail objDtPayModeDetail = new TrnPayModeDetail();
        //            List<string> Paymode=new List<string>();
        //            List<string> PayPrefix = new List<string>();
        //            List<TrnPayModeDetail> objDTListPayMode = new List<TrnPayModeDetail>();
        //            var resultPayMode = (from r in entity.M_PayModeMaster select r).ToList();
        //            //saving data in table
        //            // decimal? SessId=(from result in entity)


        //            if (objModel != null)
        //            {
        //                if (objModel.objProduct.PayDetails != null)
        //                {
        //                    if (objModel.objProduct.PayDetails.IsBD)
        //                    {
        //                        EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.BankDeposit;
        //                        string value = EnumPayModes.GetEnumDescription(enumVar);
        //                        PayPrefix.Add(value);
        //                        objDTListPayMode.Add(new TrnPayModeDetail {BillAmt= objModel.objProduct.TotalNetPayable,SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate=DateTime.Now,BillType="R",BillNo= billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value,Amount= objModel.objProduct.PayDetails.AmountByBD,BankCode=0,ChqDDDate=null,ChqDDNo="",CardNo="",Narration="",DUserId=0,DRecTimeStamp=null,BankName= objModel.objProduct.PayDetails.BDBankName, AcNo= objModel.objProduct.PayDetails.AccNo,IFSCode= objModel.objProduct.PayDetails.IFSCCode,ActiveStatus="Y", RecTimeStamp=DateTime.Now,UserId=objModel.objCustomer.UserDetails.UserId,Version= version, UserName=objModel.objCustomer.UserDetails.UserName, FSessId=FsessId??0,SBillNo=maxSbillNo});


        //                    }
        //                    if (objModel.objProduct.PayDetails.IsCC)
        //                    {
        //                        EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Card;
        //                        string value = EnumPayModes.GetEnumDescription(enumVar);
        //                        PayPrefix.Add(value);
        //                        objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now, BillType = "R", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value,AcNo="",IFSCode="",BankCode=0,Narration="",BankName="", DUserId=0,DRecTimeStamp=null,ChqDDNo="",ChqDDDate=null, Amount = objModel.objProduct.PayDetails.AmountByCard, CardNo = objModel.objProduct.PayDetails.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
        //                    }
        //                    if (objModel.objProduct.PayDetails.IsQ)
        //                    {
        //                        EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cheque;
        //                        string value = EnumPayModes.GetEnumDescription(enumVar);
        //                        PayPrefix.Add(value);
        //                        objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now, BillType = "R", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByCheque,CardNo="", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.CHBankName,ChqDDNo = objModel.objProduct.PayDetails.ChequeNo,ChqDDDate=objModel.objProduct.PayDetails.ChequeDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
        //                    }
        //                    if (objModel.objProduct.PayDetails.IsD)
        //                    {
        //                        EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.DD;
        //                        string value = EnumPayModes.GetEnumDescription(enumVar);
        //                        PayPrefix.Add(value);
        //                        objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now, BillType = "R", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByDD, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.DDBankName,ChqDDNo = objModel.objProduct.PayDetails.DDNo, ChqDDDate = objModel.objProduct.PayDetails.DDDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
        //                    }
        //                    if (objModel.objProduct.PayDetails.IsT)
        //                    {
        //                        EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Credit;
        //                        string value = EnumPayModes.GetEnumDescription(enumVar);
        //                        PayPrefix.Add(value);
        //                        objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now, BillType = "R", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value,BankName="", Amount = objModel.objProduct.PayDetails.AmountByCredit, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0,DUserId = 0, DRecTimeStamp = null,ChqDDDate=null,ChqDDNo="", Narration =objModel.objProduct.PayDetails.Narration, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
        //                    }
        //                    if (objModel.objProduct.PayDetails.IsV)
        //                    {
        //                        EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Voucher;
        //                        string value = EnumPayModes.GetEnumDescription(enumVar);
        //                        PayPrefix.Add(value);
        //                        objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now, BillType = "R", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByVoucher, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, DUserId = 0, DRecTimeStamp = null, ChqDDDate = null, ChqDDNo = "", Narration ="",BankName="", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
        //                    }
        //                    if (objModel.objProduct.PayDetails.IsW)
        //                    {
        //                        EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Wallet;
        //                        string value = EnumPayModes.GetEnumDescription(enumVar);
        //                        PayPrefix.Add(value);
        //                        objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now, BillType = "R", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByWallet,BankCode=0, BankName = "", AcNo = "", IFSCode = "",Narration="",DUserId=0,DRecTimeStamp=null,ChqDDNo="",ChqDDDate=null,CardNo="", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
        //                    }
        //                    if (objModel.objProduct.CashAmount>0)
        //                    {
        //                        EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cash;
        //                        string value = EnumPayModes.GetEnumDescription(enumVar);
        //                        PayPrefix.Add(value);
        //                        objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.CashAmount, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now, BillType = "R", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.TotalNetPayable,BankCode=0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
        //                    }
        //                    if (PayPrefix.Count > 0)
        //                    {

        //                        Paymode = (from r in entity.M_PayModeMaster where PayPrefix.Contains(r.Prefix) select r.PayMode).ToList();
        //                    }

        //                }


        //                foreach (var obj in objModel.objListProduct)
        //                {
        //                    TrnBillData objDTBillData = new TrnBillData();
        //                    objDTBillData.SBillNo = maxSbillNo;
        //                    objDTBillData.FSessId = FsessId ?? 0;
        //                    objDTBillData.SessId =SessId??0;
        //                    objDTBillData.ActiveStatus = "Y";
        //                    objDTBillData.BillDate = DateTime.Now;

        //                    objDTBillData.RefNo = "";
        //                    objDTBillData.RefId = "";
        //                    objDTBillData.RefName = "";
        //                    objDTBillData.Remarks = objModel.objCustomer.Remarks;
        //                    objDTBillData.CType = "M";
        //                    objDTBillData.SoldBy = objModel.objCustomer.UserDetails.PartyCode;
        //                    objDTBillData.BillBy = objDTBillData.SoldBy;
        //                    objDTBillData.BillNo = billPrefix+"/" + objDTBillData.BillBy + "/" + maxSbillNo;
        //                    objDTBillData.FType = "M";
        //                    objDTBillData.FCode = objModel.objCustomer.IdNo;
        //                    objDTBillData.PartyName = objModel.objCustomer.Name;
        //                    objDTBillData.SupplierId = 0;
        //                    objDTBillData.ChDDNo = 0;
        //                    objDTBillData.ChDate = DateTime.Now;
        //                    objDTBillData.ChAmt = 0;
        //                    objDTBillData.BankCode = 0;
        //                    objDTBillData.BankName = "";
        //                    objDTBillData.FormNo = objModel.objCustomer.FormNo;
        //                    objDTBillData.TotalTaxAmount = objModel.objProduct.TotalTaxAmount;
        //                    objDTBillData.TotalSTaxAmount = 0;
        //                    objDTBillData.TotalDiscount = objModel.objProduct.TotalDiscount;
        //                    objDTBillData.TotalKitBvValue = 0;
        //                    objDTBillData.TotalBvValue = objModel.objProduct.TotalBV;
        //                    objDTBillData.TotalCVValue = objModel.objProduct.TotalCV;
        //                    objDTBillData.TotalPVValue = objModel.objProduct.TotalPV;
        //                    objDTBillData.TotalRPValue = objModel.objProduct.TotalRP;

        //                    objDTBillData.DP = obj.DP??0;
        //                    objDTBillData.RP = obj.RP ?? 0;
        //                    objDTBillData.MRP = obj.MRP??0;
        //                    objDTBillData.CVValue = obj.CVValue ?? 0;
        //                    objDTBillData.CV = obj.CV ?? 0;
        //                    objDTBillData.PV = obj.PV ?? 0;
        //                    objDTBillData.BV = obj.BV ?? 0;
        //                    objDTBillData.BVValue = obj.BVValue ?? 0;
        //                    objDTBillData.PVValue = obj.PVValue ?? 0;
        //                    objDTBillData.RPValue = obj.RPValue ?? 0;
        //                    objDTBillData.Barcode = obj.Barcode.ToString();
        //                    objDTBillData.BatchNo = obj.BatchNo.ToString();
        //                    objDTBillData.TaxAmount = obj.TaxAmt??0;
        //                    objDTBillData.Tax = obj.TaxPer ?? 0;
        //                    objDTBillData.DiscountPer = obj.DiscPer ?? 0;
        //                    objDTBillData.Discount = obj.DiscAmt ?? 0;
        //                    objDTBillData.ProdCommssn = obj.CommissionPer ?? 0;
        //                    objDTBillData.ProdCommssnAmt = obj.CommissionAmt ?? 0;
        //                    objDTBillData.ProductId = obj.ProdCode.ToString();
        //                    objDTBillData.ProductName = obj.ProductName;
        //                    objDTBillData.Qty = obj.Quantity;
        //                    objDTBillData.Rate = obj.Rate??0;
        //                    objDTBillData.IsKitBV = "N";
        //                    objDTBillData.DSeries = "";
        //                    objDTBillData.DImported = "N";
        //                    objDTBillData.IMEINo = "";
        //                    objDTBillData.BNo = "";
        //                    objDTBillData.ItemType = "";



        //                    objDTBillData.JType = "Cash:" + objModel.objProduct.TotalNetPayable;
        //                    objDTBillData.BillTo = "R";
        //                    objDTBillData.BillFor = "RB";
        //                    objDTBillData.IsReceive = "R";
        //                    objDTBillData.IsCredit = "F";
        //                    objDTBillData.BillType = "R";
        //                    objDTBillData.ProdType = "P";
        //                    objDTBillData.PaymentDtl="Cash:" + objModel.objProduct.TotalNetPayable;
        //                    objDTBillData.TotalAmount = obj.TotalAmount;
        //                    objDTBillData.NetAmount = objModel.objProduct.TotalPayAmount;
        //                    objDTBillData.TaxType = obj.TaxType;
        //                    objDTBillData.CashDiscPer = obj.CashDiscPer;
        //                    objDTBillData.CashDiscAmount = obj.CashDiscAmount;
        //                    objDTBillData.NetPayable = objModel.objProduct.TotalNetPayable;
        //                    objDTBillData.RndOff = objModel.objProduct.Roundoff;
        //                    objDTBillData.CardAmount = 0;
        //                    objDTBillData.PayMode = Paymode.Count>1?string.Join(",",Paymode):Paymode[0];
        //                    objDTBillData.PayPrefix = PayPrefix.Count > 1 ? string.Join(",", PayPrefix) : PayPrefix[0];
        //                    objDTBillData.BvTransfer = "N";

        //                    objDTBillData.UserSBillNo = maxSbillNo;
        //                    objDTBillData.UserBillNo= billPrefix + "/" + objDTBillData.BillBy + "/" + maxSbillNo;

        //                    objDTBillData.DispatchStatus = "N";
        //                    objDTBillData.LR = "0";
        //                    objDTBillData.LRDate = DateTime.Now;
        //                    objDTBillData.TransporterName = "";
        //                    objDTBillData.DispatchTo = objModel.objCustomer.IdNo;
        //                    objDTBillData.FreightType = "";
        //                    objDTBillData.Series = "";
        //                    objDTBillData.Scratch = "";
        //                    objDTBillData.Unit = 0;
        //                    objDTBillData.PSessId = 0;
        //                    objDTBillData.DcNo = "";
        //                    objDTBillData.Imported = "N";
        //                    objDTBillData.FPoint=0;
        //                    objDTBillData.FPointValue = 0;
        //                    objDTBillData.OrdStatus = "";
        //                    objDTBillData.OrdQty=0;
        //                    objDTBillData.OrderType = "";
        //                    objDTBillData.OrderDate = DateTime.Now;
        //                    objDTBillData.OrderNo = "";
        //                    objDTBillData.RemQty = 0;
        //                    objDTBillData.DP1 = 0;
        //                    objDTBillData.DReason = "";
        //                    objDTBillData.DUserId = 0;
        //                    objDTBillData.DRecTimeStamp = DateTime.Now;
        //                    objDTBillData.DocWeight = 0;
        //                    objDTBillData.DocketNo = "";
        //                    objDTBillData.DocketDate = DateTime.Now;
        //                    objDTBillData.UserBillNo = "";
        //                    objDTBillData.UserSBillNo = 0;
        //                    objDTBillData.STNFormNo = "";
        //                    objDTBillData.StkRecv = "N";
        //                    objDTBillData.StkRecvDate = DateTime.Now;
        //                    objDTBillData.StkRecvUserId = 0;
        //                    objDTBillData.InTransit = "N";
        //                    objDTBillData.UID = "";
        //                    objDTBillData.OfferUID = 0;
        //                    objDTBillData.IsKit = "N";
        //                    objDTBillData.TotalCorton = "";
        //                    objDTBillData.TotalMonoCorton = "";
        //                    objDTBillData.SpclOfferId = 0;
        //                    objDTBillData.VAT = 0;
        //                    objDTBillData.BuyerAddress = "";
        //                    objDTBillData.BuyerTIN = "";

        //                    objDTBillData.TotalDiscount = objModel.objProduct.TotalDiscPer;
        //                    objDTBillData.TotalDiscountAmt = objModel.objProduct.TotalDiscount;
        //                    objDTBillData.VDiscountAmt = 0;
        //                    objDTBillData.VDiscount = 0;
        //                    objDTBillData.ReceiverID = "";
        //                    objDTBillData.ReceiverName = "";
        //                    objDTBillData.ReceiverMNo = "";
        //                    objDTBillData.ReceiverIDProof = "";
        //                    objDTBillData.TotalFPoint = 0;
        //                    objDTBillData.TotalQty = objModel.objProduct.TotalQty;
        //                    objDTBillData.CashReward = 0;
        //                    objDTBillData.CommssnAmt = objModel.objProduct.TotalCommsonAmt;
        //                    objDTBillData.RecvAmount = 0;
        //                    objDTBillData.ReturnToCustAmt = 0;
        //                    objDTBillData.ActiveStatus = "Y";
        //                    objDTBillData.RecTimeStamp = DateTime.Now;
        //                    objDTBillData.UserId = objModel.objCustomer.UserDetails.UserId;
        //                    objDTBillData.UserName= objModel.objCustomer.UserDetails.UserName;
        //                    objDTBillData.DelvPlace =string.IsNullOrEmpty(objModel.objProduct.DeliveryPlace)?"": objModel.objProduct.DeliveryPlace;
        //                    objDTBillData.DelvStatus = "";
        //                    objDTBillData.DelvUserId = 0;
        //                    objDTBillData.DelvRecTimeStamp = DateTime.Now;
        //                    objDTBillData.Version = version;
        //                    objDTBillData.IDType = "";
        //                    objDTBillData.BranchName = "";
        //                    objDTBillData.CourierId = 0;
        //                    objDTBillData.CourierName = "";
        //                    objDTBillData.LocId = 0;
        //                    objDTBillData.LocName = "";
        //                    objDTBillData.DelvAddress = "";
        //                    objDTBillData.Pincode = "";
        //                    objDTBillData.UnitName = "";
        //                    entity.TrnBillDatas.Add(objDTBillData);
        //                }
        //                try
        //                {
        //                    int i = entity.SaveChanges();
        //                    if (i == objModel.objListProduct.Count)
        //                    {

        //                        foreach(var obj in objDTListPayMode)
        //                        {
        //                            TrnPayModeDetail objTemp = new TrnPayModeDetail();
        //                            objTemp = obj;
        //                            objTemp.PayMode = (from r in resultPayMode where r.Prefix.Trim()==obj.PayPrefix.Trim() select r.PayMode).FirstOrDefault();
        //                            entity.TrnPayModeDetails.Add(objTemp);
        //                        }
        //                        i = 0;
        //                        i = entity.SaveChanges();
        //                        if (i == objDTListPayMode.Count)
        //                        {
        //                            objResponse.ResponseMessage = "Saved Successfully!";
        //                            objResponse.ResponseStatus = "OK";
        //                        }
        //                    }
        //                }
        //                catch (DbEntityValidationException e)
        //                {

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return objResponse;
        //}

        //public List<BankModel> GetBankList()
        //{
        //    List<BankModel> objListBanks = new List<BankModel>();
        //    try
        //    {
        //        using(var entity=new InventoryEntities(enttConstr))
        //        {
        //            objListBanks = (from result in entity.M_BankMaster
        //                            where result.ActiveStatus == "Y"
        //                            select new BankModel
        //                            {
        //                                BankCode = (int)result.BankCode,
        //                                BankName = result.BankName,
        //                                ActiveStatus=result.ActiveStatus,
        //                                AccNo=result.AcNo,
        //                                Remarks=result.Remarks,
        //                                 IFSCCode=result.IFSCode,

        //                            }).ToList();
        //        }
        //    }
        //    catch(Exception e)
        //    {

        //    }
        //    return objListBanks;
        //}

        public TransactionAPIController()
        {
            ConnModel obj;
            if (System.Web.HttpContext.Current.Session["ConModel"] == null)
                obj = objGetConn.GetConstr();

            obj = (System.Web.HttpContext.Current.Session["ConModel"] as ConnModel);
            dbName = obj.Db;
            invDbName = obj.InvDb;
            enttConstr = obj.EnttConnStr;
            AppConstr = obj.AppConnStr;
            InvConstr = obj.InvConnStr;
            WRPartyCode = obj.WRPartyCode;
            CompName = obj.CompName;
        }

        public List<ProductModel> GetproductInfo(string SearchType, string data, bool isCForm, string BillType, decimal CurrentStateCode, string CurrentPartyCode, bool IsBillOnMrp)
        {
            List<ProductModel> objProductModel = new List<ProductModel>();
            List<ProductModel> TempResult = new List<ProductModel>();
            bool searchByProductFlag = true;
            if (SearchType == "B")
            {
                searchByProductFlag = false;
            }

            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    using (var entity = new InventoryEntities(enttConstr))
                    {

                        if (searchByProductFlag)
                        {

                            TempResult = (from product in entity.M_ProductMaster
                                          where ((SearchType == "P" && product.ProductName.ToLower().Equals(data.ToLower())) || (SearchType == "C" && product.ProdId == data)) && product.ActiveStatus == "Y" //&& product.IsCardIssue == "N"
                                          join barcode in entity.M_BarCodeMaster on product.ProdId equals barcode.ProdId

                                          join tax in entity.M_TaxMaster on product.ProdId equals tax.ProdCode
                                          //where tax.StateCode == CurrentStateCode
                                          from c in entity.M_CatMaster
                                          where c.CatId == product.CatId
                                          select new ProductModel
                                          {
                                              ProdForBill = product.Imported,
                                              ProductName = product.ProductName,
                                              CatId = product.CatId,
                                              CatName = c.CatName,
                                              Barcode = barcode.BarCode,
                                              BatchNo = barcode.BatchNo,
                                              DP = barcode.DP,
                                              RP = product.RP,
                                              DiscPer = product.Discount,
                                              DiscAmt = product.DiscInRs,
                                              ProdCode = (int)product.ProductCode,
                                              ProductCodeStr = product.ProdId,
                                              TaxPer = tax.VatTax,
                                              ProdStateCode = tax.StateCode,
                                              MRP = barcode.MRP,
                                              BV = product.BV,
                                              PV = product.PV,
                                              CV = product.CV,
                                              Weight = product.Weight,
                                              IsExpirable = barcode.IsExpired == "Y" ? true : false,
                                              ExpDate = barcode.ExpDate,
                                              TaxType = "GST",
                                              Rate = product.PurchaseRate,
                                              CommissionPer = product.ProdCommssn,
                                              SubCatId = product.SubCatId
                                          }).ToList();


                        }
                        else
                        {
                            decimal? BarCodeData = decimal.Parse(data);


                            TempResult = (from product in entity.M_ProductMaster
                                          where product.ActiveStatus == "Y"
                                          join barcode in entity.M_BarCodeMaster on product.ProdId equals barcode.ProdId
                                          where barcode.BarCode == data
                                          join tax in entity.M_TaxMaster on product.ProdId equals tax.ProdCode
                                          // where tax.StateCode==CurrentStateCode 
                                          from c in entity.M_CatMaster
                                          where c.CatId == product.CatId
                                          select new ProductModel
                                          {
                                              ProductName = product.ProductName,
                                              CatId = product.CatId,
                                              CatName = c.CatName,
                                              Barcode = barcode.BarCode,
                                              BatchNo = barcode.BatchNo,
                                              DP = barcode.DP,
                                              RP = product.RP,
                                              ProdStateCode = tax.StateCode,
                                              DiscPer = product.Discount,
                                              DiscAmt = product.DiscInRs,
                                              ProdCode = (int)product.ProductCode,
                                              ProductCodeStr = product.ProdId,
                                              TaxPer = tax.VatTax,
                                              MRP = barcode.MRP,
                                              BV = product.BV,
                                              PV = product.PV,
                                              CV = product.CV,
                                              IsExpirable = barcode.IsExpired == "Y" ? true : false,
                                              ExpDate = barcode.ExpDate,
                                              TaxType = "GST",
                                              Rate = product.PurchaseRate,
                                              CommissionPer = product.ProdCommssn,
                                              IsCardIssue = product.IsCardIssue
                                          }).ToList();
                            if (CurrentPartyCode != WRPartyCode)
                                TempResult = TempResult.Where(m => m.IsCardIssue == "N").ToList();

                        }
                        bool IsDistributorBill = false;
                        bool IsPartyBill = false;
                        bool IsCustomerBill = false;
                        bool IsPurchaseInvoice = false;
                        bool IsOrderCreation = false;
                        bool IsPendingOrder = false;
                        if (BillType == "distributor")
                        {
                            IsDistributorBill = true;
                        }
                        else
                        {
                            IsDistributorBill = false;
                        }
                        if (BillType == "party")
                        {
                            IsPartyBill = true;
                        }
                        else
                        {
                            IsPartyBill = false;
                        }
                        if (BillType == "customer")
                        {
                            IsCustomerBill = true;
                        }
                        else
                        {
                            IsCustomerBill = false;
                        }
                        if (BillType == "purchase")
                        {
                            IsPurchaseInvoice = true;
                        }
                        else
                        {
                            IsPurchaseInvoice = false;
                        }
                        if (BillType == "order")
                        {
                            IsOrderCreation = true;
                        }
                        else
                        {
                            IsOrderCreation = false;
                        }
                        if (BillType == "pendingorder")
                        {
                            IsPendingOrder = true;
                        }
                        else
                        {
                            IsPendingOrder = false;
                        }

                        foreach (var obj in TempResult)
                        {
                            ProductModel TempObj = new ProductModel();
                            if ((obj.IsExpirable && obj.ExpDate > DateTime.Now) || (obj.IsExpirable == false))
                            {
                                TempObj = obj;
                                object valueIsDiscountAdd = 0;
                                object valueIsCommissonAdd = 0;
                                if (IsDistributorBill || IsCustomerBill || IsPurchaseInvoice || IsOrderCreation || IsPendingOrder)
                                {
                                    valueIsCommissonAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsCommissonAdd.ToString());
                                    valueIsDiscountAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsDiscountAdd.ToString());

                                }
                                else
                                {
                                    valueIsCommissonAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsCommissonAddOnPartyBill.ToString());
                                    valueIsDiscountAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsDiscountAddOnPartyBill.ToString());

                                }
                                int IsCommission = Convert.ToInt32(valueIsCommissonAdd);
                                int IsDiscount = Convert.ToInt32(valueIsDiscountAdd);
                                TempObj.IsCommissionAdd = IsCommission;
                                TempObj.IsDiscountAdd = IsDiscount;
                                TempObj.StockAvailable = (from stockAvail in entity.Im_CurrentStock
                                                          where stockAvail.BatchCode == TempObj.Barcode.ToString() && stockAvail.ProdId == TempObj.ProductCodeStr.ToString() && stockAvail.FCode.Equals(CurrentPartyCode)
                                                          select stockAvail.Qty
                                                     ).DefaultIfEmpty(0).Sum();
                                //var RatesMaster = (from r in entity.M_ProdRatesMaster where r.ProdID == TempObj.ProductCodeStr && r.SoldBy==CurrentPartyCode select r).FirstOrDefault();
                                //if (RatesMaster != null)
                                //{
                                //    TempObj.MRP = RatesMaster.MRP;
                                //    TempObj.DP = RatesMaster.DP;
                                //    TempObj.BV = RatesMaster.BV;
                                //    TempObj.DiscPer = RatesMaster.Discount;
                                //}
                                if (IsCustomerBill)
                                {
                                    TempObj.DP = obj.MRP;
                                }
                                else
                                {
                                    if (!IsPurchaseInvoice && IsBillOnMrp)
                                    {
                                        TempObj.DP = obj.MRP;
                                    }
                                }
                                CurrentStateCode = (int)(from r in entity.M_CompanyMaster select r.CompState).FirstOrDefault();
                                //if (TempObj.ProdStateCode == CurrentStateCode)
                                //{
                                //    TempObj.TaxPer = obj.TaxPer;
                                //}
                                //else
                                //{
                                //    TempObj.TaxPer = 0;
                                //}
                                objProductModel.Add(TempObj);
                            }


                        }
                        if (objProductModel.Count > 0)
                        {
                            objProductModel = objProductModel.OrderByDescending(m => m.StockAvailable).ToList();
                        }
                    }

                }
                //calculations
                //objProductModel = DoCalculation(objProductModel, Qty);


            }
            catch (Exception e)
            {

            }

            return objProductModel;
        }

        public List<ProductModel> GetproductInfoOfferWiseRate(string SearchType, string data, bool isCForm, string BillType, decimal CurrentStateCode, string CurrentPartyCode, bool IsBillOnMrp, int OfferID)
        {
            List<ProductModel> objProductModel = new List<ProductModel>();
            List<ProductModel> TempResult = new List<ProductModel>();
            bool searchByProductFlag = true;
            if (SearchType == "B")
            {
                searchByProductFlag = false;
            }

            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    SqlConnection SC = new SqlConnection(InvConstr);
                    string qry = "Select p.ProductName,p.CatId,c.CatName,b.BarCode,b.BatchNo,ISNULL(r.DP,b.DP) DP,p.RP,p.Discount as DiscPer,p.DiscInRs as DiscAmt,p.ProdId,t.VatTax,t.StateCode,b.MRP,p.BV,p.PV,p.CV,p.Weight,b.IsExpired,b.ExpDate,p.PurchaseRate,p.ProdCommssn,p.SubCatID " +
                        " FROM M_BarCodeMaster b, M_TaxMaster t, M_CatMaster c,M_ProductMaster p LEFT JOIN M_ProdRateMaster r ON p.ProdID=r.ProdID AND r.OfferID='" + OfferID.ToString() + "' WHERE p.CatID=c.CatID AND p.ProdID=b.ProdID AND p.ProdID=t.ProdCode " +
                        " AND (('" + SearchType + "' = 'P' AND p.ProductName='" + data + "') OR ('" + SearchType + "'='C' AND p.ProdId = '" + data + "') OR ('" + SearchType + "'='B' AND b.Barcode='" + data + "')) AND p.ActiveStatus = 'Y'";
                    SC.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(qry, SC);
                    DataTable Dt = new DataTable();
                    Da.Fill(Dt);
                    SC.Close();
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ProductModel objTmp = new ProductModel();
                        objTmp.ProductName = dr["ProductName"].ToString();
                        objTmp.CatId = Convert.ToDecimal(dr["CatId"].ToString());
                        objTmp.CatName = dr["CatName"].ToString();
                        objTmp.Barcode = dr["Barcode"].ToString();
                        objTmp.BatchNo = dr["BatchNo"].ToString();
                        objTmp.DP = Convert.ToDecimal(dr["DP"].ToString());
                        objTmp.RP = Convert.ToDecimal(dr["RP"].ToString());
                        objTmp.DiscPer = Convert.ToDecimal(dr["DiscPer"].ToString());
                        objTmp.DiscAmt = Convert.ToDecimal(dr["DiscAmt"].ToString());
                        objTmp.ProdCode = Convert.ToInt32(dr["ProdID"].ToString());
                        objTmp.TaxPer = Convert.ToDecimal(dr["VatTax"].ToString());
                        objTmp.ProdStateCode = Convert.ToDecimal(dr["StateCode"].ToString());
                        objTmp.ProductCodeStr = dr["ProdID"].ToString();
                        objTmp.MRP = Convert.ToDecimal(dr["MRP"].ToString());
                        objTmp.BV = Convert.ToDecimal(dr["BV"].ToString());
                        objTmp.PV = Convert.ToDecimal(dr["PV"].ToString());
                        objTmp.CV = Convert.ToDecimal(dr["CV"].ToString());
                        objTmp.Weight = Convert.ToDecimal(dr["Weight"].ToString());
                        objTmp.IsExpirable = dr["IsExpired"].ToString() == "Y" ? true : false;
                        objTmp.ExpDate = Convert.ToDateTime(dr["ExpDate"].ToString());
                        objTmp.TaxType = "GST";
                        objTmp.Rate = Convert.ToDecimal(dr["PurchaseRate"].ToString());
                        objTmp.CommissionPer = Convert.ToDecimal(dr["ProdCommssn"].ToString());
                        objTmp.SubCatId = Convert.ToDecimal(dr["SubCatId"]);

                        TempResult.Add(objTmp);
                    }
                    using (var entity = new InventoryEntities(enttConstr))
                    {

                        bool IsDistributorBill = false;
                        bool IsPartyBill = false;
                        bool IsCustomerBill = false;
                        bool IsPurchaseInvoice = false;
                        bool IsOrderCreation = false;
                        bool IsPendingOrder = false;
                        if (BillType == "distributor")
                        {
                            IsDistributorBill = true;
                        }
                        else
                        {
                            IsDistributorBill = false;
                        }
                        if (BillType == "party")
                        {
                            IsPartyBill = true;
                        }
                        else
                        {
                            IsPartyBill = false;
                        }
                        if (BillType == "customer")
                        {
                            IsCustomerBill = true;
                        }
                        else
                        {
                            IsCustomerBill = false;
                        }
                        if (BillType == "purchase")
                        {
                            IsPurchaseInvoice = true;
                        }
                        else
                        {
                            IsPurchaseInvoice = false;
                        }
                        if (BillType == "order")
                        {
                            IsOrderCreation = true;
                        }
                        else
                        {
                            IsOrderCreation = false;
                        }
                        if (BillType == "pendingorder")
                        {
                            IsPendingOrder = true;
                        }
                        else
                        {
                            IsPendingOrder = false;
                        }

                        foreach (var obj in TempResult)
                        {
                            ProductModel TempObj = new ProductModel();
                            if ((obj.IsExpirable && obj.ExpDate > DateTime.Now) || (obj.IsExpirable == false))
                            {
                                TempObj = obj;
                                object valueIsDiscountAdd = 0;
                                object valueIsCommissonAdd = 0;
                                if (IsDistributorBill || IsCustomerBill || IsPurchaseInvoice || IsOrderCreation || IsPendingOrder)
                                {
                                    valueIsCommissonAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsCommissonAdd.ToString());
                                    valueIsDiscountAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsDiscountAdd.ToString());

                                }
                                else
                                {
                                    valueIsCommissonAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsCommissonAddOnPartyBill.ToString());
                                    valueIsDiscountAdd = Enum.Parse(typeof(Enums.CalculationConditionalVar), Enums.CalculationConditionalVar.IsDiscountAddOnPartyBill.ToString());

                                }
                                int IsCommission = Convert.ToInt32(valueIsCommissonAdd);
                                int IsDiscount = Convert.ToInt32(valueIsDiscountAdd);
                                TempObj.IsCommissionAdd = IsCommission;
                                TempObj.IsDiscountAdd = IsDiscount;
                                TempObj.StockAvailable = (from stockAvail in entity.Im_CurrentStock
                                                          where stockAvail.BatchCode == TempObj.Barcode.ToString() && stockAvail.ProdId == TempObj.ProductCodeStr.ToString() && stockAvail.FCode.Equals(CurrentPartyCode)
                                                          select stockAvail.Qty
                                                     ).DefaultIfEmpty(0).Sum();
                                //var RatesMaster = (from r in entity.M_ProdRatesMaster where r.ProdID == TempObj.ProductCodeStr && r.SoldBy==CurrentPartyCode select r).FirstOrDefault();
                                //if (RatesMaster != null)
                                //{
                                //    TempObj.MRP = RatesMaster.MRP;
                                //    TempObj.DP = RatesMaster.DP;
                                //    TempObj.BV = RatesMaster.BV;
                                //    TempObj.DiscPer = RatesMaster.Discount;
                                //}
                                if (IsCustomerBill)
                                {
                                    TempObj.DP = obj.MRP;
                                }
                                else
                                {
                                    if (!IsPurchaseInvoice && IsBillOnMrp)
                                    {
                                        TempObj.DP = obj.MRP;
                                    }
                                }
                                CurrentStateCode = (int)(from r in entity.M_CompanyMaster select r.CompState).FirstOrDefault();
                                if (TempObj.ProdStateCode == CurrentStateCode)
                                {
                                    TempObj.TaxPer = obj.TaxPer;
                                }
                                else
                                {
                                    TempObj.TaxPer = 0;
                                }
                                objProductModel.Add(TempObj);
                            }


                        }
                        if (objProductModel.Count > 0)
                        {
                            objProductModel = objProductModel.OrderByDescending(m => m.StockAvailable).ToList();
                        }
                    }

                }
                //calculations
                //objProductModel = DoCalculation(objProductModel, Qty);


            }
            catch (Exception e)
            {

            }

            return objProductModel;
        }

        public List<KitDetail> GetRateOfferList()
        {
            List<KitDetail> objOffers = new List<KitDetail>();
            try
            {
                string qry = "Select * FROM M_OfferMaster WHERE ActiveStatus='Y'";
                SqlConnection SC = new SqlConnection(InvConstr);
                SC.Open();
                SqlDataAdapter Da = new SqlDataAdapter(qry, SC);
                DataTable Dt = new DataTable();
                Da.Fill(Dt);
                SC.Close();
                foreach (DataRow Dr in Dt.Rows)
                {
                    KitDetail obj = new KitDetail();
                    obj.KitId = Convert.ToDecimal(Dr["OfferID"].ToString());
                    obj.KitName = Dr["OfferName"].ToString();
                    obj.KitAmount = Convert.ToDecimal(Dr["OfferminAmt"].ToString());
                    objOffers.Add(obj);
                }
            }
            catch (Exception ex)
            {
            }
            return objOffers;
        }
        public string GetStateGstName(decimal StateCode)
        {
            string statename = "";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    string AppConnectionString = AppConstr;
                    SqlConnection SC = new SqlConnection(AppConnectionString);
                    string query = "Select StateName +' ('+ CASE WHEN LEN(Cast(DivisionCode as varchar(5)))=1 THEN '0'+Cast(DivisionCode as varchar(5)) ELSE Cast(DivisionCode as varchar(5)) END +')' as S, * FROM M_StateDivMaster WHERE RowStatus='Y' AND StateCode>0 AND StateCode=@StateCode";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@StateCode", StateCode);
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            statename = reader["S"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return statename;
        }

        public CustomerDetail GetCustName(string IdNo)
        {
            CustomerDetail objCustomerDetail = new CustomerDetail();
            if (!(string.IsNullOrEmpty(IdNo)))
            {
                try
                {
                    string AppConnectionString = AppConstr;
                    SqlConnection SC = new SqlConnection(AppConnectionString);

                    string query = "select a.Mobl,a.FormNo,a.MemFirstName+' '+ a.MemLastName as MemName,StateCode,a.Address1+','+a.Address2+','+a.City as Address FROM M_MemberMaster a WHERE a.IDno=@IdNo";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@IdNo", IdNo);
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            objCustomerDetail.Name = reader["Name"] != null ? reader["Name"].ToString() : "";
                            objCustomerDetail.Address = reader["Address"] != null ? reader["Address"].ToString() : "";
                            objCustomerDetail.FormNo = reader["FormNo"] != null ? decimal.Parse(reader["FormNo"].ToString()) : 0;
                            objCustomerDetail.StateCode = reader["StateCode"] != null ? decimal.Parse(reader["StateCode"].ToString()) : 0;
                            objCustomerDetail.MobileNo = reader["Mobl"] != null ? reader["Mobl"].ToString() : "";
                        }
                    }
                    if (SC.State == ConnectionState.Open) SC.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return objCustomerDetail;
        }

        public CustomerDetail GetCustInfo(string IdNo)
        {
            CustomerDetail objCustomerDetail = new CustomerDetail();
            if (!(string.IsNullOrEmpty(IdNo)))
            {
                try
                {
                    using (var entity = new InventoryEntities(enttConstr))
                    {

                        ConnModel objCon = objGetConn.GetDistBillInfo();
                        string AppConnectionString = objCon.AppConnStr;// System.Web.HttpContext.Current.Session["AppDBConStr"].ToString();


                        SqlConnection SC = new SqlConnection(AppConnectionString);

                        string query = "select a.AadharNo3,IIF(a.StateCode>0, s.StateName ,'') as State, a.City ,a.EMail,a.Doj,a.UpgradeDate,a.Mobl,a.FormNo,a.MemFirstName+' '+ a.MemLastName as Name,a.KitId,a.IDno as IDno,a.Address1+','+a.Address2+','+a.City + IIF(a.StateCode>0,' ['+ s.StateName +']','') + '-' + Cast(a.Pincode as varchar(10)) as Address,Cast(a.Pincode as varchar(10)) as Pincode,a.StateCode as StateCode,a.ActiveStatus as ActiveStatus,a.IsBlock as IsBlock,ISNULL(b.idno,'') as RefId,ISNULL(b.MemFirstName+' '+ b.MemLastName,'') as RefName,a.PanNo FROM M_MemberMaster a LEFT JOIN M_MemberMaster b ON a.RefFormNo=b.FormNo LEFT JOIN M_StateDivMaster s ON a.StateCode=s.StateCode WHERE a.IDno=@IdNo";
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@IdNo", IdNo);
                        cmd.Connection = SC;
                        SC.Close();
                        SC.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                objCustomerDetail.Aadhar = reader["AadharNo3"] != null ? reader["AadharNo3"].ToString() : "";
                                objCustomerDetail.Email = reader["EMail"] != null ? reader["EMail"].ToString() : "";
                                objCustomerDetail.ZipCode = reader["Pincode"] != null ? reader["Pincode"].ToString() : "";
                                objCustomerDetail.IdNo = reader["IDno"] != null ? reader["IDno"].ToString() : "";
                                objCustomerDetail.ReferenceIdNo = reader["RefId"] != null ? reader["RefId"].ToString() : "";
                                objCustomerDetail.ReferenceName = reader["RefName"] != null ? reader["RefName"].ToString() : "";
                                objCustomerDetail.Name = reader["Name"] != null ? reader["Name"].ToString() : "";
                                objCustomerDetail.Address = reader["Address"] != null ? reader["Address"].ToString() : "";
                                objCustomerDetail.FormNo = reader["FormNo"] != null ? decimal.Parse(reader["FormNo"].ToString()) : 0;
                                objCustomerDetail.StateCode = reader["StateCode"] != null ? decimal.Parse(reader["StateCode"].ToString()) : 0;
                                objCustomerDetail.MobileNo = reader["Mobl"] != null ? reader["Mobl"].ToString() : "";
                                objCustomerDetail.KitId = reader["KitId"] != null ? int.Parse(reader["KitId"].ToString()) : 0;
                                objCustomerDetail.PANNo = reader["PanNo"] != null ? reader["PanNo"].ToString() : "";
                                objCustomerDetail.Doj = reader["Doj"] != null ? reader["Doj"].ToString() : "";
                                objCustomerDetail.UpgradeDate = reader["UpgradeDate"] != null ? reader["UpgradeDate"].ToString() : "";
                                objCustomerDetail.City = reader["City"] != null ? reader["City"].ToString() : "";
                                objCustomerDetail.State = reader["State"] != null ? reader["State"].ToString() : "";
                                if (reader["IsBlock"] != null)
                                {
                                    var BlockValue = reader["IsBlock"].ToString();
                                    if (BlockValue == "Y")
                                        objCustomerDetail.IsBlock = true;
                                    else
                                        objCustomerDetail.IsBlock = false;
                                }
                                else
                                {
                                    objCustomerDetail.IsBlock = false;
                                }

                                var list = (from r in entity.TrnBillMains where r.FCode == IdNo && r.ActiveStatus == "Y" && r.FType == "M" select r).ToList();
                                objCustomerDetail.MonthBill = 0;
                                foreach (var bill in list)
                                {
                                    if (bill.BillDate.Year == DateTime.Now.Year && bill.BillDate.Month == DateTime.Now.Month)
                                    {
                                        objCustomerDetail.MonthBill = objCustomerDetail.MonthBill + 1;
                                    }
                                }

                                objCustomerDetail.NumberOfBill = list.Count();

                                objCustomerDetail.NumberOfBill += 1;// like 2 bill are made, this is third bill.
                                objCustomerDetail.MonthBill += 1;
                                if (list == null)
                                {
                                    objCustomerDetail.IsFirstBillOffer = true;
                                }
                                else
                                {
                                    objCustomerDetail.IsFirstBillOffer = false;
                                }
                                objCustomerDetail.IsFirstBill = false;
                                objCustomerDetail.IsBillOnMrp = false;
                                if (objCon.FirstActivationBill == "Y")
                                {
                                    if (reader["ActiveStatus"] != null)
                                    {
                                        var ActiveS = reader["ActiveStatus"].ToString();
                                        if (ActiveS == "Y")
                                            objCustomerDetail.IsActive = true;
                                        else
                                        {
                                            objCustomerDetail.IsActive = false;
                                            objCustomerDetail.IsFirstBill = true;
                                            if (objCon.FirstBillOnMRP == "Y")
                                            {
                                                objCustomerDetail.IsBillOnMrp = true;
                                            }
                                        }

                                    }
                                }




                                //03Sep18
                                //if (objCustomerDetail.IsFirstBill)
                                //{
                                //    objCustomerDetail.IsBillOnMrp = true;
                                //}
                                //else
                                //{
                                //    objCustomerDetail.IsBillOnMrp = false;
                                //}
                                //if (reader["ActiveStatus"] != null)
                                //{
                                //    var ActiveS = reader["ActiveStatus"].ToString();
                                //    if (ActiveS == "Y")
                                //    {

                                //}
                                //else
                                //{
                                //    objCustomerDetail.IsActive = false;
                                //    objCustomerDetail.IsBillOnMrp = true;                                        
                                //}
                                //}

                                //objCustomerDetail.MinRepurch = (from r in entity.M_ConfigMaster select r.MinRepurch).DefaultIfEmpty(0).FirstOrDefault();


                                //objCustomerDetail.WalletBalance = GetWalletBalance(IdNo);
                            }
                            else
                            {
                                objCustomerDetail = new CustomerDetail();
                                objCustomerDetail.IdNo = "Record does not exists!";
                                objCustomerDetail.Name = "";
                            }
                        }
                        SC.Close();

                        if (objCustomerDetail != null)
                        {

                            query = "Select * FROM dbo.ufnGetBalance('" + objCustomerDetail.FormNo + "','" + (objCon.WalletType ?? "R") + "')";
                            cmd = new SqlCommand();
                            cmd.CommandText = query;
                            //cmd.Parameters.AddWithValue("@IdNo", IdNo);
                            cmd.Connection = SC;
                            SC.Close();
                            SC.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    if (reader.Read())
                                    {
                                        objCustomerDetail.WalletBalance = decimal.Parse(reader["Balance"].ToString());
                                    }
                                }
                                 catch(Exception ex)
                                {

                                }
                                
                            }

                            ConnModel obj = (System.Web.HttpContext.Current.Session["ConModel"] as ConnModel);
                            if (obj.CompID == "1020")/*For Bigway */
                            {
                                query = "Select * FROM dbo.ufnGetCouponBalance('" + objCustomerDetail.FormNo + "','C')";
                                cmd = new SqlCommand();
                                cmd.CommandText = query;
                                //cmd.Parameters.AddWithValue("@IdNo", IdNo);
                                cmd.Connection = SC;
                                SC.Close();
                                SC.Open();
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        objCustomerDetail.CouponWalletAmount = decimal.Parse(reader["Balance"].ToString());
                                    }
                                }
                            }
                            else
                            {
                                objCustomerDetail.CouponWalletAmount = 0;
                            }
                            if (obj.CompID == "1007")/*For Vadic */
                            {
                                query = "Select * FROM dbo.ufnGetBalance('" + objCustomerDetail.FormNo + "','S')";
                                cmd = new SqlCommand();
                                cmd.CommandText = query;
                                cmd.Connection = SC;
                                SC.Close();
                                SC.Open();
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        objCustomerDetail.MainWalletBalance = decimal.Parse(reader["Balance"].ToString());
                                    }
                                }
                            }
                            else
                            {
                                objCustomerDetail.MainWalletBalance = 0;
                            }

                            //query = "Select * FROM dbo.ufnGetBalance('" + objCustomerDetail.FormNo + "','M')";
                            //cmd = new SqlCommand();
                            //cmd.CommandText = query;
                            ////cmd.Parameters.AddWithValue("@IdNo", IdNo);
                            //cmd.Connection = SC;
                            //SC.Close();
                            //SC.Open();
                            //using (SqlDataReader reader = cmd.ExecuteReader())
                            //{
                            //    if (reader.Read())
                            //    {
                            //        objCustomerDetail.MainWalletBalance = decimal.Parse(reader["Balance"].ToString());
                            //    }
                            //}

                            decimal Ktamt = 0;
                            objCustomerDetail.MinBillAmt = 0;
                            objCustomerDetail.MinRepurch = 0;
                            objCustomerDetail.MinPV = 0;
                            //  string db = dbName;
                            // string dbInv = invDbName;
                            if (objCon.CompID != "1032")
                            {
                                if (objCustomerDetail.IsFirstBill == true && (objCon.FirstBillonAmt == "Y" || objCon.FirstBillonBV == "Y" || objCon.FirstBillonPV == "Y"))
                                {
                                    string colmName = "KitAmount";
                                    if (objCon.FirstBillonBV == "Y")
                                        colmName = "BV";
                                    else if (objCon.FirstBillonPV == "Y")
                                        colmName = "PV";

                                    if ((objCon.FirstBillonBV == "Y" && objCon.FirstBillMinBV > 0) || (objCon.FirstBillonPV == "Y" && objCon.FirstBillMinPV > 0))
                                    {
                                        if (objCon.FirstBillonBV == "Y")
                                            objCustomerDetail.MinRepurch = objCon.FirstBillMinBV;
                                        else if (objCon.FirstBillonPV == "Y")
                                            objCustomerDetail.MinPV = objCon.FirstBillMinPV;
                                    }
                                    else
                                    {
                                        query = "Select Min(" + colmName + ") KitAmount FROM " + dbName + "..M_KitMaster WHERE MacAdrs='O' AND ActiveStatus='Y' and Kitid=2";
                                        cmd = new SqlCommand();
                                        cmd.CommandText = query;
                                        //cmd.Parameters.AddWithValue("@IdNo", IdNo);
                                        cmd.Connection = SC;
                                        SC.Close();
                                        SC.Open();
                                        string MacAdres = "";
                                        using (SqlDataReader reader = cmd.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                Ktamt = !String.IsNullOrEmpty(reader["KitAmount"].ToString()) ? decimal.Parse(reader["KitAmount"].ToString()) : 0;
                                                //MacAdres = reader["MacAdrs"] != null ? reader["MacAdrs"].ToString() : "";
                                                break;
                                            }
                                        }

                                        if (objCon.FirstBillonBV == "Y")
                                            objCustomerDetail.MinRepurch = Ktamt;
                                        if (objCon.FirstBillonAmt == "Y")
                                            objCustomerDetail.MinBillAmt = Ktamt;
                                        if (objCon.FirstBillonPV == "Y")
                                            objCustomerDetail.MinPV = Ktamt;
                                    }
                                }
                            }
                            else if (objCon.CompID == "1032")
                            {
                                string colmName = "BV";
                                if (objCustomerDetail.IsFirstBill == true && (objCon.FirstBillonAmt == "Y" || objCon.FirstBillonBV == "Y" || objCon.FirstBillonPV == "Y"))
                                {
                                    if (objCon.FirstBillonBV == "Y")
                                        colmName = "BV";
                                    else if (objCon.FirstBillonPV == "Y")
                                        colmName = "PV";

                                    if ((objCon.FirstBillonBV == "Y" && objCon.FirstBillMinBV > 0) || (objCon.FirstBillonPV == "Y" && objCon.FirstBillMinPV > 0))
                                    {
                                        if (objCon.FirstBillonBV == "Y")
                                            objCustomerDetail.MinRepurch = objCon.FirstBillMinBV;
                                        else if (objCon.FirstBillonPV == "Y")
                                            objCustomerDetail.MinPV = objCon.FirstBillMinPV;
                                    }

                                }
                                else
                                {
                                    query = "Select Min(" + colmName + ") KitAmount FROM " + dbName + "..M_KitMaster WHERE KitAmount>0 AND ActiveStatus='Y' AND TopupSeq>(Select TopupSeq FROM M_KitMaster WHERE KitID='" + objCustomerDetail.KitId.ToString() + "')";
                                    cmd = new SqlCommand();
                                    cmd.CommandText = query;
                                    //cmd.Parameters.AddWithValue("@IdNo", IdNo);
                                    cmd.Connection = SC;
                                    SC.Close();
                                    SC.Open();
                                    string MacAdres = "";
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            Ktamt = !String.IsNullOrEmpty(reader["KitAmount"].ToString()) ? decimal.Parse(reader["KitAmount"].ToString()) : 0;
                                            //MacAdres = reader["MacAdrs"] != null ? reader["MacAdrs"].ToString() : "";
                                            break;
                                        }
                                    }
                                    //if (objCon.FirstBillonBV == "Y")
                                    //    objCustomerDetail.MinRepurch = Ktamt;
                                    //if (objCon.FirstBillonAmt == "Y")
                                    //    objCustomerDetail.MinBillAmt = Ktamt;
                                    //if (objCon.FirstBillonPV == "Y")
                                    //    objCustomerDetail.MinPV = Ktamt;
                                    objCustomerDetail.MinBillAmt = Ktamt;
                                }
                            }
                            objCustomerDetail.InvoiceType = new List<string>();
                            objCustomerDetail.ShowInvType = objCon.ShowInvType;
                            objCustomerDetail.ShowOffers = objCon.ShowOffers;

                            objCustomerDetail.ShowRepurchBV = objCon.ShowRepurchBV;
                            objCustomerDetail.ShowActiveBV = objCon.ShowActiveBV;
                            objCustomerDetail.ShowRepurchPV = objCon.ShowRepurchPV;
                            objCustomerDetail.ShowActivePV = objCon.ShowActivePV;
                            objCustomerDetail.FirstIDUpgradeMandatory = objCon.FirstIDUpgrade;
                            objCustomerDetail.WalletType = objCon.WalletType;
                            //objCustomerDetail.IsBillOnMrp = objCon.ShowInvType;
                            if (objCon.ShowInvType == "Y")
                            {
                                if (objCustomerDetail.IsActive == false)
                                {
                                    objCustomerDetail.InvoiceType.Add("Activate ID,B");
                                }
                                else
                                {
                                    /*MacAdres == "O" && */
                                    if (Ktamt > 0)
                                    {
                                        objCustomerDetail.InvoiceType.Add("Upgrade ID,B");
                                        objCustomerDetail.InvoiceType.Add("Repurchase Bill,R");
                                    }
                                    else
                                    {
                                        objCustomerDetail.MinBillAmt = 0;
                                        objCustomerDetail.InvoiceType.Add("Repurchase Bill,R");
                                    }
                                }
                            }
                        }

                    }

                }
                catch (Exception e)
                {
                    objCustomerDetail = new CustomerDetail();
                    objCustomerDetail.IdNo = e.InnerException.InnerException.Message;
                    objCustomerDetail.Name = "";
                }
            }

            return objCustomerDetail;
        }

        public List<string> GetAutocompleteProductNames(bool RestrictedproductsAlso, string ProdFor, bool forFrOrder, bool forDrOrder)
        {
            List<string> objProductNames = new List<string>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (RestrictedproductsAlso == true)
                        objProductNames = (from result in entity.M_ProductMaster
                                           where result.ActiveStatus == "Y" && ((forFrOrder == true && result.IsCardIssue == "N") || forFrOrder == false) && (String.IsNullOrEmpty(ProdFor) ? 1 == 1 : (result.Imported == "A" || ProdFor == "A" || (result.Imported == ProdFor && "A" != ProdFor))) //&& result.IsCardIssue == "N" //&& result.PType != "K"
                                           select result.ProductName).ToList();
                    else
                        objProductNames = (from result in entity.M_ProductMaster
                                           where result.ActiveStatus == "Y" && result.IsCardIssue == "N" && ((forFrOrder == true && result.IsCardIssue == "N") || forFrOrder == false) && (String.IsNullOrEmpty(ProdFor) ? 1 == 1 : (result.Imported == "A" || ProdFor == "A" || (result.Imported == ProdFor && "A" != ProdFor)))//&& result.PType != "K"
                                           select result.ProductName).ToList();
                }

            }
            catch (Exception e)
            {

            }

            return objProductNames;
        }



        public List<string> GetAutocompProductsOnly(bool AllowFreeProds, string ProdFor, bool forFrOrder, bool forDrOrder)
        {
            List<string> objProductNames = new List<string>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (AllowFreeProds)
                        objProductNames = (from result in entity.M_ProductMaster
                                           where result.ActiveStatus == "Y" && result.IsCardIssue == "N" && result.PType != "K" && result.CatId != 12 && (result.Imported == "A" || ProdFor == "A" || (result.Imported == ProdFor && "A" != ProdFor))
                                           select result.ProductName).ToList();
                    else
                        objProductNames = (from result in entity.M_ProductMaster
                                           where result.ActiveStatus == "Y" && ((forFrOrder == true && result.IsCardIssue == "N") || forFrOrder == false) && result.PType != "K" && (result.Imported == "A" || ProdFor == "A" || (result.Imported == ProdFor && "A" != ProdFor))
                                           select result.ProductName).ToList();

                }
            }





            catch (Exception e)
            {

            }

            return objProductNames;
        }

        public ResponseDetail ChangeOrderAddress(string orderno, string address)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";

            objResponse.ResponseStatus = "FAILED";
            try
            {
                List<string> BillNos = new List<string>();
                if (orderno != "" && address != "")
                {
                    //dispatch code                       
                    string InvConnectionString = AppConstr;
                    SqlConnection SC = new SqlConnection(InvConnectionString);
                    string sqlQry = "INSERT TempOrderAddress (OrderNO, Formno, Address1)" +
" Select OrderNO, Formno, Address1 FROM TrnOrder WHERE OrderNo = '" + orderno + "'  ; UPDATE TrnOrder SET Address1='" + address + "' WHERE ORDERNo='" + orderno + "';";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sqlQry;
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResponse.ResponseMessage = "Orders Dispatched Successfully!";
                        objResponse.ResponseStatus = "OK";
                    }
                    else
                    {
                        objResponse.ResponseMessage = "Something went wrong!";
                        objResponse.ResponseStatus = "FAILED";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public ResponseDetail SaveDistributorBill(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            DistributorBillModel TempDistributor = new DistributorBillModel();
            ConnModel objCon = objGetConn.GetDistBillInfo();
            decimal maxUserSBillNo = 0;
            decimal? SessId = 0;
            string billPrefix = "";
            decimal maxSbillNo = 0;
            decimal? FsessId = 0;
            string UserBillNo = "";
            string version = "";
            SqlTransaction objTrans = null;
            decimal WalletBalance = 0;
            decimal MainWalletBalance = 0;
            decimal LastBillAmt = 0;
            int NewKitId = 0;
            string NewKitName = "";
            TrnPayModeDetail objDtPayModeDetail = new TrnPayModeDetail();
            List<string> Paymode = new List<string>();
            List<string> PayPrefix = new List<string>();
            List<TrnPayModeDetail> objDTListPayMode = new List<TrnPayModeDetail>();
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            string billno_ = "", narration_ = "", soldby_ = "", fcode_ = "";
            decimal netpayable_ = 0;
            try
            {
                string InvConnectionString = InvConstr;
                string AppConnectionString = AppConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                SqlConnection SC1 = new SqlConnection(InvConnectionString);

                string query = "Select Max(SessID) as MaxSessId from M_SessnMaster";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SessId = decimal.Parse(reader["MaxSessId"].ToString());
                    }
                }
                //SessId = SessId + 1;
                query = "Select * FROM dbo.ufnGetBalance('" + objModel.objCustomer.FormNo + "','" + objModel.objCustomer.WalletType + "')";
                cmd = new SqlCommand();
                cmd.CommandText = query;
                //cmd.Parameters.AddWithValue("@IdNo", IdNo);
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        WalletBalance = decimal.Parse(reader["Balance"].ToString());
                    }
                }
                ConnModel conobj = (System.Web.HttpContext.Current.Session["ConModel"] as ConnModel);
                if (conobj.CompID == "1007")/*For Vadic */
                {
                    query = "Select * FROM dbo.ufnGetBalance('" + objModel.objCustomer.FormNo + "','S')";
                    cmd = new SqlCommand();
                    cmd.CommandText = query;
                    //cmd.Parameters.AddWithValue("@IdNo", IdNo);
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MainWalletBalance = decimal.Parse(reader["Balance"].ToString());
                        }
                    }
                }
                else
                {
                    MainWalletBalance = 0;
                }

                using (var entity = new InventoryEntities(enttConstr))
                {
                    maxSbillNo = (from result in entity.TrnBillMains select result.SBillNo).DefaultIfEmpty(0).Max();
                    maxSbillNo = maxSbillNo + 1;
                    FsessId = (from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result.FSessId).Max();
                    ////decimal? SessId = (from result in entity.M_SessnMaster select result.SessID).Max();
                    billPrefix = (from result in entity.M_ConfigMaster select result.BillPrefix).FirstOrDefault();
                    // maxUserSBillNo = (from result in entity.TrnBillMains where result.FSessId == FsessId && result.SoldBy == objModel.objCustomer.UserDetails.PartyCode && result.BillType != "S" select result.UserSBillNo).DefaultIfEmpty(0).Max();
                    if (!string.IsNullOrEmpty(objModel.TaxORStock) && objModel.TaxORStock.ToLower() == "stock")
                    {
                        maxUserSBillNo = (from result in entity.TrnBillMains where result.FSessId == FsessId && result.SoldBy == objModel.objCustomer.UserDetails.PartyCode && result.BillType == "S" select result.UserSBillNo).DefaultIfEmpty(0).Max();

                    }
                    else
                    {
                        maxUserSBillNo = (from result in entity.TrnBillMains where result.FSessId == FsessId && result.SoldBy == objModel.objCustomer.UserDetails.PartyCode && result.BillType != "S" select result.UserSBillNo).DefaultIfEmpty(0).Max();

                    }
                    maxUserSBillNo = maxUserSBillNo + 1;
                    string strMaxUserSBillNo = maxUserSBillNo.ToString();
                    if (strMaxUserSBillNo.Count() < 5)
                    {
                        var countNum = strMaxUserSBillNo.Count();
                        var ToBeAddedDigits = 5 - countNum;
                        for (var j = 0; j < ToBeAddedDigits; j++)
                        {
                            strMaxUserSBillNo = "0" + strMaxUserSBillNo;
                        }
                    }

                    //maxUserSBillNo = maxUserSBillNo + 1;
                    //string strMaxUserSBillNo = maxUserSBillNo.ToString();
                    //if (strMaxUserSBillNo.Count() < 3)
                    //{
                    //    var countNum = strMaxUserSBillNo.Count();
                    //    var ToBeAddedDigits = 3 - countNum;
                    //    //strMaxUserSBillNo = maxUserSBillNo.ToString().PadLeft(ToBeAddedDigits,'0');
                    //    for (var j = 0; j < ToBeAddedDigits; j++)
                    //    {
                    //        strMaxUserSBillNo = "0" + strMaxUserSBillNo;
                    //    }
                    //    // maxUserSBillNo = decimal.Parse(strMaxUserSBillNo);
                    //}
                    //UserBillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + strMaxUserSBillNo;
                    var UserPCode = (from result in entity.M_LedgerMaster where result.ActiveStatus == "Y" && result.PartyCode == objModel.objCustomer.UserDetails.PartyCode select result.UserPartyCode).FirstOrDefault();

                    if (!string.IsNullOrEmpty(objModel.TaxORStock) && objModel.TaxORStock.ToLower() == "stock")
                    {
                        UserBillNo = billPrefix + "/ST/" + UserPCode + "/" + strMaxUserSBillNo;
                    }
                    else
                    {
                        UserBillNo = billPrefix + "/" + UserPCode + "/" + strMaxUserSBillNo;
                    }

                    version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();


                    if (string.IsNullOrEmpty(objModel.BillType) || objModel.BillType == "F" || objModel.BillType == "G" || objModel.BillType == "C")
                    //if (string.IsNullOrEmpty(objModel.BillType))
                    {
                        //saving data in table
                        // decimal? SessId=(from result in entity)
                        bool IsWalletEntry = false;

                        if (objModel != null)
                        {
                            if (objModel.objProduct.PayDetails != null)
                            {
                                if (objModel.objProduct.PayDetails.IsBD)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.BankDeposit;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByBD, BankCode = 0, ChqDDDate = null, ChqDDNo = "", CardNo = "", Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.BDBankName, AcNo = objModel.objProduct.PayDetails.AccNo, IFSCode = objModel.objProduct.PayDetails.IFSCCode, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsCC)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Card;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, AcNo = "", IFSCode = "", BankCode = 0, Narration = "", BankName = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, Amount = objModel.objProduct.PayDetails.AmountByCard, CardNo = objModel.objProduct.PayDetails.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsQ)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cheque;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByCheque, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.CHBankName, ChqDDNo = objModel.objProduct.PayDetails.ChequeNo, ChqDDDate = objModel.objProduct.PayDetails.ChequeDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsD)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.DD;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByDD, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.DDBankName, ChqDDNo = objModel.objProduct.PayDetails.DDNo, ChqDDDate = objModel.objProduct.PayDetails.DDDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsT)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Credit;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, BankName = "", Amount = objModel.objProduct.PayDetails.AmountByCredit, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, DUserId = 0, DRecTimeStamp = null, ChqDDDate = null, ChqDDNo = "", Narration = objModel.objProduct.PayDetails.Narration, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsV)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Voucher;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByVoucher, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, DUserId = 0, DRecTimeStamp = null, ChqDDDate = null, ChqDDNo = "", Narration = "", BankName = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsW)
                                {
                                    if (WalletBalance >= objModel.objProduct.PayDetails.AmountByWallet)
                                    {
                                        SC.Close();
                                        SC.Open();
                                        objTrans = SC.BeginTransaction();
                                        string dbInv = invDbName;
                                        query = "INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,AcType,VType,SessID,WSEssID) " +
                                                "Select CASE WHEN Max(VoucherNo) is NULL THEN 1 ELSE Max(VoucherNo)+1 END ,Cast(Convert(varchar,Getdate(),106) as Datetime),'" + objModel.objCustomer.FormNo + "','0','" + objModel.objProduct.PayDetails.AmountByWallet + "','Product purchased Against " + UserBillNo + ".','" + billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo + "','" + objModel.objCustomer.WalletType + "','D','" + SessId + "','" + SessId + "' FROM TrnVoucher";
                                        query = query + ";INSERT INTO " + dbInv + "..TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,VType,BType,AccDocType,SessID,FSessID) " +
             " Select ISNULL(Max(VoucherNo),0)+1, Cast(Convert(varchar,Getdate(),106) as Datetime),'" + objModel.objCustomer.FormNo + "','" + objModel.objCustomer.UserDetails.PartyCode + "','" + objModel.objProduct.PayDetails.AmountByWallet + "','Wallet credited against bill " + UserBillNo + ".','" + billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo + "','R','O','Distributor Bill.','" + SessId + "','" + FsessId + "' FROM " + dbInv + "..TrnVoucher";
                                        cmd = new SqlCommand();
                                        cmd.CommandText = query;
                                        cmd.Connection = SC;
                                        cmd.Transaction = objTrans;



                                        int i = cmd.ExecuteNonQuery();

                                        objTrans.Commit();
                                        SC.Close();
                                        if (i > 0)
                                        {
                                            EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Wallet;
                                            string value = EnumPayModes.GetEnumDescription(enumVar);
                                            PayPrefix.Add(value);
                                            objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByWallet, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = objModel.objCustomer.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                            ////insert entry into couponsalesdetails for wallet
                                            IsWalletEntry = true;
                                        }
                                        else
                                        {
                                            objResponse.ResponseStatus = "FAILED";
                                            objResponse.ResponseMessage = "Something went wrong";
                                            return objResponse;
                                        }
                                        i = 0;
                                    }
                                    else
                                    {
                                        objResponse.ResponseStatus = "FAILED";
                                        objResponse.ResponseMessage = "Sorry!Insufficient Wallet Balance.";
                                        return objResponse;
                                    }

                                }
                                if (objModel.objProduct.PayDetails.IsMW)
                                {
                                    if (MainWalletBalance >= objModel.objProduct.PayDetails.AmountByMainWallet)
                                    {
                                        SC.Close();
                                        SC.Open();
                                        objTrans = SC.BeginTransaction();
                                        string dbInv = invDbName;
                                        query = "INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,AcType,VType,SessID,WSEssID) " +
                                                "Select CASE WHEN Max(VoucherNo) is NULL THEN 1 ELSE Max(VoucherNo)+1 END ,Cast(Convert(varchar,Getdate(),106) as Datetime),'" + objModel.objCustomer.FormNo + "','0','" + objModel.objProduct.PayDetails.AmountByMainWallet + "','Product purchased Against " + UserBillNo + ".','" + billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo + "','S','D','" + SessId + "','" + SessId + "' FROM TrnVoucher";
                                        query = query + ";INSERT INTO " + dbInv + "..TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,VType,BType,AccDocType,SessID,FSessID) " +
             " Select ISNULL(Max(VoucherNo),0)+1, Cast(Convert(varchar,Getdate(),106) as Datetime),'" + objModel.objCustomer.FormNo + "','" + objModel.objCustomer.UserDetails.PartyCode + "','" + objModel.objProduct.PayDetails.AmountByMainWallet + "','Wallet credited against bill " + UserBillNo + ".','" + billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo + "','R','O','Distributor Bill.','" + SessId + "','" + FsessId + "' FROM " + dbInv + "..TrnVoucher";
                                        cmd = new SqlCommand();
                                        cmd.CommandText = query;
                                        cmd.Connection = SC;
                                        cmd.Transaction = objTrans;



                                        int i = cmd.ExecuteNonQuery();

                                        objTrans.Commit();
                                        SC.Close();
                                        if (i > 0)
                                        {
                                            EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.MainWallet;
                                            string value = EnumPayModes.GetEnumDescription(enumVar);
                                            PayPrefix.Add(value);
                                            objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByMainWallet, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = objModel.objCustomer.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                            ////insert entry into couponsalesdetails for wallet
                                            IsWalletEntry = true;
                                        }
                                        else
                                        {
                                            objResponse.ResponseStatus = "FAILED";
                                            objResponse.ResponseMessage = "Something went wrong";
                                            return objResponse;
                                        }
                                        i = 0;
                                    }
                                    else
                                    {
                                        objResponse.ResponseStatus = "FAILED";
                                        objResponse.ResponseMessage = "Sorry!Insufficient Repurchase Wallet Balance.";
                                        return objResponse;
                                    }

                                }
                                if (objModel.objProduct.PayDetails.IsP)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Paytm;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByPaytm, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = objModel.objProduct.PayDetails.PaytmTransactionId, ChqDDDate = DateTime.Now, CardNo = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.CashAmount > 0)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cash;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.CashAmount, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (PayPrefix.Count > 0)
                                {

                                    Paymode = (from r in entity.M_PayModeMaster where PayPrefix.Contains(r.Prefix) select r.PayMode).ToList();
                                }

                            }
                        }

                        query = "select ActiveStatus from M_MemberMaster WHERE IDno='" + objModel.objCustomer.IdNo + "'";
                        cmd.CommandText = query;
                        cmd.Connection = SC;
                        SC.Close();
                        SC.Open();
                        var ActiveStatus = string.Empty;
                        var IsFirstBill = false;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ActiveStatus = reader["ActiveStatus"] != null ? reader["ActiveStatus"].ToString() : "";
                            }
                        }

                        if (objCon.CompID != "1032")
                        {
                            if (objCon.FirstActivationBill == "Y")
                            {
                                if (ActiveStatus != null)
                                {
                                    if (ActiveStatus == "Y")
                                        IsFirstBill = false;
                                    else
                                        IsFirstBill = true;
                                }
                            }
                        }
                        else
                        {
                            IsFirstBill = objModel.objCustomer.IsFirstBill;
                        }
                        string SoldByCode = "";
                        List<TrnBillData> tempTableList = new List<TrnBillData>();
                        try
                        {
                            List<ProductModel> objListProductModel = new List<ProductModel>();
                            //TempDistributor.objListProduct.AddRange(objModel.objListProduct);
                            foreach (var obj in objModel.objListProduct)
                            {
                                objListProductModel.Add(obj);
                                TrnBillData objDTBillData = new TrnBillData();
                                objDTBillData.SBillNo = maxSbillNo;
                                objDTBillData.FSessId = FsessId ?? 0;
                                objDTBillData.SessId = SessId ?? 0;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.BillDate = DateTime.Now.Date;

                                objDTBillData.RefNo = "";
                                objDTBillData.RefId = 0;
                                objDTBillData.RefName = "";
                                objDTBillData.Remarks = string.IsNullOrEmpty(objModel.objCustomer.Remarks) ? "" : objModel.objCustomer.Remarks;
                                objDTBillData.CType = "M";
                                objDTBillData.SoldBy = objModel.objCustomer.UserDetails.PartyCode;
                                SoldByCode = objDTBillData.SoldBy;
                                objDTBillData.BillBy = objDTBillData.SoldBy;
                                objDTBillData.BillNo = billPrefix + "/" + objDTBillData.BillBy + "/" + maxSbillNo;
                                objDTBillData.FType = "M";
                                objDTBillData.FCode = objModel.objCustomer.IdNo;
                                objDTBillData.PartyName = objModel.objCustomer.Name;
                                objDTBillData.SupplierId = 0;
                                objDTBillData.ChDDNo = 0;
                                objDTBillData.ChDate = DateTime.Now;
                                objDTBillData.ChAmt = 0;
                                objDTBillData.BankCode = 0;
                                objDTBillData.BankName = "";
                                objDTBillData.FormNo = objModel.objCustomer.FormNo;
                                objDTBillData.TotalTaxAmount = objModel.objProduct.TotalTaxAmount;
                                objDTBillData.TotalSTaxAmount = 0;
                                objDTBillData.TotalDiscount = objModel.objProduct.TotalDiscount;
                                objDTBillData.TotalKitBvValue = 0;
                                objDTBillData.TotalBvValue = objModel.objProduct.TotalBV;
                                objDTBillData.TotalCVValue = objModel.objProduct.TotalCV;
                                objDTBillData.TotalPVValue = objModel.objProduct.TotalPV;
                                objDTBillData.TotalRPValue = objModel.objProduct.TotalRP;

                                objDTBillData.DP = obj.DP ?? 0;
                                objDTBillData.RP = obj.RP ?? 0;
                                objDTBillData.MRP = obj.MRP ?? 0;
                                objDTBillData.CVValue = obj.CVValue ?? 0;
                                objDTBillData.CV = obj.CV ?? 0;
                                objDTBillData.PV = obj.PV ?? 0;
                                objDTBillData.BV = obj.BV ?? 0;
                                objDTBillData.BVValue = obj.BVValue ?? 0;
                                objDTBillData.PVValue = obj.PVValue ?? 0;
                                objDTBillData.RPValue = obj.RPValue ?? 0;
                                objDTBillData.Barcode = obj.Barcode.ToString();
                                objDTBillData.BatchNo = obj.BatchNo.ToString();
                                //if (!string.IsNullOrEmpty(objModel.TaxType) && objModel.TaxType.ToUpper() == "I")
                                //{
                                //    objDTBillData.TaxAmount = obj.TaxAmt ?? 0;
                                //    if (obj.OldTaxAmount != 0 && obj.OldTaxAmount != obj.TaxAmt)
                                //    {
                                //        objDTBillData.TaxAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.TaxAmount) + 0.01).ToString());
                                //        objDTBillData.NetAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.NetAmount) - 0.01).ToString());
                                //    }
                                //    objDTBillData.Tax = obj.TaxPer ?? 0;
                                //    objDTBillData.CGST = 0;
                                //    objDTBillData.CGSTAmt = 0;
                                //    objDTBillData.SGST = 0;
                                //    objDTBillData.SGSTAmt = 0;
                                //    objDTBillData.TaxType = "I";
                                //}
                                //else
                                //{
                                //    objDTBillData.TaxAmount = 0;
                                //    objDTBillData.Tax = 0;
                                //    objDTBillData.CGST = obj.TaxPer / 2 ?? 0;
                                //    objDTBillData.CGSTAmt = obj.TaxAmt / 2 ?? 0;
                                //    objDTBillData.SGST = obj.TaxPer / 2 ?? 0;
                                //    objDTBillData.SGSTAmt = obj.TaxAmt / 2 ?? 0;
                                //    objDTBillData.TaxType = "S";
                                //}
                                //objDTBillData.TaxAmount = obj.TaxAmt ?? 0;
                                //objDTBillData.Tax = obj.TaxPer ?? 0;
                                objDTBillData.TaxAmount = 0;
                                //objDTBillData.Tax = 0;
                                //objDTBillData.CGST = obj.TaxPer / 2 ?? 0;
                                //objDTBillData.CGSTAmt = obj.TaxAmt / 2 ?? 0;
                                //objDTBillData.SGST = obj.TaxPer / 2 ?? 0;
                                //objDTBillData.SGSTAmt = obj.TaxAmt / 2 ?? 0;

                                if (objModel.objCustomer.StateCode == objModel.objCustomer.UserDetails.StateCode)
                                {
                                    objDTBillData.TaxAmount = 0;
                                    objDTBillData.Tax = 0;
                                    objDTBillData.CGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.CGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.SGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.SGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.TaxType = "S";
                                }
                                else
                                {
                                    objDTBillData.TaxAmount = obj.TaxAmt ?? 0;
                                    if (obj.OldTaxAmount != 0 && obj.OldTaxAmount != obj.TaxAmt)
                                    {
                                        objDTBillData.TaxAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.TaxAmount) + 0.01).ToString());
                                        objDTBillData.NetAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.NetAmount) - 0.01).ToString());
                                    }
                                    objDTBillData.Tax = obj.TaxPer ?? 0;
                                    objDTBillData.CGST = 0;
                                    objDTBillData.CGSTAmt = 0;
                                    objDTBillData.SGST = 0;
                                    objDTBillData.SGSTAmt = 0;
                                    objDTBillData.TaxType = "I";
                                }

                                objDTBillData.DiscountPer = obj.DiscPer ?? 0;
                                objDTBillData.Discount = obj.DiscAmt ?? 0;
                                objDTBillData.ProdCommssn = obj.CommissionPer ?? 0;
                                objDTBillData.ProdCommssnAmt = obj.CommissionAmt ?? 0;
                                objDTBillData.ProductId = obj.ProdCode.ToString();
                                objDTBillData.ProductName = obj.ProductName;
                                objDTBillData.Qty = obj.Quantity;
                                objDTBillData.FreeQty = obj.freeQty;
                                //if (!string.IsNullOrEmpty(obj.ProductTye) && obj.ProductTye.ToUpper() == "F")
                                //{
                                //    objDTBillData.TFreeQty = 0;
                                //}
                                //else
                                //{
                                //    objDTBillData.TFreeQty = obj.TFreeQty;
                                //}
                                objDTBillData.Rate = obj.Rate ?? 0;
                                objDTBillData.IsKitBV = "N";
                                objDTBillData.DSeries = "";
                                objDTBillData.DImported = "N";
                                objDTBillData.IMEINo = "";
                                objDTBillData.BNo = "";
                                objDTBillData.ItemType = "";



                                objDTBillData.JType = "Cash:" + objModel.objProduct.TotalNetPayable;
                                objDTBillData.BillTo = "R";
                                objDTBillData.BillFor = "RB";
                                objDTBillData.IsReceive = "R";
                                objDTBillData.IsCredit = "F";
                                //objDTBillData.BillType = "R";
                                if (IsFirstBill)
                                {
                                    objDTBillData.BillType = "B";
                                }
                                else
                                {
                                    objDTBillData.BillType = "R";
                                }
                                //if (objModel.objCustomer.IsFirstBill)
                                //{
                                //    objDTBillData.BillType = (objModel.objProduct.VoucherNo ?? "") != "" ? objModel.BillType : "B";
                                //}
                                //else
                                //{
                                //    objDTBillData.BillType = (objModel.objProduct.VoucherNo ?? "") != "" ? objModel.BillType : "R";
                                //}

                                if (!string.IsNullOrEmpty(obj.ProductTye))
                                {
                                    objDTBillData.ProdType = obj.ProductTye;
                                }
                                else
                                {
                                    objDTBillData.ProdType = "P";
                                }
                                objDTBillData.PaymentDtl = "Cash:" + objModel.objProduct.TotalNetPayable;

                                objDTBillData.TotalAmount = objModel.objProduct.TotalTotalAmount;
                                //tax excluding
                                objDTBillData.NetAmount = obj.Amount;
                                objDTBillData.CashDiscPer = obj.CashDiscPer;
                                objDTBillData.CashDiscAmount = obj.CashDiscAmount;

                                objDTBillData.NetPayable = Math.Round(objModel.objProduct.TotalNetPayable);
                                if (objModel.objProduct.Roundoff == 0)
                                {
                                    objDTBillData.RndOff = objDTBillData.NetPayable - objModel.objProduct.TotalNetPayable;
                                }
                                else
                                {
                                    objDTBillData.RndOff = objModel.objProduct.Roundoff;
                                }
                                objDTBillData.CardAmount = 0;
                                objDTBillData.PayMode = Paymode.Count > 1 ? string.Join(",", Paymode) : Paymode[0];
                                objDTBillData.PayPrefix = PayPrefix.Count > 1 ? string.Join(",", PayPrefix) : PayPrefix[0];
                                objDTBillData.BvTransfer = (!String.IsNullOrEmpty(objModel.IsCouponWallet)) ? objModel.IsCouponWallet : "N";

                                //objDTBillData.UserSBillNo = maxSbillNo;
                                //objDTBillData.UserBillNo = billPrefix + "/" + objDTBillData.BillBy + "/" + maxSbillNo;
                                objDTBillData.UserSBillNo = maxUserSBillNo;
                                objDTBillData.UserBillNo = UserBillNo;
                                objDTBillData.DispatchStatus = objModel.FreightType != "H" ? "N" : "Y";
                                objDTBillData.LR = objModel.VehicleNo ?? ""; //21May19;
                                objDTBillData.LRDate = DateTime.Now;
                                objDTBillData.TransporterName = objModel.TransporterName ?? ""; //21May19
                                objDTBillData.DispatchTo = objModel.objCustomer.IdNo;
                                objDTBillData.FreightType = objModel.FreightType;
                                objDTBillData.FreightAmt = objModel.FreightAmt;
                                objDTBillData.Series = "";
                                objDTBillData.Scratch = objModel.EWayBillNo ?? ""; //21May19
                                //if (objModel.objCustomer.IsBillOnMrp)
                                //{
                                //    objDTBillData.Unit = 1;
                                //}
                                //else
                                //{
                                objDTBillData.Unit = 0;
                                // objDTBillData.LocId = objModel.objCustomer.KitId;
                                //}
                                objDTBillData.PSessId = 0;
                                objDTBillData.DcNo = "";
                                objDTBillData.Imported = "N";
                                objDTBillData.FPoint = objModel.objProduct.Weight;
                                objDTBillData.FPointValue = objModel.objProduct.WeightVal;
                                objDTBillData.OrdStatus = "";
                                objDTBillData.OrdQty = 0;
                                // objDTBillData.OrderType = "";
                                objDTBillData.OrderDate = DateTime.Now;
                                objDTBillData.OrderNo = "";
                                objDTBillData.RemQty = 0;
                                objDTBillData.DP1 = 0;
                                objDTBillData.DReason = "";
                                objDTBillData.DUserId = 0;
                                objDTBillData.DRecTimeStamp = DateTime.Now;
                                objDTBillData.DocWeight = 0;
                                objDTBillData.DocketNo = "";
                                objDTBillData.DocketDate = DateTime.Now;
                                objDTBillData.STNFormNo = "";
                                objDTBillData.StkRecv = "N";
                                objDTBillData.StkRecvDate = DateTime.Now;
                                objDTBillData.StkRecvUserId = 0;
                                objDTBillData.InTransit = "N";
                                objDTBillData.UID = string.IsNullOrEmpty(objModel.objProduct.UID) ? "" : objModel.objProduct.UID;
                                objDTBillData.OfferUID = objModel.offerId;
                                objDTBillData.IsKit = "N";
                                objDTBillData.TotalCorton = "";
                                objDTBillData.TotalMonoCorton = "";
                                objDTBillData.SpclOfferId = obj.SpecialOfferId ?? 0;
                                objDTBillData.VAT = 0;
                                objDTBillData.BuyerAddress = "";

                                objDTBillData.BuyerTIN = string.IsNullOrEmpty(objModel.objCustomer.GSTNo) ? "" : objModel.objCustomer.GSTNo;

                                objDTBillData.TotalDiscount = objModel.objProduct.TotalDiscPer;
                                objDTBillData.TotalDiscountAmt = objModel.objProduct.TotalDiscount;
                                objDTBillData.VDiscountAmt = 0;
                                objDTBillData.VDiscount = 0;
                                objDTBillData.ReceiverID = "";
                                objDTBillData.ReceiverName = objModel.Station ?? ""; //21May19;
                                objDTBillData.ReceiverMNo = "";
                                objDTBillData.ReceiverIDProof = "";
                                objDTBillData.TotalFPoint = objModel.objProduct.TotalWeight;
                                objDTBillData.TotalQty = objModel.objProduct.TotalQty;
                                objDTBillData.CashReward = objModel.CashReward;
                                objDTBillData.CommssnAmt = objModel.objProduct.TotalCommsonAmt;
                                objDTBillData.RecvAmount = 0;
                                objDTBillData.ReturnToCustAmt = 0;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.RecTimeStamp = DateTime.Now;
                                objDTBillData.UserId = objModel.objCustomer.UserDetails.UserId;
                                objDTBillData.UserName = objModel.objCustomer.UserDetails.UserName;
                                objDTBillData.DelvPlace = string.IsNullOrEmpty(objModel.objProduct.DeliveryPlace) ? "" : objModel.objProduct.DeliveryPlace;
                                objDTBillData.DelvStatus = "";
                                objDTBillData.DelvUserId = 0;
                                objDTBillData.DelvRecTimeStamp = DateTime.Now;
                                objDTBillData.Version = version;
                                objDTBillData.IDType = "";
                                objDTBillData.BranchName = "";
                                objDTBillData.CourierId = objModel.objProduct.CourierId;
                                objDTBillData.CourierName = (string.IsNullOrEmpty(objModel.objProduct.CourierName) ? "" : objModel.objProduct.CourierName);
                                objDTBillData.LocId = 0;
                                objDTBillData.LocName = "";
                                objDTBillData.DelvAddress = "";
                                objDTBillData.Pincode = "";
                                objDTBillData.OrderType = "";
                                // objDTBillData.ReceiverMNo = "";
                                //objDTBillData.DSeries = "";
                                //objDTBillData.DImported = "";
                                // objDTBillData.FCode = string.IsNullOrEmpty(objModel.objCustomer.IdNo) ? "" : objModel.objCustomer.IdNo;
                                //objDTBillData.FormNo = objModel.objCustomer.FormNo;
                                //objDTBillData.RefId = string.IsNullOrEmpty(objModel.objCustomer.ReferenceIdNo)?0:decimal.Parse(objModel.objCustomer.ReferenceIdNo);
                                //objDTBillData.RefName= string.IsNullOrEmpty(objModel.objCustomer.ReferenceName) ? "" : objModel.objCustomer.ReferenceName;
                                //objDTBillData.Remarks = "";
                                // tempTableList.Add(objDTBillData);
                                //billno_ = objDTBillData.BillNo;
                                //if (objModel.UserType == "shoppe")
                                //    soldby_ = objDTBillData.SoldBy;
                                //else
                                //    soldby_ = objDTBillData.UserName;
                                //fcode_ = objDTBillData.FCode;
                                //netpayable_ = objDTBillData.NetPayable;
                                //narration_ = "Wallet deducted against " + objDTBillData.UserBillNo + ".";
                                entity.TrnBillDatas.Add(objDTBillData);
                                //entity.TrnBillDatas.Add(objDTBillData);
                            }
                            int i = 0;

                            using (var objDTTrans = entity.Database.BeginTransaction())
                            {
                                //entity.TrnBillDatas.AddRange(tempTableList);
                                try
                                {
                                    i = entity.SaveChanges();
                                    objDTTrans.Commit();
                                }
                                catch (DbUpdateConcurrencyException ex)
                                {
                                    objDTTrans.Rollback();
                                }
                                catch (DbUpdateException ex)
                                {

                                }

                                catch (Exception ex)
                                {
                                    objDTTrans.Rollback();
                                }
                            }
                            if (i == objModel.objListProduct.Count)
                            {
                                //DeductPartyWallet(billno_, narration_, soldby_, fcode_, netpayable_, objModel.UserType);
                                //if (!string.IsNullOrEmpty(objModel.DeliveryBy) && objModel.DeliveryBy.ToLower() == "pickup")
                                //{
                                //    var amount = objModel.objProduct.CourierCharges / 2;
                                //    AddShoppeWallet(billno_, "Against Couriercharges for bill no: " + UserBillNo, fcode_, soldby_, amount);
                                //}
                                var resultPayMode = (from r in entity.M_PayModeMaster select r).ToList();
                                foreach (var obj in objDTListPayMode)
                                {
                                    TrnPayModeDetail objTemp = new TrnPayModeDetail();
                                    objTemp = obj;
                                    if (objModel.objCustomer.IsFirstBill)
                                    {
                                        objTemp.BillType = "B";
                                    }
                                    else
                                    {
                                        objTemp.BillType = "R";
                                    }
                                    objTemp.PayMode = (from r in resultPayMode where r.Prefix.Trim() == obj.PayPrefix.Trim() select r.PayMode).FirstOrDefault();
                                    if (string.IsNullOrEmpty(objTemp.CardNo))
                                    {
                                        objTemp.CardNo = "";
                                    }
                                    entity.TrnPayModeDetails.Add(objTemp);
                                }
                                i = 0;
                                i = entity.SaveChanges();
                                if (i == objDTListPayMode.Count)
                                {
                                    objResponse.ResponseMessage = "Saved Successfully!";
                                    objResponse.ResponseStatus = "OK";
                                    objResponse.ResponseDetailsToPrint = new DistributorBillModel();
                                    objResponse.ResponseDetailsToPrint.BillNo = UserBillNo;
                                    objResponse.ResponseDetailsToPrint.SoldBy = SoldByCode;


                                }
                            }
                        }
                        catch (DbEntityValidationException e)
                        {
                            objResponse.ResponseMessage = "Something went wrong!";
                            objResponse.ResponseStatus = "FAILED";
                        }

                    }

                    else if (objModel.BillType == "party")
                    {
                        //saving party bill
                        DateTime BillDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(objModel.BillDateStr))
                        {
                            var SplitDate = objModel.BillDateStr.Split('-');
                            string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                            BillDate = Convert.ToDateTime(NewDate);
                            BillDate = BillDate.Date;
                        }




                        //saving data in table
                        // decimal? SessId=(from result in entity)
                        bool IsWalletEntry = false;

                        if (objModel != null)
                        {
                            if (objModel.objProduct.PayDetails != null)
                            {
                                if (objModel.objProduct.PayDetails.IsBD)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.BankDeposit;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "V", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByBD, BankCode = 0, ChqDDDate = null, ChqDDNo = "", CardNo = "", Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.BDBankName, AcNo = objModel.objProduct.PayDetails.AccNo, IFSCode = objModel.objProduct.PayDetails.IFSCCode, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });


                                }
                                if (objModel.objProduct.PayDetails.IsCC)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Card;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "V", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, AcNo = "", IFSCode = "", BankCode = 0, Narration = "", BankName = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, Amount = objModel.objProduct.PayDetails.AmountByCard, CardNo = objModel.objProduct.PayDetails.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsQ)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cheque;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "V", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByCheque, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.CHBankName, ChqDDNo = objModel.objProduct.PayDetails.ChequeNo, ChqDDDate = objModel.objProduct.PayDetails.ChequeDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsD)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.DD;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "V", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByDD, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.DDBankName, ChqDDNo = objModel.objProduct.PayDetails.DDNo, ChqDDDate = objModel.objProduct.PayDetails.DDDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsT)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Credit;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "V", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, BankName = "", Amount = objModel.objProduct.PayDetails.AmountByCredit, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, DUserId = 0, DRecTimeStamp = null, ChqDDDate = null, ChqDDNo = "", Narration = objModel.objProduct.PayDetails.Narration, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsV)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Voucher;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "V", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByVoucher, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, DUserId = 0, DRecTimeStamp = null, ChqDDDate = null, ChqDDNo = "", Narration = "", BankName = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsW)
                                {
                                    WalletBalance = 0;
                                    using (InventoryEntities db = new InventoryEntities(enttConstr))
                                    {
                                        var result = (from s in db.V_PartyBalance where s.PartyCode == objModel.objCustomer.PartyCode select s.Balance).FirstOrDefault();
                                        WalletBalance = result ?? 0;
                                    }





                                    if (WalletBalance >= objModel.objProduct.PayDetails.AmountByWallet)
                                    {

                                        if (SC1.State == ConnectionState.Closed)
                                            SC1.Open();
                                        objTrans = SC1.BeginTransaction();
                                        decimal cashAmt = objModel.objProduct.TotalNetPayable - objModel.objProduct.PayDetails.AmountByWallet;
                                        //query = "INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,AcType,VType,SessID,WSEssID) " +
                                        //"Select CASE WHEN Max(VoucherNo) is NULL THEN 1 ELSE Max(VoucherNo)+1 END ,Cast(Convert(varchar,Getdate(),106) as Datetime),'" + objModel.objCustomer.FormNo + "','0','" + objModel.objProduct.PayDetails.AmountByWallet + "','Product purchased Against " + UserBillNo + ".','" + billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo + "','R','D','" + SessId + "','" + SessId + "' FROM TrnVoucher";
                                        query = ";INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,VType,BType,AccDocType,SessID,FSessID) " +
                " Select ISNULL(Max(VoucherNo),0)+1, Cast(Convert(varchar,Getdate(),106) as Datetime),'" + objModel.objCustomer.PartyCode + "','" + objModel.objCustomer.UserDetails.PartyCode + "','" + objModel.objProduct.PayDetails.AmountByWallet + "','Wallet deducted against bill " + UserBillNo + ".','" + billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo + "','R','O','Party Bill.','" + SessId + "','" + FsessId + "' FROM TrnVoucher";
                                        //" UNION ALL Select ISNULL(Max(VoucherNo),0)+2, Cast(Convert(varchar,Getdate(),106) as Datetime),'','" + objModel.objCustomer.PartyCode + "','" + cashAmt + "','Wallet credited against cash in bill " + UserBillNo + ".','" + billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo + "','R','O','Party Bill.','" + SessId + "','" + FsessId + "' FROM TrnVoucher " +
                                        //" UNION ALL Select ISNULL(Max(VoucherNo),0)+3, Cast(Convert(varchar,Getdate(),106) as Datetime),'" + objModel.objCustomer.PartyCode + "','','" + cashAmt + "','Wallet debited against bill " + UserBillNo + ".','" + billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo + "','R','O','Party Bill.','" + SessId + "','" + FsessId + "' FROM TrnVoucher; ";

                                        cmd = new SqlCommand();
                                        cmd.CommandText = query;
                                        cmd.Connection = SC1;
                                        cmd.Transaction = objTrans;

                                        int i = cmd.ExecuteNonQuery();

                                        objTrans.Commit();
                                        SC1.Close();
                                        if (i > 0)
                                        {
                                            EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Wallet;
                                            string value = EnumPayModes.GetEnumDescription(enumVar);
                                            PayPrefix.Add(value);
                                            objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "V", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByWallet, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = objModel.objCustomer.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                            ////insert entry into couponsalesdetails for wallet
                                            IsWalletEntry = true;
                                        }
                                        else
                                        {
                                            objResponse.ResponseStatus = "FAILED";
                                            objResponse.ResponseMessage = "Something went wrong";
                                            return objResponse;
                                        }
                                        i = 0;
                                    }
                                    else
                                    {
                                        objResponse.ResponseStatus = "FAILED";
                                        objResponse.ResponseMessage = "Sorry!Insufficient Wallet Balance.";
                                        return objResponse;
                                    }

                                }
                                if (objModel.objProduct.PayDetails.IsP)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Paytm;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "V", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByPaytm, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = objModel.objProduct.PayDetails.PaytmTransactionId, ChqDDDate = DateTime.Now, CardNo = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.CashAmount > 0)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cash;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.CashAmount, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "V", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.TotalNetPayable, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (PayPrefix.Count > 0)
                                {
                                    Paymode = (from r in entity.M_PayModeMaster where PayPrefix.Contains(r.Prefix) select r.PayMode).ToList();
                                }

                            }
                        }
                        if (!string.IsNullOrEmpty(objModel.TaxORStock) && objModel.TaxORStock.ToLower() == "stock")
                        {
                            maxUserSBillNo = (from result in entity.TrnBillMains where result.FSessId == FsessId && result.SoldBy == objModel.objCustomer.UserDetails.PartyCode && result.BillType == "S" select result.UserSBillNo).DefaultIfEmpty(0).Max();
                            maxUserSBillNo = maxUserSBillNo + 1;
                            strMaxUserSBillNo = maxUserSBillNo.ToString();
                            if (strMaxUserSBillNo.Count() < 3)
                            {
                                var countNum = strMaxUserSBillNo.Count();
                                var ToBeAddedDigits = 3 - countNum;
                                for (var j = 0; j < ToBeAddedDigits; j++)
                                {
                                    strMaxUserSBillNo = "0" + strMaxUserSBillNo;
                                }
                            }
                            UserBillNo = billPrefix + "/ST/" + strMaxUserSBillNo;
                        }

                        string SoldByCode = "";
                        List<TrnBillData> tempTableList = new List<TrnBillData>();
                        string GroupPrefix = "";
                        string BillingPartyCode = objModel.objCustomer.PartyCode;
                        GroupPrefix = (from p in entity.M_GroupMaster
                                       where p.GroupId == (from r in entity.M_LedgerMaster where r.PartyCode == BillingPartyCode select r.GroupId).FirstOrDefault()
                                       select p.Prefix
                                       ).FirstOrDefault();

                        try
                        {
                            List<ProductModel> objListProductModel = new List<ProductModel>();
                            //TempDistributor.objListProduct.AddRange(objModel.objListProduct);
                            foreach (var obj in objModel.objListProduct)
                            {
                                objListProductModel.Add(obj);
                                TrnBillData objDTBillData = new TrnBillData();
                                objDTBillData.SBillNo = maxSbillNo;
                                objDTBillData.FSessId = FsessId ?? 0;
                                objDTBillData.SessId = SessId ?? 0;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.BillDate = BillDate.Date;

                                objDTBillData.RefNo = string.IsNullOrEmpty(objModel.objCustomer.ReferenceIdNo) ? "" : objModel.objCustomer.ReferenceIdNo;
                                objDTBillData.RefId = 0;
                                objDTBillData.RefName = "";
                                objDTBillData.Remarks = string.IsNullOrEmpty(objModel.objCustomer.Remarks) ? "" : objModel.objCustomer.Remarks;
                                objDTBillData.CType = GroupPrefix;
                                objDTBillData.SoldBy = objModel.objCustomer.UserDetails.PartyCode;
                                SoldByCode = objDTBillData.SoldBy;
                                objDTBillData.BillBy = objDTBillData.SoldBy;
                                objDTBillData.BillNo = billPrefix + "/" + objDTBillData.BillBy + "/" + maxSbillNo;
                                objDTBillData.FType = GroupPrefix;
                                objDTBillData.FCode = objModel.objCustomer.PartyCode;
                                objDTBillData.PartyName = objModel.objCustomer.PartyName;
                                objDTBillData.SupplierId = 0;
                                objDTBillData.ChDDNo = 0;
                                objDTBillData.ChDate = DateTime.Now;
                                objDTBillData.ChAmt = 0;
                                objDTBillData.BankCode = 0;
                                objDTBillData.BankName = "";
                                objDTBillData.FormNo = 0;
                                objDTBillData.TotalTaxAmount = objModel.objProduct.TotalTaxAmount;
                                objDTBillData.TotalSTaxAmount = 0;
                                objDTBillData.TotalDiscount = objModel.objProduct.TotalDiscount;
                                objDTBillData.TotalKitBvValue = 0;
                                objDTBillData.TotalBvValue = objModel.objProduct.TotalBV;
                                objDTBillData.TotalCVValue = objModel.objProduct.TotalCV;
                                objDTBillData.TotalPVValue = objModel.objProduct.TotalPV;
                                objDTBillData.TotalRPValue = objModel.objProduct.TotalRP;

                                objDTBillData.DP = obj.DP ?? 0;
                                objDTBillData.RP = obj.RP ?? 0;
                                objDTBillData.MRP = obj.MRP ?? 0;
                                objDTBillData.CVValue = obj.CVValue ?? 0;
                                objDTBillData.CV = obj.CV ?? 0;
                                objDTBillData.PV = obj.PV ?? 0;
                                objDTBillData.BV = obj.BV ?? 0;
                                objDTBillData.BVValue = obj.BVValue ?? 0;
                                objDTBillData.PVValue = obj.PVValue ?? 0;
                                objDTBillData.RPValue = obj.RPValue ?? 0;
                                objDTBillData.Barcode = obj.Barcode.ToString();
                                objDTBillData.BatchNo = obj.BatchNo.ToString();

                                objDTBillData.DiscountPer = obj.DiscPer ?? 0;
                                objDTBillData.Discount = obj.DiscAmt ?? 0;
                                objDTBillData.ProdCommssn = obj.CommissionPer ?? 0;
                                objDTBillData.ProdCommssnAmt = obj.CommissionAmt ?? 0;
                                objDTBillData.ProductId = obj.ProdCode.ToString();
                                objDTBillData.ProductName = obj.ProductName;
                                objDTBillData.Qty = obj.Quantity;
                                objDTBillData.Rate = obj.Rate ?? 0;
                                objDTBillData.IsKitBV = "N";
                                objDTBillData.DSeries = "";
                                objDTBillData.DImported = "N";
                                objDTBillData.IMEINo = "D";
                                objDTBillData.BNo = "";
                                objDTBillData.ItemType = "N";



                                objDTBillData.JType = "Cash:" + objModel.objProduct.TotalNetPayable;
                                objDTBillData.BillTo = objModel.objCustomer.PartyCode;
                                objDTBillData.BillFor = objModel.objCustomer.PartyCode;
                                objDTBillData.IsReceive = "N";
                                objDTBillData.IsCredit = "F";
                                //objDTBillData.BillType = "R";
                                if (objModel.TaxORStock.ToLower() == "tax")
                                {
                                    objDTBillData.BillType = "V";
                                }
                                else
                                {
                                    objDTBillData.BillType = "S";
                                }
                                objDTBillData.ProdType = "P";
                                objDTBillData.PaymentDtl = "Cash:" + objModel.objProduct.TotalNetPayable;

                                objDTBillData.TotalAmount = objModel.objProduct.TotalTotalAmount;
                                //tax excluding
                                objDTBillData.NetAmount = obj.Amount;
                                if (objModel.objCustomer.StateCode == objModel.objCustomer.UserDetails.StateCode)
                                {
                                    objDTBillData.TaxAmount = 0;
                                    objDTBillData.Tax = 0;
                                    objDTBillData.CGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.CGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.SGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.SGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.TaxType = "S";
                                }
                                else
                                {

                                    objDTBillData.TaxAmount = obj.TaxAmt ?? 0;
                                    if (obj.OldTaxAmount != 0 && obj.OldTaxAmount != obj.TaxAmt)
                                    {
                                        objDTBillData.TaxAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.TaxAmount) + 0.01).ToString());
                                        objDTBillData.NetAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.NetAmount) - 0.01).ToString());
                                    }
                                    objDTBillData.Tax = obj.TaxPer ?? 0;
                                    objDTBillData.CGST = 0;
                                    objDTBillData.CGSTAmt = 0;
                                    objDTBillData.SGST = 0;
                                    objDTBillData.SGSTAmt = 0;
                                    objDTBillData.TaxType = "I";
                                }
                                objDTBillData.CashDiscPer = obj.CashDiscPer;
                                objDTBillData.CashDiscAmount = obj.CashDiscAmount;

                                objDTBillData.NetPayable = Math.Round(objModel.objProduct.TotalNetPayable);
                                if (objModel.objProduct.Roundoff == 0)
                                {
                                    objDTBillData.RndOff = objDTBillData.NetPayable - objModel.objProduct.TotalNetPayable;
                                }
                                else
                                {
                                    objDTBillData.RndOff = objModel.objProduct.Roundoff;
                                }
                                objDTBillData.CardAmount = 0;
                                objDTBillData.PayMode = Paymode.Count > 1 ? string.Join(",", Paymode) : Paymode[0];
                                objDTBillData.PayPrefix = PayPrefix.Count > 1 ? string.Join(",", PayPrefix) : PayPrefix[0];
                                objDTBillData.BvTransfer = "N";

                                //objDTBillData.UserSBillNo = maxSbillNo;
                                //objDTBillData.UserBillNo = billPrefix + "/" + objDTBillData.BillBy + "/" + maxSbillNo;
                                objDTBillData.UserSBillNo = maxUserSBillNo;
                                objDTBillData.UserBillNo = UserBillNo;
                                objDTBillData.DispatchStatus = "N";
                                objDTBillData.LR = objModel.VehicleNo ?? ""; //21May19
                                objDTBillData.LRDate = DateTime.Now;
                                objDTBillData.TransporterName = objModel.TransporterName ?? ""; //21May19
                                objDTBillData.DispatchTo = "";
                                objDTBillData.FreightType = objModel.FreightType;
                                objDTBillData.FreightAmt = objModel.FreightAmt;
                                objDTBillData.Series = "";
                                objDTBillData.Scratch = objModel.EWayBillNo ?? ""; //21May19

                                objDTBillData.Unit = 0;

                                objDTBillData.PSessId = 0;
                                objDTBillData.DcNo = "";
                                objDTBillData.Imported = "N";
                                objDTBillData.FPoint = obj.Weight;
                                objDTBillData.FPointValue = obj.WeightVal;
                                objDTBillData.OrdStatus = "";
                                objDTBillData.OrdQty = 0;
                                // objDTBillData.OrderType = "";
                                objDTBillData.OrderDate = DateTime.Now;
                                objDTBillData.OrderNo = "";
                                objDTBillData.RemQty = 0;
                                objDTBillData.DP1 = 0;
                                objDTBillData.DReason = "";
                                objDTBillData.DUserId = 0;
                                objDTBillData.DRecTimeStamp = DateTime.Now;
                                objDTBillData.DocWeight = 0;
                                objDTBillData.DocketNo = "";
                                objDTBillData.DocketDate = DateTime.Now;
                                //objDTBillData.UserBillNo = "";
                                //objDTBillData.UserSBillNo = 0;
                                objDTBillData.STNFormNo = "";
                                objDTBillData.StkRecv = "N";
                                objDTBillData.StkRecvDate = DateTime.Now;
                                objDTBillData.StkRecvUserId = 0;
                                objDTBillData.InTransit = "N";
                                objDTBillData.UID = string.IsNullOrEmpty(objModel.objProduct.UID) ? "" : objModel.objProduct.UID;
                                objDTBillData.OfferUID = 0;
                                objDTBillData.IsKit = "N";
                                objDTBillData.TotalCorton = "";
                                objDTBillData.TotalMonoCorton = "";
                                objDTBillData.SpclOfferId = 0;
                                objDTBillData.VAT = 0;
                                objDTBillData.BuyerAddress = "";
                                objDTBillData.BuyerTIN = string.IsNullOrEmpty(objModel.objCustomer.GSTNo) ? "" : objModel.objCustomer.GSTNo;

                                objDTBillData.TotalDiscount = objModel.objProduct.TotalDiscPer;
                                objDTBillData.TotalDiscountAmt = objModel.objProduct.TotalDiscount;
                                objDTBillData.VDiscountAmt = 0;
                                objDTBillData.VDiscount = 0;
                                objDTBillData.ReceiverID = "";
                                objDTBillData.ReceiverName = objModel.Station ?? ""; //21May19
                                objDTBillData.ReceiverMNo = "";
                                objDTBillData.ReceiverIDProof = "";
                                objDTBillData.TotalFPoint = objModel.objProduct.TotalWeight;
                                objDTBillData.TotalQty = objModel.objProduct.TotalQty;
                                objDTBillData.CashReward = 0;
                                objDTBillData.CommssnAmt = objModel.objProduct.TotalCommsonAmt;
                                objDTBillData.RecvAmount = 0;
                                objDTBillData.ReturnToCustAmt = 0;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.RecTimeStamp = DateTime.Now;
                                objDTBillData.UserId = objModel.objCustomer.UserDetails.UserId;
                                objDTBillData.UserName = objModel.objCustomer.UserDetails.UserName;
                                objDTBillData.DelvPlace = string.IsNullOrEmpty(objModel.objProduct.DeliveryPlace) ? "" : objModel.objProduct.DeliveryPlace;
                                objDTBillData.DelvStatus = "";
                                objDTBillData.DelvUserId = 0;
                                objDTBillData.DelvRecTimeStamp = DateTime.Now;
                                objDTBillData.Version = version;
                                objDTBillData.IDType = "";
                                objDTBillData.BranchName = "";
                                objDTBillData.CourierId = objModel.objProduct.CourierId;
                                objDTBillData.CourierName = (string.IsNullOrEmpty(objModel.objProduct.CourierName) ? "" : objModel.objProduct.CourierName);
                                objDTBillData.LocId = 0;
                                objDTBillData.LocName = "";
                                objDTBillData.DelvAddress = "";
                                objDTBillData.Pincode = "";
                                objDTBillData.OrderType = "";

                                entity.TrnBillDatas.Add(objDTBillData);
                            }
                            int i = 0;

                            using (var objDTTrans = entity.Database.BeginTransaction())
                            {

                                try
                                {
                                    i = entity.SaveChanges();
                                    objDTTrans.Commit();
                                }
                                catch (Exception ex)
                                {
                                    objDTTrans.Rollback();
                                }
                            }
                            if (i == objModel.objListProduct.Count)
                            {

                                var resultPayMode = (from r in entity.M_PayModeMaster select r).ToList();
                                foreach (var obj in objDTListPayMode)
                                {
                                    TrnPayModeDetail objTemp = new TrnPayModeDetail();
                                    objTemp = obj;
                                    objTemp.BillType = "V";
                                    objTemp.PayMode = (from r in resultPayMode where r.Prefix.Trim() == obj.PayPrefix.Trim() select r.PayMode).FirstOrDefault();
                                    if (string.IsNullOrEmpty(objTemp.CardNo))
                                    {
                                        objTemp.CardNo = "";
                                    }
                                    entity.TrnPayModeDetails.Add(objTemp);
                                }
                                i = 0;
                                i = entity.SaveChanges();
                                if (i == objDTListPayMode.Count)
                                {
                                    objResponse.ResponseMessage = "Saved Successfully!";
                                    objResponse.ResponseStatus = "OK";
                                    objResponse.ResponseDetailsToPrint = new DistributorBillModel();
                                    objResponse.ResponseDetailsToPrint.BillNo = UserBillNo;
                                    objResponse.ResponseDetailsToPrint.SoldBy = SoldByCode;


                                }
                            }
                        }
                        catch (DbEntityValidationException e)
                        {
                            objResponse.ResponseMessage = "Something went wrong!";
                            objResponse.ResponseStatus = "FAILED";
                        }

                    }
                    else
                    {
                        // saving process of customer bill
                        DateTime BillDate = DateTime.Now.Date;




                        //saving data in table
                        // decimal? SessId=(from result in entity)
                        bool IsWalletEntry = false;

                        if (objModel != null)
                        {
                            if (objModel.objProduct.PayDetails != null)
                            {
                                if (objModel.objProduct.PayDetails.IsBD)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.BankDeposit;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "G", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByBD, BankCode = 0, ChqDDDate = null, ChqDDNo = "", CardNo = "", Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.BDBankName, AcNo = objModel.objProduct.PayDetails.AccNo, IFSCode = objModel.objProduct.PayDetails.IFSCCode, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });


                                }
                                if (objModel.objProduct.PayDetails.IsCC)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Card;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "G", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, AcNo = "", IFSCode = "", BankCode = 0, Narration = "", BankName = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, Amount = objModel.objProduct.PayDetails.AmountByCard, CardNo = objModel.objProduct.PayDetails.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsQ)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cheque;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "G", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByCheque, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.CHBankName, ChqDDNo = objModel.objProduct.PayDetails.ChequeNo, ChqDDDate = objModel.objProduct.PayDetails.ChequeDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsD)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.DD;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "G", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByDD, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.DDBankName, ChqDDNo = objModel.objProduct.PayDetails.DDNo, ChqDDDate = objModel.objProduct.PayDetails.DDDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsT)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Credit;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "G", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, BankName = "", Amount = objModel.objProduct.PayDetails.AmountByCredit, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, DUserId = 0, DRecTimeStamp = null, ChqDDDate = null, ChqDDNo = "", Narration = objModel.objProduct.PayDetails.Narration, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsV)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Voucher;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "G", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByVoucher, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, DUserId = 0, DRecTimeStamp = null, ChqDDDate = null, ChqDDNo = "", Narration = "", BankName = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.PayDetails.IsW)
                                {


                                    if (WalletBalance >= objModel.objProduct.PayDetails.AmountByWallet)
                                    {

                                        SC.Close();
                                        SC.Open();
                                        objTrans = SC.BeginTransaction();
                                        query = "INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,AcType,VType,SessID,WSEssID) " +
                                                       "Select CASE WHEN Max(VoucherNo) is NULL THEN 1 ELSE Max(VoucherNo)+1 END ,Cast(Convert(varchar,Getdate(),106) as Datetime),'" + objModel.objCustomer.FormNo + "','0','" + objModel.objProduct.PayDetails.AmountByWallet + "','Product purchased Against " + UserBillNo + ".','" + billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo + "','R','D','" + SessId + "','" + SessId + "' FROM TrnVoucher";
                                        cmd = new SqlCommand();
                                        cmd.CommandText = query;
                                        cmd.Connection = SC;
                                        cmd.Transaction = objTrans;



                                        int i = cmd.ExecuteNonQuery();

                                        objTrans.Commit();
                                        SC.Close();
                                        if (i > 0)
                                        {
                                            EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Wallet;
                                            string value = EnumPayModes.GetEnumDescription(enumVar);
                                            PayPrefix.Add(value);
                                            objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "G", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByWallet, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = objModel.objCustomer.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                            ////insert entry into couponsalesdetails for wallet
                                            IsWalletEntry = true;
                                        }
                                        else
                                        {
                                            objResponse.ResponseStatus = "FAILED";
                                            objResponse.ResponseMessage = "Something went wrong";
                                            return objResponse;
                                        }
                                        i = 0;
                                    }
                                    else
                                    {
                                        objResponse.ResponseStatus = "FAILED";
                                        objResponse.ResponseMessage = "Sorry!Insufficient Wallet Balance.";
                                        return objResponse;
                                    }

                                }
                                if (objModel.objProduct.PayDetails.IsP)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Paytm;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "G", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByPaytm, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = objModel.objProduct.PayDetails.PaytmTransactionId, ChqDDDate = DateTime.Now, CardNo = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (objModel.objProduct.CashAmount > 0)
                                {
                                    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cash;
                                    string value = EnumPayModes.GetEnumDescription(enumVar);
                                    PayPrefix.Add(value);
                                    objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.CashAmount, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = BillDate.Date, BillType = "G", BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.TotalNetPayable, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                }
                                if (PayPrefix.Count > 0)
                                {

                                    Paymode = (from r in entity.M_PayModeMaster where PayPrefix.Contains(r.Prefix) select r.PayMode).ToList();
                                }

                            }
                        }


                        string SoldByCode = "";
                        List<TrnBillData> tempTableList = new List<TrnBillData>();
                        string GroupPrefix = "";
                        string BillingPartyCode = objModel.objCustomer.PartyCode;
                        GroupPrefix = (from p in entity.M_GroupMaster
                                       where p.GroupId == (from r in entity.M_LedgerMaster where r.PartyCode == BillingPartyCode select r.GroupId).FirstOrDefault()
                                       select p.Prefix
                                       ).FirstOrDefault();

                        try
                        {
                            List<ProductModel> objListProductModel = new List<ProductModel>();
                            //TempDistributor.objListProduct.AddRange(objModel.objListProduct);
                            foreach (var obj in objModel.objListProduct)
                            {
                                objListProductModel.Add(obj);
                                TrnBillData objDTBillData = new TrnBillData();
                                objDTBillData.SBillNo = maxSbillNo;
                                objDTBillData.FSessId = FsessId ?? 0;
                                objDTBillData.SessId = SessId ?? 0;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.BillDate = BillDate.Date;

                                objDTBillData.RefNo = string.IsNullOrEmpty(objModel.objCustomer.ReferenceIdNo) ? "" : objModel.objCustomer.ReferenceIdNo;
                                objDTBillData.RefId = Convert.ToDecimal(objModel.objCustomer.ReferenceIdNo ?? "0");//22May19
                                objDTBillData.RefName = "";
                                objDTBillData.Remarks = string.IsNullOrEmpty(objModel.objCustomer.Remarks) ? "" : objModel.objCustomer.Remarks;
                                objDTBillData.CType = GroupPrefix;
                                objDTBillData.SoldBy = objModel.objCustomer.UserDetails.PartyCode;
                                SoldByCode = objDTBillData.SoldBy;
                                objDTBillData.BillBy = objDTBillData.SoldBy;
                                objDTBillData.BillNo = billPrefix + "/" + objDTBillData.BillBy + "/" + maxSbillNo;
                                objDTBillData.FType = GroupPrefix;
                                objDTBillData.FCode = string.IsNullOrEmpty(objModel.objCustomer.Name) ? "" : objModel.objCustomer.Name;
                                objDTBillData.PartyName = string.IsNullOrEmpty(objModel.objCustomer.Name) ? "" : objModel.objCustomer.Name;
                                objDTBillData.SupplierId = 0;
                                objDTBillData.ChDDNo = 0;
                                objDTBillData.ChDate = DateTime.Now;
                                objDTBillData.ChAmt = 0;
                                objDTBillData.BankCode = 0;
                                objDTBillData.BankName = "";
                                objDTBillData.FormNo = 0;
                                objDTBillData.TotalTaxAmount = objModel.objProduct.TotalTaxAmount;
                                objDTBillData.TotalSTaxAmount = 0;
                                objDTBillData.TotalDiscount = objModel.objProduct.TotalDiscount;
                                objDTBillData.TotalKitBvValue = 0;
                                objDTBillData.TotalBvValue = objModel.objProduct.TotalBV;
                                objDTBillData.TotalCVValue = objModel.objProduct.TotalCV;
                                objDTBillData.TotalPVValue = objModel.objProduct.TotalPV;
                                objDTBillData.TotalRPValue = objModel.objProduct.TotalRP;

                                objDTBillData.DP = obj.DP ?? 0;
                                objDTBillData.RP = obj.RP ?? 0;
                                objDTBillData.MRP = obj.MRP ?? 0;
                                objDTBillData.CVValue = obj.CVValue ?? 0;
                                objDTBillData.CV = obj.CV ?? 0;
                                objDTBillData.PV = obj.PV ?? 0;
                                objDTBillData.BV = obj.BV ?? 0;
                                objDTBillData.BVValue = obj.BVValue ?? 0;
                                objDTBillData.PVValue = obj.PVValue ?? 0;
                                objDTBillData.RPValue = obj.RPValue ?? 0;
                                objDTBillData.Barcode = obj.Barcode.ToString();
                                objDTBillData.BatchNo = obj.BatchNo.ToString();

                                objDTBillData.DiscountPer = obj.DiscPer ?? 0;
                                objDTBillData.Discount = obj.DiscAmt ?? 0;
                                objDTBillData.ProdCommssn = obj.CommissionPer ?? 0;
                                objDTBillData.ProdCommssnAmt = obj.CommissionAmt ?? 0;
                                objDTBillData.ProductId = obj.ProdCode.ToString();
                                objDTBillData.ProductName = obj.ProductName;
                                objDTBillData.Qty = obj.Quantity;
                                objDTBillData.Rate = obj.Rate ?? 0;
                                objDTBillData.IsKitBV = "N";
                                objDTBillData.DSeries = "";
                                objDTBillData.DImported = "N";
                                objDTBillData.IMEINo = "D";
                                objDTBillData.BNo = "";
                                objDTBillData.ItemType = "N";



                                objDTBillData.JType = "Cash:" + objModel.objProduct.TotalNetPayable;
                                objDTBillData.BillTo = string.IsNullOrEmpty(objModel.objCustomer.Name) ? "" : objModel.objCustomer.Name;
                                objDTBillData.BillFor = string.IsNullOrEmpty(objModel.objCustomer.Name) ? "" : objModel.objCustomer.Name;
                                objDTBillData.IsReceive = "G";
                                objDTBillData.IsCredit = "F";
                                //objDTBillData.BillType = "R";
                                objDTBillData.BillType = "G";
                                objDTBillData.ProdType = "P";
                                objDTBillData.PaymentDtl = "Cash:" + objModel.objProduct.TotalNetPayable;

                                objDTBillData.TotalAmount = objModel.objProduct.TotalTotalAmount;
                                //tax excluding
                                objDTBillData.NetAmount = obj.Amount;

                                objDTBillData.TaxAmount = 0;
                                objDTBillData.Tax = 0;
                                objDTBillData.CGST = obj.TaxPer / 2 ?? 0;
                                objDTBillData.CGSTAmt = obj.TaxAmt / 2 ?? 0;
                                objDTBillData.SGST = obj.TaxPer / 2 ?? 0;
                                objDTBillData.SGSTAmt = obj.TaxAmt / 2 ?? 0;
                                objDTBillData.TaxType = "S";

                                objDTBillData.CashDiscPer = obj.CashDiscPer;
                                objDTBillData.CashDiscAmount = obj.CashDiscAmount;

                                objDTBillData.NetPayable = Math.Round(objModel.objProduct.TotalNetPayable);
                                if (objModel.objProduct.Roundoff == 0)
                                {
                                    objDTBillData.RndOff = objDTBillData.NetPayable - objModel.objProduct.TotalNetPayable;
                                }
                                else
                                {
                                    objDTBillData.RndOff = objModel.objProduct.Roundoff;
                                }
                                objDTBillData.CardAmount = 0;
                                objDTBillData.PayMode = Paymode.Count > 1 ? string.Join(",", Paymode) : Paymode[0];
                                objDTBillData.PayPrefix = PayPrefix.Count > 1 ? string.Join(",", PayPrefix) : PayPrefix[0];
                                objDTBillData.BvTransfer = "N";

                                //objDTBillData.UserSBillNo = maxSbillNo;
                                //objDTBillData.UserBillNo = billPrefix + "/" + objDTBillData.BillBy + "/" + maxSbillNo;
                                objDTBillData.UserSBillNo = maxUserSBillNo;
                                objDTBillData.UserBillNo = UserBillNo;
                                objDTBillData.DispatchStatus = "N";
                                objDTBillData.LR = objModel.VehicleNo ?? ""; //21May19
                                objDTBillData.LRDate = DateTime.Now;
                                objDTBillData.TransporterName = objModel.TransporterName ?? ""; //21May19
                                objDTBillData.DispatchTo = "";
                                objDTBillData.FreightType = "";
                                objDTBillData.Series = "";
                                objDTBillData.Scratch = objModel.EWayBillNo ?? ""; //21May19

                                objDTBillData.Unit = 0;

                                objDTBillData.PSessId = 0;
                                objDTBillData.DcNo = "";
                                objDTBillData.Imported = "N";
                                objDTBillData.FPoint = 0;
                                objDTBillData.FPointValue = 0;
                                objDTBillData.OrdStatus = "";
                                objDTBillData.OrdQty = 0;
                                // objDTBillData.OrderType = "";
                                objDTBillData.OrderDate = DateTime.Now;
                                objDTBillData.OrderNo = "";
                                objDTBillData.RemQty = 0;
                                objDTBillData.DP1 = 0;
                                objDTBillData.DReason = "";
                                objDTBillData.DUserId = 0;
                                objDTBillData.DRecTimeStamp = DateTime.Now;
                                objDTBillData.DocWeight = 0;
                                objDTBillData.DocketNo = "";
                                objDTBillData.DocketDate = DateTime.Now;
                                //objDTBillData.UserBillNo = "";
                                //objDTBillData.UserSBillNo = 0;
                                objDTBillData.STNFormNo = "";
                                objDTBillData.StkRecv = "N";
                                objDTBillData.StkRecvDate = DateTime.Now;
                                objDTBillData.StkRecvUserId = 0;
                                objDTBillData.InTransit = "N";
                                objDTBillData.UID = string.IsNullOrEmpty(objModel.objProduct.UID) ? "" : objModel.objProduct.UID;
                                objDTBillData.OfferUID = 0;
                                objDTBillData.IsKit = "N";
                                objDTBillData.TotalCorton = "";
                                objDTBillData.TotalMonoCorton = "";
                                objDTBillData.SpclOfferId = 0;
                                objDTBillData.VAT = 0;
                                objDTBillData.BuyerAddress = "";
                                objDTBillData.BuyerTIN = "";

                                objDTBillData.TotalDiscount = objModel.objProduct.TotalDiscPer;
                                objDTBillData.TotalDiscountAmt = objModel.objProduct.TotalDiscount;
                                objDTBillData.VDiscountAmt = 0;
                                objDTBillData.VDiscount = 0;
                                objDTBillData.ReceiverID = "";
                                objDTBillData.ReceiverName = objModel.Station ?? ""; //21May19
                                objDTBillData.ReceiverMNo = string.IsNullOrEmpty(objModel.objCustomer.MobileNo) ? "" : objModel.objCustomer.MobileNo;
                                objDTBillData.ReceiverIDProof = "";
                                objDTBillData.TotalFPoint = 0;
                                objDTBillData.TotalQty = objModel.objProduct.TotalQty;
                                objDTBillData.CashReward = 0;
                                objDTBillData.CommssnAmt = objModel.objProduct.TotalCommsonAmt;
                                objDTBillData.RecvAmount = 0;
                                objDTBillData.ReturnToCustAmt = 0;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.RecTimeStamp = DateTime.Now;
                                objDTBillData.UserId = objModel.objCustomer.UserDetails.UserId;
                                objDTBillData.UserName = objModel.objCustomer.UserDetails.UserName;
                                objDTBillData.DelvPlace = string.IsNullOrEmpty(objModel.objProduct.DeliveryPlace) ? "" : objModel.objProduct.DeliveryPlace;
                                objDTBillData.DelvStatus = "";
                                objDTBillData.DelvUserId = 0;
                                objDTBillData.DelvRecTimeStamp = DateTime.Now;
                                objDTBillData.Version = version;
                                objDTBillData.IDType = "";
                                objDTBillData.BranchName = "";
                                objDTBillData.CourierId = 0;
                                objDTBillData.CourierName = (string.IsNullOrEmpty(objModel.objProduct.CourierName) ? "" : objModel.objProduct.CourierName);
                                objDTBillData.LocId = 0;
                                objDTBillData.LocName = "";
                                objDTBillData.DelvAddress = "";
                                objDTBillData.Pincode = "";
                                objDTBillData.OrderType = "";
                                entity.TrnBillDatas.Add(objDTBillData);
                            }
                            int i = 0;

                            using (var objDTTrans = entity.Database.BeginTransaction())
                            {

                                try
                                {
                                    i = entity.SaveChanges();
                                    objDTTrans.Commit();
                                }
                                catch (Exception ex)
                                {
                                    objDTTrans.Rollback();
                                }
                            }
                            if (i == objModel.objListProduct.Count)
                            {

                                var resultPayMode = (from r in entity.M_PayModeMaster select r).ToList();
                                foreach (var obj in objDTListPayMode)
                                {
                                    TrnPayModeDetail objTemp = new TrnPayModeDetail();
                                    objTemp = obj;
                                    objTemp.BillType = "G";
                                    objTemp.PayMode = (from r in resultPayMode where r.Prefix.Trim() == obj.PayPrefix.Trim() select r.PayMode).FirstOrDefault();
                                    if (string.IsNullOrEmpty(objTemp.CardNo))
                                    {
                                        objTemp.CardNo = "";
                                    }
                                    entity.TrnPayModeDetails.Add(objTemp);
                                }
                                i = 0;
                                i = entity.SaveChanges();
                                if (i == objDTListPayMode.Count)
                                {
                                    objResponse.ResponseMessage = "Saved Successfully!";
                                    objResponse.ResponseStatus = "OK";
                                    objResponse.ResponseDetailsToPrint = new DistributorBillModel();
                                    objResponse.ResponseDetailsToPrint.BillNo = UserBillNo;
                                    objResponse.ResponseDetailsToPrint.SoldBy = SoldByCode;


                                }
                            }
                        }
                        catch (DbEntityValidationException e)
                        {
                            objResponse.ResponseMessage = "Something went wrong!";
                            objResponse.ResponseStatus = "FAILED";
                        }
                    }
                }

            }
            catch (Exception e)
            {

            }
            return objResponse;
        }
        public ResponseDetail SaveBillDetail(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                // TrnBillMain obj = new TrnBillMain();
                ShiftDataintoTemptable("TrnBillMain", "TempBillMain", ",Getdate()", "  AND UserBillNo='" + objModel.BillNo + "'");
                using (var db = new InventoryEntities(enttConstr))
                {
                    var obj = (from s in db.TrnBillMains where s.UserBillNo == objModel.BillNo select s).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.ReceiverName = objModel.Station ?? "";
                        obj.DispatchStatus = objModel.FreightType != "H" ? "N" : "Y";
                        obj.LR = objModel.VehicleNo ?? "";
                        obj.TransporterName = objModel.TransporterName ?? "";
                        obj.FreightType = objModel.FreightType;
                        //obj.NetPayable=obj.NetPayable-obj.FreightAmt+ objModel.FreightAmt;
                        // obj.FreightAmt = objModel.FreightAmt;
                        DateTime BillDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(objModel.DispatchDateStr))
                        {
                            BillDate = Convert.ToDateTime(objModel.DispatchDateStr);
                            BillDate = BillDate.Date;
                            obj.DocketDate = BillDate;
                        }
                        obj.Scratch = objModel.EWayBillNo ?? "";
                        obj.CourierId = objModel.objProduct.CourierId;
                        obj.CourierName = (string.IsNullOrEmpty(objModel.objProduct.CourierName) ? "" : objModel.objProduct.CourierName);
                        obj.DelvPlace = string.IsNullOrEmpty(objModel.objProduct.DeliveryPlace) ? "" : objModel.objProduct.DeliveryPlace;
                        obj.Remarks = string.IsNullOrEmpty(objModel.objCustomer.Remarks) ? "" : objModel.objCustomer.Remarks;
                    }
                    int i = db.SaveChanges();
                    if (i != 0)
                    {
                        objResponse.ResponseStatus = "OK";
                        objResponse.ResponseMessage = "Bill updated Successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return objResponse;
        }

        public DistributorBillModel getInvoice(string BillNo, string CurrentPartyCode, string id)
     {
            DistributorBillModel objDistributorModel = new DistributorBillModel();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    decimal? TotCGSTTaxPer = 0;
                    decimal? TotCGSTTaxAmt = 0;
                    decimal? TotSGSTTaxPer = 0;
                    decimal? TotSGSTTaxAmt = 0;
                    decimal? TotalTaxAmt = 0;
                    decimal? TotalTaxPer = 0;
                    objDistributorModel.objListProduct = new List<ProductModel>();
                    objDistributorModel.objListProduct = (from r in entity.TrnBillMains

                                                          join t in entity.TrnBillDetails on r.BillNo equals t.BillNo
                                                          join M in entity.M_ProductMaster on t.ProductId equals M.ProdId
                                                          where r.UserBillNo == BillNo && (id == r.FCode || id == "F")
                                                          select new ProductModel
                                                          {
                                                              DP = t.DP,
                                                              IdNo = r.FCode,
                                                              Mobileno = r.ReceiverMNo,
                                                              PartyName = r.PartyName,
                                                              ProductCodeStr = t.ProductId,
                                                              ProductName = t.ProductName,
                                                              Rate = t.Rate,
                                                              Quantity = t.Qty,
                                                              Amount = t.NetAmount,
                                                              BVValue = t.BvValue,
                                                              CGST = t.CGST,
                                                              Roundoff = r.RndOff,
                                                              SGST = t.SGST,
                                                              CGSTAmount = t.CGSTAmt,
                                                              SGSTAmount = t.SGSTAmt,
                                                              TotalNetPayable = r.NetPayable,
                                                              BillDate = r.BillDate,
                                                              TaxAmt = t.TaxAmount,
                                                              TaxPer = t.Tax,
                                                              BillSoldBy = r.SoldBy,
                                                              OrderType = r.OrderType,
                                                              TotalBV = r.BvValue,
                                                              TotalQty = r.TotalQty,
                                                              TaxType = t.TaxType,
                                                              BillType = r.BillType,
                                                              ProductType = t.ProdType,
                                                              HSNCode = M.HSNCode,
                                                              DiscAmt = t.Discount,
                                                              PVValue = t.PVValue,
                                                              TotalPV = r.PVValue,
                                                              TotalWeight = r.TotalFPoint,
                                                              DeliveryPlace = r.DelvAddress == "" ? r.DelvPlace : r.DelvAddress,
                                                              FreightType = r.FreightType,
                                                              FreightAmt = r.FreightAmt,
                                                              CourierId = r.CourierId,
                                                              CourierName = r.CourierName,
                                                              DocketNo = r.DocketNo,
                                                              DocketDate = r.DocketDate,
                                                              buyerTin = r.BuyerTIN,
                                                              TransporterName = r.TransporterName,
                                                              VehicleNo = r.LR,
                                                              EWayBillNo = r.Scratch,
                                                              Station = r.ReceiverName,
                                                              SpclOfferId = t.SpclOfferId,
                                                              OfferUID = t.OfferUID,
                                                              Remark = r.Remarks,
                                                              DispStatus = r.JType,
                                                              IsCardIssue = r.OrderNo,
                                                              MRP = M.MRP,
                                                              DispatchDate = r.DOD
                                                          }
                                                         ).ToList();

                    //objDistributorModel.objListProduct.AddRange(objListproduct);
                    //Added on 31Aug19 [Start]






                    string Offernames = "";
                    List<VisionOffer> objOfferList = new List<VisionOffer>();
                    var IsCouponDiscount = (from r in entity.TrnBillMains where r.UserBillNo == BillNo select r.BvTransfer).FirstOrDefault();
                    var objDistOffers = (from r in objDistributorModel.objListProduct join o in entity.VisionOffers on r.OfferUID equals o.OfferId select o.OfferName).Distinct().ToList();
                    var objDistOffer1 = (from r in objDistributorModel.objListProduct join o in entity.VisionOffers on r.SpclOfferId equals o.OfferId select o.OfferName).ToList().Distinct();
                    //List<TrnPayModeDetail> objDTListPayMode = new List<TrnPayModeDetail>();

                    objDistributorModel.objDTListPayMode = (from r in entity.TrnPayModeDetails

                                        join t in entity.TrnBillMains on r.BillNo equals t.BillNo
                                        where t.UserBillNo == BillNo && (id == t.FCode || id == "F")&& r.ActiveStatus  == "Y"
                                                            select new TrnPayModeDetail11
                                        { BillNo= r.BillNo,
                                         BillDate =r.BillDate,
                                         PayMode=r.PayMode,
                                         Amount=r.Amount,
                                         BillAmt=r.BillAmt,
                                         SoldBy=r.SoldBy,
                                         BillType=r.BillType,
                                         ActiveStatus=r.ActiveStatus
                                        }).ToList();



                    foreach (var obj in objDistOffers)
                        Offernames = Offernames + " " + obj + ",";
                    foreach (var obj in objDistOffer1)
                        Offernames = Offernames + " " + obj + ",";
                    if (IsCouponDiscount == "Y")
                    {
                        Offernames = "Coupon wallet used Rs. " + (from r in objDistributorModel.objListProduct select r.DiscAmt).Sum();
                    }
                    Offernames = Offernames.TrimEnd(',');

                    if (objDistributorModel.objListProduct != null)
                    {
                        objDistributorModel.OrderNo = objDistributorModel.objListProduct[0].IsCardIssue;
                        objDistributorModel.EWayBillNo = objDistributorModel.objListProduct[0].EWayBillNo;
                        objDistributorModel.ShippedTo = objDistributorModel.objListProduct[0].DeliveryPlace;
                        objDistributorModel.TransporterName = objDistributorModel.objListProduct[0].CourierName;
                        objDistributorModel.DispatchDateStr = Convert.ToString(objDistributorModel.objListProduct[0].DocketDate);
                    }
                    objDistributorModel.objListProduct = objDistributorModel.objListProduct.OrderByDescending(m => m.ProductType).ThenByDescending(m => m.Rate).ToList();
                    //Added on 31Aug19 [End]

                    decimal? TotalTaxableAmount = 0;
                    string OrderType = objDistributorModel.objListProduct[0].OrderType;
                    objDistributorModel.objTaxSummary = new List<TaxSummary>();
                    if (OrderType != "T")
                    {
                        objDistributorModel.objTaxSummary = objDistributorModel.objListProduct

                        .GroupBy(m => new
                        {
                            m.TaxPer,
                            m.CGST,
                            m.SGST,
                            m.HSNCode

                        }).Select(m => new TaxSummary
                        {
                            SumTaxPer = m.Key.TaxPer ?? 0,
                            SumCGSTPer = m.Key.CGST,
                            SumSGSTPer = m.Key.SGST,
                            HSNCode = m.Key.HSNCode,
                            SumTaxAmt = m.Sum(r => r.TaxAmt) ?? 0,
                            SumCGSTAmt = m.Sum(r => r.CGSTAmount),
                            SumSGSTAmt = m.Sum(r => r.SGSTAmount),
                            SumAmount = m.Sum(r => r.Amount),
                            SumNetPayableAmount = m.Sum(p => p.CGSTAmount + p.SGSTAmount + p.TaxAmt + p.Amount /*+ p.FreightAmt*/) ?? 0
                        }).ToList();
                    }
                    else
                    {
                        objDistributorModel.objTaxSummary = objDistributorModel.objListProduct
                         .GroupBy(m => new
                         {
                             m.TaxPer,
                             m.CGST,
                             m.SGST
                         }).Select(m => new TaxSummary
                         {
                             SumTaxPer = m.Key.TaxPer ?? 0,
                             SumCGSTPer = m.Key.CGST,
                             SumSGSTPer = m.Key.SGST,
                             SumTaxAmt = m.Sum(r => r.TaxAmt) ?? 0,
                             SumCGSTAmt = m.Sum(r => r.CGSTAmount),
                             SumSGSTAmt = m.Sum(r => r.SGSTAmount),
                             SumAmount = m.Sum(r => r.Amount),
                             SumNetPayableAmount = m.Sum(p => p.CGSTAmount + p.SGSTAmount + p.TaxAmt + p.Amount /*+ p.FreightAmt*/) ?? 0
                         }).ToList();
                    }
                    objDistributorModel.objTaxSummary = objDistributorModel.objTaxSummary.Where(m => m.SumNetPayableAmount > 0).ToList();
                    decimal TotalNetPayableTobill = 0;
                    TotalNetPayableTobill = objDistributorModel.objTaxSummary.Sum(m => m.SumNetPayableAmount);
                    foreach (var obj in objDistributorModel.objTaxSummary)
                    {
                        //objDistributorModel.objListProduct.Add(obj);
                        TotalNetPayableTobill += obj.SumNetPayableAmount;
                        TotalTaxableAmount += obj.SumAmount;
                        TotalTaxAmt += obj.SumTaxAmt;
                        TotCGSTTaxAmt += obj.SumCGSTAmt;
                        TotSGSTTaxAmt += obj.SumSGSTAmt;
                    }
                    var result = (from r in entity.M_CompanyMaster where r.ActiveStatus == "Y" select r).FirstOrDefault();
                    var config = (from r in entity.M_ConfigMaster select r).FirstOrDefault();
                    List<string> soapandtoothpaste = config.SoapAndToothPasteProducts.Split(',').ToList();
                    var sum = objDistributorModel.objListProduct.Where(c => soapandtoothpaste.Contains(c.ProductCodeStr)).Select(c => c.BVValue).Sum();


                    if (result != null)
                    {

                        objDistributorModel.CompCity = result.CompCity;
                        string SoldBy = objDistributorModel.objListProduct[0].BillSoldBy;
                        var resultDetails = (from r in entity.M_LedgerMaster where r.PartyCode == SoldBy select r).FirstOrDefault();
                        if (resultDetails != null)
                        {
                            objDistributorModel.GSTNo = resultDetails.TinNo;
                            objDistributorModel.SoldByName = resultDetails.PartyName;
                            objDistributorModel.SoldByAddress = resultDetails.Address1;
                            objDistributorModel.SoldByCity = resultDetails.CityName;
                            objDistributorModel.SoldByTel = resultDetails.MobileNo.ToString();
                            objDistributorModel.SoldByEmail = resultDetails.E_MailAdd;
                            objDistributorModel.CinNo = resultDetails.NewFld1;
                            objDistributorModel.PanNo = resultDetails.PanNo;
                        }
                        objDistributorModel.BillNo = BillNo;
                        objDistributorModel.BillDate = objDistributorModel.objListProduct[0].BillDate.Date;
                        objDistributorModel.CompanyName = result.CompName;
                        objDistributorModel.CompanyAdd = result.CompAdd;
                        objDistributorModel.CompanyTel = result.ContactNo;
                        objDistributorModel.CompanyMail = result.CompMail;
                        objDistributorModel.objCustomer = new CustomerDetail();
                        //if Idno is in M_Ledgermaster then get details from there else from sjlabs.M_Memvbermaster
                        var Fcode = objDistributorModel.objListProduct[0].IdNo;

                        foreach (var obj in objDistributorModel.objListProduct)
                        {
                            if (obj.DocketDate == null)
                                obj.DocketDateStr = "";
                            else
                                obj.DocketDateStr = (obj.DocketDate ?? DateTime.Now).ToString("dd-MMM-yyyy");
                        }

                        var CustomerResult = (from r in entity.M_LedgerMaster where r.PartyCode == Fcode select r).FirstOrDefault();
                        if (CustomerResult != null)
                        {
                            objDistributorModel.objCustomer.IdNo = CustomerResult.PartyCode;
                            objDistributorModel.objCustomer.Name = CustomerResult.PartyName;
                            objDistributorModel.objCustomer.Address = CustomerResult.Address1;
                            objDistributorModel.objCustomer.MobileNo = CustomerResult.MobileNo.ToString();
                            objDistributorModel.objCustomer.GSTNo = CustomerResult.TinNo;
                            objDistributorModel.objCustomer.PANNo = CustomerResult.PanNo;
                            objDistributorModel.objCustomer.StateCode = CustomerResult.StateCode;
                            objDistributorModel.objCustomer.Email = CustomerResult.E_MailAdd;
                            objDistributorModel.objCustomer.ZipCode = Convert.ToString(CustomerResult.PinCode);
                            objDistributorModel.objCustomer.State = CustomerResult.StateCode != 0 ? (from r in entity.M_StateDivMaster where r.StateCode == CustomerResult.StateCode select r.StateName).FirstOrDefault() : "";
                            objDistributorModel.objCustomer.City = CustomerResult.CityCode != 0 ? (from r in entity.M_CityStateMaster where r.CityCode == CustomerResult.CityCode select r.CityName).FirstOrDefault() : "";
                        }
                        else
                        {
                            objDistributorModel.objCustomer = GetCustInfo(objDistributorModel.objListProduct[0].IdNo);
                            if (objDistributorModel.objCustomer.IdNo == "Record does not exists!")
                            {
                                objDistributorModel.objCustomer = new CustomerDetail();
                                objDistributorModel.objCustomer.IdNo = objDistributorModel.objListProduct[0].IdNo;
                                objDistributorModel.objCustomer.Name = objDistributorModel.objListProduct[0].PartyName;
                                objDistributorModel.objCustomer.Address = "";
                                objDistributorModel.objCustomer.MobileNo = objDistributorModel.objListProduct[0].Mobileno;
                                objDistributorModel.objCustomer.PANNo = "";

                            }
                        }
                        if (string.IsNullOrEmpty(objDistributorModel.objCustomer.GSTNo) && (!string.IsNullOrEmpty(objDistributorModel.objListProduct[0].buyerTin)))
                        {
                            objDistributorModel.objCustomer.GSTNo = objDistributorModel.objListProduct[0].buyerTin;
                        }
                        objDistributorModel.objCustomer.Remarks = objDistributorModel.objListProduct[0].Remark;
                        objDistributorModel.DelvAddress = objDistributorModel.objListProduct[0].DeliveryPlace;
                        objDistributorModel.StateGSTName = GetStateGstName(objDistributorModel.objCustomer.StateCode);
                        objDistributorModel.objProduct = new ProductModel();
                        objDistributorModel.objProduct.TotalTaxPer = TotalTaxPer ?? 0;
                        objDistributorModel.objProduct.TotalTaxAmount = TotalTaxAmt ?? 0;
                        objDistributorModel.objProduct.TotalCGSTPer = TotCGSTTaxPer ?? 0;
                        objDistributorModel.objProduct.TotalCGSTAmt = TotCGSTTaxAmt ?? 0;
                        objDistributorModel.objProduct.TotalSGSTPer = TotSGSTTaxPer ?? 0;
                        objDistributorModel.objProduct.TotalSGSTAmt = TotSGSTTaxAmt ?? 0;
                        objDistributorModel.objProduct.TotalTaxableAmt = TotalTaxableAmount ?? 0;
                        objDistributorModel.objProduct.TotalNetPayable = objDistributorModel.objListProduct[0].TotalNetPayable;
                        //objDistributorModel.objProduct.TotalNetPayable = TotalNetPayableTobill;
                        //objDistributorModel.objProduct.TotalAmount = TotalNetAmount ?? 0;
                        objDistributorModel.objProduct.Roundoff = objDistributorModel.objListProduct[0].Roundoff;
                        objDistributorModel.objProduct.TotalAmount = objDistributorModel.objListProduct[0].TotalAmount;
                        objDistributorModel.objProduct.TotalBV = objDistributorModel.objListProduct[0].TotalBV;
                        objDistributorModel.objProduct.TotalPV = objDistributorModel.objListProduct[0].TotalPV;
                        objDistributorModel.objProduct.TotalQty = objDistributorModel.objListProduct[0].TotalQty;
                        objDistributorModel.objProduct.OfferName = Offernames;//31Aug19
                        objDistributorModel.SoapandToothpastepv = sum;
                        objDistributorModel.OtherProductpv = objDistributorModel.objProduct.TotalBV - sum;

                        objDistributorModel.objProduct.TotalAmountString = ConvertNumbertoWords(objDistributorModel.objProduct.TotalNetPayable);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return objDistributorModel;
        }
        public List<BankModel> GetBankList()
        {
            List<BankModel> objListBanks = new List<BankModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objListBanks = (from result in entity.M_BankMaster
                                    where result.ActiveStatus == "Y"
                                    select new BankModel
                                    {
                                        BankCode = (int)result.BankCode,
                                        BankName = result.BankName,
                                        ActiveStatus = result.ActiveStatus,
                                        AccNo = result.AcNo,
                                        Remarks = result.Remarks,
                                        IFSCCode = result.IFSCode,

                                    }).ToList();
                }
            }
            catch (Exception e)
            {

            }
            return objListBanks;
        }

        public List<PartyModel> GetAllParty(string LoginPartyCode, decimal LoginStateCode, bool NeedWallet)
        {
            List<PartyModel> objpartyList = new List<PartyModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    bool IsAdmin = false;
                    bool IsSoldByHo = false;
                    decimal LoginGroupId = 0;
                    var SameLevelBilling = "";
                    if (!string.IsNullOrEmpty(LoginPartyCode))
                    {
                        var result = (from r in entity.Inv_M_UserMaster
                                      join s in entity.M_LedgerMaster on r.BranchCode equals s.PartyCode
                                      where s.PartyCode == LoginPartyCode
                                      select new
                                      {
                                          IsAdmin = r.IsAdmin,
                                          GroupID = s.GroupId,
                                          SameLevelBilling = s.RecvdCForm
                                      }).FirstOrDefault();
                        if (result != null)
                        {
                            LoginGroupId = result.GroupID;
                            if (LoginGroupId == 0)
                            {
                                IsSoldByHo = true;
                            }
                            else
                            {
                                IsSoldByHo = false;
                            }
                            IsAdmin = result.IsAdmin == "Y" ? true : false;

                            SameLevelBilling = result.SameLevelBilling;
                        }
                    }

                    if (NeedWallet)
                        objpartyList = (from party in entity.M_LedgerMaster
                                        join w in entity.V_PartyBalance on party.PartyCode equals w.PartyCode
                                        where party.ActiveStatus == "Y" && party.PartyCode != LoginPartyCode
                                        orderby party.PartyName
                                        select new PartyModel
                                        {
                                            PartyCode = party.PartyCode,
                                            ParentPartyCode = party.ParentPartyCode,
                                            PartyName = party.PartyName,
                                            StateCode = party.StateCode,
                                            GroupId = party.GroupId,
                                            UserPartyCode = party.UserPartyCode,
                                            Address1 = party.Address1,
                                            CreditLimit = w.Balance ?? 0
                                        }
                                        ).ToList();
                    else
                        objpartyList = (from party in entity.M_LedgerMaster
                                        where party.ActiveStatus == "Y" && party.PartyCode != LoginPartyCode
                                        orderby party.PartyName
                                        select new PartyModel
                                        {
                                            PartyCode = party.PartyCode,
                                            ParentPartyCode = party.ParentPartyCode,
                                            PartyName = party.PartyName,
                                            StateCode = party.StateCode,
                                            GroupId = party.GroupId,
                                            UserPartyCode = party.UserPartyCode,
                                            Address1 = party.Address1,
                                            CreditLimit = 0
                                        }).ToList();


                    if (IsSoldByHo == false)
                    {
                        objpartyList = objpartyList.Where(m => m.PartyCode != LoginPartyCode && ((NeedWallet && LoginGroupId == 1 && ((m.StateCode == LoginStateCode && m.GroupId > LoginGroupId) || (SameLevelBilling == "Y" && m.GroupId >= LoginGroupId) || m.ParentPartyCode == LoginPartyCode || m.GroupId == 0)) || (!(NeedWallet && LoginGroupId == 1) && ((m.StateCode == LoginStateCode && m.GroupId > LoginGroupId) || (SameLevelBilling == "Y" && m.GroupId >= LoginGroupId) || m.ParentPartyCode == LoginPartyCode)))).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return objpartyList;
        }

        //public ConfigDetails GetConfigDetails()
        //{
        //    ConfigDetails objConfigDetails = new ConfigDetails();
        //    try
        //    {
        //        using(var entity=new InventoryEntities(enttConstr))
        //        {
        //            objConfigDetails = (from result in entity.Inv_M_ConfigMaster
        //                                select new ConfigDetails
        //                                {
        //                                    C_PrintBill = result.C_PrintBill,
        //                                    C_IsBillOnMRP = result.C_IsBillOnMRP,
        //                                    C_AddDuplicateProd = result.C_AddDuplicateProd,
        //                                    C_AllowDiscount = result.C_AllowDiscount,
        //                                    C_DiscForAllCust = result.C_DiscForAllCust

        //                                }).FirstOrDefault();
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //    return objConfigDetails;
        //}

        //public decimal GetWalletBalance(string CustCode)
        //{
        //    decimal WalletBalance = 0;

        //    try
        //    {
        //        using (var entity = new InventoryEntities(enttConstr))
        //        {
        //            WalletBalance = (from result in entity.CouponSalesDetails
        //                             where result.ActiveStatus == "Y" && result.CustCode == CustCode

        //                             select result.Amount
        //                           ).DefaultIfEmpty(0).Sum();
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return WalletBalance;
        //}

        public List<GroupModel> GetGroupList()
        {
            List<GroupModel> objGroupList = new List<GroupModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objGroupList = (from r in entity.M_GroupMaster
                                    where r.ActiveStatus == "Y" && r.InvLogin == "Y"
                                    join p in entity.M_LedgerMaster on r.GroupId equals p.GroupId
                                    select new GroupModel
                                    {
                                        GroupName = r.GroupName,
                                        GroupId = r.GroupId
                                    }

                                  ).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objGroupList;
        }

        public List<PartyModel> GetPartyList()
        {
            List<PartyModel> objPartyList = new List<PartyModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objPartyList = (from r in entity.M_LedgerMaster
                                    where r.ActiveStatus == "Y"
                                    select new PartyModel
                                    {
                                        PartyName = r.PartyName,
                                        PartyCode = r.PartyCode,
                                        GroupId = r.GroupId
                                    }
                                  ).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objPartyList;
        }

        public ResponseDetail SaveStockJv(StockJv objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            StockJv TempStockJv = new StockJv();

            decimal maxJNo = 0;
            decimal? FsessId = 0;
            string JvNo = "";
            string version = "";
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";

            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    FsessId = (from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result.FSessId).DefaultIfEmpty(0).Max();
                    maxJNo = (from r in entity.TrnStockJvs select r.JNo).DefaultIfEmpty(0).Max();
                    if (maxJNo == 0)
                    {
                        maxJNo = 1000;
                    }
                    maxJNo = maxJNo + 1;
                    if (objModel.isAdd)
                    {
                        JvNo = "Add/" + maxJNo;
                    }
                    else
                    {
                        JvNo = "Less/" + maxJNo;
                    }
                    DateTime CurrentJvDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(objModel.JvDate))
                    {
                        var SplitDate = objModel.JvDate.Split('-');
                        string NewDate = (SplitDate[1].Length == 1 ? "0" + SplitDate[1] : SplitDate[1]) + "/" + (SplitDate[0].Length == 1 ? "0" + SplitDate[0] : SplitDate[0]) + "/" + SplitDate[2];
                        CurrentJvDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                        CurrentJvDate = CurrentJvDate.Date;
                    }
                    version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();

                    foreach (var obj in objModel.objListProduct)
                    {
                        TrnStockJv objDTStockData = new TrnStockJv();
                        objDTStockData.JNo = maxJNo;
                        objDTStockData.JvNo = JvNo;
                        objDTStockData.Version = version;
                        objDTStockData.UserId = objModel.LoginUser.UserId;
                        objDTStockData.UserName = objModel.LoginUser.UserName;
                        objDTStockData.StockDate = CurrentJvDate;
                        objDTStockData.SoldBy = objModel.LoginUser.PartyCode;
                        objDTStockData.Remarks = string.IsNullOrEmpty(objModel.Remarks) ? "" : objModel.Remarks;
                        objDTStockData.RefNo = string.IsNullOrEmpty(objModel.RefNo) ? "" : objModel.RefNo;
                        objDTStockData.RecTimeStamp = DateTime.Now;
                        objDTStockData.Qty = obj.Quantity;
                        objDTStockData.ProductName = obj.ProductName;
                        objDTStockData.ProdType = "P";
                        objDTStockData.ProdId = obj.ProdCode.ToString();
                        objDTStockData.PartyName = objModel.PartyName;
                        objDTStockData.FCode = objModel.FCode;
                        if (objModel.isAdd)
                            objDTStockData.JType = "A";
                        else
                            objDTStockData.JType = "L";
                        objDTStockData.BatchNo = obj.BatchNo;
                        objDTStockData.Barcode = obj.Barcode;
                        objDTStockData.ActiveStatus = "Y";
                        objDTStockData.FSessId = FsessId ?? 0;
                        entity.TrnStockJvs.Add(objDTStockData);
                    }
                    int i = 0;


                    try
                    {
                        i = entity.SaveChanges();
                        if (i == objModel.objListProduct.Count)
                        {

                            foreach (var obj in objModel.objListProduct)
                            {
                                Im_CurrentStock objCurrentStock = new Im_CurrentStock();
                                objCurrentStock.FSessId = FsessId ?? 0;
                                objCurrentStock.SupplierCode = "0";
                                objCurrentStock.StockDate = CurrentJvDate;
                                objCurrentStock.RefNo = JvNo;
                                objCurrentStock.FCode = objModel.FCode;
                                objCurrentStock.GroupId = objModel.GroupId;
                                objCurrentStock.ProdId = obj.ProdCode.ToString();
                                objCurrentStock.BatchCode = obj.BatchNo;
                                objCurrentStock.Barcode = obj.Barcode;

                                if (objModel.isAdd)
                                {
                                    objCurrentStock.SType = "I";
                                    objCurrentStock.Qty = obj.Quantity;
                                    objCurrentStock.BType = "A";
                                    objCurrentStock.Remarks = "Stock Added";
                                    objCurrentStock.BillType = "A";
                                }
                                else
                                {
                                    objCurrentStock.SType = "O";
                                    objCurrentStock.Qty = -(obj.Quantity);
                                    objCurrentStock.BType = "L";
                                    objCurrentStock.Remarks = "Stock Lessed";
                                    objCurrentStock.BillType = "L";
                                }
                                objCurrentStock.ActiveStatus = "Y";
                                objCurrentStock.EntryBy = objModel.LoginUser.PartyCode;
                                objCurrentStock.StockFor = objModel.FCode;
                                objCurrentStock.RecTimeStamp = DateTime.Now;
                                objCurrentStock.UserId = objModel.LoginUser.UserId;
                                objCurrentStock.Version = version;
                                objCurrentStock.IsDisp = "N";
                                objCurrentStock.InvoiceNo = "";
                                objCurrentStock.ProdType = "P";
                                objCurrentStock.DispQty = 0;

                                entity.Im_CurrentStock.Add(objCurrentStock);
                            }
                            i = 0;
                            try
                            {
                                i = entity.SaveChanges();
                                if (i == objModel.objListProduct.Count)
                                {
                                    objResponse.ResponseMessage = "Saved Successfully!";
                                    objResponse.ResponseStatus = "OK";
                                }
                                else
                                {
                                    objResponse.ResponseMessage = "Something went wrong!";
                                    objResponse.ResponseStatus = "FAILED";
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            catch (Exception ex)
            {

            }

            return objResponse;
        }

        public ResponseDetail SavePurchaseInvoice(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            M_InwardData objInwardData = new M_InwardData();

            DistributorBillModel TempDistributor = new DistributorBillModel();

            decimal maxUserSBillNo = 0;
            decimal? SessId = 0;
            string billPrefix = "";
            decimal maxGrNo = 0;
            decimal? FsessId = 0;
            string InwardNo = "";
            string version = "";


            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";

            try
            {
                string AppConnectionString = AppConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);

                string query = "Select Max(SessID) as MaxSessId from M_SessnMaster";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SessId = decimal.Parse(reader["MaxSessId"].ToString());
                    }
                }
                using (var entity = new InventoryEntities(enttConstr))
                {
                    maxGrNo = (from result in entity.M_InwardMain select result.GrNo).DefaultIfEmpty(0).Max();
                    maxGrNo = maxGrNo + 1;
                    FsessId = (from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result.FSessId).DefaultIfEmpty(0).Max();
                    ////decimal? SessId = (from result in entity.M_SessnMaster select result.SessID).Max();

                    billPrefix = (from result in entity.M_ConfigMaster select result.BillPrefix).FirstOrDefault();
                    //maxUserSBillNo = (from result in entity.TrnBillMains where result.FSessId == FsessId && result.SoldBy == objModel.objCustomer.UserDetails.PartyCode && result.BillType != "S" select result.UserSBillNo).Max();
                    //maxUserSBillNo = maxUserSBillNo + 1;
                    InwardNo = "PI/" + "WR" + "/" + maxGrNo;
                    version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();


                    if (objModel != null)
                    {
                        DateTime BillDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(objModel.BillDateStr))
                        {
                            var SplitDate = objModel.BillDateStr.Split('-');
                            string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                            BillDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                            BillDate = BillDate.Date;
                            string SoldByCode = "";
                            string GroupPrefix = "";
                            string BillingPartyCode = objModel.objCustomer.PartyCode;
                            //GroupPrefix = (from p in entity.M_GroupMaster
                            //               where p.GroupId == (from r in entity.M_LedgerMaster where r.PartyCode == BillingPartyCode select r.GroupId).FirstOrDefault()
                            //               select p.Prefix
                            //               ).FirstOrDefault();
                            GroupPrefix = "PI";
                            List<ProductModel> objListProductModel = new List<ProductModel>();
                            foreach (var obj in objModel.objListProduct)
                            {
                                objListProductModel.Add(obj);
                                M_InwardData objDTBillData = new M_InwardData();

                                objDTBillData.FSessId = FsessId ?? 0;
                                objDTBillData.InwardBy = objModel.objCustomer.UserDetails.PartyCode;
                                objDTBillData.InwardByName = objModel.objCustomer.UserDetails.PartyName;
                                objDTBillData.GrNo = maxGrNo;
                                objDTBillData.InwardNo = InwardNo;
                                objDTBillData.SupplierCode = objModel.objCustomer.PartyCode;
                                objDTBillData.SupplierName = objModel.objCustomer.PartyName;
                                objDTBillData.OrderDate = DateTime.Now.Date;
                                objDTBillData.DeliveryDate = DateTime.Now.Date;
                                objDTBillData.RecvDate = BillDate.Date;
                                objDTBillData.RefNo = string.IsNullOrEmpty(objModel.objCustomer.ReferenceIdNo) ? "" : objModel.objCustomer.ReferenceIdNo;
                                objDTBillData.ProdCode = obj.ProdCode.ToString();
                                objDTBillData.ProdName = obj.ProductName;
                                objDTBillData.BatchNo = obj.BatchNo.ToString();
                                objDTBillData.Barcode = obj.Barcode.ToString();
                                objDTBillData.Qty = obj.Quantity;
                                objDTBillData.FreeQty = 0;
                                objDTBillData.MRP = obj.MRP ?? 0;
                                objDTBillData.PRate = obj.Rate ?? 0;
                                objDTBillData.DP = obj.DP ?? 0;
                                //to be asked
                                objDTBillData.TradeDiscount = obj.DiscPer ?? 0;
                                objDTBillData.TradeAmount = obj.DiscAmt ?? 0;

                                objDTBillData.LotDiscount = 0;
                                objDTBillData.TotalLotDiscount = 0;
                                //tax excluding
                                objDTBillData.Amount = obj.Amount;
                                objDTBillData.AValue = 0;
                                objDTBillData.AValueAmt = 0;
                                objDTBillData.DiscountAmt = obj.DiscAmt ?? 0;
                                objDTBillData.TotalAmt = obj.TotalAmount;
                                objDTBillData.PStatus = "Y";
                                objDTBillData.TotalAmount = objModel.objProduct.TotalTotalAmount;
                                objDTBillData.TotalTradeDiscount = objModel.objProduct.TotalDiscount;

                                objDTBillData.CashDiscPer = obj.CashDiscPer;
                                objDTBillData.TotalCashDiscount = obj.CashDiscAmount;
                                objDTBillData.TotalQty = objModel.objProduct.TotalQty;
                                objDTBillData.TotalFreeQty = 0;
                                objDTBillData.TotalDiscount = objModel.objProduct.TotalDiscount + objModel.objProduct.CashDiscAmount;
                                objDTBillData.TotalTaxAmt = objModel.objProduct.TotalTaxAmount;
                                objDTBillData.TotalEAmt = 0;
                                objDTBillData.RndOff = objModel.objProduct.Roundoff;
                                objDTBillData.NetPayable = objModel.objProduct.TotalNetPayable;
                                objDTBillData.Remarks = string.IsNullOrEmpty(objModel.objCustomer.Remarks) ? "" : objModel.objCustomer.Remarks;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.InwardFor = objModel.objCustomer.UserDetails.PartyCode;
                                objDTBillData.InwardForName = objModel.objCustomer.UserDetails.PartyName;
                                objDTBillData.Status = "P";
                                objDTBillData.RecTimeStamp = DateTime.Now;
                                objDTBillData.UserId = objModel.objCustomer.UserDetails.UserId;
                                objDTBillData.UserName = objModel.objCustomer.UserDetails.UserName;
                                objDTBillData.Version = version;
                                objDTBillData.DUserId = 0;
                                objDTBillData.DReason = "";
                                objDTBillData.DRecTimeStamp = DateTime.Now;
                                objDTBillData.OrdQty = 0;
                                objDTBillData.ShortQty = 0;
                                objDTBillData.DemQty = 0;
                                objDTBillData.ExpQty = 0;
                                objDTBillData.RemQty = 0;
                                objDTBillData.TotalOrdQty = 0;
                                objDTBillData.TotalShortQty = 0;
                                objDTBillData.TotalDemQty = 0;
                                objDTBillData.TotalExpQty = 0;
                                objDTBillData.TotalRemQty = 0;
                                objDTBillData.OrderNo = "";
                                objDTBillData.OrderBy = "";
                                objDTBillData.CourierId = 0;
                                objDTBillData.CourierName = "";
                                objDTBillData.TransId = "";
                                objDTBillData.TransName = "";
                                objDTBillData.LRNo = "";
                                objDTBillData.LRDate = DateTime.Now;
                                objDTBillData.UID = string.IsNullOrEmpty(objModel.objProduct.UID) ? "" : objModel.objProduct.UID;
                                objDTBillData.FreightAmt = 0;
                                objDTBillData.OtherCharges = 0;




                                objDTBillData.ShortAmt = 0;
                                objDTBillData.DemAmt = 0;
                                objDTBillData.ExpAmt = 0;
                                objDTBillData.TtlDedcAmt = 0;
                                objDTBillData.TotalShortAmt = 0;
                                objDTBillData.TotalDemAmt = 0;
                                objDTBillData.TotalExpAmt = 0;
                                objDTBillData.TotalDedcAmt = 0;
                                objDTBillData.GenDN = "N";
                                objDTBillData.GenDNBy = "";
                                objDTBillData.DNNo = "";
                                objDTBillData.DNDate = null;
                                //to be asked
                                objDTBillData.BType = "U";
                                objDTBillData.MfgDate = null;
                                objDTBillData.ExpDate = null;

                                if (objModel.objCustomer.StateCode == objModel.objCustomer.UserDetails.StateCode)
                                {
                                    objDTBillData.TaxAmt = 0;
                                    objDTBillData.Tax = 0;
                                    objDTBillData.CGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.CGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.SGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.SGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.TaxType = "S";
                                    objDTBillData.TaxBase = "S";
                                }
                                else
                                {

                                    objDTBillData.TaxAmt = obj.TaxAmt ?? 0;
                                    if (obj.OldTaxAmount != 0 && obj.OldTaxAmount != obj.TaxAmt)
                                    {
                                        objDTBillData.TaxAmt = Decimal.Parse((Convert.ToDouble(objDTBillData.TaxAmt) + 0.01).ToString());
                                        objDTBillData.Amount = Decimal.Parse((Convert.ToDouble(objDTBillData.Amount) - 0.01).ToString());
                                    }
                                    objDTBillData.Tax = obj.TaxPer ?? 0;
                                    objDTBillData.CGST = 0;
                                    objDTBillData.CGSTAmt = 0;
                                    objDTBillData.SGST = 0;
                                    objDTBillData.SGSTAmt = 0;
                                    objDTBillData.TaxType = "I";
                                    objDTBillData.TaxBase = "I";
                                }





                                entity.M_InwardData.Add(objDTBillData);
                            }
                            int i = 0;
                            try
                            {
                                i = entity.SaveChanges();
                                if (i > 0)
                                {
                                    objResponse.ResponseMessage = "Saved Succesfully!";
                                    objResponse.ResponseStatus = "OK";
                                }
                                else
                                {
                                    objResponse.ResponseMessage = "Something went wrong!";
                                    objResponse.ResponseStatus = "FAILED";
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return objResponse;
        }

        public List<PartyModel> GetAllSupplierList(string LoginPartyCode, decimal LoginStateCode)
        {
            List<PartyModel> objPartyList = new List<PartyModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    bool IsAdmin = false;
                    decimal LoginGroupId = 0;
                    if (!string.IsNullOrEmpty(LoginPartyCode))
                    {
                        var result = (from r in entity.Inv_M_UserMaster where r.BranchCode == LoginPartyCode select r).FirstOrDefault();
                        if (result != null)
                        {
                            LoginGroupId = result.GroupId;
                            IsAdmin = result.IsAdmin == "Y" ? true : false;
                        }
                    }
                    if (IsAdmin)
                    {
                        objPartyList = (from r in entity.M_LedgerMaster
                                        where r.ActiveStatus == "Y" && r.GroupId == 5 && r.PartyCode != LoginPartyCode

                                        select new PartyModel
                                        {
                                            GroupId = r.GroupId,
                                            StateCode = r.StateCode,
                                            PartyCode = r.PartyCode,
                                            PartyName = r.PartyName,

                                        }
                                              ).ToList();

                    }
                    else
                    {

                        objPartyList = (from party in entity.M_LedgerMaster
                                        where party.ActiveStatus == "Y" && party.PartyCode != LoginPartyCode && ((/*party.StateCode == LoginStateCode &&*/ party.GroupId == 5) /*|| party.ParentPartyCode == LoginPartyCode*/)
                                        select new PartyModel
                                        {
                                            PartyCode = party.PartyCode,
                                            PartyName = party.PartyName,
                                            StateCode = party.StateCode,
                                            GroupId = party.GroupId,
                                        }
                                     ).ToList();
                    }

                }


            }
            catch (Exception ex)
            {

            }
            return objPartyList;
        }


        //public ReferenceModel CheckReferenceId(string CustCode)
        //{

        //    ReferenceModel objReference = new ReferenceModel();
        //    try
        //    {
        //        using(var entity=new InventoryEntities(enttConstr))
        //        {
        //            if (!string.IsNullOrEmpty(CustCode)) {
        //                M_CustomerMaster objDTCustomer = (from r in entity.M_CustomerMaster where r.CustCode == CustCode select r).FirstOrDefault();
        //                if (objDTCustomer != null)
        //                {
        //                    objReference.objresponse = new ResponseDetail();
        //                    objReference.objresponse.ResponseMessage = "Record Found!";
        //                    objReference.objresponse.ResponseStatus = "OK";
        //                    objReference.RefId = objDTCustomer.CustCode;
        //                    objReference.RefName = objDTCustomer.CustName;
        //                }
        //                else
        //                {
        //                    objReference.objresponse.ResponseMessage = "Record Not Found!";
        //                    objReference.objresponse.ResponseStatus = "FAILED";
        //                    objReference.RefId = "";
        //                    objReference.RefName = "";
        //                }
        //            }
        //            else
        //            {
        //                objReference.objresponse = new ResponseDetail();
        //                objReference.objresponse.ResponseMessage = "Something went wrong!";
        //                objReference.objresponse.ResponseStatus = "FAILED";
        //                objReference.RefId = "";
        //                objReference.RefName = "";
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //    return objReference;
        //}

        //public async Task<ResponseDetail> IssueCard(string CardNo,string IdNo, string ContactNo,string CustomerType)
        //{
        //    ResponseDetail objResponse = new ResponseDetail();
        //    string TempCustCode ="";

        //    try
        //    {
        //        using (var entity = new InventoryEntities(enttConstr))
        //        {
        //            if ((!string.IsNullOrEmpty(CardNo)) && (!string.IsNullOrEmpty(IdNo)))
        //            {
        //                if (CheckCard(CardNo))
        //                {
        //                    if (CustomerType=="New" || (CustomerType == "Existing" && CheckCustomer(IdNo)))
        //                    {
        //                        CustomerDetail objcustomer = GetCustInfo(IdNo, false);
        //                        if (CustomerType == "New" && (!string.IsNullOrEmpty(ContactNo)))
        //                        {
        //                            TempCustCode = ContactNo;
        //                        }
        //                        else
        //                        {
        //                            TempCustCode = IdNo;
        //                        }
        //                        //issue card
        //                        SqlConnection SC = new SqlConnection("Data Source=64.62.143.84;Initial Catalog=ResetAgro;Integrated Security=False;User Id=agritech;Password=tech$reset#32;");
        //                        string tableName = "TempCardGeneration";
        //                        //     string cmdText = @"IF EXISTS(SELECT * FROM ResetAgro.INFORMATION_SCHEMA.TABLES 
        //                        //WHERE TABLE_SCHEMA ='dbo' AND TABLE_NAME='" + tableName + "') SELECT 1 ELSE SELECT 0";
        //                        //     SC.Close();
        //                        //     SC.Open();
        //                        //     SqlCommand TableCheck = new SqlCommand(cmdText, SC);
        //                        //     int x = Convert.ToInt32(TableCheck.ExecuteScalar());
        //                        //     if (x != 1)
        //                        //     {

        //                        //     }
        //                        string cmdText = "Insert into TempCardGeneration Select *,Getdate() FROM M_CardGeneration WHERE UsedBy='" + IdNo + "' AND ActiveStatus='Y' ;";
        //                        SqlCommand cmd = new SqlCommand(cmdText, SC);
        //                        SC.Close();
        //                        SC.Open();
        //                        cmd.ExecuteNonQuery();
        //                        SC.Close();

        //                        M_CardGeneration ObjDTCardGeneration = (from r in entity.M_CardGeneration
        //                                                                where r.ActiveStatus == "Y" && r.UsedBy == IdNo
        //                                                                select r
        //                                            ).FirstOrDefault();
        //                        ObjDTCardGeneration.ActiveStatus = "N";
        //                        int i = entity.SaveChanges();
        //                        if (i > 0)
        //                        {
        //                            M_CardGeneration objDT1CardGeneration = (from r in entity.M_CardGeneration
        //                                                                     where r.ActiveStatus == "Y" && r.CardNO == CardNo && (r.UsedBy == null || r.UsedBy == "")
        //                                                                     select r
        //                                                                   ).FirstOrDefault();
        //                            objDT1CardGeneration.UsedBy = IdNo;
        //                            objDT1CardGeneration.UsedDate = DateTime.Now;
        //                            i = 0;
        //                            i = entity.SaveChanges();
        //                            if (i > 0)
        //                            {
        //                                var result = (from r in entity.M_CompanyMaster where r.ActiveStatus == "Y" select r).FirstOrDefault();
        //                                if (result != null)
        //                                {
        //                                    string IssueSms = "Dear Customer,Card has been successfully issued to " + IdNo + " .Regards " + result.CompName;
        //                                    var IsSendIssue = (from r in entity.M_ConfigMaster select r.SendSMSOnIssue).FirstOrDefault();
        //                                    if (IsSendIssue == "Y")
        //                                    {
        //                                        bool IsSuccess = await Task.Run(() => Program.SendSMS(result.smsUserNm, result.smPass, result.smsSenderId, TempCustCode, IssueSms));
        //                                        if (IsSuccess)
        //                                        {
        //                                            objResponse.ResponseStatus = "OK";
        //                                            objResponse.ResponseMessage = "Congratulations, Card Issued Successfully!";
        //                                        }
        //                                        else
        //                                        {
        //                                            objResponse.ResponseStatus = "FAILED";
        //                                            objResponse.ResponseMessage = "Sorry, Something went wrong!";
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        objResponse.ResponseStatus = "FAILED";
        //                        objResponse.ResponseMessage = "Invalid Customer ID!";
        //                    }
        //                }
        //                else
        //                {
        //                    objResponse.ResponseStatus = "FAILED";
        //                    objResponse.ResponseMessage = "Invalid Card No.!";
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //    return objResponse;
        //}



        //public bool CheckCard(string CardNo)
        //{
        //    try
        //    {
        //        using(var entity=new InventoryEntities(enttConstr))
        //        {
        //            if (!string.IsNullOrEmpty(CardNo))
        //            {
        //                var result = (from r in entity.M_CardGeneration
        //                              where r.ActiveStatus == "Y" && r.CardNO == CardNo
        //                              select r
        //                            ).FirstOrDefault();
        //                if (result != null)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //    return false;
        //}

        //public bool CheckCustomer(string IdNo)
        //{
        //    try
        //    {
        //        using (var entity = new InventoryEntities(enttConstr))
        //        {
        //            if (!string.IsNullOrEmpty(IdNo))
        //            {
        //                var result = (from r in entity.M_CustomerMaster
        //                              where r.ActiveStatus == "Y" && r.CustCode == IdNo
        //                              select r
        //                            ).FirstOrDefault();
        //                if (result != null)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return false;
        //}

        public async Task<ResponseDetail> SendOTP(string MobileNo, string TotalBillAmount)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                string OTPGenerated = "";
                System.Text.StringBuilder builder = new System.Text.StringBuilder(5);
                string numberAsString = "";
                //int numberAsNumber = 0;
                for (var i = 0; i < 5; i++)
                {
                    builder.Append(_numbers[random.Next(0, _numbers.Length)]);
                }

                OTPGenerated = builder.ToString();

                string message = "Your OTP is " + OTPGenerated + " for Repurchase Bill with Amount " + TotalBillAmount;
                bool IsSuccess = await Task.Run(() => Program.SendSMSOTP("vintage352487", "9891303393", "vroots", message, MobileNo));
                if (IsSuccess)
                {
                    objResponse.ResponseStatus = "OK";
                    objResponse.ResponseMessage = "OTP has send Successfully!";
                    objResponse.GeneratedOTP = OTPGenerated;
                }
                else
                {
                    objResponse.ResponseStatus = "FAILED";
                    objResponse.ResponseMessage = "Sorry, Something went wrong!";
                    objResponse.GeneratedOTP = "";
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = "Sorry, Something went wrong!";
                objResponse.GeneratedOTP = "";
            }
            return objResponse;
        }

        public async Task<ResponseDetail> SendLoginOTP(string MobileNo, string MailTo, int UserID)
        {
            ResponseDetail objResponse = new ResponseDetail();
            var objLoginOTP = new SendLoginSM();
            string message = string.Empty;
            try
            {
                objLoginOTP = getOTP(UserID);
                if (objLoginOTP == null)
                {
                    string OTPGenerated = "";
                    System.Text.StringBuilder builder = new System.Text.StringBuilder(5);
                    string numberAsString = "";
                    //int numberAsNumber = 0;
                    for (var i = 0; i < 5; i++)
                    {
                        builder.Append(_numbers[random.Next(0, _numbers.Length)]);
                    }

                    OTPGenerated = builder.ToString();

                    message = "Your OTP to login Vision Root is:" + OTPGenerated;
                    objLoginOTP = new SendLoginSM();
                    objLoginOTP.Sms = message;
                    objLoginOTP.RecTimeStamp = DateTime.Now;
                    objLoginOTP.MobileNo = MobileNo;
                    objLoginOTP.Email = MailTo;
                    objLoginOTP.UserId = UserID;
                    objLoginOTP.OTP = OTPGenerated;
                    objLoginOTP.IsExpired = "N";
                    objLoginOTP.IsSent = "N";
                    //objLoginOTP.SentTime = new DateTime();
                    using (var entities = new InventoryEntities(enttConstr))
                    {
                        entities.SendLoginSMS.Add(objLoginOTP);
                        entities.SaveChanges();
                    }
                }
                else
                {
                    message = objLoginOTP.Sms;
                }
                bool IsSuccessMail = await Task.Run(() => Program.SendEmail(message, MailTo));
                bool IsSuccess = await Task.Run(() => Program.SendSMSOTP("vintage352487", "9891303393", "vroots", message, MobileNo));
                if (IsSuccess)
                {
                    objResponse.ResponseStatus = "OK";
                    objResponse.ResponseMessage = "OTP has send Successfully!";
                    objResponse.GeneratedOTP = objLoginOTP.OTP;
                }
                else
                {
                    objResponse.ResponseStatus = "FAILED";
                    objResponse.ResponseMessage = "Sorry, Something went wrong!";
                    objResponse.GeneratedOTP = "";
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = "Sorry, Something went wrong!";
                objResponse.GeneratedOTP = "";
            }
            return objResponse;
        }

        public List<PurchaseReport> GetPurchaseInvoice(string InvoiceNo)
        {
            List<PurchaseReport> objListPurchaseInvoice = new List<PurchaseReport>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objListPurchaseInvoice = (from r in entity.M_InwardMain
                                              where r.InwardNo == InvoiceNo
                                              join p in entity.M_InwardData
                                              on r.InwardNo equals p.InwardNo
                                              select new PurchaseReport
                                              {
                                                  RefNo = r.RefNo,
                                                  SupplierCode = r.SupplierCode,
                                                  SupplierName = r.SupplierName,
                                                  Remarks = r.Remarks,
                                                  InvoiceNo = r.InwardNo,
                                                  InvoiceDateStr = r.RecvDate,
                                                  InvoiceFor = r.InwardForName + "-" + r.InwardFor,
                                                  objproduct = new ProductModel
                                                  {
                                                      ProductCodeStr = p.ProdCode,
                                                      ProductName = p.ProdName,
                                                      Barcode = p.Barcode,
                                                      Quantity = p.Qty,
                                                      Rate = p.PRate,
                                                      Amount = p.Amount,
                                                      DiscAmt = p.TradeAmount,
                                                      DiscPer = p.TradeDiscount,

                                                      CGST = p.CGST,
                                                      CGSTAmount = p.CGSTAmt,
                                                      SGST = p.SGST,
                                                      SGSTAmount = p.SGSTAmt,
                                                      TaxAmt = p.TaxAmt,
                                                      TaxPer = p.Tax,
                                                      TotalAmount = p.TotalAmt,
                                                      Roundoff = p.RndOff,
                                                      TaxType = p.TaxType
                                                  }
                                                  ,
                                                  TotalTradeDisc = r.TotalTradeDiscount.ToString(),
                                                  TotalTaxAmt = r.TotalTaxAmt.ToString(),
                                                  CashDiscount = r.TotalCashDiscount.ToString(),
                                                  NetPayable = r.NetPayable.ToString(),
                                                  RndOff = r.RndOff.ToString(),
                                                  TotalAmount = r.TotalAmt.ToString()
                                              }
                                            ).ToList();

                    DateTime CurrentDate = new DateTime(2017, 07, 01);
                    bool isNewBill = false;
                    if (objListPurchaseInvoice.Count > 0)
                    {
                        if (objListPurchaseInvoice[0].InvoiceDateStr.Date >= CurrentDate.Date)
                        {
                            isNewBill = true;

                        }
                        else
                        {
                            isNewBill = false;
                        }
                        objListPurchaseInvoice[0].IsNewBill = isNewBill;
                        objListPurchaseInvoice[0].NetAmount = (decimal.Parse(objListPurchaseInvoice[0].TotalAmount) + decimal.Parse(objListPurchaseInvoice[0].TotalTaxAmt)).ToString();
                        objListPurchaseInvoice[0].NetPayable = (decimal.Parse(objListPurchaseInvoice[0].NetAmount) + decimal.Parse(objListPurchaseInvoice[0].RndOff)).ToString();
                        //var result = (from r in entity.M_CompanyMaster where r.ActiveStatus == "Y" select r).FirstOrDefault();
                        //if (result != null)
                        //{
                        string SoldBy = objListPurchaseInvoice[0].InvoiceFor;
                        var SoldByCode = SoldBy.Split('-');
                        var SoldByCodeStr = SoldByCode[1];
                        var resultDetails = (from r in entity.M_LedgerMaster where r.PartyCode == SoldByCodeStr select r).FirstOrDefault();
                        if (resultDetails != null)
                        {
                            objListPurchaseInvoice[0].GSTIN = resultDetails.TinNo;
                            objListPurchaseInvoice[0].SoldByName = resultDetails.PartyName;
                            objListPurchaseInvoice[0].SoldByAddress = resultDetails.Address1;
                            objListPurchaseInvoice[0].SoldByCity = resultDetails.CityName;
                        }

                        //objListPurchaseInvoice[0].CompanyName = result.CompName;
                        //objListPurchaseInvoice[0].CompanyAdd = result.CompAdd;
                        //}
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return objListPurchaseInvoice;
        }

        public decimal GetPartyWalletBalance(string LoginPartyCode)
        {
            decimal WalletBalance = 0;
            try
            {
                if (!string.IsNullOrEmpty(LoginPartyCode))
                {
                    using (var entity = new InventoryEntities(enttConstr))
                    {
                        string Sql = "";
                        string InvConnectionString = InvConstr;
                        string AppConnectionString = AppConstr;
                        string idno = "";
                        SqlConnection Sc = new SqlConnection(InvConnectionString);

                        SqlCommand cmd = new SqlCommand();
                        Sql = "Select CrAmt-DrAmt as AcBalance FROM (Select ISNULL(SUM(Amount),0) as CrAmt FROM TrnVoucher WHERE Vtype='R' AND Crto='" + LoginPartyCode + "') a," +
                       "(Select ISNULL(SUM(Amount),0) as DrAmt FROM TrnVoucher WHERE Vtype='R' AND Drto='" + LoginPartyCode + "') b";
                        // Sql = "Select UserPartyCode FROM M_LedgerMaster WHERE PartyCode='" + LoginPartyCode + "';";
                        cmd.CommandText = Sql;
                        cmd.Connection = Sc;
                        Sc.Close();
                        Sc.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WalletBalance = reader["AcBalance"] != null ? Convert.ToDecimal(reader["AcBalance"].ToString()) : 0;
                                //  idno = reader["UserPartyCode"] != null ?reader["UserPartyCode"].ToString() : "";
                            }
                        }
                        Sc.Close();
                        //Sql = "Select * FROM ufnGetBalance('" + idno + "','S');";
                        //cmd.CommandText = Sql;
                        //SqlConnection Sc1 = new SqlConnection(AppConnectionString);
                        //cmd.Connection = Sc1; 
                        //Sc1.Open();
                        //using (SqlDataReader reader = cmd.ExecuteReader())
                        //{
                        //    while (reader.Read())
                        //    {
                        //        WalletBalance = reader["Balance"] != null ? Convert.ToDecimal( reader["Balance"].ToString()) :0;
                        //    }
                        //}
                        //Sc1.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return WalletBalance;
        }

        public List<PartyModel> GetOrderToParties(string LoginPartyCode)
        {
            List<PartyModel> objParty = new List<PartyModel>();
            try
            {
                using (var db = new InventoryEntities(enttConstr))
                {
                    var loginPartydetail = (from s in db.M_LedgerMaster where s.PartyCode == LoginPartyCode select s).FirstOrDefault();
                    objParty = (from s in db.M_LedgerMaster
                                where s.ActiveStatus == "Y" && ((s.GroupId < loginPartydetail.GroupId && s.StateCode == loginPartydetail.StateCode) || s.GroupId == 0)
                                select new PartyModel { PartyCode = s.PartyCode, PartyName = s.PartyName, StateCode = s.StateCode, GroupId = s.GroupId }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return objParty;
        }

        public string GetOrderNo(string LoginPartyCode)
        {
            string OrderNo = "ORD/";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    var maxOrderNo = (from r in entity.TrnPartyOrderMains
                                      where r.OrderBy == LoginPartyCode
                                      select r.SOrderNo
                                    ).DefaultIfEmpty(10000).DefaultIfEmpty(0).Max();
                    maxOrderNo = maxOrderNo + 1;
                    OrderNo = OrderNo + LoginPartyCode + "/" + maxOrderNo;
                }
            }
            catch (Exception ex)
            {

            }
            return OrderNo;
        }

        public ResponseDetail SavePartyOrderDetails(PartyOrderModel objPartyOrderModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            try
            {
                decimal WalletBalance = 0;
                decimal? SessId = 0;
                decimal? FsessId = 0;
                string version = "";
                string billPrefix = "";
                DateTime BillDate = DateTime.Now.Date;
                //Cmnted on 01Sep18
                //if (!string.IsNullOrEmpty(objPartyOrderModel.OrderDateStr))
                //{
                //    var SplitDate = objPartyOrderModel.OrderDateStr.Split('-');
                //    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                //    BillDate = Convert.ToDateTime(NewDate);
                //    BillDate = BillDate.Date;
                //}
                TrnPayModeDetail objDtPayModeDetail = new TrnPayModeDetail();
                List<string> Paymode = new List<string>();
                List<string> PayPrefix = new List<string>();
                List<TrnPayModeDetail> objDTListPayMode = new List<TrnPayModeDetail>();
                WalletBalance = GetPartyWalletBalance(objPartyOrderModel.OrderBy);

                string InvConnectionString = InvConstr;
                string AppConnectionString = AppConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                SqlConnection SC1 = new SqlConnection(InvConnectionString);

                string db = dbName;
                string dbInv = invDbName;
                string query = "Select Max(SessID) as MaxSessId from M_SessnMaster";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SessId = decimal.Parse(reader["MaxSessId"].ToString());
                    }
                }
                using (var entity = new InventoryEntities(enttConstr))
                {
                    bool IsWalletEntry = false;
                    billPrefix = (from result in entity.M_ConfigMaster select result.BillPrefix).FirstOrDefault();
                    version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();
                    if (objPartyOrderModel != null)
                    {
                        if (objPartyOrderModel.objProduct.PayDetails != null)
                        {
                            if (objPartyOrderModel.objProduct.PayDetails.PayMode == "BD")
                            {
                                EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.BankDeposit;
                                string value = EnumPayModes.GetEnumDescription(enumVar);
                                PayPrefix.Add(value);
                                objPartyOrderModel.objProduct.PayDetails.PayMode = "Bank Deposit";
                                objPartyOrderModel.objProduct.PayDetails.CHBankName = objPartyOrderModel.objProduct.PayDetails.BDBankName;
                                objPartyOrderModel.objProduct.PayDetails.ChequeNo = objPartyOrderModel.objProduct.PayDetails.AccNo;
                                if (!string.IsNullOrEmpty(objPartyOrderModel.objProduct.PayDetails.ChequeDateStr))
                                {
                                    var SplitDate = objPartyOrderModel.objProduct.PayDetails.ChequeDateStr.Split('-');
                                    string NewDate = (SplitDate[1].Length == 1 ? "0" + SplitDate[1] : SplitDate[1]) + "/" + (SplitDate[0].Length == 1 ? "0" + SplitDate[0] : SplitDate[0]) + "/" + SplitDate[2];
                                    objPartyOrderModel.objProduct.PayDetails.ChequeDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                    objPartyOrderModel.objProduct.PayDetails.ChequeDate = objPartyOrderModel.objProduct.PayDetails.ChequeDate.Date;
                                }
                                else
                                {
                                    objPartyOrderModel.objProduct.PayDetails.ChequeDate = DateTime.Now.Date;
                                }
                                objPartyOrderModel.objProduct.PayDetails.AmountByCheque = objPartyOrderModel.objProduct.PayDetails.AmountByBD;
                                //objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByBD, BankCode = 0, ChqDDDate = null, ChqDDNo = "", CardNo = "", Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.BDBankName, AcNo = objModel.objProduct.PayDetails.AccNo, IFSCode = objModel.objProduct.PayDetails.IFSCCode, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });


                            }
                            if (objPartyOrderModel.objProduct.PayDetails.PayMode == "CC")
                            {
                                EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Card;
                                string value = EnumPayModes.GetEnumDescription(enumVar);
                                PayPrefix.Add(value);
                                objPartyOrderModel.objProduct.PayDetails.PayMode = objPartyOrderModel.objProduct.PayDetails.CardType;
                                objPartyOrderModel.objProduct.PayDetails.CHBankName = "";
                                objPartyOrderModel.objProduct.PayDetails.ChequeNo = objPartyOrderModel.objProduct.PayDetails.CardNo;
                                if (!string.IsNullOrEmpty(objPartyOrderModel.objProduct.PayDetails.ChequeDateStr))
                                {
                                    var SplitDate = objPartyOrderModel.objProduct.PayDetails.ChequeDateStr.Split('-');
                                    string NewDate = (SplitDate[1].Length == 1 ? "0" + SplitDate[1] : SplitDate[1]) + "/" + (SplitDate[0].Length == 1 ? "0" + SplitDate[0] : SplitDate[0]) + "/" + SplitDate[2];
                                    objPartyOrderModel.objProduct.PayDetails.ChequeDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                    objPartyOrderModel.objProduct.PayDetails.ChequeDate = objPartyOrderModel.objProduct.PayDetails.ChequeDate.Date;
                                }
                                else
                                {
                                    objPartyOrderModel.objProduct.PayDetails.ChequeDate = DateTime.Now.Date;
                                }
                                objPartyOrderModel.objProduct.PayDetails.AmountByCheque = objPartyOrderModel.objProduct.PayDetails.AmountByCard;
                                //objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, AcNo = "", IFSCode = "", BankCode = 0, Narration = "", BankName = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, Amount = objModel.objProduct.PayDetails.AmountByCard, CardNo = objModel.objProduct.PayDetails.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                            }
                            if (objPartyOrderModel.objProduct.PayDetails.PayMode == "Q")
                            {
                                EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cheque;
                                string value = EnumPayModes.GetEnumDescription(enumVar);
                                PayPrefix.Add(value);
                                objPartyOrderModel.objProduct.PayDetails.PayMode = "Cheque";
                                objPartyOrderModel.objProduct.PayDetails.CHBankName = objPartyOrderModel.objProduct.PayDetails.BDBankName;
                                objPartyOrderModel.objProduct.PayDetails.ChequeNo = objPartyOrderModel.objProduct.PayDetails.ChequeNo;
                                if (!string.IsNullOrEmpty(objPartyOrderModel.objProduct.PayDetails.ChequeDateStr))
                                {
                                    var SplitDate = objPartyOrderModel.objProduct.PayDetails.ChequeDateStr.Split('-');
                                    string NewDate = (SplitDate[1].Length == 1 ? "0" + SplitDate[1] : SplitDate[1]) + "/" + (SplitDate[0].Length == 1 ? "0" + SplitDate[0] : SplitDate[0]) + "/" + SplitDate[2];
                                    objPartyOrderModel.objProduct.PayDetails.ChequeDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                    objPartyOrderModel.objProduct.PayDetails.ChequeDate = objPartyOrderModel.objProduct.PayDetails.ChequeDate.Date;
                                }
                                else
                                {
                                    objPartyOrderModel.objProduct.PayDetails.ChequeDate = DateTime.Now.Date;
                                }
                                objPartyOrderModel.objProduct.PayDetails.AmountByCheque = objPartyOrderModel.objProduct.PayDetails.AmountByCheque;
                                //objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByCheque, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.CHBankName, ChqDDNo = objModel.objProduct.PayDetails.ChequeNo, ChqDDDate = objModel.objProduct.PayDetails.ChequeDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                            }
                            if (objPartyOrderModel.objProduct.PayDetails.PayMode == "NE")
                            {
                                EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cheque;
                                string value = EnumPayModes.GetEnumDescription(enumVar);
                                PayPrefix.Add(value);
                                objPartyOrderModel.objProduct.PayDetails.PayMode = "NEFT";
                                objPartyOrderModel.objProduct.PayDetails.CHBankName = objPartyOrderModel.objProduct.PayDetails.BDBankName;
                                objPartyOrderModel.objProduct.PayDetails.ChequeNo = objPartyOrderModel.objProduct.PayDetails.ChequeNo;
                                objPartyOrderModel.objProduct.PayDetails.ChequeDate = DateTime.Now.Date;
                                objPartyOrderModel.objProduct.PayDetails.AmountByCheque = objPartyOrderModel.objProduct.PayDetails.AmountByCheque;
                                //objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByCheque, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.CHBankName, ChqDDNo = objModel.objProduct.PayDetails.ChequeNo, ChqDDDate = objModel.objProduct.PayDetails.ChequeDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                            }
                            if (objPartyOrderModel.objProduct.PayDetails.IsD)
                            {
                                EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.DD;
                                string value = EnumPayModes.GetEnumDescription(enumVar);
                                PayPrefix.Add(value);
                                objPartyOrderModel.objProduct.PayDetails.PayMode = "Bank Deposit";
                                objPartyOrderModel.objProduct.PayDetails.CHBankName = objPartyOrderModel.objProduct.PayDetails.DDBankName;
                                objPartyOrderModel.objProduct.PayDetails.ChequeNo = objPartyOrderModel.objProduct.PayDetails.DDNo;
                                objPartyOrderModel.objProduct.PayDetails.ChequeDate = DateTime.Now.Date;
                                objPartyOrderModel.objProduct.PayDetails.AmountByCheque = objPartyOrderModel.objProduct.PayDetails.AmountByDD;
                                //objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByDD, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, Narration = "", DUserId = 0, DRecTimeStamp = null, BankName = objModel.objProduct.PayDetails.DDBankName, ChqDDNo = objModel.objProduct.PayDetails.DDNo, ChqDDDate = objModel.objProduct.PayDetails.DDDate, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                            }
                            if (objPartyOrderModel.objProduct.PayDetails.IsT)
                            {
                                EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Credit;
                                string value = EnumPayModes.GetEnumDescription(enumVar);
                                PayPrefix.Add(value);
                                objPartyOrderModel.objProduct.PayDetails.PayMode = "Credit";
                                objPartyOrderModel.objProduct.PayDetails.CHBankName = "";
                                objPartyOrderModel.objProduct.PayDetails.ChequeNo = "0";
                                objPartyOrderModel.objProduct.PayDetails.ChequeDate = DateTime.Now.Date;
                                objPartyOrderModel.objProduct.PayDetails.AmountByCheque = objPartyOrderModel.objProduct.PayDetails.AmountByCredit;
                                //objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, BankName = "", Amount = objModel.objProduct.PayDetails.AmountByCredit, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, DUserId = 0, DRecTimeStamp = null, ChqDDDate = null, ChqDDNo = "", Narration = objModel.objProduct.PayDetails.Narration, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                            }
                            if (objPartyOrderModel.objProduct.PayDetails.IsV)
                            {
                                EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Voucher;
                                string value = EnumPayModes.GetEnumDescription(enumVar);
                                PayPrefix.Add(value);
                                objPartyOrderModel.objProduct.PayDetails.PayMode = "Voucher";
                                objPartyOrderModel.objProduct.PayDetails.CHBankName = "";
                                objPartyOrderModel.objProduct.PayDetails.ChequeNo = "0";
                                objPartyOrderModel.objProduct.PayDetails.ChequeDate = DateTime.Now.Date;
                                objPartyOrderModel.objProduct.PayDetails.AmountByCheque = objPartyOrderModel.objProduct.PayDetails.AmountByVoucher;
                                //objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByVoucher, CardNo = "", AcNo = "", IFSCode = "", BankCode = 0, DUserId = 0, DRecTimeStamp = null, ChqDDDate = null, ChqDDNo = "", Narration = "", BankName = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                            }
                            if (objPartyOrderModel.objProduct.PayDetails.IsW)
                            {

                                objPartyOrderModel.objProduct.PayDetails.ChequeDate = DateTime.Now.Date;


                                if (WalletBalance >= objPartyOrderModel.objProduct.PayDetails.AmountByWallet)
                                {

                                    // var maxVocuherNo = (from r in entity.TrnVouchers
                                    //                   select r.VoucherNo).DefaultIfEmpty(1000).DefaultIfEmpty(0).Max();
                                    // maxVocuherNo = maxVocuherNo + 1;

                                    // TrnVoucher objVoucher = new TrnVoucher();
                                    // objVoucher.VoucherNo = maxVocuherNo;
                                    // objVoucher.VoucherDate = DateTime.Now.Date;
                                    // objVoucher.DrTo = objPartyOrderModel.OrderBy;
                                    // objVoucher.CrTo = objPartyOrderModel.OrderTo;
                                    // objVoucher.Amount = decimal.Parse(objPartyOrderModel.objProduct.PayDetails.AmountByWallet.ToString());
                                    // objVoucher.Narration = "Order " + objPartyOrderModel.OrderNo + " generated for product.";
                                    // objVoucher.RefNo = objPartyOrderModel.OrderNo;
                                    // objVoucher.VType = "R";
                                    // objVoucher.BType = "O";
                                    // objVoucher.AccDocType = "Order Generated";
                                    // objVoucher.SessID = Convert.ToInt32( SessId??0);
                                    // objVoucher.FSessId = Convert.ToInt32(FsessId ?? 1) ;
                                    // entity.TrnVouchers.Add(objVoucher);
                                    //var i = entity.SaveChanges();
                                    if (SC1.State == ConnectionState.Closed)
                                        SC1.Open();

                                    // query = "INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,CrTo,Amount,Narration,RefNo,AcType,VType,SessID,WSessID)" +
                                    //" Select ISNULL(Max(VoucherNo),0)+1 ,Cast(Getdate() as Date),(Select UserPartyCode FROM "+ dbInv + "..M_LedgerMaster WHERE PartyCode='"+ objPartyOrderModel.OrderBy + "'),0,'" + decimal.Parse(objPartyOrderModel.objProduct.PayDetails.AmountByWallet.ToString()) + "','Order " + (objPartyOrderModel.OrderNo).Trim() + " generated for product.','" + (objPartyOrderModel.OrderNo).Trim() + "','S','D',Convert(varchar,Getdate(),112),'" + SessId + "' FROM TrnVoucher;";
                                    query = ";INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,VType,BType,AccDocType,SessID,FSessID) " +
                                         " Select CASE WHEN Max(VoucherNo) is NULL THEN 1 ELSE Max(VoucherNo)+1 END ,Cast(Convert(varchar,Getdate(),106) as Datetime),'" + objPartyOrderModel.OrderBy + "','" + objPartyOrderModel.OrderTo + "','" + decimal.Parse(objPartyOrderModel.objProduct.PayDetails.AmountByWallet.ToString()) + "','Order " + objPartyOrderModel.OrderNo + " generated for product.','" + objPartyOrderModel.OrderNo + "','R','O','Order Generated',(Select Max(SessID) FROM " + db + "..M_SessnMaster),(Select Max(FSessID) FROM M_FiscalMaster) FROM TrnVoucher ;";

                                    cmd = new SqlCommand();
                                    cmd.CommandText = query;
                                    cmd.Connection = SC1;
                                    int i = cmd.ExecuteNonQuery();

                                    SC1.Close();
                                    if (i > 0)
                                    {
                                        EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Wallet;
                                        string value = EnumPayModes.GetEnumDescription(enumVar);
                                        PayPrefix.Add(value);
                                        //objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.TotalNetPayable, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.PayDetails.AmountByWallet, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = objModel.objCustomer.CardNo, ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                                        ////insert entry into couponsalesdetails for wallet
                                        IsWalletEntry = true;
                                    }
                                    else
                                    {
                                        objResponse.ResponseStatus = "FAILED";
                                        objResponse.ResponseMessage = "Something went wrong";
                                        return objResponse;
                                    }
                                    i = 0;
                                }
                                else
                                {
                                    objResponse.ResponseStatus = "FAILED";
                                    objResponse.ResponseMessage = "Sorry! Insufficient Wallet Balance.";
                                    return objResponse;
                                }

                            }
                            if (objPartyOrderModel.objProduct.PayDetails.IsP)
                            {
                                EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Paytm;
                                string value = EnumPayModes.GetEnumDescription(enumVar);
                                PayPrefix.Add(value);
                                objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objPartyOrderModel.objProduct.TotalNetPayable, BillDate = DateTime.Now.Date, PayPrefix = value, Amount = objPartyOrderModel.objProduct.PayDetails.AmountByPaytm, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = objPartyOrderModel.objProduct.PayDetails.PaytmTransactionId, ChqDDDate = DateTime.Now, CardNo = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objPartyOrderModel.LoginUser.UserId, Version = version, UserName = objPartyOrderModel.LoginUser.UserName, FSessId = FsessId ?? 0 });
                            }
                            //if (objPartyOrderModel.objProduct.CashAmount > 0)
                            //{
                            //    EnumPayModes.PayModes enumVar = EnumPayModes.PayModes.Cash;
                            //    string value = EnumPayModes.GetEnumDescription(enumVar);
                            //    PayPrefix.Add(value);
                            //    //objDTListPayMode.Add(new TrnPayModeDetail { BillAmt = objModel.objProduct.CashAmount, SoldBy = objModel.objCustomer.UserDetails.PartyCode, BillDate = DateTime.Now.Date, BillType = objModel.objCustomer.SelectedInvoiceType, BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo, PayPrefix = value, Amount = objModel.objProduct.TotalNetPayable, BankCode = 0, BankName = "", AcNo = "", IFSCode = "", Narration = "", DUserId = 0, DRecTimeStamp = null, ChqDDNo = "", ChqDDDate = null, CardNo = "", ActiveStatus = "Y", RecTimeStamp = DateTime.Now, UserId = objModel.objCustomer.UserDetails.UserId, Version = version, UserName = objModel.objCustomer.UserDetails.UserName, FSessId = FsessId ?? 0, SBillNo = maxSbillNo });
                            //}
                            if (PayPrefix.Count > 0)
                            {

                                Paymode = (from r in entity.M_PayModeMaster where PayPrefix.Contains(r.Prefix) select r.PayMode).ToList();
                            }

                        }
                    }
                    string SoldByCode = "";
                    List<TrnBillData> tempTableList = new List<TrnBillData>();
                    try
                    {
                        List<ProductModel> objListProductModel = new List<ProductModel>();

                        foreach (var obj in objPartyOrderModel.objListProduct)
                        {
                            objListProductModel.Add(obj);
                            TrnPartyOrderDetail objDTBillData = new TrnPartyOrderDetail();
                            objDTBillData.OrderNo = objPartyOrderModel.OrderNo;
                            objDTBillData.OrderTo = objPartyOrderModel.OrderTo;
                            objDTBillData.OrderBy = objPartyOrderModel.OrderBy;
                            var splitValues = objPartyOrderModel.OrderNo.Split('/');
                            objDTBillData.SOrderNo = decimal.Parse(splitValues[2]);
                            objDTBillData.PLNo = 0;
                            objDTBillData.PLDate = DateTime.Now;
                            objDTBillData.ProductCode = obj.ProdCode.ToString();
                            objDTBillData.ProductName = obj.ProductName;
                            objDTBillData.Qty = obj.Quantity;
                            objDTBillData.DispatchQty = 0;
                            objDTBillData.RemQty = obj.Quantity;
                            objDTBillData.Weight = 0;
                            objDTBillData.Carton = "";
                            objDTBillData.MonoCarton = "";
                            objDTBillData.MRP = obj.MRP ?? 0;
                            objDTBillData.DP = obj.DP ?? 0;
                            objDTBillData.Rate = obj.Rate ?? 0;
                            objDTBillData.Amount = obj.Amount;
                            objDTBillData.NetWeight = 0;
                            objDTBillData.DispatchAmt = 0;
                            objDTBillData.DispWeight = 0;
                            objDTBillData.ProdStatus = "O";
                            objDTBillData.PLGen = "N";
                            objDTBillData.OrdType = "O";
                            objDTBillData.Status = "P";
                            objDTBillData.CardStatus = "";
                            objDTBillData.ActiveStatus = "Y";
                            objDTBillData.Version = version;
                            objDTBillData.UserId = objPartyOrderModel.LoginUser.UserId;
                            objDTBillData.RecTimeStamp = DateTime.Now;
                            objDTBillData.PLUser = "0";
                            objDTBillData.PLUser = "";
                            objDTBillData.PLRecTimeStamp = DateTime.Now;
                            objDTBillData.Transporter = "";
                            objDTBillData.LRNo = "";
                            objDTBillData.LRDate = DateTime.Now;
                            objDTBillData.Fld1 = "";
                            objDTBillData.Fld2 = "";
                            objDTBillData.Fld3 = "";
                            //objDTBillData.BatchNo = obj.BatchNo;
                            //objDTBillData.Barcode = obj.Barcode;
                            objDTBillData.BatchNo = "";
                            objDTBillData.Barcode = "";
                            objDTBillData.PLQty = 0;
                            objDTBillData.PLDispQty = 0;
                            objDTBillData.PLRemQty = 0;
                            objDTBillData.PLStatus = objPartyOrderModel.PLStatus;
                            objDTBillData.MID = 0;
                            objDTBillData.DiscPer = obj.DiscPer ?? 0;
                            objDTBillData.Discount = obj.DiscAmt ?? 0;
                            objDTBillData.FSessId = FsessId ?? 0;
                            objDTBillData.IsKit = "N";
                            objDTBillData.ProdType = "P";
                            objDTBillData.BV = obj.BV ?? 0;
                            objDTBillData.BVValue = obj.BVValue ?? 0;
                            objDTBillData.RP = obj.RP ?? 0;
                            objDTBillData.RPValue = obj.RPValue ?? 0;
                            objDTBillData.OfferUId = 0;
                            objDTBillData.VAT = 0;
                            objDTBillData.TaxAmount = obj.TaxAmt ?? 0;
                            objDTBillData.Tax = obj.TaxPer ?? 0;
                            //objDTBillData.CGST = obj.TaxPer / 2 ?? 0;
                            //objDTBillData.CGSTAmt = obj.TaxAmt / 2 ?? 0;
                            //objDTBillData.SGST = obj.TaxPer / 2 ?? 0;
                            //objDTBillData.SGSTAmt = obj.TaxAmt / 2 ?? 0;
                            objDTBillData.TaxType = "I";
                            entity.TrnPartyOrderDetails.Add(objDTBillData);
                        }
                        int i = 0;
                        try
                        {
                            i = entity.SaveChanges();
                            if (i == objPartyOrderModel.objListProduct.Count())
                            {
                                TrnPartyOrderMain objDTBillMain = new TrnPartyOrderMain();
                                objDTBillMain.OrderBy = objPartyOrderModel.OrderBy;
                                objDTBillMain.OrderTo = objPartyOrderModel.OrderTo;
                                objDTBillMain.OrderDate = BillDate.Date;
                                objDTBillMain.GroupId = objPartyOrderModel.LoginUser.GroupId;
                                objDTBillMain.PGroupId = 0;
                                var splitValues = objPartyOrderModel.OrderNo.Split('/');
                                objDTBillMain.SOrderNo = decimal.Parse(splitValues[2]);
                                objDTBillMain.OrderNo = objPartyOrderModel.OrderNo;
                                objDTBillMain.PLNo = 0;
                                objDTBillMain.PLDate = DateTime.Now;
                                objDTBillMain.BillNo = "";
                                objDTBillMain.BillDate = BillDate.Date;
                                objDTBillMain.PartyName = objPartyOrderModel.LoginUser.PartyName;
                                objDTBillMain.RefNo = "";
                                objDTBillMain.Paymode = Paymode[0];
                                objDTBillMain.chNo = string.IsNullOrEmpty(objPartyOrderModel.objProduct.PayDetails.ChequeNo) ? 0 : decimal.Parse(objPartyOrderModel.objProduct.PayDetails.ChequeNo);
                                //objPartyOrderModel.objProduct.PayDetails.ChequeDate = DateTime.Now;

                                objDTBillMain.ChDate = objPartyOrderModel.objProduct.PayDetails.ChequeDate;
                                objDTBillMain.ChAmt = objPartyOrderModel.objProduct.PayDetails.AmountByCheque;
                                objDTBillMain.BankName = objPartyOrderModel.objProduct.PayDetails.CHBankName;
                                objDTBillMain.TotalWeight = objPartyOrderModel.objProduct.PayDetails.AmountByWallet;
                                objDTBillMain.TotalOrdQty = objPartyOrderModel.objProduct.TotalQty;
                                objDTBillMain.TotalDispQty = 0;
                                objDTBillMain.TotalRemQty = objPartyOrderModel.objProduct.TotalQty;
                                objDTBillMain.TotalAmount = objPartyOrderModel.objProduct.TotalTotalAmount;
                                objDTBillMain.TotalTaxAmt = objPartyOrderModel.objProduct.TotalTaxAmount;
                                objDTBillMain.RndOff = objPartyOrderModel.objProduct.Roundoff;
                                objDTBillMain.NetPayable = objPartyOrderModel.objProduct.TotalNetPayable;
                                objDTBillMain.LastPLDate = DateTime.Now;
                                objDTBillMain.Remarks = string.IsNullOrEmpty(objPartyOrderModel.Remarks) ? "" : objPartyOrderModel.Remarks;
                                objDTBillMain.OType = "O";
                                objDTBillMain.PLUserId = 0;
                                objDTBillMain.PLUser = "";
                                objDTBillMain.PLRecTimeStamp = DateTime.Now;
                                objDTBillMain.IsModify = "N";
                                objDTBillMain.PLStatus = "P";
                                objDTBillMain.MID = 0;
                                objDTBillMain.Status = "P";
                                objDTBillMain.ActiveStatus = "Y";
                                objDTBillMain.Version = version;
                                objDTBillMain.UserId = objPartyOrderModel.LoginUser.UserId;
                                objDTBillMain.RecTimeStamp = DateTime.Now;
                                objDTBillMain.FSessId = FsessId ?? 0;
                                objDTBillMain.UserName = objPartyOrderModel.LoginUser.UserName;
                                objDTBillMain.IsConfirm = "N";
                                objDTBillMain.ConfDate = DateTime.Now;
                                objDTBillMain.ConfUserID = 0;
                                objDTBillMain.TotalDiscount = objPartyOrderModel.objProduct.TotalDiscount;
                                objDTBillMain.BankCode = objPartyOrderModel.objProduct.PayDetails.BankCode;
                                objDTBillMain.TotalBV = objPartyOrderModel.objProduct.TotalBV;
                                objDTBillMain.TotalRP = objPartyOrderModel.objProduct.TotalRP;
                                objDTBillMain.UID = objPartyOrderModel.objProduct.UID;
                                objDTBillMain.OrderAmount = objPartyOrderModel.objProduct.TotalNetPayable;

                                entity.TrnPartyOrderMains.Add(objDTBillMain);
                                i = 0;
                                i = entity.SaveChanges();

                                if (i > 0)
                                {
                                    if (SC1.State == ConnectionState.Closed)
                                        SC1.Open();
                                    query = "Exec OrderKitProducts '" + (objPartyOrderModel.OrderNo).Trim() + "';";
                                    cmd = new SqlCommand();
                                    cmd.CommandText = query;
                                    cmd.Connection = SC1;
                                    i = cmd.ExecuteNonQuery();
                                    SC1.Close();
                                    if (objPartyOrderModel.Processwishlist == true)
                                    {
                                        using (var entities = new InventoryEntities(enttConstr))
                                        {
                                            var wishlistProducts = (from r in entities.StockOrderWishlists where r.OrderBy == objPartyOrderModel.OrderBy && r.ActiveStatus == "Y" select r).ToList();
                                            foreach (var product in wishlistProducts)
                                            {
                                                product.ActiveStatus = "N";
                                            }
                                            entities.SaveChanges();
                                        }
                                    }
                                    objResponse.ResponseMessage = "Saved Successfully!";
                                    objResponse.ResponseStatus = "OK";
                                }
                                else
                                {
                                    objResponse.ResponseMessage = "Something went wrong!";
                                    objResponse.ResponseStatus = "FAILED";
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }

            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public List<PartyOrderModel> GetOrderList(string Orderby, string OrderTo, string Status)
        {
            List<PartyOrderModel> objPartyOrderModel = new List<PartyOrderModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objPartyOrderModel = (from r in entity.TrnPartyOrderMains
                                          where r.ActiveStatus == "Y"
                                          from l in entity.M_LedgerMaster
                                          where r.OrderBy == l.PartyCode
                                          select new PartyOrderModel
                                          {
                                              OrderNo = r.OrderNo,
                                              PartyCode = r.OrderBy,
                                              PartyName = l.PartyName,
                                              OrderDate = r.OrderDate,
                                              OrderAmt = r.OrderAmount,
                                              ChNo = r.chNo.ToString() ?? "0",
                                              ChDate = r.ChDate ?? DateTime.Now,
                                              ChAmt = r.ChAmt ?? 0,
                                              BankName = r.BankName,
                                              WalletAmt = r.TotalWeight,
                                              OrderBy = r.OrderBy,
                                              OrderTo = r.OrderTo,
                                              TotalOrdQty = r.TotalOrdQty,
                                              TotalDispQty = r.TotalDispQty,
                                              DispStatus = r.Status,
                                              PLStatus = r.PLStatus,
                                              SaveTo = r.PLUser
                                          }
                                        ).ToList();
                    if (Orderby.ToUpper() != "ALL")
                        objPartyOrderModel = objPartyOrderModel.Where(m => m.OrderBy == Orderby).ToList();
                    if (OrderTo.ToUpper() != "ALL")
                        objPartyOrderModel = objPartyOrderModel.Where(m => m.OrderTo == OrderTo).ToList();
                    if (Status.ToUpper() != "A")
                        objPartyOrderModel = objPartyOrderModel.Where(m => m.DispStatus == Status).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objPartyOrderModel;
        }
        public List<ProductModel> GetOrderProductList(string OrderNo, string OrderBy, bool isPending)
        {
            List<ProductModel> objOrderProductModel = new List<ProductModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objOrderProductModel = (from r in entity.TrnPartyOrderDetails
                                            where r.ActiveStatus == "Y" && r.OrderNo == OrderNo && r.OrderBy == OrderBy //&& r.Status == "P"// && r.ProdType=="P"
                                            from p in entity.TrnPartyOrderMains
                                            where p.OrderNo == OrderNo
                                            select new ProductModel
                                            {
                                                ProductName = r.ProductName,
                                                Barcode = r.Barcode,
                                                BatchNo = r.BatchNo,
                                                DP = r.DP,
                                                RP = r.RP,
                                                DiscPer = r.DiscPer,
                                                DiscAmt = r.Discount,
                                                ProductCodeStr = r.ProductCode,
                                                TaxPer = r.Tax,
                                                TaxAmt = r.TaxAmount,
                                                MRP = r.MRP,
                                                BV = r.BV,
                                                PV = 0,
                                                CV = 0,
                                                Rate = r.Rate,
                                                Amount = r.Amount,
                                                TotalNetPayable = p.NetPayable,
                                                OrderedOty = isPending == false ? r.Qty : r.RemQty,
                                                DispQty = r.DispatchQty,
                                                OfferUID = r.OfferUId,
                                                ProductType = r.ProdType,
                                                DispStatus = r.Status
                                            }
                                          ).ToList();
                    if (isPending)
                        objOrderProductModel = objOrderProductModel.Where(m => m.DispStatus == "P").ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objOrderProductModel;
        }

        public List<ProductModel> GetDistOrderProductList(string OrderNo)
        {
            List<ProductModel> objOrderProductModel = new List<ProductModel>();
            try
            {
                string AppConnectionString = AppConstr;
                string db = dbName;
                string dbInv = invDbName;
                SqlConnection SC = new SqlConnection(AppConnectionString);

                string query = "Select DISTINCT a.*,b.Barcode,b.BatchNo,c.VatTax as Tax FROM TrnOrderDetail a, " + dbInv + "..V#AvailProdStockBarcodes b," + dbInv + "..M_TaxMaster c WHERE ProductID=b.ProdID AND b.ProdID=c.ProdCode AND a.OrderNo='" + OrderNo + "' AND a.DispStatus<>'C' ORDER BY a.ProductName";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel objProduct = new ProductModel();
                        objProduct.ProductName = reader["ProductName"].ToString();
                        objProduct.Barcode = reader["Barcode"].ToString();
                        objProduct.BatchNo = reader["BatchNo"].ToString();
                        objProduct.DP = Convert.ToDecimal(reader["Rate"].ToString());
                        objProduct.RP = Convert.ToDecimal(reader["RP"].ToString());
                        objProduct.DiscPer = 0;
                        objProduct.DiscAmt = Convert.ToDecimal(reader["TotalDiscount"].ToString());
                        objProduct.ProductCodeStr = reader["ProductID"].ToString();
                        objProduct.TaxPer = Convert.ToDecimal(reader["Tax"].ToString());
                        objProduct.TaxAmt = 0;
                        objProduct.MRP = Convert.ToDecimal(reader["MRP"].ToString());
                        objProduct.BV = Convert.ToDecimal(reader["BV"].ToString());
                        objProduct.PV = 0;
                        objProduct.CV = 0;
                        objProduct.Rate = Convert.ToDecimal(reader["Rate"].ToString());
                        objProduct.Amount = 0;
                        objProduct.TotalNetPayable = 0;
                        objProduct.OrderedOty = Convert.ToDecimal(reader["RemQty"].ToString());
                        objProduct.DispQty = Convert.ToDecimal(reader["DispQty"].ToString());
                        objProduct.OfferUID = 0;
                        objProduct.ProductType = reader["ProdType"].ToString();
                        objOrderProductModel.Add(objProduct);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return objOrderProductModel;
        }


        public ResponseDetail SaveDispatchOrder(PartyOrderModel objPartyDispatchOrder)
        {
            ResponseDetail objResponse = new ResponseDetail();
            TrnPartyOrderDetail objDTPartyOrderDetail = new TrnPartyOrderDetail();
            TrnPartyOrderMain objDtPartyOrderMain = new TrnPartyOrderMain();
            decimal maxUserSBillNo = 0;
            decimal? SessId = 0;
            string billPrefix = "";
            decimal maxSbillNo = 0;
            decimal? FsessId = 0;
            string UserBillNo = "";
            string version = "";
            string BillNo = "";
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            try
            {
                if (objPartyDispatchOrder.OrderNo.All(char.IsNumber))
                    objPartyDispatchOrder.FType = "M";
                string AppConnectionString = AppConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                SqlConnection SC1 = new SqlConnection(InvConstr);
                string query = "Select Max(SessID) as MaxSessId from M_SessnMaster";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SessId = decimal.Parse(reader["MaxSessId"].ToString());
                    }
                }
                using (var entity = new InventoryEntities(enttConstr))
                {
                    maxSbillNo = (from result in entity.TrnBillMains select result.SBillNo).DefaultIfEmpty(0).Max();
                    maxSbillNo = maxSbillNo + 1;
                    FsessId = (from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result.FSessId).DefaultIfEmpty(0).Max();
                    ////decimal? SessId = (from result in entity.M_SessnMaster select result.SessID).Max();

                    billPrefix = (from result in entity.M_ConfigMaster select result.BillPrefix).FirstOrDefault();

                    version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();
                    if (objPartyDispatchOrder.objProduct.BillType != "S")
                    {
                        maxUserSBillNo = (from result in entity.TrnBillMains where result.FSessId == FsessId && result.SoldBy == objPartyDispatchOrder.LoginUser.PartyCode && result.BillType != "S" select result.UserSBillNo).DefaultIfEmpty(0).Max();
                        maxUserSBillNo = maxUserSBillNo + 1;
                        UserBillNo = billPrefix + "/" + objPartyDispatchOrder.LoginUser.PartyCode + "/" + maxUserSBillNo;
                    }
                    else
                    {
                        maxUserSBillNo = (from result in entity.TrnBillMains where result.FSessId == FsessId && result.SoldBy == objPartyDispatchOrder.LoginUser.PartyCode && result.BillType == "S" select result.UserSBillNo).DefaultIfEmpty(0).Max();
                        maxUserSBillNo = maxUserSBillNo + 1;
                        string strMaxUserSBillNo = maxUserSBillNo.ToString();
                        if (strMaxUserSBillNo.Count() < 3)
                        {
                            var countNum = strMaxUserSBillNo.Count();
                            var ToBeAddedDigits = 3 - countNum;
                            for (var j = 0; j < ToBeAddedDigits; j++)
                            {
                                strMaxUserSBillNo = "0" + strMaxUserSBillNo;
                            }
                        }
                        UserBillNo = billPrefix + "/ST/" + strMaxUserSBillNo;
                    }



                    DateTime BillDate = DateTime.Now.Date;

                    string SoldByCode = "";
                    List<TrnBillData> tempTableList = new List<TrnBillData>();
                    string GroupPrefix = "";
                    string BillingPartyCode = objPartyDispatchOrder.PartyCode;
                    GroupPrefix = (from p in entity.M_GroupMaster
                                   where p.GroupId == (from r in entity.M_LedgerMaster where r.PartyCode == BillingPartyCode select r.GroupId).FirstOrDefault()
                                   select p.Prefix
                                   ).FirstOrDefault();

                    try
                    {
                        List<ProductModel> objListProductModel = new List<ProductModel>();
                        //TempDistributor.objListProduct.AddRange(objModel.objListProduct);
                        foreach (var obj in objPartyDispatchOrder.objListProduct)
                        {

                            TrnBillData objDTBillData = new TrnBillData();
                            if (obj.Quantity > 0)
                            {
                                objListProductModel.Add(obj);
                                objDTBillData.SBillNo = maxSbillNo;
                                objDTBillData.FSessId = FsessId ?? 0;
                                objDTBillData.SessId = SessId ?? 0;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.BillDate = BillDate.Date;

                                objDTBillData.RefNo = "";
                                objDTBillData.RefId = 0;
                                objDTBillData.RefName = "";
                                objDTBillData.Remarks = objPartyDispatchOrder.Remarks ?? "";
                                objDTBillData.CType = objPartyDispatchOrder.FType == "M" ? "M" : GroupPrefix;
                                objDTBillData.SoldBy = objPartyDispatchOrder.LoginUser.PartyCode;
                                SoldByCode = objDTBillData.SoldBy;
                                objDTBillData.BillBy = objDTBillData.SoldBy;
                                objDTBillData.BillNo = billPrefix + "/" + objDTBillData.BillBy + "/" + maxSbillNo;
                                BillNo = objDTBillData.BillNo;
                                objDTBillData.FType = objPartyDispatchOrder.FType == "M" ? "M" : GroupPrefix;
                                objDTBillData.FCode = objPartyDispatchOrder.PartyCode;
                                objDTBillData.PartyName = objPartyDispatchOrder.PartyName;
                                objDTBillData.SupplierId = 0;
                                objDTBillData.ChDDNo = 0;
                                objDTBillData.ChDate = DateTime.Now;
                                objDTBillData.ChAmt = 0;
                                objDTBillData.BankCode = 0;
                                objDTBillData.BankName = "";
                                objDTBillData.FormNo = objPartyDispatchOrder.FType == "M" ? objPartyDispatchOrder.FormNo : 0;
                                objDTBillData.TotalTaxAmount = objPartyDispatchOrder.objProduct.TotalTaxAmount;
                                objDTBillData.TotalSTaxAmount = 0;
                                objDTBillData.TotalDiscount = objPartyDispatchOrder.objProduct.TotalDiscount;
                                objDTBillData.TotalKitBvValue = 0;
                                objDTBillData.TotalBvValue = objPartyDispatchOrder.objProduct.TotalBV;
                                objDTBillData.TotalCVValue = objPartyDispatchOrder.objProduct.TotalCV;
                                objDTBillData.TotalPVValue = objPartyDispatchOrder.objProduct.TotalPV;
                                objDTBillData.TotalRPValue = objPartyDispatchOrder.objProduct.TotalRP;

                                objDTBillData.DP = obj.DP ?? 0;
                                objDTBillData.RP = obj.RP ?? 0;
                                objDTBillData.MRP = obj.MRP ?? 0;
                                objDTBillData.CVValue = obj.CVValue ?? 0;
                                objDTBillData.CV = obj.CV ?? 0;
                                objDTBillData.PV = obj.PV ?? 0;
                                objDTBillData.BV = obj.BV ?? 0;
                                objDTBillData.BVValue = obj.BVValue ?? 0;
                                objDTBillData.PVValue = obj.PVValue ?? 0;
                                objDTBillData.RPValue = obj.RPValue ?? 0;
                                objDTBillData.Barcode = obj.Barcode.ToString();
                                objDTBillData.BatchNo = obj.BatchNo.ToString();

                                objDTBillData.DiscountPer = obj.DiscPer ?? 0;
                                objDTBillData.Discount = obj.DiscAmt ?? 0;
                                objDTBillData.ProdCommssn = obj.CommissionPer ?? 0;
                                objDTBillData.ProdCommssnAmt = obj.CommissionAmt ?? 0;
                                objDTBillData.ProductId = obj.ProdCode.ToString();
                                objDTBillData.ProductName = obj.ProductName;
                                objDTBillData.Qty = obj.Quantity;
                                objDTBillData.Rate = obj.Rate ?? 0;
                                objDTBillData.IsKitBV = "N";
                                objDTBillData.DSeries = "";
                                objDTBillData.DImported = "N";
                                objDTBillData.IMEINo = "D";
                                objDTBillData.BNo = "";
                                objDTBillData.ItemType = "N";
                                objDTBillData.JType = "Cash:" + objPartyDispatchOrder.objProduct.TotalNetPayable;
                                objDTBillData.BillTo = objPartyDispatchOrder.PartyCode;
                                objDTBillData.BillFor = objPartyDispatchOrder.PartyCode;
                                objDTBillData.IsReceive = objPartyDispatchOrder.FType == "M" ? "R" : "N";
                                objDTBillData.IsCredit = "F";
                                //objDTBillData.BillType = "R";
                                objDTBillData.BillType = objPartyDispatchOrder.FType == "M" ? "R" : objPartyDispatchOrder.objProduct.BillType ?? "V";
                                objDTBillData.ProdType = obj.ProductType;
                                objDTBillData.PaymentDtl = "Cash:" + objPartyDispatchOrder.objProduct.TotalNetPayable;

                                objDTBillData.TotalAmount = objPartyDispatchOrder.objProduct.TotalTotalAmount;
                                //tax excluding
                                objDTBillData.NetAmount = obj.Amount;
                                decimal PartyStateCode = 0;
                                if (objPartyDispatchOrder.FType == "M")
                                    PartyStateCode = GetCustInfo(objPartyDispatchOrder.PartyCode).StateCode;
                                else
                                    PartyStateCode = (from r in entity.M_LedgerMaster where r.PartyCode == objPartyDispatchOrder.PartyCode select r.StateCode).FirstOrDefault();

                                if (PartyStateCode == objPartyDispatchOrder.LoginUser.StateCode)
                                {
                                    objDTBillData.TaxAmount = 0;
                                    objDTBillData.Tax = 0;
                                    objDTBillData.CGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.CGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.SGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.SGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.TaxType = "S";
                                }
                                else
                                {

                                    objDTBillData.TaxAmount = obj.TaxAmt ?? 0;
                                    if (obj.OldTaxAmount != 0 && obj.OldTaxAmount != obj.TaxAmt)
                                    {
                                        objDTBillData.TaxAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.TaxAmount) + 0.01).ToString());
                                        objDTBillData.NetAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.NetAmount) - 0.01).ToString());
                                    }
                                    objDTBillData.Tax = obj.TaxPer ?? 0;
                                    objDTBillData.CGST = 0;
                                    objDTBillData.CGSTAmt = 0;
                                    objDTBillData.SGST = 0;
                                    objDTBillData.SGSTAmt = 0;
                                    objDTBillData.TaxType = "I";
                                }
                                objDTBillData.CashDiscPer = obj.CashDiscPer;
                                objDTBillData.CashDiscAmount = obj.CashDiscAmount;

                                objDTBillData.NetPayable = objPartyDispatchOrder.objProduct.TotalNetPayable;
                                objDTBillData.RndOff = objPartyDispatchOrder.objProduct.Roundoff;
                                objDTBillData.CardAmount = 0;
                                objDTBillData.PayMode = "Cash";
                                objDTBillData.PayPrefix = "";
                                objDTBillData.BvTransfer = "N";

                                //objDTBillData.UserSBillNo = maxSbillNo;
                                //objDTBillData.UserBillNo = billPrefix + "/" + objDTBillData.BillBy + "/" + maxSbillNo;
                                objDTBillData.UserSBillNo = maxUserSBillNo;
                                objDTBillData.UserBillNo = UserBillNo;
                                objDTBillData.DispatchStatus = objPartyDispatchOrder.FreightType != "H" ? "N" : "Y";
                                objDTBillData.LR = objPartyDispatchOrder.VehicleNo ?? ""; //21May19;
                                objDTBillData.LRDate = DateTime.Now;
                                objDTBillData.TransporterName = objPartyDispatchOrder.TransporterName ?? ""; //21May19;
                                objDTBillData.DispatchTo = "";
                                objDTBillData.FreightType = objPartyDispatchOrder.FreightType ?? "C";
                                objDTBillData.Series = "";
                                objDTBillData.Scratch = objPartyDispatchOrder.EWayBillNo ?? ""; //21May19

                                objDTBillData.Unit = 0;

                                objDTBillData.PSessId = 0;
                                objDTBillData.DcNo = "";
                                objDTBillData.Imported = "N";
                                objDTBillData.FPoint = 0;
                                objDTBillData.FPointValue = 0;
                                objDTBillData.OrdStatus = "";
                                objDTBillData.OrdQty = 0;
                                // objDTBillData.OrderType = "";
                                objDTBillData.OrderDate = DateTime.Now;
                                objDTBillData.OrderNo = objPartyDispatchOrder.OrderNo;
                                objDTBillData.RemQty = 0;
                                objDTBillData.DP1 = 0;
                                objDTBillData.DReason = "";
                                objDTBillData.DUserId = 0;
                                objDTBillData.DRecTimeStamp = DateTime.Now;
                                objDTBillData.DocWeight = 0;
                                objDTBillData.DocketNo = "";
                                objDTBillData.DocketDate = DateTime.Now;
                                //objDTBillData.UserBillNo = "";
                                //objDTBillData.UserSBillNo = 0;
                                objDTBillData.STNFormNo = "";
                                objDTBillData.StkRecv = "N";
                                objDTBillData.StkRecvDate = DateTime.Now;
                                objDTBillData.StkRecvUserId = 0;
                                objDTBillData.InTransit = "N";
                                objDTBillData.UID = string.IsNullOrEmpty(objPartyDispatchOrder.objProduct.UID) ? "" : objPartyDispatchOrder.objProduct.UID;
                                objDTBillData.OfferUID = obj.OfferUID;
                                objDTBillData.IsKit = "N";
                                objDTBillData.TotalCorton = "";
                                objDTBillData.TotalMonoCorton = "";
                                objDTBillData.SpclOfferId = Convert.ToInt32(obj.OfferUID);
                                objDTBillData.VAT = 0;
                                objDTBillData.BuyerAddress = "";
                                objDTBillData.BuyerTIN = "";

                                objDTBillData.TotalDiscount = objPartyDispatchOrder.objProduct.TotalDiscPer;
                                objDTBillData.TotalDiscountAmt = objPartyDispatchOrder.objProduct.TotalDiscount;
                                objDTBillData.VDiscountAmt = 0;
                                objDTBillData.VDiscount = 0;
                                objDTBillData.ReceiverID = "";
                                objDTBillData.ReceiverName = objPartyDispatchOrder.Station ?? ""; //21May19;
                                objDTBillData.ReceiverMNo = "";
                                objDTBillData.ReceiverIDProof = "";
                                objDTBillData.TotalFPoint = 0;
                                objDTBillData.TotalQty = objPartyDispatchOrder.objProduct.TotalQty;
                                objDTBillData.CashReward = 0;
                                objDTBillData.CommssnAmt = objPartyDispatchOrder.objProduct.TotalCommsonAmt;
                                objDTBillData.RecvAmount = 0;
                                objDTBillData.ReturnToCustAmt = 0;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.RecTimeStamp = DateTime.Now;
                                objDTBillData.UserId = objPartyDispatchOrder.LoginUser.UserId;
                                objDTBillData.UserName = objPartyDispatchOrder.LoginUser.UserName;
                                objDTBillData.DelvPlace = string.IsNullOrEmpty(objPartyDispatchOrder.objProduct.DeliveryPlace) ? "" : objPartyDispatchOrder.objProduct.DeliveryPlace;
                                objDTBillData.DelvStatus = "";
                                objDTBillData.DelvUserId = 0;
                                objDTBillData.DelvRecTimeStamp = DateTime.Now;
                                objDTBillData.Version = version;
                                objDTBillData.IDType = objPartyDispatchOrder.FType == "M" ? "D" : "";
                                objDTBillData.BranchName = "";
                                objDTBillData.CourierId = objPartyDispatchOrder.objProduct.CourierId;
                                objDTBillData.CourierName = (string.IsNullOrEmpty(objPartyDispatchOrder.objProduct.CourierName) ? "" : objPartyDispatchOrder.objProduct.CourierName);
                                objDTBillData.LocId = 0;
                                objDTBillData.LocName = "";
                                objDTBillData.DelvAddress = "";
                                objDTBillData.Pincode = "";
                                objDTBillData.OrderType = objPartyDispatchOrder.OrderType;
                                entity.TrnBillDatas.Add(objDTBillData);

                                //updating entries in trnpartyorderdetails
                                //objDTPartyOrderDetail = (from r in entity.TrnPartyOrderDetails
                                //                         where r.ProductCode == objDTBillData.ProductId && r.OrderNo == objPartyDispatchOrder.OrderNo
                                //                         select r
                                //                       ).FirstOrDefault();
                                //if (objDTPartyOrderDetail != null)
                                //{

                                //    objDTPartyOrderDetail.Status = "D";

                                //    objDTPartyOrderDetail.RemQty = objDTPartyOrderDetail.Qty - obj.Quantity;
                                //    objDTPartyOrderDetail.DispatchQty = obj.Quantity;

                                //}
                                //objDtPartyOrderMain = (from r in entity.TrnPartyOrderMains
                                //                       where r.OrderNo == objPartyDispatchOrder.OrderNo
                                //                       select r
                                //                       ).FirstOrDefault();
                                //if (objDtPartyOrderMain != null)
                                //{
                                //    objDtPartyOrderMain.Status = "D";
                                //    objDtPartyOrderMain.BillNo = UserBillNo;
                                //    objDtPartyOrderMain.BillDate = DateTime.Now.Date;

                                //}
                            }
                        }
                        int i = 0;



                        try
                        {
                            i = entity.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        string sms, mobileno;
                        if (i > 0)

                        {
                            string db = dbName;
                            string dbInv = invDbName;
                            //string CompName = System.Configuration.ConfigurationManager.AppSettings["CompName"];
                            if (objPartyDispatchOrder.FType != "M")
                            {

                                sms = "Stock of Amount " + objPartyDispatchOrder.objProduct.TotalNetPayable.ToString() + " has been dispatched against order " + objPartyDispatchOrder.OrderNo + ". info: " + CompName;
                                string Sql = "Update TrnPartyOrderDetail Set DispatchQty=a.DispQty,DispatchAmt=a.DispAmt,Discount=a.DiscountAmt";
                                Sql = Sql + " FROM (";
                                Sql = Sql + " Select a.FSessId,a.OrderNo,b.ProductId,b.ProdType,b.OfferUId,IsNull(SUM(b.Discount),0) as DiscountAmt,IsNull(SUM(b.Qty),0) as DispQty,IsNull(SUM(b.TaxAmount),0)+IsNull(SUM(b.NetAmount),0) as DispAmt";
                                Sql = Sql + " FROM TrnBillMain as a,TrnBillDetails as b Where a.FSessId=b.FSessId And a.BillNo=b.BillNo And a.OrderNo='" + objPartyDispatchOrder.OrderNo + "'";
                                Sql = Sql + " Group BY a.FSessId,a.OrderNo,b.ProductId,b.ProdType,b.OfferUId) as a,TrnPartyOrderDetail as b";
                                Sql = Sql + " Where a.OrderNo=b.OrderNo And a.ProductId=b.ProductCode And a.ProdType=b.ProdType AND a.OfferUId=b.OfferUId";
                                Sql = Sql + " ;Update TrnPartyOrderDetail Set RemQty=Qty-DispatchQty Where OrderNo='" + objPartyDispatchOrder.OrderNo + "' AND ActiveStatus='Y'";
                                Sql = Sql + " ;Update TrnPartyOrderDetail Set Status=Case When RemQty<=0 Then 'C' Else 'P' End Where OrderNo='" + objPartyDispatchOrder.OrderNo + "' AND ActiveStatus='Y'";
                                Sql = Sql + " ;Update TrnPartyOrderMain Set TotalDiscount=a.TotalDiscount,TotalAmount=a.TotalAmount,";
                                Sql = Sql + " TotalTaxAmt=a.TotalTaxAmt,RndOff=Round(a.TotalAmount+a.TotalTaxAmt,0)-Round(a.TotalAmount+a.TotalTaxAmt,2),NetPayable=Round(a.NetPayable,0)";
                                Sql = Sql + " FROM (";
                                Sql = Sql + " Select FSessId,OrderNo,IsNull(SUM(Discount),0) as TotalDiscount,IsNull(SUM(TotalQty),0) as TotalDispQty,";
                                Sql = Sql + " IsNull(SUM(Amount),0) as TotalAmount,";
                                Sql = Sql + " IsNull(SUM(TaxAmount),0)+IsNull(SUM(STaxAmount),0) as TotalTaxAmt,";
                                Sql = Sql + " IsNull(SUM(NetPayable),0) as NetPayable";
                                Sql = Sql + " FROM TrnBillMain Where OrderType='S'";
                                Sql = Sql + " Group By FSessId,OrderNo) as a,TrnPartyOrderMain as b ";
                                Sql = Sql + " Where a.OrderNo=b.OrderNo And b.OrderNo='" + objPartyDispatchOrder.OrderNo + "'";

                                Sql = Sql + ";Update TrnPartyOrderMain Set TotalDispQty=a.TotalDispQty FROM ( ";
                                Sql = Sql + "Select a.FSessId, a.OrderNo, IsNull(SUM(b.Qty), 0) as TotalDispQty FROM TrnBillMain a,TrnBillDetails b Where a.BillNo=b.BillNo AND a.OrderType = 'S' AND b.ProdType = 'P' Group By a.FSessId, a.OrderNo " +
                                    ") as a,TrnPartyOrderMain as b Where a.OrderNo = b.OrderNo And b.OrderNo = '" + objPartyDispatchOrder.OrderNo + "'";

                                Sql = Sql + " ;Update TrnPartyOrderMain Set TotalRemQty=TotalOrdQty-TotalDispQty Where OrderNo='" + objPartyDispatchOrder.OrderNo + "' AND ActiveStatus='Y'";
                                Sql = Sql + " ;Update TrnPartyOrderMain Set Status=Case When TotalRemQty<=0 Then 'C' Else 'P' End Where OrderNo='" + objPartyDispatchOrder.OrderNo + "' AND ActiveStatus='Y'";

                                Sql = Sql + ";INSERT INTO " + db + "..SendSMS(Formno,MobileNo,sms,IsSent) " +
                                " Select 0, mobileno ,'" + sms + "','N' FROM M_LedgerMaster WHERE PartyCode='" + objPartyDispatchOrder.PartyCode + "' AND ISNUMERIC(MobileNo)=1 AND LEN(Cast(MobileNo as varchar(20)))=10;";


                                if (SC1.State == ConnectionState.Closed)
                                    SC1.Open();
                                cmd = new SqlCommand();
                                cmd.CommandText = Sql;
                                cmd.Connection = SC1;
                                i = cmd.ExecuteNonQuery();
                                SC1.Close();
                                RejectFranchiseOrder(objPartyDispatchOrder.OrderNo, "Dispatched", objPartyDispatchOrder.LoginUser.UserId, false, BillNo);

                            }
                            else
                            {
                                sms = "Your order " + objPartyDispatchOrder.OrderNo + " has been dispatched and will be delivered shortly. info: " + CompName;
                                string Sql = "Update TrnOrderDetail Set DispQty=a.DispQty,DispAmt=a.DispAmt FROM ( Select a.FSessId,a.OrderNo,b.ProductId,b.ProdType,b.OfferUId,IsNull(SUM(b.Discount),0) as DiscountAmt,IsNull(SUM(b.Qty),0) as DispQty,IsNull(SUM(b.TaxAmount+b.CGSTAmt+b.SGSTAmt),0)+IsNull(SUM(b.NetAmount),0) as DispAmt FROM " + dbInv + "..TrnBillMain as a," + dbInv + "..TrnBillDetails as b Where a.FSessId=b.FSessId And a.BillNo=b.BillNo And a.FSessId=" + FsessId + " And a.OrderNo='" + objPartyDispatchOrder.OrderNo + "' Group BY a.FSessId,a.OrderNo,b.ProductId,b.ProdType,b.OfferUId) as a,TrnOrderDetail as b Where a.FSessId = b.FSessId And a.OrderNo=b.OrderNo And a.ProductId=b.ProductId ;" +
                                "Update TrnOrderDetail Set RemQty = Qty - DispQty Where FSessId =" + FsessId + " And OrderNo = '" + objPartyDispatchOrder.OrderNo + "';" +
                                "Update TrnOrderDetail Set DispStatus = Case When RemQty<= 0 Then 'C' Else 'N' End Where FSessId = " + FsessId + " And OrderNo = '" + objPartyDispatchOrder.OrderNo + "';" +
                                "Update TrnOrder Set DispatchQty = a.TotalDispQty,DispatchAmount = Round(a.NetPayable, 0) FROM (Select FSessId, OrderNo, IsNull(SUM(Discount), 0) as TotalDiscount, IsNull(SUM(TotalQty), 0) as TotalDispQty, IsNull(SUM(Amount), 0) as TotalAmount, IsNull(SUM(TaxAmount), 0) + IsNull(SUM(STaxAmount), 0) as TotalTaxAmt, IsNull(SUM(NetPayable), 0) as NetPayable FROM " + dbInv + "..TrnBillMain Group By FSessId, OrderNo) as a,TrnOrder as b  Where a.FSessId = b.FSessId And a.OrderNo = Cast(b.OrderNo as varchar(20)) And b.FSessId = " + FsessId + " And b.OrderNo = ' " + objPartyDispatchOrder.OrderNo + "';" +
                                "Update TrnOrder Set RemainQty = OrderQty - DispatchQty,DispatchDate = Getdate() Where FSessId = " + FsessId + " And OrderNo = '" + objPartyDispatchOrder.OrderNo + "';" +
                                "Update TrnOrder Set DispatchStatus = Case When RemainQty<= 0 Then 'C' Else 'N' End Where FSessId = " + FsessId + " And OrderNo =  '" + objPartyDispatchOrder.OrderNo + "'";
                                Sql = Sql + ";INSERT INTO " + db + "..SendSMS(Formno,MobileNo,sms,IsSent) " +
                               " Select FormNo, Mobl ,'Hi '+ IIF(CHARINDEX(' ',MemFirstName)<=0,MemFirstName,SUBSTRING(MemFirstName,1,CHARINDEX(' ',MemFirstName)-1)) +'! " + sms + "','N' FROM TrnOrder WHERE OrderNo='" + objPartyDispatchOrder.OrderNo + "' AND ISNUMERIC(Mobl)=1 AND LEN(Cast(Mobl as varchar(20)))=10;";

                                if (SC.State == ConnectionState.Closed)
                                    SC.Open();
                                cmd = new SqlCommand();
                                cmd.CommandText = Sql;
                                cmd.Connection = SC;
                                i = cmd.ExecuteNonQuery();
                                SC.Close();
                                RejectOrder(objPartyDispatchOrder.OrderNo, Convert.ToString(objPartyDispatchOrder.FormNo), "Rejected after Dispatching.", objPartyDispatchOrder.LoginUser.UserId, "N");
                                //  RejectOrder(objPartyDispatchOrder.OrderNo, "Dispatched", objPartyDispatchOrder.LoginUser.UserId, false);
                            }

                            objResponse.ResponseMessage = "Saved Successfully!";
                            objResponse.ResponseStatus = "OK";
                        }
                        else
                        {
                            objResponse.ResponseMessage = "Something went wrong!";
                            objResponse.ResponseStatus = "FAILED";
                        }
                    }

                    catch (DbEntityValidationException e)
                    {
                        objResponse.ResponseMessage = "Something went wrong!";
                        objResponse.ResponseStatus = "FAILED";
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }
        public List<ProductModel> GetOrderProduct(string OrderNo, string CurrentPartyCode)
        {
            List<ProductModel> objOrderProduct = new List<ProductModel>();
            List<ProductModel> objTempOrderProduct = new List<ProductModel>();
            try
            {
                string AppConnectionString = AppConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);

                decimal Orderno = 0;
                if (!string.IsNullOrEmpty(OrderNo))
                {
                    Orderno = decimal.Parse(OrderNo);
                }
                string query = "Select * from TrnOrderDetail where OrderNo=" + OrderNo;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                //cmd.Parameters.AddWithValue("@OrdeNo", Orderno);
                cmd.Connection = SC;
                SC.Close();
                SC.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        objTempOrderProduct.Add(new ProductModel
                        {
                            ProdCode = int.Parse(reader["ProductID"].ToString()),
                            ProductCodeStr = reader["ProductID"].ToString(),
                            ProductName = reader["ProductName"].ToString(),
                            Quantity = decimal.Parse(reader["Qty"].ToString()),
                            ProductType = reader["ProdType"].ToString(),
                            StockAvailable = 0,


                        });
                    }
                }
                using (var entity = new InventoryEntities(enttConstr))
                {
                    foreach (var obj in objTempOrderProduct)
                    {
                        obj.StockAvailable = (from stockAvail in entity.Im_CurrentStock
                                              where stockAvail.ProdId == obj.ProductCodeStr && stockAvail.FCode.Equals(CurrentPartyCode)
                                              select stockAvail.Qty
                                                     ).DefaultIfEmpty(0).Sum();
                        objOrderProduct.Add(obj);
                    }
                }


            }
            catch (Exception ex)
            {

            }
            return objOrderProduct;
        }
        public ResponseDetail RejectOrder(string OrderNo, string FormNo, string RejectReason, decimal RejectedByUserId, string DeleteOrder)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            try
            {


                //string AppConnectionString = AppConstr;
                //SqlConnection SC1 = new SqlConnection(AppConnectionString);
                string InvConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(InvConnectionString);
                SqlCommand cmd = new SqlCommand();

                //decimal Orderno = 0;
                //if (!string.IsNullOrEmpty(OrderNo))
                //{
                //    Orderno = decimal.Parse(OrderNo);
                //}
                //string query = "Select * from TrnOrder where OrderNo=" + OrderNo;               
                //cmd.CommandText = query;               
                //cmd.Connection = SC1;
                //SC1.Close();
                //SC1.Open();
                //decimal FormNo =0 ;
                //bool IsOtherBillExist = false;

                //using (SqlDataReader reader = cmd.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        FormNo = Convert.ToDecimal(reader["FormNo"].ToString());
                //    }
                //}



                //query = "Select count(*) as OrderCount from TrnOrder where FormNo = "+ FormNo + " and OrderNo<>" + OrderNo;
                //cmd.CommandText = query;
                //cmd.Connection = SC1;
                //SC1.Close();
                //SC1.Open();
                //int count = 0;
                //using (SqlDataReader reader = cmd.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        count = Convert.ToInt16(reader["OrderCount"].ToString());
                //    }
                //    if (count > 0)
                //    {
                //        IsOtherBillExist = true;
                //    }
                //}

                //using (var entity = new InventoryEntities((enttConstr)))
                //{
                // count = from r in entity.TrnBillMains where rbillno
                //}
                string Query = string.Empty;
                ConnModel obj = (System.Web.HttpContext.Current.Session["ConModel"] as ConnModel);
                if (obj.CompID == "1007" || obj.CompID == "1029")
                {
                    Query = "Exec REJECTORDER '" + decimal.Parse(OrderNo.Trim()) + "','" + FormNo + "','" + (RejectReason.Trim()) + " :Web Inv','" + RejectedByUserId + "','" + DeleteOrder + "'";
                }
                else
                {
                    Query = "Exec REJECTORDER '" + decimal.Parse(OrderNo.Trim()) + "','" + FormNo + "','" + (RejectReason.Trim()) + " :Web Inv','" + RejectedByUserId + "','" + DeleteOrder + "'";
                }
                cmd.CommandText = Query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    objResponse.ResponseMessage = "Rejected Successfully!";
                    objResponse.ResponseStatus = "OK";
                }
                else
                {
                    objResponse.ResponseMessage = "Something went wrong!";
                    objResponse.ResponseStatus = "FAILED";
                }


            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }


        public List<DisptachOrderModel> GetDispatchOrderList(string FromDate, string ToDate, string PartyCode, string ViewType, string IdNo, string OrderNo, string DispMode, bool notOfferOrder)
        {
            List<DisptachOrderModel> objOrderList = new List<DisptachOrderModel>();
            try
            {
                DateTime StartDate = DateTime.Now;
                DateTime EndDate = DateTime.Now;

                string AppConnectionString = AppConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);

                using (var entity = new InventoryEntities(enttConstr))
                {
                    string WhereCondition = "";
                    string DispStatus = "N";
                    if (!string.IsNullOrEmpty(ViewType))
                    {
                        if (ViewType == "Pending")
                        {
                            DispStatus = "N";
                        }
                        else if (ViewType == "PendingRepurch")
                        {
                            //WhereCondition = WhereCondition + " AND OrderType='O'";
                            WhereCondition = WhereCondition + " AND OrderType='Y'";
                            DispStatus = "N";
                        }
                        else
                        {
                            DispStatus = "C";
                        }
                    }
                    if (notOfferOrder)
                        WhereCondition = WhereCondition + " AND a.OfferUID=0";


                    if (!string.IsNullOrEmpty(PartyCode) && PartyCode != "0" && PartyCode != "All")
                    {
                        WhereCondition = WhereCondition + "AND a.OrderFor='" + PartyCode + "'";
                    }
                    else
                    {
                        WhereCondition = WhereCondition + "";
                    }
                    if (!string.IsNullOrEmpty(IdNo) && IdNo != "0" && IdNo != "All")
                    {
                        WhereCondition = WhereCondition + "AND a.IDNo='" + IdNo + "'";
                    }
                    else
                    {
                        WhereCondition = WhereCondition + "";
                    }
                    if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "0" && OrderNo != "All")
                    {
                        WhereCondition = WhereCondition + "AND a.OrderNo='" + OrderNo + "'";
                    }
                    else
                    {
                        WhereCondition = WhereCondition + "";
                    }
                    if (!string.IsNullOrEmpty(DispMode) && DispMode != "0" && DispMode != "All" && DispMode != "A")
                    {
                        WhereCondition = WhereCondition + "AND HostIP='" + DispMode + "'";
                    }

                    if (!string.IsNullOrEmpty(FromDate) && (!string.IsNullOrEmpty(ToDate)))
                    {
                        if (FromDate != "All")
                        {
                            //var sqlFromdate = FromDate.Split('-');
                            //StartDate = new DateTime(Convert.ToInt16(sqlFromdate[2]), Convert.ToInt16(sqlFromdate[1]), Convert.ToInt16(sqlFromdate[0]));
                            //var SplitFromDate = FromDate.Split('-');
                            //FromDate = (SplitFromDate[1].Length == 1 ? "0" + SplitFromDate[1] : SplitFromDate[1]) + "/" + (SplitFromDate[0].Length == 1 ? "0" + SplitFromDate[0] : SplitFromDate[0]) + "/" + SplitFromDate[2];
                            //StartDate = Convert.ToDateTime(DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                            StartDate = Convert.ToDateTime(FromDate);
                        }
                        if (ToDate != "All")
                        {
                            //var sqlFromdate = ToDate.Split('-');
                            //EndDate = new DateTime(Convert.ToInt16(sqlFromdate[2]), Convert.ToInt16(sqlFromdate[1]), Convert.ToInt16(sqlFromdate[0]));
                            //var SplitToDate = ToDate.Split('-');

                            //ToDate = (SplitToDate[1].Length == 1 ? "0" + SplitToDate[1] : SplitToDate[1]) + "/" + (SplitToDate[0].Length == 1 ? "0" + SplitToDate[0] : SplitToDate[0]) + "/" + SplitToDate[2];
                            //EndDate = Convert.ToDateTime(DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                            EndDate = Convert.ToDateTime(ToDate);
                        }
                    }
                    string NewFromDate = StartDate.Date.ToString("dd-MMM-yyyy");
                    string NewToDate = EndDate.Date.ToString("dd-MMM-yyyy");
                    if (FromDate != "All" && ToDate != "All")
                    {
                        WhereCondition = WhereCondition + " and a.OrderDate>='" + NewFromDate + "' and a.OrderDate<='" + NewToDate + "'";
                    }
                    else if (FromDate != "All" && ToDate == "All")
                    {
                        WhereCondition = WhereCondition + " and a.OrderDate>='" + NewFromDate + "'";
                    }
                    else if (FromDate == "All" && ToDate != "All")
                    {
                        WhereCondition = WhereCondition + "and a.OrderDate<='" + NewToDate + "'";
                    }
                    else
                    {

                    }

                    string db = dbName;
                    string dbInv = invDbName;

                    string query = "";
                    ConnModel obj = (System.Web.HttpContext.Current.Session["ConModel"] as ConnModel);
                    if (obj.CompID == "1006")/*For Bigway */
                        query = "Select  0 as Blank,a.FormNo,a.OrderNo,Replace(Convert(varchar,a.OrderDate,106),' ','-') as ODte,a.IDNo,a.MemFirstName + ' ' + a.MemLastName as MemName,a.OrderAmt,a.Remark,a.Address1,a.Pincode,a.Mobl ,a.OrderType ,CASE WHEN a.OrderTYpe='T' THEN 'Activation' ELSE 'Product Request' END AS OType,CASE WHEN HostIP='C' THEN 'By Courier' WHEN HostIP='S' THEN 'By Speed Post' WHEN HostIP='H' THEN 'By Hand - '+ b.PartyName ELSE b.PartyName END As OrdFor,CASE WHEN Paymode='U' THEN 'UPI' WHEN Paymode='B' THEN 'Net Banking' WHEN Paymode='W' THEN 'Wallet' ELSE 'Unknown Paymode' END as Paymode,Case WHEN a.DispatchStatus='N' AND a.DispatchQty=0 THEN 'Reject' ELSE '' END as Reject,a.Paymode,CASE WHEN a.ChDDno>0 THEN Cast(a.ChDDno as varchar(50)) ELSE '' END as Transno,CASE WHEN a.ChDDno>0 THEN Convert(varchar,a.ChDate,106) ELSE '' END as TransDate,a.ScannedImage  From " + db + "..TrnOrder a," + dbInv + "..M_LedgerMaster b Where a.OrderFor=b.PartyCode AND a.ActiveStatus='Y' AND a.DispatchStatus='" + DispStatus + "' " + WhereCondition +
                   " Order By OrderDate";
                  
                    else
                        query = "Select  0 as Blank,a.FormNo,a.OrderNo,Replace(Convert(varchar,a.OrderDate,106),' ','-') as ODte,a.IDNo,a.MemFirstName + ' ' + a.MemLastName as MemName,a.OrderAmt,a.Remark,a.Address1,a.Pincode,a.Mobl ,a.OrderType ,CASE WHEN a.OrderTYpe='T' THEN 'Activation' ELSE 'Product Request' END AS OType,CASE WHEN HostIP='C' THEN 'By Courier' WHEN HostIP='S' THEN 'By Speed Post' WHEN HostIP='H' THEN 'By Hand - '+ b.PartyName ELSE b.PartyName END As OrdFor,'Wallet' as Paymode,Case WHEN a.DispatchStatus='N' AND a.DispatchQty=0 THEN 'Reject' ELSE '' END as Reject,'' as Transno,'' as TransDate,'' ScannedImage  From " + db + "..TrnOrder a With(Nolock) INNER JOIN " + dbInv + "..M_LedgerMaster b With(Nolock) ON a.OrderFor=b.PartyCode Where a.ActiveStatus='Y' AND a.DispatchStatus='" + DispStatus + "' " + WhereCondition +
              " Order By OrderDate";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;

                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            objOrderList.Add(new DisptachOrderModel
                            {
                                OrderNo = !string.IsNullOrEmpty(reader["OrderNo"].ToString()) ? long.Parse(reader["OrderNo"].ToString()) : 0,
                                OrderDateStr = reader["ODte"].ToString(),
                                OrderDate = DateTime.Parse(reader["ODte"].ToString()),
                                IdNo = reader["IDNo"].ToString(),
                                Name = reader["MemName"].ToString(),
                                OrderAmount = decimal.Parse(reader["OrderAmt"].ToString()),
                                Remarks = reader["Remark"].ToString(),
                                Address = reader["Address1"].ToString(),
                                Pincode = decimal.Parse(reader["Pincode"].ToString()),
                                Mobile = decimal.Parse(reader["Mobl"].ToString()),
                                OrderType = reader["OrderType"].ToString(),
                                DispBy = reader["OrdFor"].ToString(),
                                FormNo = Convert.ToDecimal(reader["FormNo"].ToString()),
                                Paymode = reader["Paymode"].ToString(),
                                TransNo = reader["TransNo"].ToString(),
                                TransDate = reader["TransDate"].ToString(),
                                ScannedImage = "cpanel." + System.Web.HttpContext.Current.Session["URL"] + "/images/UploadImage/" + reader["ScannedImage"].ToString(),
                                SoldBy = "",
                                IsDispatched = false,
                                Reject = reader["Reject"].ToString()

                            });
                        }
                    }


                }
            }
            catch (Exception ex)
            {

            }
            return objOrderList;
        }

        public ResponseDetail SaveDispatchOrderdetails(List<DisptachOrderModel> objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";

            objResponse.ResponseStatus = "FAILED";
            try
            {
                List<string> ProdIds = new List<string>();
                List<string> OrderIds = new List<string>();
                bool FlagIsSave = false;
                bool FlagToDispatch = true;
                foreach (var obj in objModel)
                {
                    ProdIds = new List<string>();
                    FlagToDispatch = true;
                    //using (var entity=new InventoryEntities(enttConstr))
                    //{
                    if (obj.IsDispatched)
                    {
                        List<ProductModel> objProductList = GetOrderProduct(obj.OrderNo.ToString(), obj.SoldBy);
                        if (objProductList.Count() == 0)
                        {
                            FlagToDispatch = false;
                        }
                        else
                        {
                            foreach (var objProduct in objProductList)
                            {
                                if (objProduct.StockAvailable < objProduct.Quantity && objProduct.ProductType == "P")
                                {
                                    ProdIds.Add(objProduct.ProductCodeStr);

                                    FlagToDispatch = false;
                                }

                            }
                        }

                        //dispatch code
                        if (FlagToDispatch)
                        {

                            string InvConnectionString = InvConstr;
                            SqlConnection SC = new SqlConnection(InvConnectionString);
                            string sqlQry = "Exec DispatchOrder '" + obj.OrderNo + "','" + obj.SoldBy + "';";


                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = sqlQry;

                            cmd.Connection = SC;


                            SC.Close();
                            SC.Open();
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                FlagIsSave = true;

                            }
                            else
                            {
                                FlagIsSave = false;
                                objResponse.ResponseMessage = "Something went wrong!";
                                objResponse.ResponseStatus = "FAILED";
                            }


                        }
                        else
                        {
                            OrderIds.Add(obj.OrderNo.ToString() + " can't be dispatched.Stock of products " + string.Join(",", ProdIds) + " is not available.");
                        }
                    }
                    else
                    {

                    }
                    //if (FlagIsSave)
                    //{
                    //    objResponse.ResponseMessage = "Dispatched Successfully!";

                    //   objResponse.ResponseStatus = "OK";
                    //}
                    //}


                }

                if (OrderIds.Count() > 0)
                {
                    objResponse.ResponseMessage = string.Join(";", OrderIds);

                    objResponse.ResponseStatus = "FAILED";
                }
                else
                {
                    objResponse.ResponseMessage = "Orders Dispatched Successfully!";
                    objResponse.ResponseStatus = "OK";
                }
            }
            catch (Exception ex)
            {

            }

            return objResponse;
        }
        public ResponseDetail RejectFranchiseOrder(string OrderNo, string RejectReason, decimal RejectedByUserId, bool RejectCompletely, string BillNo)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            TrnPartyOrderMain objPartyOrderMain = new TrnPartyOrderMain();
            try
            {
                string AppConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                string Sql = "Select CASE WHEN a.OrderAmount-b.NetPayable<TotalWeight THEN a.OrderAmount-b.NetPayable ELSE TotalWeight END AS WalletAmt,a.OrderBy,a.OrderTo ";
                Sql = Sql + " FROM TrnPartyOrderMain a LEFT JOIN (Select OrderNo, SUM(NetPayable) NetPayable FROM TrnBillMain WHERE OrderNo='" + OrderNo + "' GROUP BY OrderNo) b  ";
                Sql = Sql + " On a.OrderNo=b.OrderNo AND a.ActiveStatus='Y' WHERE a.OrderNo='" + OrderNo + "'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Sql;
                cmd.Connection = SC;
                if (SC.State == ConnectionState.Closed)
                    SC.Open();
                Sql = "";
                string db = dbName;
                string dbInv = invDbName;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        decimal WalletAmt = Convert.ToDecimal(reader["WalletAmt"].ToString());
                        string OrderTo = reader["OrderTo"].ToString();
                        string OrderBy = reader["OrderBy"].ToString();
                        if (WalletAmt > 0)
                        {
                            if (BillNo == "")
                                BillNo = OrderNo;//Added on 06Sep19
                                                 //Sql = ";INSERT INTO "+ db + "..TrnVoucher (VoucherNo,VoucherDate,DrTo,CrTo,Amount,Narration,RefNo,AcType,VType,SessID,WSessID) ";
                                                 //Sql = Sql + " Select ISNULL(Max(VoucherNo),0)+1 ,Cast(Getdate() as Date),0,(Select UserPartyCode FROM M_LedgerMaster WHERE PartyCode='" + OrderBy + "'),'" + WalletAmt + "','Order " + OrderNo + " Rejected.','" + (OrderNo) + "','S','C',Convert(varchar,Getdate(),112),(Select Max(SessID) FROM " + db + "..M_SessnMaster) FROM " + db + "..TrnVoucher ;";
                            Sql = ";INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,VType,BType,AccDocType,SessID,FSessID) " +
            " Select CASE WHEN Max(VoucherNo) is NULL THEN 1 ELSE Max(VoucherNo)+1 END ,Cast(Convert(varchar,Getdate(),106) as Datetime),'" + OrderTo + "','" + OrderBy + "','" + WalletAmt + "','Refund against Order " + OrderNo + ".','" + BillNo + "','R','O','Order Rejected',(Select Max(SessID) FROM " + db + "..M_SessnMaster),(Select Max(FSessID) FROM M_FiscalMaster) FROM TrnVoucher ;";
                        }
                    }
                }

                if (RejectCompletely == true)
                {
                    Sql = Sql + "UPDATE TrnPartyOrderMain SET Status='C',ActiveStatus='D',OrderNo= 'Del:'+ OrderNo ,Remarks=Remarks +'; Del:  " + RejectReason + " by " + RejectedByUserId + " on " + DateTime.Now + "' WHERE OrderNo='" + OrderNo + "';";
                    Sql = Sql + "UPDATE TrnPartyOrderDetail SET Status='C',ActiveStatus='D',OrderNo= 'Del:'+ OrderNo WHERE OrderNo='" + OrderNo + "';";
                }
                else
                {
                    Sql = Sql + "UPDATE TrnPartyOrderMain SET Status='C' WHERE OrderNo='" + OrderNo + "';";
                    Sql = Sql + "UPDATE TrnPartyOrderDetail SET Status='C' WHERE OrderNo='" + OrderNo + "';";
                }

                cmd = new SqlCommand();
                cmd.CommandText = Sql;
                cmd.Connection = SC;
                if (SC.State == ConnectionState.Closed)
                    SC.Open();
                cmd.ExecuteNonQuery();
                SC.Close();
            }
            catch (Exception ex)
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }
            return objResponse;
        }

        public ResponseDetail TransferFranchiseOrder(string OrderNo, string RejectReason, decimal TransferredByUserId)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            TrnPartyOrderMain objPartyOrderMain = new TrnPartyOrderMain();
            try
            {
                string AppConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                string WrPartyCode = WRPartyCode;
                string Sql = "";
                Sql = Sql + "INSERT INTO TrnOrderTransferDetail (OrderNo,OldOrderTo,NewOrderto,UserID)" +
"Select OrderNo, OrderTO,'" + WrPartyCode + "','" + TransferredByUserId + "' FROM TrnPartyOrderMain WHERE OrderNo='" + OrderNo + "'";
                Sql = Sql + "Update TrnPartyOrderMain SET PLUser=OrderTo WHERE OrderNo='" + OrderNo + "'";
                Sql = Sql + "Update TrnPartyOrderMain SET OrderTo='" + WrPartyCode + "' WHERE OrderNo='" + OrderNo + "'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Sql;
                cmd.Connection = SC;
                if (SC.State == ConnectionState.Closed)
                    SC.Open();

                cmd.ExecuteNonQuery();
                SC.Close();
                objResponse.ResponseMessage = "Transferred successfully!";
                objResponse.ResponseStatus = "OK";
            }
            catch (Exception ex)
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }
            return objResponse;
        }

        public KitDescriptionModel GetKitDescription(decimal KitId)
        {
            KitDescriptionModel objKitDesc = new KitDescriptionModel();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    //objKitDesc = (from r in entity.KitDescriptions
                    //              where r.KitID == KitId
                    //              select new KitDescriptionModel
                    //              {
                    //                  KitId=r.KitID,
                    //                  Cat1=r.Cat1,
                    //                  Cat2=r.Cat2,
                    //                  Cat3=r.Cat3,
                    //                  Cat4=r.Cat4,
                    //                  Cat5=r.Cat5,
                    //                  Cat6=r.Cat6,
                    //                  Others=r.Others
                    //              }
                    //            ).FirstOrDefault();
                    //var result =(from r in entity.M_CatMaster
                    //              where r.ActiveStatus == "Y"
                    //              select new
                    //              {
                    //                 CatId= r.CatId,
                    //                 CatName= r.CatName
                    //              }

                    //            ).ToList();
                    List<string> objListCategory = new List<string>();
                    //foreach(var obj in result)
                    //{
                    //    objListCategory.Add(obj.CatId+"&"+obj.CatName);
                    //}
                    //var resultCategory = string.Join(",", objListCategory);
                    //objKitDesc.CategoryNames = resultCategory;
                }
            }
            catch (Exception ex)
            {

            }
            return objKitDesc;
        }
        public List<KitDetail> GetKitIdList()
        {
            List<KitDetail> KidIDs = new List<KitDetail>();
            try
            {
                string AppConnectionString = AppConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                string query = "select KitId,KitName from M_KitMaster where KitAmount>0";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;

                cmd.Connection = SC;
                SC.Close();
                SC.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        KidIDs.Add(new KitDetail { KitId = decimal.Parse(reader["KitId"].ToString()), KitName = reader["KitName"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return KidIDs;
        }
        public ResponseDetail DeleteBills(string BillNo, string FsessId, decimal UserId, string Reason)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";

            objResponse.ResponseStatus = "FAILED";
            try
            {


                string InvConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(InvConnectionString);

                string sqlQry = "";
                var billNoArray = BillNo.Split(',');
                var FsessIdArray = FsessId.Split(',');
                List<TrnBillMain> BillList = new List<TrnBillMain>();
                string CantDeleteBill = string.Empty;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    BillList = (from r in entity.TrnBillMains where billNoArray.Contains(r.BillNo) select r).ToList();

                    for (var j = 0; j < billNoArray.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(billNoArray[j]))
                        {
                            var FirstBill = BillList.Where(r => r.BillNo == billNoArray[j] && r.BillType == "B").FirstOrDefault();
                            if (FirstBill != null)
                            {
                                var RepurchBill = entity.TrnBillMains.Where(r => r.BillType == "R" && r.FormNo == FirstBill.FormNo).FirstOrDefault();
                                if (RepurchBill == null)
                                {
                                    sqlQry += "Exec ROLLBACKBILL '" + billNoArray[j] + "','" + Reason + "'," + UserId + "," + FsessIdArray[j] + ";";
                                }
                                else
                                {
                                    CantDeleteBill += billNoArray[j] + ",";
                                }
                            }
                            else
                            {
                                sqlQry += "Exec ROLLBACKBILL '" + billNoArray[j] + "','" + Reason + "'," + UserId + "," + FsessIdArray[j] + ";";
                            }
                        }
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlQry;

                cmd.Connection = SC;


                SC.Close();
                SC.Open();
                int i = 0;
                if (!string.IsNullOrEmpty(sqlQry))
                {
                    i = cmd.ExecuteNonQuery();
                }
                if (i > 0)
                {
                    objResponse.ResponseMessage = "Successfully Deleted!";
                    objResponse.ResponseStatus = "OK";
                }

                if (!string.IsNullOrEmpty(CantDeleteBill))
                {
                    objResponse.ResponseMessage = "Successfully Deleted! Except following bills are not allowed to delete as their repurchase bill is already created :- " + CantDeleteBill;
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }
        public ResponseDetail DeletePurchaseInvoice(string InwardNo, decimal FsessId, decimal UserId, string Reason)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";

            objResponse.ResponseStatus = "FAILED";
            try
            {

                string InvConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(InvConnectionString);

                string sqlQry = "Exec ROLLBACK_PI '" + InwardNo + "','" + Reason + "'," + UserId + "," + FsessId + ";";


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlQry;

                cmd.Connection = SC;


                SC.Close();
                SC.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    objResponse.ResponseMessage = "Successfully Deleted!";
                    objResponse.ResponseStatus = "OK";


                }
                else
                {

                    objResponse.ResponseMessage = "Something went wrong!";
                    objResponse.ResponseStatus = "FAILED";
                }

            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public ResponseDetail SaveOrderReturn(SalesReturnModel objSalesReturnOrder)
        {

            ResponseDetail objResponse = new ResponseDetail();
            decimal? SessId = 0;
            string UserReturnNo = "";
            string Loggedinparty = "";
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            try
            {
                Loggedinparty = objSalesReturnOrder.EntryBy;

                string AppConnectionString = AppConstr;
                string InvConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                SqlConnection SC1 = new SqlConnection(InvConnectionString);
                string query = "Select Max(SessID) as MaxSessId from M_SessnMaster";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SessId = decimal.Parse(reader["MaxSessId"].ToString());
                    }
                }
                using (var entity = new InventoryEntities(enttConstr))
                {

                    try
                    {

                        string part1 = (from r in entity.M_ConfigMaster select r.BillPrefix).FirstOrDefault();
                        if (!String.IsNullOrEmpty(part1))
                        {
                            UserReturnNo += part1 + "GR/" + Loggedinparty + "/";
                        }

                        decimal part2 = (from r in entity.TrnSalesReturnMains select r.SRNo).DefaultIfEmpty(0).Max();
                        part2 += 1;
                        UserReturnNo += part2;

                        int fessid = Convert.ToInt32((from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result.FSessId).DefaultIfEmpty(0).Max());
                        string version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();

                        var billDetail = (from r in entity.TrnBillMains where r.BillNo == objSalesReturnOrder.BillNo select r).FirstOrDefault();

                        TrnSalesReturnMain objreturnMain = new TrnSalesReturnMain();

                        objreturnMain.SReturnNo = UserReturnNo;
                        objreturnMain.SRNo = part2;
                        objreturnMain.ReturnBy = objSalesReturnOrder.partyCode;
                        objreturnMain.ReturnByName = objSalesReturnOrder.partyName;
                        objreturnMain.ReturnTo = objSalesReturnOrder.returnto;
                        objreturnMain.ReturnToName = (from result in entity.M_LedgerMaster where result.PartyCode == objSalesReturnOrder.returnto select result.PartyName).FirstOrDefault();
                        objreturnMain.Ftype = objSalesReturnOrder.Ftype;
                        objreturnMain.FormNo = 0;
                        objreturnMain.ReturnDate = DateTime.Now.Date;
                        objreturnMain.RefNo = objSalesReturnOrder.refno ?? "";
                        objreturnMain.TotalAmount = objSalesReturnOrder.tAmt;
                        objreturnMain.Remarks = objSalesReturnOrder.remark;
                        objreturnMain.RndOff = objSalesReturnOrder.rndOff;
                        objreturnMain.NetPayable = objSalesReturnOrder.netPay;
                        objreturnMain.RType = "S";
                        objreturnMain.Reason = objSalesReturnOrder.reason;
                        objreturnMain.RecTimeStamp = DateTime.Now;
                        objreturnMain.ActiveStatus = "Y";
                        objreturnMain.Status = "P";
                        objreturnMain.Version = version;
                        objreturnMain.UserID = objSalesReturnOrder.LoggedInUserId;
                        objreturnMain.TotalBV = objSalesReturnOrder.TotalBV;
                        objreturnMain.TotalRP = objSalesReturnOrder.TotalRP;
                        objreturnMain.UID = objSalesReturnOrder.LoggedInUserIP;
                        objreturnMain.OrderNo = objSalesReturnOrder.BillNo;
                        objreturnMain.OrderDate = objSalesReturnOrder.BillDate;
                        objreturnMain.FSessId = fessid;
                        objreturnMain.EntryBy = objSalesReturnOrder.EntryBy;

                        decimal Totaltax = 0;
                        decimal TotalSGSTAmt = 0;
                        decimal TotalCGSTAmt = 0;
                        foreach (var product in objSalesReturnOrder.ProductList)
                        {

                            if (product.ReturnQty > 0)
                            {
                                TrnSalesReturnDetail objDTBillData = new TrnSalesReturnDetail();
                                objDTBillData.SReturnNo = UserReturnNo;
                                objDTBillData.SRNo = part2;
                                objDTBillData.ReturnBy = objSalesReturnOrder.partyCode;
                                objDTBillData.ReturnTo = objSalesReturnOrder.returnto;
                                objDTBillData.BatchNo = product.BatchNo;
                                objDTBillData.Ftype = objSalesReturnOrder.Ftype;
                                objDTBillData.ReturnDate = DateTime.Now.Date;
                                objDTBillData.ProdId = product.ProductCodeStr;
                                objDTBillData.ProductName = product.ProductName;
                                objDTBillData.ReturnQty = product.ReturnQty;
                                objDTBillData.FreeQty = 0;
                                objDTBillData.AcceptQty = 0;
                                objDTBillData.RemainingQty = 0;
                                objDTBillData.Rate = product.Rate ?? 0;
                                objDTBillData.Amount = product.Amount;
                                objDTBillData.RecTimeStamp = DateTime.Now;
                                objDTBillData.UserID = objSalesReturnOrder.LoggedInUserId;
                                objDTBillData.Version = version;
                                objDTBillData.BV = product.BV ?? 0;
                                objDTBillData.RP = product.RP ?? 0;
                                objDTBillData.RPValue = product.RPValue ?? 0;
                                objDTBillData.BVValue = product.BVValue ?? 0;
                                objDTBillData.TaxType = "";
                                objDTBillData.Status = "P";
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.IsKit = "N";
                                objDTBillData.ProdType = "P";
                                objDTBillData.ROn = "D";

                                objDTBillData.OfferUId = 0;
                                objDTBillData.MRP = product.MRP ?? 0;
                                objDTBillData.DP = product.DP ?? 0;
                                objDTBillData.FSessId = fessid;
                                objDTBillData.EntryBy = objSalesReturnOrder.EntryBy;

                                if (billDetail.TaxAmount != 0)
                                {
                                    objDTBillData.Tax = 0;
                                    objDTBillData.TaxAmount = 0;



                                    objDTBillData.CGST = product.GSTPer / 2;
                                    objDTBillData.CGSTAmt = ((product.TaxAmt ?? 0) / 2);
                                    objDTBillData.SGST = product.GSTPer / 2;
                                    objDTBillData.SGSTAmt = ((product.TaxAmt ?? 0) / 2);
                                    Totaltax += objDTBillData.TaxAmount;
                                    TotalSGSTAmt += objDTBillData.SGSTAmt;
                                    TotalCGSTAmt += objDTBillData.CGSTAmt;

                                }
                                else
                                {
                                    objDTBillData.Tax = product.GSTPer;
                                    objDTBillData.TaxAmount = product.TaxAmt ?? 0;



                                    objDTBillData.CGST = 0;
                                    objDTBillData.CGSTAmt = 0;
                                    objDTBillData.SGST = 0;
                                    objDTBillData.SGSTAmt = 0;
                                    Totaltax += objDTBillData.TaxAmount;
                                    TotalSGSTAmt += objDTBillData.SGSTAmt;
                                    TotalCGSTAmt += objDTBillData.CGSTAmt;
                                }

                                entity.TrnSalesReturnDetails.Add(objDTBillData);

                            }

                            objreturnMain.TaxAmount = Totaltax;

                            objreturnMain.CGSTAmt = TotalSGSTAmt;
                            objreturnMain.SGSTAmt = TotalCGSTAmt;

                            entity.TrnSalesReturnMains.Add(objreturnMain);
                        }
                        int i = 0;
                        try
                        {
                            //  i = entity.SaveChanges();
                            entity.Configuration.ValidateOnSaveEnabled = false;
                            entity.SaveChanges();
                        }

                        catch (DbUpdateException ex)
                        {

                        }

                        catch (Exception ex)
                        {


                        }

                        //if (i > 0)
                        //{
                        string sqlQry = "Exec Inv_SRWallet '" + UserReturnNo + "';";
                        cmd = new SqlCommand();
                        if (SC1.State == ConnectionState.Closed) SC1.Open();
                        cmd.CommandText = sqlQry;
                        cmd.Connection = SC1;

                        i = cmd.ExecuteNonQuery();
                        if (SC1.State == ConnectionState.Open) SC1.Close();
                        if (i > 0)
                        {
                            objResponse.ResponseMessage = "Saved Successfully!";
                            objResponse.ResponseStatus = "OK";
                        }


                        // }

                    }
                    catch (DbEntityValidationException e)
                    {
                        objResponse.ResponseMessage = "Something went wrong!";
                        objResponse.ResponseStatus = "FAILED";
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public List<PartyBill> GetBillList(string partyType, string Fcode)
        {
            List<PartyBill> billList = new List<PartyBill>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    DateTime date1 = DateTime.Now.Date.AddDays(-30).Date;
                    DateTime date2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01).Date;
                    int lastdate = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    date1 = DateTime.Now.Date;
                    List<PartyBill> objbillList = new List<PartyBill>();


                    string dbInv = invDbName;
                    string sql = "";

                    sql += "Select DISTINCT a.BillNo,m.UserBillNo,a.BillDate,a.SoldBy,l.Partyname as SoldByName,a.FType,a.Fcode as FCode FROM TrnBillMain m LEFT JOIN TrnBillDetails a On a.BillNo = m.BillNo";
                    sql += " LEFT JOIN TrnSalesReturnMain b ON a.BillNo = b.OrderNo";
                    sql += " LEFT JOIN TrnSalesReturnDetail c ON b.SReturnNo = c.SReturnNo AND a.ProductID = c.ProdID";
                    sql += " LEFT JOIN  M_LedgerMaster l ON a.SoldBy = l.PartyCode";
                    sql += " WHERE a.Qty - ISNULL(c.ReturnQty, 0) > 0";


                    string InvConnectionString = InvConstr;
                    SqlConnection SC = new SqlConnection(InvConnectionString);

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = sql;
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PartyBill tempobj = new PartyBill();
                            tempobj.BillNo = reader["BillNo"] != null ? reader["BillNo"].ToString() : "";
                            tempobj.UserBillNo = reader["UserBillNo"] != null ? reader["UserBillNo"].ToString() : "";
                            tempobj.SoldBy = reader["SoldBy"] != null ? reader["SoldBy"].ToString() : "";
                            tempobj.SoldByName = reader["SoldByName"] != null ? reader["SoldByName"].ToString() : "";
                            tempobj.FType = reader["FType"] != null ? reader["FType"].ToString() : "";
                            tempobj.FCode = reader["FCode"] != null ? reader["FCode"].ToString() : "";
                            tempobj.BillDate = reader["BillDate"] != null ? Convert.ToDateTime(reader["BillDate"]) : new DateTime();
                            objbillList.Add(tempobj);
                        }
                    }



                    if (partyType == "customer")
                    {
                        billList = (from r in objbillList
                                        // let date = r.OrderType == "T" ? date1 : date2
                                    where (r.FType == "GC" || r.FType == "W")  //&& (r.BillDate >= date2) && (r.BillDate <= date1)
                                    select new PartyBill
                                    {
                                        BillNo = r.BillNo,
                                        UserBillNo = r.UserBillNo,
                                        SoldBy = r.SoldBy,
                                        SoldByName = r.SoldByName,
                                        BillDate = r.BillDate

                                    }).OrderByDescending(o => o.BillDate).ToList();
                    }
                    else if (partyType == "party")
                    {
                        billList = (from r in objbillList
                                        // let date = r.OrderType == "T" ? date1 : date2
                                    where r.FCode == Fcode && (r.FType != "GC" && r.FType != "M") //&& (r.BillDate >= date2) && (r.BillDate <= DateTime.Now.Date)
                                    select new PartyBill
                                    {
                                        BillNo = r.BillNo,
                                        UserBillNo = r.UserBillNo,
                                        SoldBy = r.SoldBy,
                                        SoldByName = r.SoldByName,
                                        BillDate = r.BillDate

                                    }).ToList().OrderByDescending(o => o.BillDate).ToList();
                    }
                    else
                    {
                        billList = (from r in objbillList
                                    let date = r.OrderType == "T" ? date1 : date2
                                    where r.FCode == Fcode && (r.BillDate >= date2) && (r.BillDate <= date1) //&& r.Imported == "N"
                                    select new PartyBill
                                    {
                                        BillNo = r.BillNo,
                                        UserBillNo = r.UserBillNo,
                                        SoldBy = r.SoldBy,
                                        SoldByName = r.SoldByName,
                                        BillDate = r.BillDate

                                    }).ToList().OrderByDescending(o => o.BillDate).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return billList;
        }

        public PartyBill GetBillDetail(string BillNo, string CurrentPartyCode)
        {
            PartyBill billdetail = null;
            try
            {

                List<ProductModel> ProductReturnDetail = new List<ProductModel>();
                string dbInv = invDbName;
                string sql = "";

                sql += "Select a.ProductID as ProductID,a.ProductName,a.MRP,a.DP,a.Rate,a.Qty,ISNULL(c.ReturnQty,0) as PReturn,a.Qty-ISNULL(c.ReturnQty,0) as PRemaing FROm TrnBillDetails a ";
                sql += " LEFT JOIN (Select b.OrderNo,c.ProdID,SUM(ISNULL(c.ReturnQty,0)) ReturnQty  FROM TrnSalesReturnMain b";
                sql += " INNER JOIN TrnSalesReturnDetail c ON b.SReturnNo = c.SReturnNo WHERE OrderNo = '" + BillNo + "' GROUP BY c.ProdID,b.OrderNo) c ON a.BillNo = c.OrderNo AND a.ProductID=c.ProdID";
                sql += " WHERE a.BillNo = '" + BillNo + "' AND a.Qty-ISNULL(c.ReturnQty,0)>0";

                string InvConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(InvConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = sql;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel tempobj = new ProductModel();
                        tempobj.ProductCodeStr = reader["ProductID"] != null ? reader["ProductID"].ToString() : "";
                        tempobj.PReturnQuantity = reader["PReturn"] != null ? Convert.ToDecimal(reader["PReturn"].ToString()) : 0;
                        tempobj.Remaining = reader["PRemaing"] != null ? Convert.ToDecimal(reader["PRemaing"].ToString()) : 0;
                        ProductReturnDetail.Add(tempobj);
                    }
                }


                using (var entity = new InventoryEntities(enttConstr))
                {

                    billdetail = (from r in entity.TrnBillMains
                                  where r.BillNo == BillNo
                                  select new PartyBill
                                  {
                                      BillNo = r.BillNo,
                                      SoldBy = r.SoldBy,
                                      partyCode = r.FCode,
                                      partyName = r.PartyName,
                                      BillDate = r.BillDate,
                                      NetPayable = r.NetPayable,
                                      RoundOff = r.RndOff,
                                      TotalTax = r.TaxAmount,
                                      TotalAmount = r.Amount
                                  }).FirstOrDefault();
                    billdetail.BillDateStr = billdetail.BillDate.ToString("dd-MMM-yyyy");


                    billdetail.ProductList = new List<ProductModel>();
                    billdetail.ProductList = (from r in entity.TrnBillDetails
                                              join t in entity.M_TaxMaster on r.ProductId equals t.ProdCode
                                              where r.BillNo == BillNo
                                              select new ProductModel
                                              {
                                                  Barcode = r.Barcode,
                                                  BatchNo = r.BatchNo,
                                                  ProductName = r.ProductName,

                                                  ProductCodeStr = r.ProductId,
                                                  Rate = r.Rate,
                                                  MRP = r.MRP,
                                                  BV = r.BV,
                                                  BVValue = r.BvValue,
                                                  RP = r.RP,
                                                  RPValue = r.RPValue,
                                                  TaxAmt = r.TaxAmount,
                                                  Amount = r.NetAmount,
                                                  TotalAmount = r.NetAmount,
                                                  Quantity = r.Qty,
                                                  GSTPer = t.VatTax,
                                                  ReturnQty = 0,
                                                  DP = r.DP,
                                                  DiscPer = r.DiscountPer,
                                                  DiscAmt = r.Discount,
                                                  ProdStateCode = t.StateCode,
                                                  PV = r.PV,
                                                  CV = r.CV,
                                                  CommissionPer = r.ProdCommssn,
                                                  CommissionAmt = r.ProdCommssnAmt,
                                                  CVValue = r.CVValue,
                                                  PVValue = r.PVValue
                                              }).ToList();

                    foreach (var record in billdetail.ProductList)
                    {

                        record.StockAvailable = (from stockAvail in entity.Im_CurrentStock
                                                 where stockAvail.BatchCode == record.Barcode.ToString() && stockAvail.ProdId == record.ProductCodeStr.ToString() && stockAvail.FCode.Equals(billdetail.partyCode)
                                                 select stockAvail.Qty
                                                 ).DefaultIfEmpty(0).Sum();
                        record.PReturnQuantity = (from r in ProductReturnDetail where r.ProductCodeStr == record.ProductCodeStr select r.PReturnQuantity).FirstOrDefault();
                        record.Remaining = (from r in ProductReturnDetail where r.ProductCodeStr == record.ProductCodeStr select r.Remaining).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return billdetail;
        }

        public List<PartyBill> GetListOfSupplierBills(string supplier)
        {
            List<PartyBill> billList = null;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {

                    billList = (from r in entity.M_InwardMain
                                where r.SupplierCode == supplier
                                select new PartyBill
                                {
                                    BillNo = r.InwardNo,
                                    BillDate = r.OrderDate
                                }).ToList().OrderByDescending(o => o.BillDate).ToList();

                }
            }
            catch (Exception ex)
            {

            }
            return billList;
        }

        public string GetSalesReturnNumber(string Loggedinparty)
        {
            string returnNo = string.Empty;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    string part1 = (from r in entity.M_ConfigMaster select r.BillPrefix).FirstOrDefault();
                    if (!String.IsNullOrEmpty(part1))
                    {
                        returnNo += part1 + "GR/" + Loggedinparty + "/";
                    }

                    decimal part2 = (from r in entity.TrnSalesReturnMains select r.SRNo).DefaultIfEmpty(0).Max();
                    part2 += 1;
                    returnNo += part2;
                }
            }
            catch (Exception ex)
            {

            }
            return returnNo;
        }

        public ResponseDetail SavePartyTargetDetails(PartyTargetMaster objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            TargetMaster targetMaster = new TargetMaster();
            TargetDetail targetDetail = new TargetDetail();
            string Version = "";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {

                    decimal maxTargetId = 0;
                    maxTargetId = (from r in entity.TargetMasters
                                   select r.TID
                               ).DefaultIfEmpty(0).Max();
                    maxTargetId = maxTargetId + 1;

                    targetMaster.TID = maxTargetId;
                    targetMaster.UserID = objModel.UserID;
                    targetMaster.ToDate = objModel.ToDate;
                    targetMaster.FrmDate = objModel.FrmDate;
                    targetMaster.RecTimeStamp = DateTime.Now;
                    targetMaster.MaxValue = objModel.MaxValue;
                    targetMaster.CommPer = objModel.CommPer;
                    targetMaster.OnValue = "P";

                    entity.TargetMasters.Add(targetMaster);


                    targetDetail.TID = maxTargetId;
                    targetDetail.CatID = objModel.CatId;
                    targetDetail.RecTimeStamp = DateTime.Now;

                    entity.TargetDetails.Add(targetDetail);

                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseMessage = "Saved Successfully!";
                        objResponse.ResponseStatus = "OK";
                    }
                    else
                    {
                        objResponse.ResponseMessage = "Something went wrong!";
                        objResponse.ResponseStatus = "FAILED";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public List<SalesReport> GetRecordToUpdateDelDetails(string FromDate, string ToDate, string PartyCode, string Fcode, string status, string ordtype)
        {
            List<SalesReport> objReport = new List<SalesReport>();
            string WhereCondition = string.Empty;
            string Fld = string.Empty;
            var dataTable = new DataTable();

            try
            {
                DateTime StartDate = new DateTime();
                DateTime EndDate = new DateTime();

                if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                {
                    var SplitDate = FromDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    StartDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    StartDate = StartDate.Date;
                }
                if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                {
                    var SplitDate = ToDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    EndDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    EndDate = EndDate.Date;
                }

                string NewFromDate = StartDate.Date.ToString("dd-MMM-yyyy");
                string NewToDate = EndDate.Date.ToString("dd-MMM-yyyy");

                string wherea = string.Empty;



                if (!string.IsNullOrEmpty(PartyCode) && PartyCode.ToUpper() != "ALL" && PartyCode.ToUpper() != "0")
                {
                    wherea = wherea + " and SoldBy='" + PartyCode.Trim() + "' ";
                }

                if (!string.IsNullOrEmpty(Fcode) && Fcode.ToUpper() != "ALL" && Fcode.ToUpper() != "0")
                {
                    wherea = wherea + " and a.FCode='" + Fcode.Trim() + "' ";
                }

                if (!string.IsNullOrEmpty(status) && status.ToUpper() != "ALL")
                {
                    if (status.ToUpper() == "PENDING")
                        wherea = wherea + " And o.DispatchStatus<>'C'";
                    else
                        wherea = wherea + " And o.DispatchStatus='C'";
                }

                string db = dbName;
                string dbInv = invDbName;
                string sql = "";
                if (ordtype == "M")
                {
                    sql = "Select  a.UserBillNo,BillDate,SoldBy as BillBy,b.PartyName,IsNull(c.IDNo,a.FCode) as Code,Case When (IsNull(c.MemFirstName,'') + ' ' + IsNull(c.MemLastName,'')) = '' Then a.PartyName Else IsNull(c.MemFirstName,'') + ' ' + IsNull(c.MemLastName,'') End as Name,c.Address1+ ', '+c.City + '-' + Cast(c.Pincode as varchar(10)) as MemAddress,a.CourierName,a.DocWeight,a.DocketNo,a.DocketDate as DocketDate,a.DOD as DOD,a.DelvAddress,a.CourierId as CID,a.BillNo,LRDate as DispDate,NetPayable,c.Mobl as MobileNO,a.OrderNo ";
                    sql += "From M_LedgerMaster as b , " + db + "..M_MemberMaster as c,TrnBillMain as a LEFT JOIN " + db + "..TrnOrder o On a.OrderNo=Cast(o.OrderNo as varchar(20)) Where a.BillFor=Cast(c.FormNo as Varchar) AND o.HostIp<>'H' AND a.SoldBy=b.PartyCode AND BillDate between '" + NewFromDate + "' AND '" + NewToDate + "' " + wherea;
                }
                else
                {
                    sql = "Select  a.UserBillNo,a.BillDate,SoldBy as BillBy,b.PartyName,IsNull(c.PartyCode,a.FCode) as Code,ISNULL(c.PartyName,'')  as Name,c.Address1+ ', '+c.CityName + '-' + Cast(c.Pincode as varchar(10)) as MemAddress,a.CourierName,a.DocWeight,a.DocketNo,a.DocketDate as DocketDate,a.DOD as DOD,a.DelvAddress,a.CourierId as CID,a.BillNo,LRDate as DispDate,a.NetPayable,c.MobileNo,a.OrderNo ";
                    sql += "From M_LedgerMaster as b, M_LedgerMaster as c,TrnBillMain as a LEFT JOIN TrnPartyOrderMain o On a.OrderNo=Cast(o.OrderNo as varchar(20)) Where a.FCode=Cast(c.PartyCode as Varchar) AND a.SoldBy=b.PartyCode AND a.BillDate between '" + NewFromDate + "' AND '" + NewToDate + "' " + wherea;
                }
                string InvConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(InvConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = sql;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalesReport tempobj = new SalesReport();

                        tempobj.BillNo = reader["UserBillNo"] != null ? reader["UserBillNo"].ToString() : "";
                        tempobj.BillDate = reader["BillDate"] != System.DBNull.Value ? Convert.ToDateTime(reader["BillDate"]) : (DateTime?)null;
                        tempobj.SoldBy = reader["BillBy"] != null ? reader["BillBy"].ToString() : "";
                        tempobj.PartyName = reader["PartyName"] != null ? reader["PartyName"].ToString() : "";
                        tempobj.PartyCode = reader["Code"] != null ? reader["Code"].ToString() : "";
                        tempobj.Name = reader["Name"] != null ? reader["Name"].ToString() : "";
                        tempobj.CourierName = reader["CourierName"] != null ? reader["CourierName"].ToString() : "";
                        tempobj.DocWeight = reader["DocWeight"] != null ? reader["DocWeight"].ToString() : "";

                        tempobj.DocketNo = reader["DocketNo"] != null ? reader["DocketNo"].ToString() : "";
                        tempobj.DocketDate = reader["DocketDate"] != System.DBNull.Value ? Convert.ToDateTime(reader["DocketDate"]) : DateTime.Now;
                        tempobj.DOD = reader["DOD"] != System.DBNull.Value ? Convert.ToDateTime(reader["DOD"]) : (DateTime?)null;
                        tempobj.DelvAddress = reader["DelvAddress"] != null ? reader["DelvAddress"].ToString() : reader["MemAddress"].ToString();

                        tempobj.CID = reader["CID"] != null ? reader["CID"].ToString() : "";
                        // tempobj.BillNo = reader["BillNo"] != null ? reader["BillNo"].ToString() : "";
                        tempobj.DispDate = reader["DispDate"] != System.DBNull.Value ? Convert.ToDateTime(reader["DispDate"]) : (DateTime?)null;
                        tempobj.DelvAddress = reader["DelvAddress"] != null ? reader["DelvAddress"].ToString() : "";

                        tempobj.NetPayable = reader["NetPayable"] != null ? reader["NetPayable"].ToString() : "";
                        tempobj.MobileNO = reader["MobileNO"] != null ? reader["MobileNO"].ToString() : "";
                        tempobj.OrderNo = reader["OrderNo"] != null ? reader["OrderNo"].ToString() : "";
                        tempobj.DelvAddress = reader["DelvAddress"] != null ? reader["DelvAddress"].ToString() : "";

                        objReport.Add(tempobj);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objReport;
        }

        public ResponseDetail UpdateDeliveryDetails(UpdateDeliveryDetails obj)
        {
            ResponseDetail objresponse = new ResponseDetail();
            int fessid = 0;
            int MaxCode = 0;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    fessid = Convert.ToInt32((from r in entity.M_FiscalMaster where r.ActiveStatus == "Y" select r.FSessId).DefaultIfEmpty(0).Max());
                    MaxCode = Convert.ToInt32((from r in entity.TrnOrderDeliveryDetails select r.SNo).DefaultIfEmpty(0).Max());
                    MaxCode += 1;

                }

                string Sql = string.Empty;
                foreach (var record in obj.DeliverDetailList)
                {
                    DateTime docketDate = record.DocketDate ?? DateTime.Now;
                    string db = dbName;
                    string dbInv = invDbName;
                    Sql += ";INSERT INTO " + db + "..SendSMS (FormNo,Mobileno,SMS) ";
                    Sql += " Select 0,b.MobileNo,'Package/Products, Inv No. '+ a.UserBillNo +' VRID No. '+ b.UserPartyCode +' have been dispatched by " + record.CourierName + ", AWB no. " + record.DocketNo + ", dated " + docketDate.ToString("dd-MMM-yyyy") + ".Team VR' FROM TrnBillmain a,M_LedgerMaster b WHERE a.FCode = b.PartyCode AND LEN(Cast(b.MobileNo as varchar(15))) >= 10 AND(a.CourierName <> '" + record.CourierName + "' OR a.DocketNo <> '" + record.DocketNo + "')  AND a.UserBillNo = '" + record.BillNo + "'";
                    Sql += " UNION ";
                    Sql += " Select b.Formno, b.Mobl, 'Package/Products, Inv No. '+ a.UserBillNo +' VRID No. '+ a.FCode +' have been dispatched by " + record.CourierName + ", AWB no. " + record.DocketNo + ", dated " + docketDate.ToString("dd-MMM-yyyy") + ".Team VR' FROM TrnBillmain a, " + db + "..M_MemberMaster b WHERE a.Formno = b.Formno AND LEN(Cast(b.Mobl as varchar(15))) >= 10 AND(a.CourierName <> '" + record.CourierName + "' OR a.DocketNo <> '" + record.DocketNo + "')  AND a.UserBillNo = '" + record.BillNo + "'";
                    //Sql += "UPDATE TrnBillMain SET CourierId = '" + record.CID +"',";
                    Sql += ";UPDATE TrnBillMain SET CourierName = '" + record.CourierName + "',";
                    Sql += "DocketNo = '" + record.DocketNo + "',";
                    Sql += "DocketDate = " + (record.DocketDate != null ? ("'" + record.DocketDate + "'") : "null") + ",";
                    Sql += "DocWeight = '" + record.DocWeight + "',";
                    //Sql += "DOD = "  + (record.DOD != null ? ("'" + record.DOD + "'") : "null") + ",";
                    Sql += "DelvAddress = '" + record.DelvAddress + "',";
                    Sql += "DelvStatus = 'C', DelvRecTimeStamp = GetDate(), DelvUserId = '" + obj.LoggedInUser + "'";
                    Sql += "WHERE FSessId = '" + fessid + "' AND UserBillNo = '" + record.BillNo + "' ;";
                    //Sql += "UPDATE TrnProductDispatchDetail SET  CourierId='" + record.CID + "',CourierName='" + record.CourierName + "', DocketNo='" + record.DocketNo + "',DocketDate=" + (record.DocketDate != null ? ("'" + record.DocketDate + "'") : "null") + ",DocWeight='" + record.DocWeight + "',DOD=" + (record.DOD != null ? ("'" + record.DOD + "'") : "null") + ",DelvAddress='" + record.DelvAddress + "',DelvStatus='C',DelvRecTimeStamp=GetDate(),DelvUserId='" + obj.LoggedInUser + "' WHERE FSessId='" + fessid + "' AND BillNo='" + record.BillNo + "'; ";
                    Sql += ";UPDATE TrnProductDispatchDetail SET  CourierName='" + record.CourierName + "', DocketNo='" + record.DocketNo + "',DocketDate=" + (record.DocketDate != null ? ("'" + record.DocketDate + "'") : "null") + ",DocWeight='" + record.DocWeight + "',DelvAddress='" + record.DelvAddress + "',DelvStatus='C',DelvRecTimeStamp=GetDate(),DelvUserId='" + obj.LoggedInUser + "' WHERE FSessId='" + fessid + "' AND UserBillNo='" + record.BillNo + "'; ";
                }

                var billnos = (from r in obj.DeliverDetailList select "'" + r.BillNo + "'").ToArray();

                var result = String.Join(", ", billnos.ToArray());

                Sql += "; INSERT INTO TrnOrderDeliveryDetail(SNo, SoldBy, OrderNo, OrderDate, BillNo, BillDate, CourierId, CourierName, DocWeight, DocketNo, DocketDate, DOD, DelvAddress, UserId) ";
                Sql += " SELECT " + MaxCode + " as SNo,SoldBy,OrderNo,OrderDate,BillNo,BillDate,CourierId,CourierName,DocWeight,DocketNo,DocketDate,DOD,DelvAddress,UserId From TrnBillMain Where FSessId='" + fessid + "' And UserBillNo In(" + result + ")";

                SqlTransaction objTrans = null;
                string InvConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(InvConnectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Sql;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objresponse.ResponseStatus = "OK";
                        objresponse.ResponseMessage = "Records Updated Successfully";
                    }
                    else
                    {
                        objresponse.ResponseStatus = "FAILED";
                        objresponse.ResponseMessage = "Something went wrong";
                    }
                }
                catch (Exception ex)
                {

                }


                SC.Close();

            }
            catch (Exception ex)
            {
                objresponse.ResponseStatus = "FAILED";
                objresponse.ResponseMessage = "Something went wrong";
            }

            return objresponse;
        }

        public ResponseDetail CheckForOfferOLD(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                VisionOffer monthOfferList = new VisionOffer();
                VisionOffer EarlyRiserOffer = new VisionOffer();
                var CurrentDate = DateTime.Now;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    EarlyRiserOffer = (from r in entity.VisionOffers where r.OfferDatePart == "D" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable && r.OfferOnBV <= objModel.objProduct.TotalBV select r).OrderByDescending(o => o.OfferOnValue).OrderByDescending(o => o.OfferOnBV).FirstOrDefault();
                    if (EarlyRiserOffer != null)
                    {
                        if (CurrentDate.Date.Day >= EarlyRiserOffer.OfferFromDt.Date.Day && CurrentDate.Date.Day <= EarlyRiserOffer.OfferToDt.Date.Day)
                        {
                            objResponse.ResponseStatus = "Success";
                            var freeproduct = EarlyRiserOffer.FreeProdIDs.Split(',');
                            string productList = string.Empty;
                            string Quant = string.Empty;
                            foreach (var prod in freeproduct)
                            {
                                var product = (from r in entity.M_ProductMaster where r.ProdId == prod select r).FirstOrDefault();
                                productList += product.ProductName + "~" + prod + ",";
                            }
                            productList = productList.Substring(0, productList.Length - 1);
                            objResponse.ResponseMessage = EarlyRiserOffer.OfferDatePart + "-" + EarlyRiserOffer.AID + "-" + productList + "-" + EarlyRiserOffer.FreeProdQty;
                        }
                        else
                        {
                            objResponse.ResponseMessage = "NoOffer";
                        }
                    }
                    else
                    {
                        monthOfferList = (from r in entity.VisionOffers where r.OfferDatePart == "R" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable select r).OrderByDescending(o => o.OfferOnValue).FirstOrDefault();
                        if (monthOfferList != null)
                        {
                            if (CurrentDate.Date >= monthOfferList.OfferFromDt.Date && CurrentDate.Date <= monthOfferList.OfferToDt.Date)
                            {
                                objResponse.ResponseStatus = "Success";
                                var product = (from r in entity.M_ProductMaster where r.ProdId == monthOfferList.FreeProdIDs select r.ProductName).FirstOrDefault();
                                objResponse.ResponseMessage = monthOfferList.OfferDatePart + "-" + monthOfferList.AID + "-" + product + "-" + monthOfferList.FreeProdQty;
                            }
                            else
                            {
                                objResponse.ResponseMessage = "NoOffer";
                            }
                        }
                        else
                        {
                            objResponse.ResponseMessage = "NoOffer";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = "Something went wrong";
            }
            return objResponse;
        }

        public ResponseDetail CheckForOffer(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                VisionOffer monthOfferList = new VisionOffer();
                VisionOffer EarlyRiserOffer = null;
                VisionOffer FirstBillOffer = null;
                var CurrentDate = DateTime.Now;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    string OfferDatePart = ""; int OfferID = 0; string FreeProdIDs = ""; string FreeProdQtys = ""; string confProdIDs = ""; string ConfProdQtys = ""; string offerbillvalue = "0";

                    //if (objModel.objCustomer.IsFirstBill)
                    //{
                    if (!string.IsNullOrEmpty(objModel.objCustomer.Doj))
                    {
                        DateTime dateofjoining = Convert.ToDateTime(objModel.objCustomer.Doj);
                        DateTime checkDate = dateofjoining.AddDays(30);
                        //if (CurrentDate <= checkDate)
                        //{
                        //FirstBillOffer = (from r in entity.VisionOffers where r.OfferType.ToUpper() == "OFFERONVALUE" && r.OfferBillType.ToUpper()=="FIRSTBILL" && r.ActiveStatus.ToUpper() == "Y" && r.SortFirstBy.ToLower() == "B" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable && r.OfferOnBV <= objModel.objProduct.TotalBV select r).OrderByDescending(o => o.OfferOnBV).FirstOrDefault();
                        FirstBillOffer = (from r in entity.VisionOffers where r.OfferType.ToUpper() == "OFFERONVALUE" && r.ActiveStatus.ToUpper() == "Y" && r.SortFirstBy.ToUpper() == "B" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable && r.OfferOnBV <= objModel.objProduct.TotalBV select r).OrderByDescending(o => o.OfferOnBV).FirstOrDefault();
                        if (FirstBillOffer != null)
                        {
                            if (CurrentDate.Date >= FirstBillOffer.OfferFromDt.Date && CurrentDate.Date <= FirstBillOffer.OfferToDt.Date)
                                objResponse.ResponseStatus = "Success";
                            OfferDatePart = FirstBillOffer.OfferDatePart;
                            OfferID = FirstBillOffer.OfferId;
                            FreeProdIDs = FirstBillOffer.FreeProdIDs; FreeProdQtys = FirstBillOffer.FreeProdQtys.ToString();
                            confProdIDs = FirstBillOffer.ConfFreeProdIDs; ConfProdQtys = FirstBillOffer.ConfFreeProdQtys;
                            offerbillvalue = FirstBillOffer.OfferOnValue.ToString();
                        }
                        //}
                    }
                    //}
                    if (FirstBillOffer == null)
                    {
                        EarlyRiserOffer = (from r in entity.VisionOffers where r.OfferType.ToUpper() == "OFFERONVALUE" && r.ActiveStatus.ToUpper() == "Y" && r.SortFirstBy.ToUpper() != "B" && r.OfferDatePart == "D" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable && r.OfferOnBV <= objModel.objProduct.TotalBV select r).OrderByDescending(o => o.OfferOnValue).FirstOrDefault();

                        if (EarlyRiserOffer != null)
                        {
                            if (CurrentDate.Date >= EarlyRiserOffer.OfferFromDt.Date && CurrentDate.Date <= EarlyRiserOffer.OfferToDt.Date)
                            {
                                if (CurrentDate.Date.Day >= EarlyRiserOffer.OfferStartDay && CurrentDate.Date.Day <= EarlyRiserOffer.OfferEndDay)
                                {
                                    objResponse.ResponseStatus = "Success";
                                    OfferDatePart = EarlyRiserOffer.OfferDatePart;
                                    OfferID = EarlyRiserOffer.OfferId;
                                    FreeProdIDs = EarlyRiserOffer.FreeProdIDs; FreeProdQtys = EarlyRiserOffer.FreeProdQtys.ToString();
                                    confProdIDs = EarlyRiserOffer.ConfFreeProdIDs; ConfProdQtys = EarlyRiserOffer.ConfFreeProdQtys;
                                    offerbillvalue = EarlyRiserOffer.OfferOnValue.ToString();
                                }
                            }
                        }
                        else
                        {
                            monthOfferList = (from r in entity.VisionOffers where r.OfferType.ToUpper() == "OFFERONVALUE" && r.ActiveStatus.ToUpper() == "Y" && r.SortFirstBy.ToUpper() != "B" && r.OfferDatePart == "R" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable && r.OfferOnBV <= objModel.objProduct.TotalBV select r).OrderByDescending(o => o.OfferOnValue).FirstOrDefault();
                            if (monthOfferList != null)
                            {
                                if (CurrentDate.Date >= monthOfferList.OfferFromDt.Date && CurrentDate.Date <= monthOfferList.OfferToDt.Date)
                                    objResponse.ResponseStatus = "Success";
                                OfferDatePart = monthOfferList.OfferDatePart;
                                OfferID = monthOfferList.OfferId;
                                FreeProdIDs = monthOfferList.FreeProdIDs; FreeProdQtys = monthOfferList.FreeProdQtys.ToString();
                                confProdIDs = monthOfferList.ConfFreeProdIDs; ConfProdQtys = monthOfferList.ConfFreeProdQtys;
                                offerbillvalue = monthOfferList.OfferOnValue.ToString();
                            }
                        }
                    }
                    if (objResponse.ResponseStatus == "Success")
                    {
                        var freeproduct = FreeProdIDs.Split(',');
                        string productList = string.Empty;
                        //string Quant = string.Empty;
                        foreach (var prod in freeproduct)
                        {
                            if (prod != "")
                            {
                                var product = (from r in entity.M_ProductMaster where r.ProdId == prod select r.ProductName).FirstOrDefault();
                                productList += product + "~" + prod + ",";
                            }
                        }
                        if (productList.Length > 0) productList = productList.Substring(0, productList.Length - 1);
                        string confproductList = ""; string confproductQtyList = "";
                        if (confProdIDs != "")
                        {
                            string[] confproduct = confProdIDs.Split(',');
                            string[] confproductQty = ConfProdQtys.Split(',');
                            for (int i = 0; i < confproduct.Length; i++)
                            {
                                string ProdID_ = confproduct[i];
                                var product = (from r in entity.M_ProductMaster where r.ProdId == ProdID_ select r.ProductName).FirstOrDefault();
                                confproductList += product + "~" + confproduct[i] + ",";
                                confproductQtyList += confproductQty[i].ToString() + ",";
                            }
                            confproductList = confproductList.Substring(0, confproductList.Length - 1);
                        }
                        objResponse.ResponseMessage = OfferDatePart + "δ" + OfferID.ToString() + "δ" + productList + "δ" + FreeProdQtys.ToString() + "δ" + confproductList + "δ" + confproductQtyList + "δ" + offerbillvalue;
                    }
                    else
                        objResponse.ResponseMessage = "NoOffer";
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = "Something went wrong";
            }
            return objResponse;
        }

        public List<OldBills> GetOldBills(string FromDate, string ToDate, string IdNo, string BillNo, string PartyCode)
        {
            List<OldBills> objOldBillList = new List<OldBills>();
            try
            {
                DateTime StartDate = DateTime.Now;
                DateTime EndDate = DateTime.Now;

                string AppConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);

                using (var entity = new InventoryEntities(enttConstr))
                {
                    string WhereCondition = "";

                    if (!string.IsNullOrEmpty(IdNo) && IdNo != "0" && IdNo != "All")
                    {
                        WhereCondition = WhereCondition + "AND a.IDNo='" + IdNo + "'";
                    }

                    if (!string.IsNullOrEmpty(BillNo) && BillNo != "0" && BillNo != "All")
                    {
                        WhereCondition = WhereCondition + "AND a.BillNo='" + BillNo + "'";
                    }
                    if (PartyCode.ToUpper() != "ALL")
                    {
                        WhereCondition = WhereCondition + "AND b.PartyCode='" + PartyCode + "'";
                    }
                    if (!string.IsNullOrEmpty(FromDate) && (!string.IsNullOrEmpty(ToDate)))
                    {
                        if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                        {
                            var SplitDate = FromDate.Split('-');
                            string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                            StartDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                            StartDate = StartDate.Date;
                        }
                        if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                        {
                            var SplitDate = ToDate.Split('-');
                            string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                            EndDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                            EndDate = EndDate.Date;
                        }
                    }
                    string NewFromDate = StartDate.Date.ToString("dd-MMM-yyyy");
                    string NewToDate = EndDate.Date.ToString("dd-MMM-yyyy");
                    if (FromDate != "All" && ToDate != "All")
                    {
                        WhereCondition = WhereCondition + " and a.BillDate>='" + NewFromDate + "' and a.BillDate<='" + NewToDate + "'";
                    }
                    else if (FromDate != "All" && ToDate == "All")
                    {
                        WhereCondition = WhereCondition + " and a.BillDate>='" + NewFromDate + "'";
                    }
                    else if (FromDate == "All" && ToDate != "All")
                    {
                        WhereCondition = WhereCondition + "and a.BillDate<='" + NewToDate + "'";
                    }

                    string query = "Select  0 as Blank,a.BillNo,Replace(Convert(varchar,a.BillDate,106),' ','-') as ODte,a.FCode as IDNO,a.BVValue,a.PartyName as MemName,a.NetPayable as OrderAmt,a.Username,b.PartyName,CASE WHEN a.DRecTimeStamp is null THEN 'No' ELSE 'Deleted' END as IsDeleted From OldBillMain a,M_LedgerMaster b Where b.UserPartyCode=a.Username " + WhereCondition +
              " Order By BillDate DESC";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;

                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objOldBillList.Add(new OldBills
                            {
                                BillNo = reader["BillNo"].ToString(),
                                BillDateStr = reader["ODte"].ToString(),
                                BillDate = DateTime.Parse(reader["ODte"].ToString()),
                                FCode = reader["IDNo"].ToString(),
                                PartyName = reader["MemName"].ToString(),
                                NetPayable = decimal.Parse(reader["OrderAmt"].ToString()),
                                BVValue = decimal.Parse(reader["BVValue"].ToString()),
                                IsDeleted = reader["IsDeleted"].ToString(),
                                Username = reader["Username"].ToString(),
                            });
                        }
                    }


                }
            }
            catch (Exception ex)
            {

            }
            return objOldBillList;
        }

        public List<ProductModel> GetOldBillProducts(string BillNo)
        {
            List<ProductModel> objOrderProductModel = new List<ProductModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objOrderProductModel = (from r in entity.OLDBillDetails
                                            where r.BillNo == BillNo.ToString()
                                            select new ProductModel
                                            {
                                                ProductName = r.ProductName,
                                                DP = r.DP,
                                                ProductCodeStr = r.ProductID,
                                                MRP = r.MRP,
                                                BV = r.PValue,
                                                Amount = r.NetAmount,
                                                DispQty = r.Qty
                                            }
                                          ).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objOrderProductModel;
        }

        public List<kit> GetKitList()
        {
            ResponseDetail objresponse = new ResponseDetail();
            List<kit> kitllist = new List<kit>();

            try
            {
                kitllist.Add(new kit
                {
                    kitName = "Select Kit",
                    kitId = 0,
                    productId = "0"
                });
                using (var entity = new InventoryEntities(enttConstr))
                {
                    var kitllistitem = (from r in entity.M_ProductMaster
                                        where r.ActiveStatus == "Y" && r.PType == "K"
                                        select new kit
                                        {
                                            kitId = r.BrandCode,
                                            kitName = r.ProductName,
                                            productId = r.ProdId
                                        }).ToList();
                    kitllist.AddRange(kitllistitem);
                }
            }
            catch (Exception ex)
            {
                objresponse.ResponseStatus = "FAILED";
                objresponse.ResponseMessage = "Something went wrong";
            }
            return (kitllist);
        }

        public List<PackUnpackProduct> GetPackUnpackProducts(string PackUnpack, decimal KitId, string prodID, string LoginPartyCode)
        {
            ResponseDetail objresponse = new ResponseDetail();
            string db = dbName;
            string dbInv = invDbName;
            List<PackUnpackProduct> kitproductlist = new List<PackUnpackProduct>();
            try
            {
                string Sql = string.Empty;
                if (PackUnpack.ToLower() == "pack")
                {
                    Sql = "Select a.ProdID ,a.ProductName ,SUM(a.Qty ) as Qty ,SUM(b.AvailStock) as AvailStock FROM (";
                    Sql += " Select b.ProdID,b.ProductName,a.Qty,0 as AvailStock FROM " + db + "..M_KitProductDetail a,M_ProductMaster b ";
                    Sql += " WHERE a.ProdID=b.ProdID AND a.KItID=" + KitId + " AND a.RowStatus='Y' AND a.Qty>0 ) a ";
                    Sql += "Left Join";
                    Sql += "(Select b.ProdID,b.ProductName,0 Qty,SUM(a.Qty) as AvailStock FROM IM_CurrentStock a,M_ProductMaster b ";
                    Sql += " WHERE a.ProdID=b.ProdID AND a.FCode='" + LoginPartyCode + "' GROUP BY b.ProdID,b.ProductName) b";
                    Sql += " ON a.ProdID=b.ProdID";
                    Sql += " GROUP BY a.ProdID,a.ProductName";
                }
                else
                {
                    //Sql = "Select b.ProdID,b.ProductName,1 Qty,ISNULL(SUM(a.Qty),0) as AvailStock FROM IM_CurrentStock a RIGHT JOIN M_ProductMaster b ";
                    //Sql += " ON a.ProdID=b.ProdID AND a.FCode='" + LoginPartyCode + "' AND a.ProdID='" + prodID + "' GROUP BY b.ProdID,b.ProductName";
                    Sql = "Select b.ProdID,a.ProductName,1 Qty,ISNULL(AvailStock,0) as AvailStock  FROM M_ProductMaster a LEFT JOIN ( ";
                    Sql += "Select ProdID, ISNULL(SUM(a.Qty), 0) as AvailStock FROM IM_CurrentStock a WHERE a.FCode='" + LoginPartyCode + "' AND a.ProdID = '" + prodID + "'  GROUP BY ProdID) b ";
                    Sql += "ON a.ProdID = b.ProdID WHERE a.ProdID ='" + prodID + "' ";
                }

                string InvConnectionString = InvConstr;
                SqlConnection SC = new SqlConnection(InvConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = Sql;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();
                decimal NoOfKit = 0;
                decimal MaxKit = 10000;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var availStock = Convert.ToString(reader["AvailStock"]);
                        var Qty = Convert.ToString(reader["Qty"]);
                        NoOfKit = Convert.ToDecimal(String.IsNullOrEmpty(availStock) ? "0" : availStock) / Convert.ToDecimal(String.IsNullOrEmpty(Qty) ? "1" : Qty);
                        MaxKit = Math.Min(NoOfKit, MaxKit);
                        PackUnpackProduct tempobj = new PackUnpackProduct();
                        tempobj.ProductId = reader["ProdID"] != null ? reader["ProdID"].ToString() : "";
                        tempobj.ProductName = reader["ProductName"] != null ? reader["ProductName"].ToString() : "";
                        tempobj.Qunatity = reader["Qty"] != null ? reader["Qty"].ToString() : "0";
                        tempobj.AvailStock = reader["AvailStock"] != null ? reader["AvailStock"].ToString() : "0";
                        kitproductlist.Add(tempobj);
                    }
                }

                foreach (PackUnpackProduct tempobj in kitproductlist)
                {
                    tempobj.MaxPack = Convert.ToInt32(MaxKit);
                }
            }
            catch (Exception ex)
            {
                objresponse.ResponseStatus = "FAILED";
                objresponse.ResponseMessage = "Something went wrong";
            }
            return (kitproductlist);
        }

        public ResponseDetail SavePackUnpack(PackUnpack obj)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            MakeKit objmakekit = new MakeKit();
            try
            {
                objmakekit.KitID = obj.kitId;
                objmakekit.KitName = obj.kitName;
                objmakekit.ActiveStatus = "Y";
                if (obj.PackOrUnpack.ToLower() == "pack")
                    objmakekit.ConvType = "B";
                else
                    objmakekit.ConvType = "U";
                objmakekit.UserID = Convert.ToDecimal(obj.UserId);
                objmakekit.RecTimeStamp = DateTime.Now;
                objmakekit.Qty = obj.qunatity;

                int i = 0;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    decimal maxSbillNo = (from result in entity.MakeKits select result.ID).DefaultIfEmpty(0).Max();
                    maxSbillNo = maxSbillNo + 1;
                    objmakekit.ID = maxSbillNo;
                    entity.MakeKits.Add(objmakekit);


                    int fessid = Convert.ToInt32((from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result.FSessId).DefaultIfEmpty(0).Max());
                    string version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();

                    decimal maxStockNo = (from result in entity.Im_CurrentStock select result.StockId).DefaultIfEmpty(0).Max();
                    maxStockNo = maxStockNo + 1;

                    Im_CurrentStock objkit = new Im_CurrentStock();

                    objkit.StockId = maxStockNo;
                    objkit.FSessId = fessid;
                    objkit.SupplierCode = "0";
                    objkit.StockDate = DateTime.Now;
                    objkit.RecTimeStamp = DateTime.Now;
                    if (obj.PackOrUnpack.ToLower() == "pack")
                    {
                        objkit.RefNo = "Pack Kit/" + maxSbillNo;
                        objkit.Qty = obj.qunatity;
                        objkit.BillType = "A";
                        objkit.Remarks = obj.qunatity + " Kits Packed.";
                    }
                    else
                    {
                        objkit.RefNo = "UnPack Kit/" + maxSbillNo;
                        objkit.Qty = (-1 * obj.qunatity);
                        objkit.BillType = "L";
                        objkit.Remarks = obj.qunatity + " Kits UnPacked.";

                        string db = dbName;
                        string dbInv = invDbName;
                        string Sql = " Select b.ProdID,b.ProductName,a.Qty " +
                       "FROM " + db + "..M_KitProductDetail a, M_ProductMaster b WHERE a.ProdID = b.ProdID AND a.KItID = " + obj.kitId +
                       " AND a.ActiveStatus = 'Y' AND a.RowStatus='Y' AND a.Qty>0";

                        string InvConnectionString = InvConstr;
                        SqlConnection SC = new SqlConnection(InvConnectionString);

                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandText = Sql;
                        cmd.Connection = SC;
                        SC.Close();
                        SC.Open();
                        obj.productList.RemoveAt(0);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PackUnpackProduct objpackProd = new PackUnpackProduct();
                                objpackProd.ProductId = reader["ProdID"].ToString();
                                objpackProd.ProductName = reader["ProductName"].ToString();
                                objpackProd.Qunatity = reader["Qty"].ToString();
                                obj.productList.Add(objpackProd);
                            }
                        }

                    }
                    objkit.InvoiceNo = "";
                    objkit.FCode = obj.UserCode;
                    objkit.StockFor = obj.UserCode;
                    objkit.EntryBy = obj.UserCode;
                    objkit.GroupId = 0;
                    objkit.ActiveStatus = "Y";
                    objkit.BType = "P";
                    objkit.SType = "I";
                    objkit.ProdType = "P";

                    var barcodedetail = (from result in entity.M_ProductMaster where result.BrandCode == obj.kitId select result).FirstOrDefault();
                    objkit.ProdId = barcodedetail.ProdId;
                    objkit.Barcode = barcodedetail.Barcode;
                    objkit.BatchCode = Convert.ToString(barcodedetail.BCode);
                    objkit.Version = version;
                    objkit.IsDisp = "N";
                    objkit.UserId = obj.UserId;

                    entity.Im_CurrentStock.Add(objkit);

                    foreach (var product in obj.productList)
                    {
                        maxStockNo = (from result in entity.Im_CurrentStock select result.StockId).DefaultIfEmpty(0).Max();
                        maxStockNo = maxStockNo + 1;

                        Im_CurrentStock objprod = new Im_CurrentStock();

                        objprod.StockId = maxStockNo;
                        objprod.FSessId = fessid;
                        objprod.SupplierCode = "0";
                        objprod.StockDate = DateTime.Now;
                        objprod.RecTimeStamp = DateTime.Now;
                        int prodqty = Convert.ToInt32(product.Qunatity);
                        if (obj.PackOrUnpack.ToLower() == "pack")
                        {
                            objprod.RefNo = "Pack Kit/" + maxSbillNo;
                            objprod.Qty = -1 * obj.qunatity * prodqty;
                            objprod.BillType = "L";
                            objprod.Remarks = obj.qunatity + " Kits Packed.";
                        }
                        else
                        {
                            objprod.RefNo = "UnPack Kit/" + maxSbillNo;
                            objprod.Qty = (obj.qunatity * prodqty);
                            objprod.BillType = "A";
                            objprod.Remarks = obj.qunatity + " Kits UnPacked.";
                        }
                        objprod.InvoiceNo = "";
                        objprod.FCode = obj.UserCode;
                        objprod.StockFor = obj.UserCode;
                        objprod.EntryBy = obj.UserCode;
                        objprod.UserId = obj.UserId;
                        objprod.GroupId = 0;
                        objprod.ActiveStatus = "Y";
                        objprod.BType = "P";
                        objprod.SType = "I";
                        objprod.ProdType = "P";
                        objprod.ProdId = product.ProductId;
                        var prodbarcodedetail = (from result in entity.M_BarCodeMaster where result.ProdId == product.ProductId select result).FirstOrDefault();
                        objprod.Barcode = prodbarcodedetail.BarCode;
                        var prodDetail = (from result in entity.M_ProductMaster where result.ProdId == product.ProductId select result).FirstOrDefault();
                        objprod.BatchCode = Convert.ToString(prodbarcodedetail.BCode);
                        objprod.Version = version;
                        objprod.IsDisp = "N";
                        entity.Im_CurrentStock.Add(objprod);
                    }

                    i = entity.SaveChanges();
                }

                if (i > 0)
                {
                    objResponse.ResponseMessage = "Saved Successfully!";
                    objResponse.ResponseStatus = "OK";
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }
            return objResponse;
        }

        public UpgradeID GetCustomerKitDetail(string IdNo)
        {
            UpgradeID objCustomerDetail = new UpgradeID();
            if (!(string.IsNullOrEmpty(IdNo)))
            {
                try
                {

                    string AppConnectionString = AppConstr;
                    SqlConnection SC = new SqlConnection(AppConnectionString);

                    string query = "select a.Mobl,a.FormNo,a.MemFirstName+' '+ a.MemLastName as Name,a.KitId,a.IDno as IDno,a.Address1+','+a.Address2+','+a.City as Address,a.StateCode as StateCode,a.ActiveStatus as ActiveStatus,a.IsBlock as IsBlock,b.idno as RefId,b.MemFirstName+' '+ b.MemLastName as RefName FROM M_MemberMaster a,M_MemberMaster b WHERE a.RefFormNo=b.FormNo AND a.IDno=@IdNo";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@IdNo", IdNo);
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            objCustomerDetail.MemberName = reader["Name"] != null ? reader["Name"].ToString() : "";
                            objCustomerDetail.KitId = reader["KitId"] != null ? int.Parse(reader["KitId"].ToString()) : 0;
                        }
                        else
                        {
                            objCustomerDetail = new UpgradeID();
                            objCustomerDetail.IDno = "Record does not exists!";
                            objCustomerDetail.MemberName = "";
                        }
                    }
                    SC.Close();
                    if (objCustomerDetail != null)
                    {
                        decimal Ktamt = 0;
                        string KitName = "";
                        string MacAdres = "";
                        decimal BV = 0;

                        query = "select * FROM M_KitMaster WHERE KitId=" + objCustomerDetail.KitId;
                        cmd = new SqlCommand();
                        cmd.CommandText = query;
                        cmd.Connection = SC;
                        SC.Close();
                        SC.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                KitName = reader["KitName"] != null ? reader["KitName"].ToString() : "";
                                Ktamt = reader["KitAmount"] != null ? decimal.Parse(reader["KitAmount"].ToString()) : 0;
                                MacAdres = reader["MacAdrs"] != null ? reader["MacAdrs"].ToString() : "";
                                BV = reader["BV"] != null ? decimal.Parse(reader["BV"].ToString()) : 0;
                                break;
                            }
                        }

                        objCustomerDetail.KitAmount = Ktamt;
                        objCustomerDetail.KitName = KitName;
                        objCustomerDetail.MacAdres = MacAdres;
                        objCustomerDetail.KitBV = BV;
                    }

                    //if (objCustomerDetail.KitAmount == 0)
                    //{
                    //    if (objCustomerDetail.KitId == 1)
                    //    {
                    query = "Select * FROM M_KitMaster WHERE  RowStatus='Y' AND ActiveStatus='Y' AND isBill='N' AND Macadrs not in ('O','P','U') AND TopupSeq>(Select TopupSeq FROM M_KitMaster WHERE KitID in (" + objCustomerDetail.KitId + "))";
                    //    }
                    //}
                    //else if (objCustomerDetail.MacAdres == "O" && objCustomerDetail.KitAmount < 3900)
                    //{
                    //    query = "Select * FROM M_KitMaster WHERE  RowStatus='Y' AND ActiveStatus='Y' AND isBill='N' AND Macadrs not in ('O','P','U') AND TopupSeq>(Select TopupSeq FROM M_KitMaster WHERE KitID in (" + objCustomerDetail.KitId + ")) AND KitAmount>(Select KitAmount FROM M_KitMaster WHERE KitID in (" + objCustomerDetail.KitId + "))";
                    //}
                    //else
                    //{
                    //    if (objCustomerDetail.KitAmount == 2100)
                    //    {
                    //        query = "Select * FROM M_KitMaster WHERE  RowStatus='Y' AND ActiveStatus='Y' AND isBill='N' AND Bv=7100 AND Macadrs not in ('O','P') AND TopupSeq>(Select TopupSeq FROM M_KitMaster WHERE KitID in (" + objCustomerDetail.KitId + "))";
                    //    }
                    //    else
                    //    {
                    //        query = "Select * FROM M_KitMaster WHERE  RowStatus='Y' AND ActiveStatus='Y' AND isBill='N' AND Macadrs not in ('O','P') AND TopupSeq>(Select TopupSeq FROM M_KitMaster WHERE KitID in (" + objCustomerDetail.KitId + "))";
                    //    }

                    //}

                    cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();
                    objCustomerDetail.NewKitList = new List<kits>();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kits newkit = new kits();
                            newkit.kitName = reader["KitName"] != null ? reader["KitName"].ToString() : "";
                            newkit.kitId = reader["KitId"] != null ? int.Parse(reader["KitId"].ToString()) : 0;
                            objCustomerDetail.NewKitList.Add(newkit);
                        }
                    }
                }
                catch (Exception e)
                {
                    objCustomerDetail = new UpgradeID();
                    objCustomerDetail.IDno = "Something went wrong!";
                    objCustomerDetail.MemberName = "";
                }
            }

            return objCustomerDetail;
        }

        public UpgradeID GetKitProductList(string kitId)
        {
            UpgradeID objCustomerDetail = new UpgradeID();
            objCustomerDetail.objListProduct = new List<ProductModel>();
            string query = "";
            if (!(string.IsNullOrEmpty(kitId)))
            {
                try
                {
                    string AppConnectionString = AppConstr;
                    SqlConnection SC = new SqlConnection(AppConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    string promoID = "0";
                    query = "select * FROM M_KitMaster WHERE KitId=" + kitId;
                    cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            promoID = reader["PromoId"] != null ? reader["PromoId"].ToString() : "0";
                            objCustomerDetail.promoId = Convert.ToInt16(promoID);
                            objCustomerDetail.KitAmount = reader["KitAmount"] != null ? Convert.ToDecimal(reader["KitAmount"].ToString()) : 0;
                            break;
                        }
                    }

                    decimal CompStateCode = 0;
                    string db = dbName;
                    string dbInv = invDbName;
                    using (var entity = new InventoryEntities(enttConstr))
                    {
                        CompStateCode = (from r in entity.M_CompanyMaster select r.CompState).FirstOrDefault();
                    }

                    query = "Select a.ProdId,a.ProductName,'1' as Qty,CASE WHEN " + promoID + " = 0 THEN CAST((b.KitAmount * 100 / (100 + t.VatTax)) as numeric(18, 2)) ELSE 0 END as Rate,t.VatTax,CASE WHEN " + promoID + " = 0 THEN CAST((b.KitAmount * 100 / (100 + t.VatTax)) as numeric(18, 2)) ELSE '0' END as NetAmount,CASE WHEN " + promoID + " = 0 THEN b.KitAmount - CAST((b.KitAmount * 100 / (100 + t.VatTax)) as numeric(18, 2)) ELSE '0' END as TaxAmount,'N' as DispStatus,";
                    query += " a.MRP,0 as DiscAmt,a.DP,b.RP as RP,b.bv as bv,'P' as ProdType,c.Barcode,a.Cv,a.PV from " + dbInv + "..M_ProductMaster as a, " + db + "..M_KitMaster as b," + dbInv + "..M_TaxMaster t, " + dbInv + "..V#AvailProdStockBarcodes c ";
                    query += " where a.brandCode=b.KitId and a.activeStatus='Y' and b.ActiveStatus='Y' and b.RowStatus='Y' AND a.ProdID=t.ProdCode AND a.ProdId =c.ProdId AND t.StateCode='" + CompStateCode + "' and  b.kitId='" + kitId + "'";
                    query += " UNION ALL ";
                    query += " Select a.ProdID,a.ProductName,b.Qty as Qty,CASE WHEN " + promoID + "=0 THEN 0 ELSE CAST(((b.MRP-(b.DiscAmt/b.Qty)) * 100/(100+t.VatTax)) as numeric(18,2)) END as Rate,t.VatTax,CASE WHEN " + promoID + "=0 THEN 0 ELSE CAST(((b.MRP-(b.DiscAmt/b.Qty)) * 100/(100+t.VatTax)) as numeric(18,2)) END as NetAmount,CASE WHEN " + promoID + "=0 THEN 0 ELSE (b.MRP-(b.DiscAmt/b.Qty))-CAST(((b.MRP-(b.DiscAmt/b.Qty)) * 100/(100+t.VatTax)) as numeric(18,2)) END as TaxAmount,'N' as DispStatus,"; //a.DP replaced by (b.MRP-(b.DiscAmt/b.Qty))
                    query += " a.MRP,b.DiscAmt,(b.MRP-(b.DiscAmt/b.Qty)),0 as RP,0 as bv,'F' as ProdType,c.Barcode,a.Cv,a.PV from " + dbInv + "..M_ProductMaster as a, " + db + "..M_KitProductDetail as b, " + dbInv + "..V#AvailProdStockBarcodes c," + dbInv + "..M_TaxMaster t  ";
                    query += " where Cast(a.ProdId as varchar(10))=Cast(b.ProdId as varchar(10)) and b.ActiveStatus='Y' and b.RowStatus='Y' AND a.ProdId =c.ProdId AND a.ProdID=t.ProdCode and a.activeStatus='Y' and b.activeStatus='Y' and b.kitId='" + kitId + "'";

                    cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductModel product = new ProductModel();
                            product.IdNo = reader["ProdId"] != null ? Convert.ToString(reader["ProdId"].ToString()) : "";
                            product.ProductName = reader["ProductName"] != null ? reader["ProductName"].ToString() : "";
                            product.Rate = reader["Rate"] != null ? decimal.Parse(reader["Rate"].ToString()) : 0;
                            product.Quantity = reader["Qty"] != null ? decimal.Parse(reader["Qty"].ToString()) : 0;
                            product.Amount = reader["NetAmount"] != null ? decimal.Parse(reader["NetAmount"].ToString()) : 0;
                            product.TaxAmt = reader["TaxAmount"] != null ? decimal.Parse(reader["TaxAmount"].ToString()) : 0;
                            product.DiscAmt = reader["DiscAmt"] != null ? decimal.Parse(reader["DiscAmt"].ToString()) : 0;
                            product.TaxPer = reader["VatTax"] != null ? decimal.Parse(reader["VatTax"].ToString()) : 0;
                            product.DispStatus = reader["DispStatus"] != null ? reader["DispStatus"].ToString() : "";
                            product.RP = reader["RP"] != null ? decimal.Parse(reader["RP"].ToString()) : 0;
                            product.DP = reader["DP"] != null ? decimal.Parse(reader["DP"].ToString()) : 0;
                            product.MRP = reader["MRP"] != null ? decimal.Parse(reader["MRP"].ToString()) : 0;
                            product.BV = reader["bv"] != null ? decimal.Parse(reader["bv"].ToString()) : 0;
                            product.CV = reader["Cv"] != null ? decimal.Parse(reader["Cv"].ToString()) : 0;
                            product.PV = reader["PV"] != null ? decimal.Parse(reader["PV"].ToString()) : 0;
                            product.ProductTye = reader["ProdType"] != null ? reader["ProdType"].ToString() : "";
                            product.Barcode = reader["Barcode"] != null ? reader["Barcode"].ToString() : "";
                            objCustomerDetail.objListProduct.Add(product);
                        }
                    }
                }
                catch (Exception e)
                {
                    objCustomerDetail.objListProduct = new List<ProductModel>();
                }
            }

            return objCustomerDetail;
        }

        public List<PaymodeModel> GetMPaymodes()
        {
            List<PaymodeModel> objResp = new List<PaymodeModel>();
            try
            {
                string AppConnectionString = AppConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                string PModes = "";
                string PIds = "";
                using (var entity = new InventoryEntities(enttConstr))
                {

                    string query = "Select*,LTRIM(RTRIM(AllBank)) as BankType FROM M_PaymodeMaster WHERE ActiveStatus='Y' ORDER BY Paymode";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;

                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PaymodeModel objPaymode = new PaymodeModel();
                            objPaymode.PID = reader["PId"].ToString();
                            objPaymode.Paymode = reader["Paymode"].ToString();
                            objPaymode.TransNoLbl = reader["TransNoLbl"].ToString();
                            objPaymode.TransDateLbl = reader["TransDateLbl"].ToString();
                            objPaymode.IsBankDtl = reader["IsBankDtl"].ToString();
                            objPaymode.IsBranchDtl = reader["IsBranchDtl"].ToString();
                            objPaymode.BankType = reader["BankType"].ToString();
                            objResp.Add(objPaymode);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return objResp;
        }
        public List<BankModel> GetMBanks()
        {
            List<BankModel> objResp = new List<BankModel>();
            try
            {
                string AppConnectionString = AppConstr;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                using (var entity = new InventoryEntities(enttConstr))
                {

                    string query = "Select * FROM M_BankMaster WHERE ActiveStatus='Y' AND RowStatus='Y' ORDER BY BankName";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BankModel objBank = new BankModel();
                            objBank.BankCode = Convert.ToInt32(reader["BankCode"].ToString());
                            objBank.BankName = reader["BankName"].ToString();
                            objBank.BankType = reader["MacAdrs"].ToString();
                            objBank.IsPayTmType = reader["BranchName"].ToString();
                            objResp.Add(objBank);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return objResp;
        }
        public string SaveWalletRequest(WalletRequest objWallet)
        {
            string objResp = "";
            try
            {
                decimal reqNo = 0;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    reqNo = (from result in entity.WalletReqs select result.ReqNo).DefaultIfEmpty(1000).Max();
                    reqNo = reqNo + 1;
                    WalletReq objWReq = new WalletReq();
                    objWReq.ReqNo = reqNo;
                    objWReq.ReqDate = DateTime.Now.Date;
                    objWReq.Amount = objWallet.Amount;
                    objWReq.BankId = objWallet.BankID;// != null ? objWallet.BankID :0; 
                    objWReq.BankName = objWallet.BankName != null ? objWallet.BankName : "";
                    objWReq.BranchName = objWallet.BranchName != null ? objWallet.BranchName : "";
                    objWReq.ChqDate = objWallet.ChqDate;
                    objWReq.ChqNo = objWallet.ChqNo;
                    objWReq.Paymode = objWallet.Paymode;
                    objWReq.PID = objWallet.PID;
                    objWReq.Remarks = objWallet.Remarks != null ? objWallet.Remarks : "";
                    objWReq.ReqBy = objWallet.ReqBy;
                    objWReq.ScannedFile = objWallet.ScannedFileName != null ? objWallet.ScannedFileName : "";
                    objWReq.RecTimeStamp = DateTime.Now;
                    objWReq.TransNo = "0";
                    objWReq.IsApprove = "N";
                    objWReq.ApproveBy = 0;
                    objWReq.ApproveRemark = "";
                    entity.WalletReqs.Add(objWReq);
                    entity.SaveChanges();
                    objResp = "OK";
                }
            }
            catch (DbEntityValidationException e)
            {
            }
            return objResp;
        }

        public List<WalletRequest> GetAllWalletRequest(string PartyCode, string dateType, string FromDate, string ToDate, string IsApproved)
        {
            List<WalletRequest> list = new List<WalletRequest>();
            try
            {
                DateTime startDate = DateTime.Now.AddYears(-5);
                DateTime endDate = DateTime.Now.AddDays(1);

                if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                {
                    startDate = Convert.ToDateTime(FromDate);
                    startDate = startDate.Date;
                }
                if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                {
                    endDate = Convert.ToDateTime(ToDate);
                    endDate = endDate.Date;
                }

                using (var entity = new InventoryEntities(enttConstr))
                {
                    list = (from r in entity.WalletReqs
                            join l in entity.M_LedgerMaster on r.ReqBy equals l.PartyCode

                            select new WalletRequest
                            {
                                ReqNo = r.ReqNo.ToString(),
                                ReqDate = r.ReqDate,
                                ReqDateStr = r.ReqDate.ToString(),
                                Remarks = r.Remarks,
                                ScannedFileName = r.ScannedFile == "" ? "noImg.jpeg" : r.ScannedFile,
                                Amount = r.Amount,
                                BankName = r.BankName,
                                BranchName = r.BranchName,
                                ChqDate = r.ChqDate,
                                ChqNo = r.ChqNo,
                                Paymode = r.Paymode,
                                ReqBy = r.ReqBy,
                                ReqByName = l.PartyName,
                                IsApproved = r.IsApprove,
                                ApprovedDate = r.ApproveDate,
                                ApproveDateStr = r.ApproveDate.ToString(),
                                ApproveRemark = r.ApproveRemark,
                                ApproveBy = r.ApproveBy
                            }).ToList();
                    if (PartyCode != null)//Page: WalletRequestReport
                    {
                        if (PartyCode != null && PartyCode != "" && PartyCode != "All" && PartyCode != "0")
                            list = list.Where(m => m.ReqBy == PartyCode).ToList();
                        if (dateType == "R")
                        {
                            list = list.Where(m => m.ReqDate >= startDate).ToList();
                            list = list.Where(m => m.ReqDate <= endDate).ToList();
                        }
                        else
                        {
                            list = list.Where(m => m.ApprovedDate != null).ToList();
                            list = list.Where(m => m.ApprovedDate >= startDate).ToList();
                            list = list.Where(m => m.ApprovedDate <= endDate).ToList();
                        }

                        if (IsApproved != "A" && IsApproved != null)
                            list = list.Where(m => m.IsApproved == IsApproved).ToList();
                    }
                    else//Page: ApproveWalletRequest 
                        list = list.Where(m => m.IsApproved == "N").ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public ResponseDetail IsDuplicateCourierName(string Name)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {

                    var result = (from r in entity.M_CourierMaster
                                  where r.CourierName == Name
                                  select r
                                ).FirstOrDefault();
                    if (result != null)
                    {
                        objResponse.ResponseStatus = "FAILED";
                        objResponse.ResponseMessage = "Match Found!";
                    }
                    else
                    {
                        objResponse.ResponseStatus = "OK";
                        objResponse.ResponseMessage = "No Match Found!";
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public ResponseDetail SaveCourierDetails(Courier objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            bool isEdited = false;
            try
            {

                M_CourierMaster newCourier = new M_CourierMaster();

                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (objModel.ID > 0)
                    {
                        newCourier = (from result in entity.M_CourierMaster where result.CourierId == objModel.ID select result).FirstOrDefault();
                        isEdited = true;
                    }
                    else
                    {
                       
                        decimal maxNo = (from result in entity.M_CourierMaster select result.CourierId).DefaultIfEmpty(0).Max();
                        maxNo = maxNo + 1;
                        newCourier.CourierId = maxNo;
                    }

                    newCourier.CourierName = objModel.Name;
                    newCourier.UserId = objModel.UserId;
                    newCourier.Website = objModel.Website;
                    newCourier.Remarks = objModel.Remark;
                    newCourier.ActiveStatus = objModel.ActiveStatus;
                    newCourier.RecTimeStamp = DateTime.Now;
                    if (isEdited == false)
                        entity.M_CourierMaster.Add(newCourier);

                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseStatus = "OK";
                        if (isEdited == false)
                            objResponse.ResponseMessage = "Saved Successfully!";
                        else
                            objResponse.ResponseMessage = "Modified Successfully!";
                    }
                    else
                    {
                        objResponse.ResponseStatus = "Failed";
                        objResponse.ResponseMessage = "Something went wrong.";
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "Failed";
                objResponse.ResponseMessage = ex.InnerException.Message;
            }
            return objResponse;
        }

        public ResponseDetail SaveCourierAmount(Courier objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {

                M_CourierDetails newCourier = new M_CourierDetails();
                newCourier.CourierId = objModel.ID;
                newCourier.UserId = objModel.UserId;
                newCourier.Remarks = objModel.Remark;
                newCourier.ActiveStatus = objModel.ActiveStatus;
                newCourier.RecTimeStamp = DateTime.Now;
                newCourier.Weight = objModel.Weight;
                newCourier.Price = objModel.Amount;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    entity.M_CourierDetails.Add(newCourier);
                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseStatus = "OK";
                        objResponse.ResponseMessage = "Saved Successfully!";
                    }
                    else
                    {
                        objResponse.ResponseStatus = "Failed";
                        objResponse.ResponseMessage = "Something went wrong.";
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "Failed";
                objResponse.ResponseMessage = ex.InnerException.Message;
            }
            return objResponse;
        }

        public List<Courier> GetCouierDetailList(decimal id)
        {
            List<Courier> list = new List<Courier>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    list = (from r in entity.M_CourierDetails
                            where r.CourierId == id
                            join t in entity.M_CourierMaster on r.CourierId equals t.CourierId

                            select new Courier
                            {
                                ID = r.Id,
                                Name = t.CourierName,
                                Amount = r.Price,
                                Weight = r.Weight,
                                ActiveStatus = "Y",
                                Remark = r.Remarks
                            }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<Courier> GetCouierList(int CourierID)
        {
            List<Courier> list = new List<Courier>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    list = (from r in entity.M_CourierMaster
                            //where r.CourierId != 0
                            select new Courier
                            {
                                ID = r.CourierId,
                                Name = r.CourierName,
                                Website = r.Website,
                                ActiveStatus = r.ActiveStatus,
                                Remark = r.Remarks
                            }).ToList();
                    if (CourierID > 0)
                        list = list.Where(m => m.ID == CourierID).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public ResponseDetail SaveApproveWaletRequest(List<WalletRequest> objModelList)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                var PartyCode = "";
                decimal Amount = 0;
                string sms, Sql = "";
                string db = dbName;
                string compName = CompName;// System.Configuration.ConfigurationManager.AppSettings["CompName"];
                string InvConnectionString = InvConstr;
                SqlConnection SC1 = new SqlConnection(InvConnectionString);
                using (var entity = new InventoryEntities(enttConstr))
                {
                    foreach (var record in objModelList)
                    {
                        if (record.IsApproved != "N")
                        {
                            var obj = (from r in entity.WalletReqs where r.ReqNo.ToString() == record.ReqNo select r).FirstOrDefault();
                            if (obj.IsApprove == "N")
                            {

                                if (obj.IsApprove != "Y" && record.IsApproved == "Y")
                                {
                                    //if (SC1.State == ConnectionState.Closed)
                                    //    SC1.Open();
                                    Sql = Sql + ";INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,VType,BType,AccDocType,SessID,FSessID) " +
                                           " Select CASE WHEN Max(VoucherNo) is NULL THEN 1 ELSE Max(VoucherNo)+1 END ,Cast(Convert(varchar,Getdate(),106) as Datetime),'','" + obj.ReqBy + "','" + obj.Amount + "','Wallet credited against Req No. " + record.ReqNo + ".','WReq/" + record.ReqNo + "','R','O','Wallet Request Approved.',(Select Max(SessID) FROM " + db + "..M_SessnMaster),(Select Max(FSessID) FROM M_FiscalMaster) FROM TrnVoucher ;";
                                    //SqlCommand cmd = new SqlCommand();
                                    //cmd.CommandText = Sql;
                                    //cmd.Connection = SC1;
                                    //cmd.ExecuteNonQuery();
                                    sms = "Your wallet request [Req. No. " + obj.ReqNo.ToString() + "] for amount " + obj.Amount.ToString() + " has been Approved. info: " + compName;
                                }
                                else
                                    sms = "Your wallet request [Req. No. " + obj.ReqNo.ToString() + "] for amount " + obj.Amount.ToString() + " has been Rejected. info: " + compName;

                                //obj.IsApprove = record.IsApproved;
                                //obj.ApproveRemark = record.ApproveRemark != null ? record.ApproveRemark : "";
                                //obj.ApproveDate = DateTime.Now;
                                //obj.ApproveBy = record.ApproveBy;
                                //PartyCode = obj.ReqBy;
                                //Amount = obj.Amount;

                                Sql = Sql + ";Update WalletReq SET IsApprove='" + record.IsApproved + "',ApproveRemark='" + (record.ApproveRemark != null ? record.ApproveRemark : "") + "'," +
                                    " ApproveDate=Getdate(),ApproveBy='" + record.ApproveBy + "' WHERE ReqNo='" + record.ReqNo + "';";
                                Sql = Sql + ";INSERT INTO " + db + "..SendSMS(Formno,MobileNo,sms,IsSent) " +
                                      " Select 0,MobileNo,'" + sms + "','N' FROM M_LedgerMaster WHERE PartyCode='" + obj.ReqBy + "' AND ISNUMERIC(MobileNo)=1 AND LEN(Cast(MobileNo as varchar(20)))=10;";

                            }
                        }
                    }
                    int i = 0;
                    using (SqlCommand cmd1 = new SqlCommand())
                    {
                        if (SC1.State == ConnectionState.Closed)
                            SC1.Open();
                        Sql = " Begin Try Begin Transaction " + Sql + " Commit Transaction  End Try  BEGIN CATCH  ROLLBACK Transaction END CATCH";
                        cmd1.CommandText = Sql;
                        cmd1.Connection = SC1;
                        i = cmd1.ExecuteNonQuery();
                    }
                    if (SC1.State == ConnectionState.Open)
                        SC1.Close();
                    //entity.SaveChanges();
                    if (i != 0)
                    {
                        objResponse.ResponseStatus = "OK";
                        objResponse.ResponseMessage = "Saved Successfully!";
                    }
                    else
                    {
                        objResponse.ResponseStatus = "Failed";
                        objResponse.ResponseMessage = "Something went wrong.";
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {

            }
            return objResponse;
        }


        public ResponseDetail RejectWalletRequest(string ReqNo, string RejectReason, int UserID)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                var PartyCode = "";
                decimal Amount = 0;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    var obj = (from r in entity.WalletReqs where r.ReqNo.ToString() == ReqNo select r).FirstOrDefault();
                    if (obj.IsApprove == "N")
                    {
                        obj.IsApprove = "R";
                        obj.ApproveRemark = RejectReason != null ? RejectReason : "";
                        obj.ApproveDate = DateTime.Now;
                        obj.ApproveBy = UserID;
                        PartyCode = obj.ReqBy;
                        Amount = obj.Amount;
                    }

                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseStatus = "OK";
                        objResponse.ResponseMessage = "Rejected Successfully!";
                    }
                    else
                    {
                        objResponse.ResponseStatus = "Failed";
                        objResponse.ResponseMessage = "Something went wrong.";
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {

            }
            return objResponse;
        }

        public Courier CourierDetailByweight(int CourierId, int Weight)
        {
            Courier list = new Courier();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    list = (from r in entity.M_CourierDetails
                            where r.CourierId == CourierId && (r.Weight >= Weight)
                            join t in entity.M_CourierMaster on r.CourierId equals t.CourierId
                            select new Courier
                            {
                                ID = r.Id,
                                Name = t.CourierName,
                                Amount = r.Price,
                                Weight = r.Weight,
                                ActiveStatus = "Y",
                                Remark = r.Remarks
                            }).OrderBy(o => o.Weight).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public string ConvertNumbertoWords(decimal rupee)
        {
            long number = Convert.ToInt64(rupee);
            if (number == 0) return "Zero";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " Lakhs ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " Hundred ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "") words += "And ";
                var unitsMap = new[]
                {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };
                var tensMap = new[]
                {
            "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public ResponseDetail UpdateBillDate(List<SalesReport> objModel, int UserID)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";

            objResponse.ResponseStatus = "FAILED";
            try
            {
                List<string> BillNos = new List<string>();
                foreach (var obj in objModel)
                {
                    if (obj.IsUpdate)
                    {
                        //dispatch code                       
                        string InvConnectionString = InvConstr;
                        SqlConnection SC = new SqlConnection(InvConnectionString);
                        string sqlQry = "Exec sp_ChangeBillDate '" + obj.BillDate + "','" + obj.BillNo + "','" + UserID + "';";
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = sqlQry;
                        cmd.Connection = SC;
                        SC.Close();
                        SC.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            objResponse.ResponseMessage = "Orders Dispatched Successfully!";
                            objResponse.ResponseStatus = "OK";
                        }
                        else
                        {
                            objResponse.ResponseMessage = "Something went wrong!";
                            objResponse.ResponseStatus = "FAILED";
                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }

            return objResponse;
        }

        public ResponseDetail saveOfferDynamic(Offer offerDetail)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    VisionOffer offer = new VisionOffer();
                    if (offerDetail.ActionName.ToLower() == "edit")
                    {
                        offer = (from r in entity.VisionOffers where r.OfferId == offerDetail.AID select r).FirstOrDefault();
                    }
                    var SplitDate = offerDetail.OfferFromDtStr.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    offer.OfferFromDt = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));

                    SplitDate = offerDetail.OfferToDtStr.Split('-');
                    NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    offer.OfferToDt = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));

                    if (!string.IsNullOrEmpty(offerDetail.IdDateStr))
                    {
                        SplitDate = offerDetail.IdDateStr.Split('-');
                        NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                        offer.IdDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    }

                    offer.ActiveStatus = offerDetail.ActiveStatus;
                    if (!string.IsNullOrEmpty(offerDetail.ConfFreeProdIDs))
                        offer.ConfFreeProdIDs = offerDetail.ConfFreeProdIDs.Trim(',');
                    else
                        offer.ConfFreeProdIDs = "";
                    if (!string.IsNullOrEmpty(offerDetail.ConfFreeProdQtys))
                        offer.ConfFreeProdQtys = offerDetail.ConfFreeProdQtys.Trim(',');
                    else
                        offer.ConfFreeProdQtys = "";

                    if (!string.IsNullOrEmpty(offerDetail.FreeProdIDs))
                        offer.FreeProdIDs = offerDetail.FreeProdIDs.Trim(',');
                    else
                        offer.FreeProdIDs = "0";

                    if (!string.IsNullOrEmpty(offerDetail.FreeProdQty))
                        offer.FreeProdQtys = offerDetail.FreeProdQty.Trim(',');
                    else
                        offer.FreeProdQtys = "";

                    offer.OfferExceptSubCat = "0";

                    offer.OfferBillType = offerDetail.OfferBillType;

                    offer.OfferOnBV = offerDetail.OfferOnBV ?? 0;
                    offer.OfferOnValue = offerDetail.OfferOnValue;

                    offer.ForNewIds = offerDetail.ForNewIds;
                    offer.IdStaus = offerDetail.IdStaus;
                    offer.OfferType = "OfferOnValue";
                    offer.IdDays = offerDetail.IdDays;

                    offer.ContinueForMonth = offerDetail.ForMonth;
                    if (offer.OfferBillType.ToLower() == "all")
                    {
                        offer.SortFirstBy = "V";
                    }
                    else
                    {
                        offer.SortFirstBy = "B";
                    }

                    offer.OfferDatePart = offerDetail.OfferDatePart;
                    offer.CombineWithOffer = offerDetail.CombineWithOffer;
                    if (offerDetail.OfferDatePart == "D")
                    {
                        offer.OfferStartDay = offerDetail.OfferStartDay;
                        offer.OfferEndDay = offerDetail.OfferEndDay;
                    }
                    if (offerDetail.ActionName.ToLower() == "add")
                    {
                        var maxaid = (from r in entity.VisionOffers select r.OfferId).DefaultIfEmpty(0).Max();
                        maxaid += 1;
                        offer.OfferId = maxaid;
                        entity.VisionOffers.Add(offer);
                    }
                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseMessage = "Saved Successfully!";
                        objResponse.ResponseStatus = "OK";
                    }
                    else
                    {
                        objResponse.ResponseMessage = "Something went wrong!";
                        objResponse.ResponseStatus = "FAILED";
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }
            return objResponse;
        }

        public List<Offer> GetAllExtraPVOfferList()
        {
            List<Offer> offers = new List<Offer>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    offers = (from r in entity.VisionOffers
                              where r.OfferType.ToLower() == "extrapv"
                              join t in entity.VisionOfferProducts on r.OfferId equals t.OfferID
                              group t by new { r.OfferId, r.OfferFromDt, r.OfferToDt, r.OfferOnBV, r.ActiveStatus }
                              into OfferResult
                              select new Offer
                              {
                                  ActiveStatus = OfferResult.Key.ActiveStatus,
                                  AID = OfferResult.Key.OfferId,
                                  OfferFromDt = OfferResult.Key.OfferFromDt,
                                  OfferToDt = OfferResult.Key.OfferToDt,
                                  OfferOnBV = OfferResult.Key.OfferOnBV,
                                  TotalOfferBv = OfferResult.Sum(m => m.OfferPV),
                                  TotalOfferBvper = OfferResult.Sum(m => m.OfferPVPer)
                              }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return offers;
        }

        public List<Offer> GetAllOfferOnValueList()
        {
            List<Offer> offers = new List<Offer>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    offers = (from r in entity.VisionOffers
                              where r.OfferType.ToLower() == "offeronvalue"
                              select new Offer
                              {
                                  ActiveStatus = r.ActiveStatus,
                                  AID = r.OfferId,
                                  OfferFromDt = r.OfferFromDt,
                                  OfferToDt = r.OfferToDt,
                                  OfferOnValue = r.OfferOnValue,
                                  OfferOnBV = r.OfferOnBV,
                              }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return offers;
        }

        public ResponseDetail SaveExtraPVOffer(Offer objOffer)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                VisionOffer offer = new VisionOffer();
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (objOffer.ActionName.ToLower() == "edit")
                    {
                        offer = (from r in entity.VisionOffers where r.OfferId == objOffer.AID select r).FirstOrDefault();
                    }

                    var SplitDate = objOffer.OfferFromDtStr.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    offer.OfferFromDt = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));

                    SplitDate = objOffer.OfferToDtStr.Split('-');
                    NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    offer.OfferToDt = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));

                    offer.OfferDatePart = "R";

                    offer.ConfFreeProdIDs = "";
                    offer.FreeProdIDs = "";
                    offer.ConfFreeProdQtys = "";
                    offer.OfferOnBV = objOffer.OfferOnBV ?? 0;
                    offer.OfferOnValue = 0;
                    offer.OfferExceptSubCat = "0";
                    offer.SortFirstBy = "";
                    offer.FreeProdQtys = "";
                    offer.ActiveStatus = objOffer.ActiveStatus;
                    offer.OfferBillType = "All";
                    offer.ForNewIds = "A";
                    offer.IdStaus = "A";
                    offer.IdDays = 0;
                    offer.OfferType = "ExtraPV";
                    int maxaid = 0;
                    if (objOffer.ActionName.ToLower() == "add")
                    {

                        maxaid = (from r in entity.VisionOffers select r.OfferId).DefaultIfEmpty(0).Max();
                        maxaid += 1;
                        offer.OfferId = maxaid;
                        entity.VisionOffers.Add(offer);

                    }
                    else
                    {
                        maxaid = objOffer.AID;
                        List<VisionOfferProduct> offerproduct = (from r in entity.VisionOfferProducts where r.OfferID == maxaid select r).ToList();
                        foreach (var record in offerproduct)
                        {
                            entity.VisionOfferProducts.Remove(record);
                        }
                    }

                    foreach (var product in objOffer.objProductList)
                    {
                        VisionOfferProduct prod = new VisionOfferProduct();
                        prod.ProdID = product.ProductCode.ToString();
                        prod.ProdName = product.ProductName.ToString();
                        prod.Qty = product.Qty;
                        prod.OfferID = maxaid;
                        prod.OfferPV = product.PVValue;
                        prod.OfferPVPer = product.PVPer;
                        prod.RectimeStamp = DateTime.Now;
                        prod.ActiveStatus = "Y";
                        prod.IsConfirm = "Y";
                        prod.IsBuyProduct = "N";
                        prod.IsBVApplied = "N";
                        entity.VisionOfferProducts.Add(prod);
                    }
                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseMessage = "Saved Successfully!";
                        objResponse.ResponseStatus = "OK";
                    }
                    else
                    {
                        objResponse.ResponseMessage = "Something went wrong!";
                        objResponse.ResponseStatus = "FAILED";
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }
            return objResponse;
        }



        public List<OfferProduct> getfreeproducts(int id)
        {
            List<OfferProduct> objProductList = new List<OfferProduct>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    VisionOffer offer = (from r in entity.VisionOffers
                                         where r.OfferId == id
                                         select r).FirstOrDefault();

                    string[] ConfirmProd = offer.ConfFreeProdIDs.Split(',');
                    string[] ConfirmQty = offer.ConfFreeProdQtys.Split(',');
                    string[] FreeProd = offer.FreeProdIDs.Split(',');
                    string[] FreeProdQty = offer.FreeProdQtys.Split(',');
                    List<OfferProduct> products = new List<OfferProduct>();
                    for (var i = 0; i < ConfirmProd.Length; i++)
                    {
                        OfferProduct prod = new OfferProduct();
                        if (!string.IsNullOrEmpty(ConfirmProd[i]) && ConfirmProd[i] != "0")
                        {
                            prod.ProductCode = ConfirmProd[i];
                            decimal p = decimal.Parse(ConfirmProd[i]);
                            prod.ProductName = (from r in entity.M_ProductMaster where r.ProductCode == p select r.ProductName).FirstOrDefault();
                            prod.Qty = int.Parse(ConfirmQty[i]);
                            prod.Confirm = "Y";
                            objProductList.Add(prod);
                        }
                    }

                    for (var i = 0; i < FreeProd.Length; i++)
                    {
                        OfferProduct prod = new OfferProduct();
                        if (!string.IsNullOrEmpty(FreeProd[i]) && FreeProd[i] != "0")
                        {
                            prod.ProductCode = FreeProd[i];
                            decimal p = decimal.Parse(FreeProd[i]);
                            prod.ProductName = (from r in entity.M_ProductMaster where r.ProductCode == p select r.ProductName).FirstOrDefault();
                            prod.Qty = int.Parse(FreeProdQty[i]);
                            prod.Confirm = "N";
                            objProductList.Add(prod);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objProductList;
        }



        public List<OfferProduct> CheckForExtraPVOffer(string OfferIDs)
        {
            List<OfferProduct> objResponse = new List<OfferProduct>();
            try
            {
                int OfferID = Convert.ToInt16(OfferIDs);
                VisionOffer PVOffer = null;
                DateTime currentDate = DateTime.Now;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    PVOffer = (from r in entity.VisionOffers
                               where r.OfferId == OfferID
                               select r).FirstOrDefault();
                    if (PVOffer != null)
                    {
                        //objResponse = Getoffer(PVOffer.OfferId);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public List<PartyModel> GetPartyBalance()
        {
            List<PartyModel> objpartyList = new List<PartyModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objpartyList = (from party in entity.V_PartyBalance
                                    orderby party.PartyName
                                    select new PartyModel
                                    {
                                        PartyCode = party.PartyCode,
                                        PartyName = party.PartyName,
                                        StateCode = party.StateCode,
                                        GroupId = party.GroupID,
                                        UserPartyCode = party.UserPartyCode,
                                        CreditLimit = party.Balance ?? 0
                                    }
                                 ).ToList();


                }
            }
            catch (Exception ex)
            {

            }
            return objpartyList;
        }

        public ResponseDetail DebitCreditWallet(Wallet objWallet)
        {
            ResponseDetail objResponse = new ResponseDetail();
            TrnVoucher objvoucher = new TrnVoucher();


            string InvConnectionString = InvConstr;
            SqlConnection SC = new SqlConnection(InvConnectionString);
            if (objWallet.Amount > 0)
            {
                try
                {
                    decimal FsessId = 0;
                    using (var entity = new InventoryEntities(enttConstr))
                    {
                        FsessId = (from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result.FSessId).Max();
                    }
                    //string Sql = ";INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,Refno,VType,BType,AccDocType,SessID,FSessID) " +
                    //                           " Select CASE WHEN Max(VoucherNo) is NULL THEN 1 ELSE Max(VoucherNo)+1 END ,Cast(Convert(varchar,Getdate(),106) as Datetime),'','" + obj.ReqBy + "','" + obj.Amount + "','Wallet credited against Req No. " + record.ReqNo + ".','WReq/" + record.ReqNo + "','R','O','Wallet Request Approved.',(Select Max(SessID) FROM " + db + "..M_SessnMaster),(Select Max(FSessID) FROM M_FiscalMaster) FROM TrnVoucher ;";
                    string query = "INSERT INTO TrnVoucher(VoucherNo,VoucherDate,DrTo,Crto,Amount,Narration,VType,SessID,FSessId,RefNo) ";
                    query += "Select CASE WHEN Max(VoucherNo) is NULL THEN 1 ELSE Max(VoucherNo)+1 END ,Cast(Convert(varchar,Getdate(),106) as Datetime),";
                    if (!string.IsNullOrEmpty(objWallet.DrCr) && objWallet.DrCr.ToLower() == "credit")
                    {
                        query += "'','" + objWallet.FCode + "',";
                    }
                    else if (!string.IsNullOrEmpty(objWallet.DrCr) && objWallet.DrCr.ToLower() == "debit")
                    {
                        query += "'" + objWallet.FCode + "','',";
                    }
                    query += objWallet.Amount + ",'" + objWallet.Narration + "','R','" + FsessId + "','" + FsessId + "','Manual Debit/Credit.' FROM TrnVoucher";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    int i = cmd.ExecuteNonQuery();

                    SC.Close();

                    if (i > 0)
                    {
                        objResponse.ResponseMessage = "Saved Successfully";
                        objResponse.ResponseStatus = "OK";
                    }
                    else
                    {
                        objResponse.ResponseMessage = "Something went wrong!";
                        objResponse.ResponseStatus = "FAILED";
                    }

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                objResponse.ResponseMessage = "Please enter valid amount!";
                objResponse.ResponseStatus = "FAILED";
            }
            return objResponse;
        }

        public List<Offer> GetAllBuyThisGetThatOfferList(string IsActive, bool InDateRange)
        {
            List<Offer> offers = new List<Offer>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    offers = (from r in entity.VisionOffers
                              where r.OfferType.ToLower() == "buythisgetthat"
                              select new Offer
                              {
                                  ActiveStatus = r.ActiveStatus,
                                  AID = r.OfferId,
                                  OfferFromDt = r.OfferFromDt,
                                  OfferToDt = r.OfferToDt,
                                  OfferName = r.OfferName
                              }).ToList();
                }
                if (!string.IsNullOrEmpty(IsActive) && IsActive == "Active")
                {
                    offers = offers.Where(r => r.ActiveStatus.ToUpper() == "Y").ToList();
                }
                if (InDateRange)
                {
                    DateTime current = DateTime.Now;
                    offers = offers.Where(r => r.OfferFromDt.Date <= current.Date && current.Date <= r.OfferToDt.Date).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return offers;
        }

        public ResponseDetail SaveBuyThisGetThatOffer(Offer objOffer)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                VisionOffer offer = new VisionOffer();
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (objOffer.ActionName.ToLower() == "edit")
                    {
                        offer = (from r in entity.VisionOffers where r.OfferId == objOffer.AID select r).FirstOrDefault();
                    }

                    var SplitDate = objOffer.OfferFromDtStr.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    offer.OfferFromDt = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));

                    SplitDate = objOffer.OfferToDtStr.Split('-');
                    NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    offer.OfferToDt = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));

                    offer.OfferDatePart = "R";

                    offer.ConfFreeProdIDs = "";
                    offer.FreeProdIDs = "";
                    offer.ConfFreeProdQtys = "";
                    offer.OfferOnBV = 0;
                    offer.OfferOnValue = 0;
                    offer.OfferExceptSubCat = "0";
                    offer.SortFirstBy = "";
                    offer.FreeProdQtys = "";
                    offer.OfferBillType = "All";
                    offer.ForNewIds = "A";
                    offer.IdStaus = "A";
                    offer.IdDays = 0;
                    offer.ActiveStatus = objOffer.ActiveStatus;
                    offer.OfferType = "BuythisGetthat";
                    offer.OfferName = objOffer.OfferName;

                    int maxaid = 0;
                    if (objOffer.ActionName.ToLower() == "add")
                    {
                        maxaid = (from r in entity.VisionOffers select r.OfferId).DefaultIfEmpty(0).Max();
                        maxaid += 1;
                        offer.OfferId = maxaid;
                        entity.VisionOffers.Add(offer);

                    }
                    else
                    {
                        maxaid = objOffer.AID;
                        List<VisionOfferProduct> offerproduct = (from r in entity.VisionOfferProducts where r.OfferID == maxaid select r).ToList();
                        foreach (var record in offerproduct)
                        {
                            entity.VisionOfferProducts.Remove(record);
                        }
                    }

                    foreach (var product in objOffer.objProductList)
                    {
                        VisionOfferProduct prod = new VisionOfferProduct();
                        prod.ProdID = product.ProductCode.ToString();
                        prod.ProdName = product.ProductName.ToString();
                        prod.Qty = product.Qty;
                        prod.OfferID = maxaid;
                        prod.OfferPV = 0;
                        prod.OfferPVPer = 0;
                        prod.RectimeStamp = DateTime.Now;
                        prod.ActiveStatus = "Y";
                        prod.IsConfirm = "N";
                        prod.IsBuyProduct = product.IsParent ? "Y" : "N";
                        prod.OnMRP = product.OnMRP ?? "N";
                        prod.IsBVApplied = product.IsBvApplied ?? "N";
                        entity.VisionOfferProducts.Add(prod);
                    }
                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseMessage = "Saved Successfully!";
                        objResponse.ResponseStatus = "OK";
                    }
                    else
                    {
                        objResponse.ResponseMessage = "Something went wrong!";
                        objResponse.ResponseStatus = "FAILED";
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }
            return objResponse;
        }


        public List<Offer> GetValidOfferList(string Doj, string UpgradeDate, string IsFirstBill, string ActiveStatus)
        {
            List<Offer> OfferList = new List<Offer>();
            try
            {

                var CurrentDate = DateTime.Now;
                int CurrentDay = CurrentDate.Date.Day;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    List<VisionOffer> offers = new List<VisionOffer>();

                    offers = (from r in entity.VisionOffers
                              where r.ActiveStatus == "Y" && (r.OfferDatePart == "R" || (r.OfferDatePart == "D" && r.OfferStartDay <= CurrentDay && r.OfferEndDay >= CurrentDay))
                              select r).ToList();

                    OfferList = (from r in offers
                                 where r.OfferFromDt.Date <= CurrentDate.Date && r.OfferToDt.Date >= CurrentDate.Date
                                 select new Offer
                                 {
                                     AID = r.OfferId,
                                     OfferOnBV = r.OfferOnBV,
                                     OfferOnValue = r.OfferOnValue,
                                     OfferType = r.OfferType,
                                     ForNewIds = r.ForNewIds,
                                     OfferBillType = r.OfferBillType,
                                     OfferName = r.OfferName
                                 }).OrderBy(r => r.OfferType).ToList();

                    //if (IsFirstBill == "true")
                    //{
                    //    OfferList = (from r in OfferList where r.OfferBillType.ToLower() != "repurchase" select r).ToList();
                    //}
                    //else
                    //{
                    //    OfferList = (from r in OfferList where r.OfferBillType.ToLower() != "firstbill" select r).ToList();
                    //}                                                         
                }
            }
            catch (Exception ex)
            {

            }
            return OfferList;
        }

        public ResponseDetail SelectedOfferDetail(string Offer)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                VisionOffer OfferDetail = new VisionOffer();

                var CurrentDate = DateTime.Now;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    string OfferDatePart = ""; int OfferID = 0; string FreeProdIDs = ""; string FreeProdQtys = ""; string confProdIDs = ""; string ConfProdQtys = ""; string offerbillvalue = "0"; string CombineWithOffer = "0";
                    decimal offerId = 0;
                    if (!string.IsNullOrEmpty(Offer))
                    {
                        offerId = Convert.ToDecimal(Offer);
                    }

                    OfferDetail = (from r in entity.VisionOffers where r.OfferId == offerId select r).FirstOrDefault();
                    if (OfferDetail != null)
                    {
                        objResponse.ResponseStatus = "Success";
                        OfferDatePart = OfferDetail.OfferDatePart;
                        OfferID = OfferDetail.OfferId;
                        FreeProdIDs = OfferDetail.FreeProdIDs; FreeProdQtys = OfferDetail.FreeProdQtys.ToString();
                        confProdIDs = OfferDetail.ConfFreeProdIDs; ConfProdQtys = OfferDetail.ConfFreeProdQtys;
                        offerbillvalue = OfferDetail.OfferOnValue.ToString();
                        CombineWithOffer = OfferDetail.CombineWithOffer.ToString();
                    }

                    if (objResponse.ResponseStatus == "Success")
                    {
                        var freeproduct = FreeProdIDs.Split(',');
                        string productList = string.Empty;
                        foreach (var prod in freeproduct)
                        {
                            if (prod != "")
                            {
                                var product = (from r in entity.M_ProductMaster where r.ProdId == prod select r.ProductName).FirstOrDefault();
                                productList += product + "~" + prod + ",";
                            }
                        }
                        if (productList.Length > 0) productList = productList.Substring(0, productList.Length - 1);
                        string confproductList = ""; string confproductQtyList = "";
                        if (confProdIDs != "")
                        {
                            string[] confproduct = confProdIDs.Split(',');
                            string[] confproductQty = ConfProdQtys.Split(',');
                            for (int i = 0; i < confproduct.Length; i++)
                            {
                                string ProdID_ = confproduct[i];
                                var product = (from r in entity.M_ProductMaster where r.ProdId == ProdID_ select r.ProductName).FirstOrDefault();
                                confproductList += product + "~" + confproduct[i] + ",";
                                confproductQtyList += confproductQty[i].ToString() + ",";
                            }
                            confproductList = confproductList.Substring(0, confproductList.Length - 1);
                        }
                        objResponse.ResponseMessage = OfferDatePart + "δ" + OfferID.ToString() + "δ" + productList + "δ" + FreeProdQtys.ToString() + "δ" + confproductList + "δ" + confproductQtyList + "δ" + offerbillvalue + "δ" + CombineWithOffer;
                    }
                    else
                        objResponse.ResponseMessage = "NoOffer";
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = "Something went wrong";
            }
            return objResponse;
        }

        public ResponseDetail saveOfferDynamicNew(Offer offerDetail)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    VisionOffer offer = new VisionOffer();
                    if (offerDetail.ActionName.ToLower() == "edit")
                    {
                        offer = (from r in entity.VisionOffers where r.OfferId == offerDetail.AID select r).FirstOrDefault();
                    }

                    var SplitDate = offerDetail.OfferFromDtStr.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    offer.OfferFromDt = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));

                    SplitDate = offerDetail.OfferToDtStr.Split('-');
                    NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    offer.OfferToDt = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));

                    if (!string.IsNullOrEmpty(offerDetail.IdDateStr))
                    {
                        SplitDate = offerDetail.IdDateStr.Split('-');
                        NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                        offer.IdDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    }

                    offer.ActiveStatus = offerDetail.ActiveStatus;

                    offer.OfferExceptSubCat = "0";

                    offer.OfferBillType = offerDetail.OfferBillType;

                    offer.OfferOnBV = offerDetail.OfferOnBV ?? 0;
                    offer.OfferOnValue = offerDetail.OfferOnValue;

                    offer.IsPVApplicable = offerDetail.ExtraPVApplicable;
                    if (offer.IsPVApplicable == "Y")
                    {
                        offer.ExtraPV = offerDetail.PVValue;
                    }
                    else
                    {
                        offer.ExtraPV = 0;
                    }

                    offer.ForNewIds = offerDetail.ForNewIds;

                    offer.IdStaus = offerDetail.IdStaus;

                    offer.OfferType = offerDetail.OfferType;

                    offer.IdDays = offerDetail.IdDays;

                    offer.OfferBillType = offerDetail.OfferBillType;

                    offer.OfferName = offerDetail.OfferName;
                    offer.ConfFreeProdIDs = "";
                    offer.ConfFreeProdQtys = "";
                    offer.FreeProdIDs = "";

                    offer.ContinueForMonth = offerDetail.ForMonth;
                    if (offer.OfferBillType.ToLower() == "all")
                    {
                        offer.SortFirstBy = "V";
                    }
                    else
                    {
                        offer.SortFirstBy = "B";
                    }

                    offer.OfferDatePart = offerDetail.OfferDatePart;
                    if (offerDetail.OfferDatePart == "D")
                    {
                        offer.OfferStartDay = offerDetail.OfferStartDay;
                        offer.OfferEndDay = offerDetail.OfferEndDay;
                    }
                    int maxaid = 0;
                    if (offerDetail.ActionName.ToLower() == "add")
                    {
                        maxaid = (from r in entity.VisionOffers select r.OfferId).DefaultIfEmpty(0).Max();
                        maxaid += 1;
                        offer.OfferId = maxaid;
                        entity.VisionOffers.Add(offer);

                    }
                    else
                    {
                        maxaid = offerDetail.AID;
                        List<VisionOfferProduct> offerproduct = (from r in entity.VisionOfferProducts where r.OfferID == maxaid select r).ToList();
                        foreach (var record in offerproduct)
                        {
                            entity.VisionOfferProducts.Remove(record);
                        }
                    }

                    foreach (var product in offerDetail.objProductList)
                    {
                        VisionOfferProduct prod = new VisionOfferProduct();
                        prod.ProdID = product.ProductCode.ToString();
                        prod.ProdName = product.ProductName.ToString();
                        prod.Qty = product.Qty;
                        prod.OfferID = maxaid;
                        prod.OfferPV = product.PVValue;
                        prod.OfferPVPer = product.PVPer;
                        prod.RectimeStamp = DateTime.Now;
                        prod.ActiveStatus = "Y";
                        prod.IsConfirm = product.Confirm ?? "N";
                        prod.IsBuyProduct = product.IsParent ? "Y" : "N";
                        prod.OnMRP = product.OnMRP ?? "N";
                        prod.IsBVApplied = product.IsBvApplied ?? "N";
                        prod.scheme = product.Scheme;
                        prod.discount = product.Discount;
                        prod.discountPer = product.DiscountPer;
                        prod.ForRupee = product.Rupee;

                        entity.VisionOfferProducts.Add(prod);
                    }
                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseMessage = "Saved Successfully!";
                        objResponse.ResponseStatus = "OK";
                    }
                    else
                    {
                        objResponse.ResponseMessage = "Something went wrong!";
                        objResponse.ResponseStatus = "FAILED";
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }
            return objResponse;
        }

        public ResponseDetail CheckForOfferNew(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                VisionOffer monthOfferList = new VisionOffer();
                VisionOffer EarlyRiserOffer = null;
                VisionOffer FirstBillOffer = null;
                var CurrentDate = DateTime.Now;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    string OfferDatePart = ""; int OfferID = 0; string FreeProdIDs = ""; string FreeProdQtys = ""; string confProdIDs = ""; string ConfProdQtys = ""; string offerbillvalue = "0";

                    //if (objModel.objCustomer.IsFirstBill)
                    //{
                    if (!string.IsNullOrEmpty(objModel.objCustomer.Doj))
                    {
                        DateTime dateofjoining = Convert.ToDateTime(objModel.objCustomer.Doj);
                        DateTime checkDate = dateofjoining.AddDays(30);
                        //if (CurrentDate <= checkDate)
                        //{
                        //FirstBillOffer = (from r in entity.VisionOffers where r.OfferType.ToUpper() == "OFFERONVALUE" && r.OfferBillType.ToUpper()=="FIRSTBILL" && r.ActiveStatus.ToUpper() == "Y" && r.SortFirstBy.ToLower() == "B" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable && r.OfferOnBV <= objModel.objProduct.TotalBV select r).OrderByDescending(o => o.OfferOnBV).FirstOrDefault();
                        FirstBillOffer = (from r in entity.VisionOffers where r.OfferType.ToUpper() == "OFFERONVALUE" && r.ActiveStatus.ToUpper() == "Y" && r.SortFirstBy.ToUpper() == "B" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable && r.OfferOnBV <= objModel.objProduct.TotalBV select r).OrderByDescending(o => o.OfferOnBV).FirstOrDefault();
                        if (FirstBillOffer != null)
                        {
                            if (CurrentDate.Date >= FirstBillOffer.OfferFromDt.Date && CurrentDate.Date <= FirstBillOffer.OfferToDt.Date)
                                objResponse.ResponseStatus = "Success";
                            OfferDatePart = FirstBillOffer.OfferDatePart;
                            OfferID = FirstBillOffer.OfferId;
                            FreeProdIDs = FirstBillOffer.FreeProdIDs; FreeProdQtys = FirstBillOffer.FreeProdQtys.ToString();
                            confProdIDs = FirstBillOffer.ConfFreeProdIDs; ConfProdQtys = FirstBillOffer.ConfFreeProdQtys;
                            offerbillvalue = FirstBillOffer.OfferOnValue.ToString();
                        }
                        //}
                    }
                    //}
                    if (FirstBillOffer == null)
                    {
                        EarlyRiserOffer = (from r in entity.VisionOffers where r.OfferType.ToUpper() == "OFFERONVALUE" && r.ActiveStatus.ToUpper() == "Y" && r.SortFirstBy.ToUpper() != "B" && r.OfferDatePart == "D" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable && r.OfferOnBV <= objModel.objProduct.TotalBV select r).OrderByDescending(o => o.OfferOnValue).FirstOrDefault();

                        if (EarlyRiserOffer != null)
                        {
                            if (CurrentDate.Date >= EarlyRiserOffer.OfferFromDt.Date && CurrentDate.Date <= EarlyRiserOffer.OfferToDt.Date)
                            {
                                if (CurrentDate.Date.Day >= EarlyRiserOffer.OfferStartDay && CurrentDate.Date.Day <= EarlyRiserOffer.OfferEndDay)
                                {
                                    objResponse.ResponseStatus = "Success";
                                    OfferDatePart = EarlyRiserOffer.OfferDatePart;
                                    OfferID = EarlyRiserOffer.OfferId;
                                    FreeProdIDs = EarlyRiserOffer.FreeProdIDs; FreeProdQtys = EarlyRiserOffer.FreeProdQtys.ToString();
                                    confProdIDs = EarlyRiserOffer.ConfFreeProdIDs; ConfProdQtys = EarlyRiserOffer.ConfFreeProdQtys;
                                    offerbillvalue = EarlyRiserOffer.OfferOnValue.ToString();
                                }
                            }
                        }
                        else
                        {
                            monthOfferList = (from r in entity.VisionOffers where r.OfferType.ToUpper() == "OFFERONVALUE" && r.ActiveStatus.ToUpper() == "Y" && r.SortFirstBy.ToUpper() != "B" && r.OfferDatePart == "R" && r.OfferOnValue <= objModel.objProduct.TotalNetPayable && r.OfferOnBV <= objModel.objProduct.TotalBV select r).OrderByDescending(o => o.OfferOnValue).FirstOrDefault();
                            if (monthOfferList != null)
                            {
                                if (CurrentDate.Date >= monthOfferList.OfferFromDt.Date && CurrentDate.Date <= monthOfferList.OfferToDt.Date)
                                    objResponse.ResponseStatus = "Success";
                                OfferDatePart = monthOfferList.OfferDatePart;
                                OfferID = monthOfferList.OfferId;
                                FreeProdIDs = monthOfferList.FreeProdIDs; FreeProdQtys = monthOfferList.FreeProdQtys.ToString();
                                confProdIDs = monthOfferList.ConfFreeProdIDs; ConfProdQtys = monthOfferList.ConfFreeProdQtys;
                                offerbillvalue = monthOfferList.OfferOnValue.ToString();
                            }
                        }
                    }
                    if (objResponse.ResponseStatus == "Success")
                    {
                        var freeproduct = FreeProdIDs.Split(',');
                        string productList = string.Empty;
                        //string Quant = string.Empty;
                        foreach (var prod in freeproduct)
                        {
                            if (prod != "")
                            {
                                var product = (from r in entity.M_ProductMaster where r.ProdId == prod select r.ProductName).FirstOrDefault();
                                productList += product + "~" + prod + ",";
                            }
                        }
                        if (productList.Length > 0) productList = productList.Substring(0, productList.Length - 1);
                        string confproductList = ""; string confproductQtyList = "";
                        if (confProdIDs != "")
                        {
                            string[] confproduct = confProdIDs.Split(',');
                            string[] confproductQty = ConfProdQtys.Split(',');
                            for (int i = 0; i < confproduct.Length; i++)
                            {
                                string ProdID_ = confproduct[i];
                                var product = (from r in entity.M_ProductMaster where r.ProdId == ProdID_ select r.ProductName).FirstOrDefault();
                                confproductList += product + "~" + confproduct[i] + ",";
                                confproductQtyList += confproductQty[i].ToString() + ",";
                            }
                            confproductList = confproductList.Substring(0, confproductList.Length - 1);
                        }
                        objResponse.ResponseMessage = OfferDatePart + "δ" + OfferID.ToString() + "δ" + productList + "δ" + FreeProdQtys.ToString() + "δ" + confproductList + "δ" + confproductQtyList + "δ" + offerbillvalue;
                    }
                    else
                        objResponse.ResponseMessage = "NoOffer";
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = "Something went wrong";
            }
            return objResponse;
        }

        public Offer SelectedOfferDetailNew(string Offer)
        {
            Offer OfferDetail = new Offer();
            try
            {
                var CurrentDate = DateTime.Now;
                using (var entity = new InventoryEntities(enttConstr))
                {
                    decimal offerId = 0;
                    if (!string.IsNullOrEmpty(Offer))
                    {
                        offerId = Convert.ToDecimal(Offer);
                    }

                    OfferDetail = (from r in entity.VisionOffers
                                   where r.OfferId == offerId
                                   select new Offer
                                   {
                                       ForNewIds = r.ForNewIds,
                                       IdDate = r.IdDate,
                                       IdDays = r.IdDays,
                                       IdStaus = r.IdStaus,
                                       OfferBillType = r.OfferBillType,
                                       ExtraPVApplicable = r.IsPVApplicable,
                                       PVValue = r.ExtraPV,
                                       OfferOnBV = r.OfferOnBV,
                                       OfferOnValue = r.OfferOnValue
                                   }).FirstOrDefault();
                    if (OfferDetail != null)
                    {
                        OfferDetail.ParentProductList = new List<OfferProduct>();
                        OfferDetail.ParentProductList = (from r in entity.VisionOfferProducts
                                                         where r.OfferID == offerId && r.IsBuyProduct == "Y"
                                                         select new OfferProduct
                                                         {
                                                             ProductName = r.ProdName,
                                                             Qty = r.Qty,
                                                             OnMRP = r.OnMRP,
                                                             IsParent = true
                                                         }).ToList();

                        OfferDetail.objProductList = new List<OfferProduct>();
                        OfferDetail.objProductList = (from r in entity.VisionOfferProducts
                                                      where r.OfferID == offerId && r.IsBuyProduct == "N"
                                                      select new OfferProduct
                                                      {
                                                          IsParent = false,
                                                          ProductName = r.ProdName,
                                                          Qty = r.Qty,
                                                          Scheme = r.scheme,
                                                          Discount = r.discount,
                                                          DiscountPer = r.discountPer,
                                                          PVPer = r.OfferPVPer,
                                                          PVValue = r.OfferPV,
                                                          Rupee = r.ForRupee
                                                      }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return OfferDetail;
        }

        public List<Offer> GetAllOffers()
        {
            List<Offer> offers = new List<Offer>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    offers = (from r in entity.VisionOffers

                              select new Offer
                              {
                                  ActiveStatus = r.ActiveStatus,
                                  AID = r.OfferId,
                                  OfferFromDt = r.OfferFromDt,
                                  OfferToDt = r.OfferToDt,
                                  OfferOnValue = r.OfferOnValue,
                                  OfferOnBV = r.OfferOnBV,
                                  OfferType = r.OfferType,
                                  OfferName = r.OfferName
                              }).OrderBy(r => r.OfferType).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return offers;
        }

        public ResponseDetail SaveCourierReturn(string BillNo, string ReturnDate, string Reason)
        {
            ResponseDetail objresponse = new ResponseDetail();
            int fessid = 0;
            int MaxCode = 0;
            try
            {

                using (var entity = new InventoryEntities(enttConstr))
                {
                    var billno = (from r in entity.TrnBillMains where r.UserBillNo == BillNo select r.BillNo).FirstOrDefault();
                    var recordToUpdate = (from r in entity.TrnOrderDeliveryDetails where r.BillNo == billno select r).OrderBy(o => o.RecTimeStamp).FirstOrDefault();
                    if (recordToUpdate != null)
                    {
                        var SplitDate = ReturnDate.Split('-');
                        string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                        var BillReturnDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                        recordToUpdate.ReturnDate = BillReturnDate;
                        recordToUpdate.ReturnRemark = Reason;
                        int i = entity.SaveChanges();
                        if (i > 0)
                        {
                            objresponse.ResponseStatus = "OK";
                            objresponse.ResponseMessage = "Saved Successfully!!";
                        }
                    }
                    else
                    {
                        objresponse.ResponseStatus = "OK";
                        objresponse.ResponseMessage = "Order is not dispathed yet.";
                    }
                }

            }
            catch (Exception ex)
            {
                objresponse.ResponseStatus = "FAILED";
                objresponse.ResponseMessage = "Something went wrong";
            }

            return objresponse;
        }

        public ResponseDetail SaveToWishList(PartyOrderModel objPartyOrder)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {

                    foreach (var product in objPartyOrder.objListProduct)
                    {
                        var previouslist = (from r in entity.StockOrderWishlists where r.OrderBy == objPartyOrder.OrderBy && r.ProductId == product.ProductCodeStr && r.ActiveStatus == "Y" select r).FirstOrDefault();
                        if (previouslist != null)
                        {
                            previouslist.ActiveStatus = "N";
                        }
                        StockOrderWishlist objWishList = new StockOrderWishlist();
                        objWishList.ActiveStatus = "Y";
                        objWishList.OrderBy = objPartyOrder.OrderBy;
                        objWishList.OrderTo = objPartyOrder.OrderTo;
                        objWishList.ProductId = product.ProductCodeStr;
                        objWishList.ProductName = product.ProductName;
                        objWishList.OrderDate = DateTime.Now;
                        objWishList.Qunatity = Convert.ToString(product.Quantity);
                        objWishList.RecTimeStamp = DateTime.Now;
                        entity.StockOrderWishlists.Add(objWishList);
                    }
                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseStatus = "OK";
                        objResponse.ResponseMessage = "Cart Saved Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = "Something went wrong";
            }
            return objResponse;
        }

        public List<ProductModel> GetFromWishList(string id)
        {
            List<ProductModel> objResponse = new List<ProductModel>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objResponse = (from r in entity.StockOrderWishlists
                                   where r.OrderBy == id && r.ActiveStatus == "Y"
                                   select new ProductModel
                                   {
                                       ProductCodeStr = r.ProductId,
                                       ProductName = r.ProductName,
                                       QtyStr = r.Qunatity
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }


        public ResponseDetail SaveIssueSampleProducts(DistributorBillModel products)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                decimal maxJNo = 0;
                decimal? FsessId = 0;
                string JvNo = "";
                string version = "";
                using (var entity = new InventoryEntities(enttConstr))
                {

                    FsessId = (from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result.FSessId).DefaultIfEmpty(0).Max();
                    DateTime CurrentJvDate = DateTime.Now;

                    var maxrecord = (from result in entity.TrnSampleProducts select result).OrderByDescending(o => o.RecTimeStamp).Take(1).FirstOrDefault();
                    var maxSbillNo = (decimal)0;
                    if (maxrecord != null)
                    {
                        maxSbillNo = Convert.ToDecimal(maxrecord.TransNo);
                    }

                    maxSbillNo += 1;
                    string strMaxUserSBillNo = maxSbillNo.ToString();
                    if (strMaxUserSBillNo.Count() < 4)
                    {
                        var countNum = strMaxUserSBillNo.Count();
                        var ToBeAddedDigits = 4 - countNum;
                        for (var j = 0; j < ToBeAddedDigits; j++)
                        {
                            strMaxUserSBillNo = "0" + strMaxUserSBillNo;
                        }
                    }
                    JvNo = "Less/" + strMaxUserSBillNo;
                    version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();
                    foreach (var sample in products.objListProduct)
                    {
                        TrnSampleProduct sampleprod = new TrnSampleProduct();
                        sampleprod.ActiveStatus = "Y";
                        sampleprod.RecTimeStamp = DateTime.Now;
                        sampleprod.UserID = products.objCustomer.UserDetails.UserId;
                        sampleprod.TransNo = strMaxUserSBillNo.ToString();

                        var SplitDate = products.BillDateStr.Split('-');
                        string NewDate = (SplitDate[1].Length == 1 ? "0" + SplitDate[1] : SplitDate[1]) + "/" + (SplitDate[0].Length == 1 ? "0" + SplitDate[0] : SplitDate[0]) + "/" + SplitDate[2];
                        var BillDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                        sampleprod.TransDate = BillDate;
                        sampleprod.Remarks = products.objCustomer.Remarks ?? "";
                        sampleprod.SoldBy = "";
                        sampleprod.PartyName = products.objCustomer.PartyName ?? "";
                        sampleprod.ProdID = sample.ProductCodeStr;
                        sampleprod.ProductName = sample.ProductName;
                        sampleprod.Barcode = sample.Barcode ?? "";
                        sampleprod.BatchNo = sample.BatchNo ?? "";
                        sampleprod.Qty = sample.Quantity;
                        sampleprod.RefNo = products.objCustomer.ReferenceIdNo ?? "";
                        sampleprod.BillNO = JvNo;

                        entity.TrnSampleProducts.Add(sampleprod);



                        Im_CurrentStock objCurrentStock = new Im_CurrentStock();
                        objCurrentStock.FSessId = FsessId ?? 0;
                        objCurrentStock.SupplierCode = "0";
                        objCurrentStock.StockDate = CurrentJvDate;
                        objCurrentStock.RefNo = JvNo;
                        objCurrentStock.FCode = products.objCustomer.UserDetails.FCode;
                        objCurrentStock.GroupId = products.objCustomer.UserDetails.GroupId;
                        objCurrentStock.ProdId = sample.ProductCodeStr;
                        objCurrentStock.BatchCode = sample.BatchNo;
                        objCurrentStock.Barcode = sample.Barcode;

                        objCurrentStock.SType = "O";
                        objCurrentStock.Qty = -(sample.Quantity);
                        objCurrentStock.BType = "L";
                        objCurrentStock.Remarks = "Stock Lessed";
                        objCurrentStock.BillType = "L";

                        objCurrentStock.ActiveStatus = "Y";
                        objCurrentStock.EntryBy = products.objCustomer.UserDetails.PartyCode;
                        objCurrentStock.StockFor = products.objCustomer.UserDetails.FCode;
                        objCurrentStock.RecTimeStamp = DateTime.Now;
                        objCurrentStock.UserId = products.objCustomer.UserDetails.UserId;
                        objCurrentStock.Version = version;
                        objCurrentStock.IsDisp = "N";
                        objCurrentStock.InvoiceNo = "";
                        objCurrentStock.ProdType = "P";
                        objCurrentStock.DispQty = 0;

                        entity.Im_CurrentStock.Add(objCurrentStock);

                    }

                    int i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseMessage = "Saved Successfully";
                        objResponse.ResponseStatus = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseMessage = ex.InnerException.InnerException.Message;
                objResponse.ResponseStatus = "FAILED";

            }
            return objResponse;
        }

        private void ShiftDataintoTemptable(string TblName, string TempTblName, string columns, string wherecond)
        {
            try
            {
                string AppConnectionString = InvConstr;
                using (SqlConnection SC = new SqlConnection(AppConnectionString))
                {
                    string query = "INSERT " + TempTblName + " Select *" + columns + " FROM " + TblName + " WHERE 1=1 " + wherecond;
                    SC.Open();
                    SqlCommand cmd = new SqlCommand(query, SC);
                    cmd.ExecuteNonQuery();
                    SC.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public SendLoginSM getOTP(int userId)
        {
            var otp = new SendLoginSM();
            try
            {
                using (var entities = new InventoryEntities(enttConstr))
                {
                    otp = (from r in entities.SendLoginSMS where r.UserId == userId && r.IsExpired == "N" select r).OrderByDescending(r => r.RecTimeStamp).FirstOrDefault();
                    DateTime currentTime = DateTime.Now;
                    if (otp != null)
                    {
                        TimeSpan diff = currentTime - otp.RecTimeStamp;
                        double minutes = diff.TotalMinutes;
                        if (minutes > 240)
                        {
                            otp.IsExpired = "Y";
                            entities.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return otp;
        }


        public async Task<ResponseDetail> VerifyOTP(string Enteredotp, int userId)
        {
            ResponseDetail response = new ResponseDetail();
            try
            {
                using (var entities = new InventoryEntities(enttConstr))
                {
                    var otp = await Task.Run(() => (from r in entities.SendLoginSMS where r.UserId == userId select r).OrderByDescending(r => r.RecTimeStamp).FirstOrDefault());
                    DateTime currentTime = DateTime.Now;
                    TimeSpan diff = currentTime - otp.RecTimeStamp;
                    double minutes = diff.TotalMinutes;
                    if (minutes < 240 && otp.OTP == Enteredotp)
                    {
                        otp.SentTime = DateTime.Now;
                        otp.IsExpired = "Y";
                        response.ResponseStatus = "true";
                        response.ResponseMessage = "OK";
                        await Task.Run(() => entities.SaveChanges());
                    }
                    else if (otp.OTP == Enteredotp)
                    {
                        response.ResponseStatus = "true";
                        response.ResponseMessage = "Entered OTP is Expired.";
                    }
                    else
                    {
                        response.ResponseStatus = "true";
                        response.ResponseMessage = "Invalid OTP.";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public ResponseDetail ActivateIdWithPackage(UpgradeID objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            DistributorBillModel TempDistributor = new DistributorBillModel();

            decimal maxUserSBillNo = 0;
            decimal? SessId = 0;
            string billPrefix = "";
            decimal maxSbillNo = 0;
            decimal? FsessId = 0;
            string billseries = "";
            string UserBillNo = "";
            string version = "";
            string BillNo = "";
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            try
            {
                //** Added om 21Jan19 (Code Start)
                List<string> ProdIds = new List<string>();
                List<string> OrderIds = new List<string>();

                string OutStockProdIDs = "";
                int Count_ = 0;
                foreach (var obj in objModel.objListProduct)
                {
                    ProdIds = new List<string>();
                    decimal stock = CheckStock(obj.ProductCodeStr, objModel.objCustomer.UserDetails.PartyCode);
                    if (stock < obj.Quantity)
                    {
                        OutStockProdIDs = OutStockProdIDs + obj.ProductCodeStr + ",";
                        Count_ += 1;
                    }
                }
                if (Count_ > 0 && OutStockProdIDs.Length > 1)
                {
                    OutStockProdIDs = OutStockProdIDs.Substring(0, OutStockProdIDs.Length - 1);
                    objResponse.ResponseMessage = "Stock not Found of Products " + OutStockProdIDs + "!";
                    objResponse.ResponseStatus = "FAILED";
                }
                //** Added om 21Jan19 (Code End)
                else
                {
                    //string InvConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryServices"].ConnectionString;
                    //string AppConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                    SqlConnection SC = new SqlConnection(AppConstr);
                    SqlConnection SC1 = new SqlConnection(InvConstr);

                    string query = "Select Max(SessID) as MaxSessId from M_SessnMaster";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            SessId = decimal.Parse(reader["MaxSessId"].ToString());
                        }
                    }

                    using (var entity = new InventoryEntities(enttConstr))
                    {
                        CustomerDetail objCustomerDetail = new CustomerDetail();
                        objCustomerDetail = GetCustInfo(objModel.IDno);
                        maxSbillNo = (from result in entity.TrnBillMains select result.SBillNo).DefaultIfEmpty(0).Max();
                        maxSbillNo = maxSbillNo + 1;
                        FsessId = (from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result.FSessId).DefaultIfEmpty(0).Max();

                        billseries = (from result in entity.M_FiscalMaster select result.BillSeries).FirstOrDefault();
                        billPrefix = (from result in entity.M_ConfigMaster select result.BillPrefix).FirstOrDefault();
                        maxUserSBillNo = (from result in entity.TrnBillMains where result.FSessId == FsessId && result.SoldBy == objModel.objCustomer.UserDetails.PartyCode && result.BillType != "S" select result.UserSBillNo).DefaultIfEmpty(0).Max();
                        maxUserSBillNo = maxUserSBillNo + 1;
                        UserBillNo = billPrefix + "/" + billseries.Trim() + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxUserSBillNo;
                        BillNo = billPrefix + "/" + objModel.objCustomer.UserDetails.PartyCode + "/" + maxSbillNo;
                        version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();



                        List<TrnBillData> tempTableList = new List<TrnBillData>();
                        try
                        {
                            List<ProductModel> objListProductModel = new List<ProductModel>();
                            foreach (var obj in objModel.objListProduct)
                            {
                                TrnBillData objDTBillData = new TrnBillData();

                                objDTBillData.FSessId = FsessId ?? 0;
                                objDTBillData.SessId = SessId ?? 0;
                                objDTBillData.SBillNo = maxSbillNo;
                                objDTBillData.BillNo = BillNo;
                                objDTBillData.RefNo = "";
                                objDTBillData.BillDate = DateTime.Now.Date;
                                objDTBillData.CType = "M";
                                objDTBillData.SoldBy = objModel.objCustomer.UserDetails.PartyCode;
                                objDTBillData.BillBy = objModel.objCustomer.UserDetails.PartyCode;
                                objDTBillData.FType = "M";
                                objDTBillData.FCode = objCustomerDetail.IdNo;
                                objDTBillData.PartyName = objCustomerDetail.Name;
                                objDTBillData.SupplierId = 0;
                                objDTBillData.ChDDNo = 0;
                                objDTBillData.ChDate = DateTime.Now;
                                objDTBillData.ChAmt = 0;
                                objDTBillData.BankCode = 0;
                                objDTBillData.BankName = "";
                                objDTBillData.FormNo = objCustomerDetail.FormNo;
                                objDTBillData.TotalTaxAmount = objModel.objProduct.TotalTaxAmount;
                                objDTBillData.TotalSTaxAmount = 0;//check
                                objDTBillData.Discount = 0;
                                objDTBillData.TotalKitBvValue = objModel.objProduct.TotalBV;
                                objDTBillData.TotalBvValue = objModel.objProduct.TotalBV;
                                objDTBillData.TotalCVValue = objModel.objProduct.TotalCV;
                                objDTBillData.TotalPVValue = objModel.objProduct.TotalPV;
                                objDTBillData.TotalRPValue = objModel.objProduct.TotalRP;
                                objDTBillData.CashDiscPer = 0;
                                objDTBillData.CashDiscAmount = 0;
                                objDTBillData.NetPayable = objModel.objProduct.TotalAmount + objModel.objProduct.TotalTaxAmount;
                                objDTBillData.TotalAmount = objModel.objProduct.TotalAmount;
                                objDTBillData.RndOff = 0;//check
                                objDTBillData.CardAmount = 0;
                                objDTBillData.PayMode = "";
                                objDTBillData.PayPrefix = "";
                                objDTBillData.BvTransfer = "N";
                                objDTBillData.Remarks = "Upgraded by ";
                                objDTBillData.DispatchStatus = "Y";
                                objDTBillData.LR = "0";
                                objDTBillData.LRDate = DateTime.Now;
                                objDTBillData.TransporterName = "";
                                objDTBillData.DispatchTo = "";
                                objDTBillData.FreightType = "";
                                objDTBillData.FreightAmt = 0;
                                objDTBillData.Series = "";//check
                                objDTBillData.Scratch = "";
                                objDTBillData.RefId = 0;
                                objDTBillData.RefName = "";
                                objDTBillData.JType = "";
                                objDTBillData.Unit = 0;
                                objDTBillData.BillTo = "R";
                                objDTBillData.PSessId = 0;
                                objDTBillData.BillFor = "RB";
                                objDTBillData.DcNo = "";
                                objDTBillData.Imported = "N";
                                objDTBillData.IsReceive = "R";
                                objDTBillData.IsCredit = "F";
                                objDTBillData.BillType = "B";
                                objDTBillData.TotalDiscountAmt = 0;
                                objDTBillData.VDiscountAmt = 0;
                                objDTBillData.ReceiverID = "";
                                objDTBillData.ReceiverName = "";
                                objDTBillData.ReceiverMNo = "";

                                if (objModel.promoId == 1)
                                    objDTBillData.ReceiverIDProof = "E";
                                else
                                    objDTBillData.ReceiverIDProof = "";

                                objDTBillData.TotalFPoint = 0;
                                objDTBillData.CashReward = 0;
                                objDTBillData.RecvAmount = 0;
                                objDTBillData.ReturnToCustAmt = 0;
                                objDTBillData.ActiveStatus = "Y";
                                objDTBillData.RecTimeStamp = DateTime.Now;
                                objDTBillData.UserId = objModel.objCustomer.UserDetails.UserId;
                                objDTBillData.UserName = objModel.objCustomer.UserDetails.UserName;
                                objDTBillData.Version = version;
                                objDTBillData.DelvPlace = "";
                                objDTBillData.PaymentDtl = "Cash: " + objDTBillData.NetPayable;
                                objDTBillData.IDType = "";
                                objDTBillData.BranchName = "";
                                objDTBillData.LocId = 0;
                                objDTBillData.LocName = "";
                                objDTBillData.Pincode = "";
                                objDTBillData.CourierId = 0;
                                objDTBillData.CourierName = "";
                                objDTBillData.ProductId = obj.ProductCodeStr;
                                objDTBillData.ProductName = obj.ProductName;
                                objDTBillData.Barcode = obj.Barcode ?? "";
                                objDTBillData.BatchNo = obj.BatchNo ?? (obj.Barcode ?? "");
                                objDTBillData.Qty = obj.Quantity;
                                objDTBillData.MRP = obj.MRP ?? 0;
                                objDTBillData.DP = obj.DP ?? 0;
                                objDTBillData.Rate = obj.Rate ?? 0;
                                objDTBillData.BV = obj.BV ?? 0;
                                objDTBillData.BVValue = objDTBillData.BV * objDTBillData.Qty;
                                objDTBillData.CV = obj.CV ?? 0;
                                objDTBillData.CVValue = objDTBillData.CV * objDTBillData.Qty;
                                objDTBillData.PV = obj.PV ?? 0;
                                objDTBillData.PVValue = objDTBillData.PV * objDTBillData.Qty;
                                objDTBillData.RP = obj.RP ?? 0;
                                objDTBillData.RPValue = objDTBillData.RP * objDTBillData.Qty;
                                objDTBillData.IsKitBV = "N";
                                if (objCustomerDetail.StateCode == objModel.objCustomer.UserDetails.StateCode)
                                {
                                    objDTBillData.TaxAmount = 0;
                                    objDTBillData.Tax = 0;
                                    objDTBillData.CGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.CGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.SGST = obj.TaxPer / 2 ?? 0;
                                    objDTBillData.SGSTAmt = obj.TaxAmt / 2 ?? 0;
                                    objDTBillData.TaxType = "S";
                                }
                                else
                                {
                                    objDTBillData.TaxAmount = obj.TaxAmt ?? 0;
                                    if (obj.OldTaxAmount != 0 && obj.OldTaxAmount != obj.TaxAmt)
                                    {
                                        objDTBillData.TaxAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.TaxAmount) + 0.01).ToString());
                                        objDTBillData.NetAmount = Decimal.Parse((Convert.ToDouble(objDTBillData.NetAmount) - 0.01).ToString());
                                    }
                                    objDTBillData.Tax = obj.TaxPer ?? 0;
                                    objDTBillData.CGST = 0;
                                    objDTBillData.CGSTAmt = 0;
                                    objDTBillData.SGST = 0;
                                    objDTBillData.SGSTAmt = 0;
                                    objDTBillData.TaxType = "I";
                                }
                                objDTBillData.DiscountPer = 0;
                                objDTBillData.Discount = obj.DiscAmt ?? 0;
                                objDTBillData.NetAmount = obj.Amount;
                                objDTBillData.DSeries = "";
                                objDTBillData.DImported = "N";
                                objDTBillData.IMEINo = "";
                                objDTBillData.BNo = "";
                                objDTBillData.ItemType = "";
                                objDTBillData.VDiscount = 0;
                                objDTBillData.VDiscountValue = 0;
                                objDTBillData.FPoint = 0;
                                objDTBillData.FPointValue = 0;
                                objDTBillData.ProdCommssn = 0;
                                objDTBillData.ProdCommssnAmt = 0;
                                objDTBillData.OrdStatus = "";
                                objDTBillData.OrdQty = 0;
                                objDTBillData.RemQty = 0;
                                objDTBillData.DP1 = 0;
                                objDTBillData.DReason = "";
                                objDTBillData.DUserId = 0;
                                objDTBillData.DRecTimeStamp = null;
                                objDTBillData.DocWeight = 0;
                                objDTBillData.DocketNo = "";
                                objDTBillData.DOD = null;
                                objDTBillData.DelvAddress = "";
                                objDTBillData.OrderNo = "";
                                objDTBillData.OrderDate = null;
                                objDTBillData.DocketDate = null;
                                objDTBillData.DelvStatus = "P";
                                objDTBillData.DelvUserId = 0;
                                objDTBillData.DelvRecTimeStamp = null;
                                objDTBillData.OrderType = "T";
                                objDTBillData.UserBillNo = UserBillNo;
                                objDTBillData.UserSBillNo = maxUserSBillNo;
                                objDTBillData.STNFormNo = "";
                                objDTBillData.StkRecv = "N";
                                objDTBillData.StkRecvDate = null;
                                objDTBillData.StkRecvUserId = 0;
                                objDTBillData.InTransit = "N";
                                objDTBillData.UID = string.IsNullOrEmpty(objModel.objProduct.UID) ? "" : objModel.objProduct.UID;
                                objDTBillData.OfferUID = 0;
                                objDTBillData.IsKit = "N";
                                objDTBillData.ProdType = obj.ProductTye;
                                objDTBillData.TotalCorton = "";
                                objDTBillData.TotalMonoCorton = "";
                                objDTBillData.SpclOfferId = 0;
                                objDTBillData.VAT = 0;
                                objDTBillData.BuyerAddress = "";
                                objDTBillData.BuyerTIN = "";


                                entity.TrnBillDatas.Add(objDTBillData);
                            }
                            int i = 0;

                            using (var objDTTrans = entity.Database.BeginTransaction())
                            {
                                try
                                {
                                    i = entity.SaveChanges();
                                    objDTTrans.Commit();
                                    if (i > 0)
                                    {
                                        query = "Exec Sp_ActivateMembers '" + (objCustomerDetail.IdNo).Trim() + "','" + objModel.NewKitId + "','" + BillNo + "','" + objModel.objProduct.TotalBV + "';";
                                        cmd.CommandText = query;
                                        cmd.Connection = SC;
                                        SC.Close();
                                        SC.Open();
                                        using (SqlDataReader reader = cmd.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                string msg = reader["Result"].ToString();

                                            }
                                        }

                                        objResponse.ResponseMessage = "Saved Successfully.";
                                        objResponse.ResponseStatus = "OK";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    objDTTrans.Rollback();
                                }
                            }


                        }
                        catch (Exception e)
                        {
                            objResponse.ResponseMessage = "Something went wrong!";
                            objResponse.ResponseStatus = "FAILED";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public decimal CheckStock(string ProdID, string PartyCode)
        {
            decimal stock = 0;
            using (var entity = new InventoryEntities(enttConstr))
            {
                stock = (from stockAvail in entity.Im_CurrentStock
                         where stockAvail.ProdId == ProdID && stockAvail.FCode.Equals(PartyCode)
                         select stockAvail.Qty
                                                      ).DefaultIfEmpty(0).Sum();
            }
            return stock;

        }



    }
}


