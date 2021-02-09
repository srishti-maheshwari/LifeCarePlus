using InventoryManagement.API.Models;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagement.API.Controllers
{
    public class ProductAPIController : ApiController
    {
        GenConnection objGetConn = new GenConnection();
        private static string dbName, invDbName, enttConstr, AppConstr, InvConstr, WRPartyCode;
        public ProductAPIController()
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
            //CompName = obj.CompName;
        }
        public ResponseDetail AddCategoryDetails(CategoryDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseStatus = "FAIL";
            objResponse.ResponseMessage = "Something went wrong!";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    M_CatMaster objDTCategory = new M_CatMaster();
                    objDTCategory = (from category in entity.M_CatMaster where category.CatId == model.CategoryId select category).FirstOrDefault();
                    if (objDTCategory == null)
                    {
                        objDTCategory = new M_CatMaster();
                    }
                    if (model.IsAdd != "Delete")
                    {
                        if (model.IsAdd == "Add")
                        {
                            decimal CategoryId = (from r in entity.M_CatMaster select r.CatId).DefaultIfEmpty(0).Max();
                            CategoryId = CategoryId + 1;
                            objDTCategory.CatId = (int)CategoryId;
                        }
                        objDTCategory.CatName = model.CategoryName;
                        objDTCategory.CatDescription = string.IsNullOrEmpty(model.Description) ? "" : model.Description;
                        objDTCategory.ActiveStatus = model.IsActive ? "Y" : "N";
                        objDTCategory.RecTimeStamp = DateTime.Now;
                        objDTCategory.UserId = model.UserDetails.UserId;
                        objDTCategory.IsForPC = "";
                        objDTCategory.DelCharge = "";
                        objDTCategory.Company = "";
                        objDTCategory.LastModified = DateTime.Now.ToString();
                        objDTCategory.WebSequence = 0;
                        objDTCategory.Remarks = "";
                        objDTCategory.Prefix = "";
                        objDTCategory.OnWebSite = "Y";

                        //objDTCategory.AlterID = model.UserDetails.UserId;

                        if (model.IsAdd == "Add")
                        {
                            objDTCategory.RecTimeStamp = DateTime.Now;
                            entity.M_CatMaster.Add(objDTCategory);
                        }

                    }
                    else
                    {
                        if (objDTCategory != null)
                        {
                            //  entity.M_CatMaster.Remove(objDTCategory);
                            objDTCategory.ActiveStatus = "N";
                        }
                    }
                    try
                    {
                        int isSaved = entity.SaveChanges();
                        if (isSaved > 0)
                        {
                            if (model.IsAdd == "Add")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Saved Successfully!";
                            }
                            else if (model.IsAdd == "Edit")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Updated Successfully!";

                            }
                            else
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Deleted Successfully!";

                            }
                        }
                        else
                        {
                            objResponse.ResponseStatus = "Failed";
                            objResponse.ResponseMessage = "Something went wrong!";

                        }
                    }
                    catch (DbEntityValidationException ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public ResponseDetail AddSubCategoryDetails(SubCategoryDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseStatus = "FAIL";
            objResponse.ResponseMessage = "Something went wrong!";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    M_SubCatMaster objDTSubCategory = new M_SubCatMaster();
                    objDTSubCategory = (from subcategory in entity.M_SubCatMaster where subcategory.SubCatId == model.SubCatId select subcategory).FirstOrDefault();
                    if (objDTSubCategory == null)
                    {
                        objDTSubCategory = new M_SubCatMaster();
                    }
                    if (model.IsAdd != "Delete")
                    {
                        if (model.IsAdd == "Add")
                        {
                            decimal subCategoryId = (from r in entity.M_SubCatMaster select r.SubCatId).DefaultIfEmpty(0).Max();
                            subCategoryId = subCategoryId + 1;
                            objDTSubCategory.SubCatId = (int)subCategoryId;
                        }
                        objDTSubCategory.Remarks = "";
                        objDTSubCategory.OfferHtml = "";
                        objDTSubCategory.IsForPC = "Y";
                        objDTSubCategory.OnWebSite = "Y";
                        objDTSubCategory.Description = string.IsNullOrEmpty(model.Description) ? "" : model.Description;
                        //objDTSubCategory.AlterID = model.UserDetails.UserId;
                        objDTSubCategory.SubCatName = model.subCategoryName;
                        //objDTSubCategory.Description = model.Description;
                        objDTSubCategory.CatId = model.CategoryId;
                        objDTSubCategory.ActiveStatus = model.IsActive ? "Y" : "N";

                        objDTSubCategory.UserId = model.UserDetails.UserId;
                        objDTSubCategory.LastModified = DateTime.Now.ToString();
                        if (model.IsAdd == "Add")
                        {
                            objDTSubCategory.RecTimeStamp = DateTime.Now;
                            entity.M_SubCatMaster.Add(objDTSubCategory);
                        }
                    }
                    else
                    {
                        if (objDTSubCategory != null)
                        {
                            //entity.M_SubCatMaster.Remove(objDTSubCategory);
                            objDTSubCategory.ActiveStatus = "N";
                        }
                    }
                    try
                    {
                        int isSaved = entity.SaveChanges();
                        if (isSaved > 0)
                        {
                            if (model.IsAdd == "Add")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Saved Successfully!";
                            }
                            else if (model.IsAdd == "Edit")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Updated Successfully!";
                            }
                            else
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Deleted Successfully!";
                            }
                        }
                        else
                        {
                            objResponse.ResponseStatus = "Failed";
                            objResponse.ResponseMessage = "Something went wrong!";

                        }
                    }
                    catch (DbEntityValidationException ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        //public ResponseDetail IsMasterExists(CheckDuplicateModel model)
        //{
        //    ResponseDetail objResponse = new ResponseDetail();
        //    objResponse.ResponseStatus = "FAIL";
        //    objResponse.ResponseMessage = "Match found!";
        //    try
        //    {
        //        using (var entity = new InventoryEntities(enttConstr))
        //        {

        //            switch (model.masterTable)
        //            {
        //                case "":
        //                    objResponse.ResponseStatus = "FAIL";
        //                    objResponse.ResponseMessage = "Something went wrong!";
        //                    break;
        //                case "CategoryMaster":
        //                    if (!string.IsNullOrEmpty(model.masterName))
        //                    {
        //                        var result = (from cm in entity.M_CatMaster where cm.CatName.ToLower().Equals(model.masterName.ToLower()) select cm.CatName).FirstOrDefault();
        //                        if (result == null)
        //                        {
        //                            objResponse.ResponseStatus = "OK";
        //                            objResponse.ResponseMessage = "Match not found!";
        //                        }
        //                        else if (model.isAdd == "Edit" || model.isAdd == "Delete")
        //                        {
        //                            objResponse.ResponseStatus = "OK";
        //                            objResponse.ResponseMessage = "Match not found!";


        //                        }
        //                        else
        //                        {
        //                            objResponse.ResponseStatus = "FAIL";
        //                            objResponse.ResponseMessage = "This Category Name already exists!";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        objResponse.ResponseStatus = "FAIL";
        //                        objResponse.ResponseMessage = "Something went wrong!";
        //                    }
        //                    break;
        //                case "SubCategoryMaster":
        //                    var result1 = (from cm in entity.M_SubCatMaster where cm.CatId == model.CategoryId && cm.SubCatName == model.masterName select cm).FirstOrDefault();
        //                    if (result1 == null)
        //                    {
        //                        objResponse.ResponseStatus = "OK";
        //                        objResponse.ResponseMessage = "Match not found!";
        //                    }
        //                    else if (model.isAdd == "Edit" || model.isAdd == "Delete")
        //                    {
        //                        objResponse.ResponseStatus = "OK";
        //                        objResponse.ResponseMessage = "Match not found!";


        //                    }
        //                    else
        //                    {
        //                        objResponse.ResponseStatus = "FAIL";
        //                        objResponse.ResponseMessage = "This Sub Category already exists!";

        //                    }
        //                    break;
        //                case "ProductMaster":
        //                    var result2 = (from cm in entity.M_ProductMaster where cm.CatId == model.CategoryId && cm.SubCatId == model.SubCategoryId && cm.ProductName==model.masterName select cm).FirstOrDefault();
        //                    if (result2 == null)
        //                    {
        //                        objResponse.ResponseStatus = "OK";
        //                        objResponse.ResponseMessage = "Match not found!";
        //                    }
        //                    else if (model.isAdd == "Edit" || model.isAdd == "Delete")
        //                    {
        //                        objResponse.ResponseStatus = "OK";
        //                        objResponse.ResponseMessage = "Match not found!";


        //                    }
        //                    else
        //                    {
        //                        objResponse.ResponseStatus = "FAIL";
        //                        objResponse.ResponseMessage = "This Product already exists!";

        //                    }
        //                    break;
        //                default:
        //                    objResponse.ResponseStatus = "FAIL";
        //                    objResponse.ResponseMessage = "Something went wrong!";
        //                    break;

        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return objResponse;
        //}

        public ResponseDetail IsMasterExists(CheckDuplicateModel model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseStatus = "FAIL";
            objResponse.ResponseMessage = "Match found!";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {

                    switch (model.masterTable)
                    {
                        case "":
                            objResponse.ResponseStatus = "FAIL";
                            objResponse.ResponseMessage = "Something went wrong!";
                            break;
                        case "CategoryMaster":
                            if (!string.IsNullOrEmpty(model.masterName))
                            {
                                var result = (from cm in entity.M_CatMaster where cm.CatName.ToLower().Equals(model.masterName.ToLower()) select cm.CatName).FirstOrDefault();
                                if (result == null)
                                {
                                    objResponse.ResponseStatus = "OK";
                                    objResponse.ResponseMessage = "Match not found!";
                                }
                                else if (model.isAdd == "Edit" || model.isAdd == "Delete")
                                {
                                    objResponse.ResponseStatus = "OK";
                                    objResponse.ResponseMessage = "Match not found!";


                                }
                                else
                                {
                                    objResponse.ResponseStatus = "FAIL";
                                    objResponse.ResponseMessage = "This Category Name already exists!";
                                }
                            }
                            else
                            {
                                objResponse.ResponseStatus = "FAIL";
                                objResponse.ResponseMessage = "Something went wrong!";
                            }
                            break;
                        case "SubCategoryMaster":
                            var result1 = (from cm in entity.M_SubCatMaster where cm.CatId == model.CategoryId && cm.SubCatName == model.masterName select cm).FirstOrDefault();
                            if (result1 == null)
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Match not found!";
                            }
                            else if (model.isAdd == "Edit" || model.isAdd == "Delete")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Match not found!";


                            }
                            else
                            {
                                objResponse.ResponseStatus = "FAIL";
                                objResponse.ResponseMessage = "This Sub Category already exists!";

                            }
                            break;
                        case "ProductMaster":
                            var result2 = (from cm in entity.M_ProductMaster where cm.CatId == model.CategoryId && cm.SubCatId == model.SubCategoryId && cm.ProductName == model.masterName select cm).FirstOrDefault();
                            if (result2 == null)
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Match not found!";
                            }
                            else if (model.isAdd == "Edit" || model.isAdd == "Delete")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Match not found!";


                            }
                            else
                            {
                                objResponse.ResponseStatus = "FAIL";
                                objResponse.ResponseMessage = "This Product already exists!";

                            }
                            break;
                        case "SizeMaster":
                            var resultSize = (from cm in entity.M_SizeMaster where cm.Size == model.masterName select cm).FirstOrDefault();
                            if (resultSize == null)
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Match not found!";
                            }
                            else if (model.isAdd == "Edit" || model.isAdd == "Delete")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Match not found!";


                            }
                            else
                            {
                                objResponse.ResponseStatus = "FAIL";
                                objResponse.ResponseMessage = "This Size already exists!";

                            }
                            break;
                        case "ColorMaster":
                            var resultColor = (from cm in entity.M_ColorMaster where cm.ColorName == model.masterName select cm).FirstOrDefault();
                            if (resultColor == null)
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Match not found!";
                            }
                            else if (model.isAdd == "Edit" || model.isAdd == "Delete")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Match not found!";


                            }
                            else
                            {
                                objResponse.ResponseStatus = "FAIL";
                                objResponse.ResponseMessage = "This Color already exists!";

                            }
                            break;
                        default:
                            objResponse.ResponseStatus = "FAIL";
                            objResponse.ResponseMessage = "Something went wrong!";
                            break;

                    }


                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        public List<CategoryDetails> GetCategoryList(string ActiveFlag)
        {
            List<CategoryDetails> objCategoryList = new List<CategoryDetails>();

            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (!string.IsNullOrEmpty(ActiveFlag))
                    {
                        objCategoryList = (from category in entity.M_CatMaster
                                           where category.ActiveStatus == ActiveFlag
                                           select new CategoryDetails
                                           {
                                               CategoryId = (int)category.CatId,
                                               CategoryName = category.CatName,
                                               Description = category.CatDescription,
                                               IsActive = category.ActiveStatus == "Y" ? true : false
                                           }
                                           ).ToList();
                    }
                    else
                    {
                        objCategoryList = (from category in entity.M_CatMaster
                                               // where category.ActiveStatus == "Y"
                                           select new CategoryDetails
                                           {
                                               CategoryId = (int)category.CatId,
                                               CategoryName = category.CatName,
                                               Description = category.CatDescription,
                                               IsActive = category.ActiveStatus == "Y" ? true : false
                                           }
                                          ).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return objCategoryList;
        }

        public List<SubCategoryDetails> GetSubcategoryDetails(int CategoryId, string ActiveStatus)
        {
            List<SubCategoryDetails> objSubCategoryDetails = new List<SubCategoryDetails>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (!string.IsNullOrEmpty(ActiveStatus))
                    {
                        if (CategoryId != 0)
                        {
                            objSubCategoryDetails = (from c in entity.M_CatMaster
                                                         //join sc in entity.SubCategoryMasters on c.CategoryId equals sc.ParentCategoryId
                                                     join sc in entity.M_SubCatMaster on c.CatId equals sc.CatId
                                                     where sc.CatId == CategoryId && sc.ActiveStatus == ActiveStatus && c.ActiveStatus == "Y"
                                                     select new SubCategoryDetails
                                                     {
                                                         SubCategoryId = (int)sc.AId,
                                                         SubCatId = (int)sc.SubCatId,
                                                         CategoryId = (int)sc.CatId,
                                                         CategoryName = c.CatName,
                                                         IsActive = sc.ActiveStatus == "Y" ? true : false,
                                                         Description = sc.Description,
                                                         subCategoryName = sc.SubCatName
                                                     }).ToList();
                        }
                        else
                        {
                            objSubCategoryDetails = (from c in entity.M_CatMaster
                                                         //join sc in entity.SubCategoryMasters on c.CategoryId equals sc.ParentCategoryId
                                                     join sc in entity.M_SubCatMaster on c.CatId equals sc.CatId
                                                     where sc.ActiveStatus == ActiveStatus && c.ActiveStatus=="Y"
                                                     select new SubCategoryDetails
                                                     {
                                                         SubCategoryId = (int)sc.AId,
                                                         SubCatId = (int)sc.SubCatId,
                                                         CategoryId = (int)sc.CatId,
                                                         CategoryName = c.CatName,
                                                         Description = sc.Description,
                                                         IsActive = sc.ActiveStatus == "Y" ? true : false,
                                                         subCategoryName = sc.SubCatName
                                                     }).ToList();
                        }
                    }
                    else
                    {
                        if (CategoryId != 0)
                        {
                            objSubCategoryDetails = (from c in entity.M_CatMaster
                                                         //join sc in entity.SubCategoryMasters on c.CategoryId equals sc.ParentCategoryId
                                                     join sc in entity.M_SubCatMaster on c.CatId equals sc.CatId
                                                     where sc.CatId == CategoryId && c.ActiveStatus == "Y"
                                                     select new SubCategoryDetails
                                                     {
                                                         SubCategoryId = (int)sc.AId,
                                                         SubCatId = (int)sc.SubCatId,
                                                         CategoryId = (int)sc.CatId,
                                                         CategoryName = c.CatName,
                                                         IsActive = sc.ActiveStatus == "Y" ? true : false,
                                                         subCategoryName = sc.SubCatName,
                                                         Description = sc.Description,
                                                     }).ToList();
                        }
                        else
                        {
                            objSubCategoryDetails = (from c in entity.M_CatMaster
                                                         //join sc in entity.SubCategoryMasters on c.CategoryId equals sc.ParentCategoryId
                                                     join sc in entity.M_SubCatMaster on c.CatId equals sc.CatId
                                                     // where sc.ActiveStatus == ActiveStatus
                                                     where c.ActiveStatus == "Y"
                                                     select new SubCategoryDetails
                                                     {
                                                         SubCategoryId = (int)sc.AId,
                                                         SubCatId = (int)sc.SubCatId,
                                                         CategoryId = (int)sc.CatId,
                                                         CategoryName = c.CatName,
                                                         IsActive = sc.ActiveStatus == "Y" ? true : false,
                                                         subCategoryName = sc.SubCatName,
                                                         Description = sc.Description
                                                     }).ToList();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return objSubCategoryDetails;
        }

        public int MaxProductCode()
        {
            decimal maxCode = 1000;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    maxCode = (from result in entity.M_ProductMaster select result.ProductCode).DefaultIfEmpty(0).Max();

                }

            }
            catch (Exception ex)
            {

            }
            if (maxCode == 0)
            {
                maxCode = 1000;
            }
            return ((int)maxCode + 1);
        }

        public int MaxBarCode()
        {
            decimal maxCode = 1000000;
            string maxCodeStr = "0";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    maxCode = (from result in entity.M_BarCodeMaster where result.BType == "W" select result.BCode).DefaultIfEmpty(1000000).Max();

                }

            }
            catch (Exception ex)
            {

            }
            //if (!string.IsNullOrEmpty(maxCodeStr)&& maxCodeStr!="0")
            //{
            //maxCode = decimal.Parse(maxCodeStr);
            //}
            if (maxCode == 0)
            {
                maxCode = 1000000;
            }
            return ((int)maxCode + 1);
        }

        public ResponseDetail SaveProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            M_ProductMaster objDTProduct = new M_ProductMaster();
            M_BarCodeMaster objDTBarcode = new M_BarCodeMaster();
            M_TaxMaster objDTTax = new M_TaxMaster();
            TrnStockJv objDtStock = new TrnStockJv();
            Im_CurrentStock objDtCurrentStock = new Im_CurrentStock();
            string objversion = "";
            M_FiscalMaster objFiscalMaster = new M_FiscalMaster();
            int i = 0;
            // decimal ProductBarcodeId = 0;
            //decimal ProductTaxId = 0 ;
            decimal BatchCode = 1000000;
            decimal StockJNo = 1001;
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (model != null)
                    {
                        objFiscalMaster = (from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result).FirstOrDefault();
                        objDTProduct = (from result in entity.M_ProductMaster where result.ProductCode == model.ProductCode select result).FirstOrDefault();
                        objversion = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();
                        if (objDTProduct == null)
                        {
                            objDTProduct = new M_ProductMaster();
                            model.ProductCode = MaxProductCode();
                        }

                        i = 0;
                       
                        objDTProduct.ProductCode = model.ProductCode;
                        objDTProduct.ProductName = model.ProductName;
                        objDTProduct.ProdId = model.ProductCode.ToString();
                        objDTProduct.PV = model.PV;
                        objDTProduct.RP = model.RP;
                        objDTProduct.SpclOffer = model.SpecialOffer;
                        objDTProduct.HotSell = model.HotSell;
                        objDTProduct.ImagePath = string.IsNullOrEmpty(model.ProductImagePath) ? "" : model.ProductImagePath;
                        objDTProduct.IsImage = string.IsNullOrEmpty(model.ProductImagePath) ? "N" : "Y";
                        objDTProduct.ActiveStatus = model.IsActive ? "Y" : "N";
                        objDTProduct.OnWebSite = model.OnWebsite;
                        objDTProduct.MsgText = string.IsNullOrEmpty(model.Message)?"":model.Message;
                        objDTProduct.MsgStatus = model.MessageStatus;
                        // objDTProduct.MinQty = model.MinQty;
                        objDTProduct.IMEINo = model.MinQty.ToString();
                        objDTProduct.ProdCommssn = model.ProductCommission;
                        objDTProduct.Discount = model.DiscountPer;
                        objDTProduct.DiscInRs = model.DiscountInRs;
                        objDTProduct.UserProdId = string.IsNullOrEmpty(model.UserDefinedCode)?"":model.UserDefinedCode;
                        objDTProduct.SubCatId = model.SubCatgeoryId;
                        objDTProduct.CatId = model.CategoryId;
                        objDTProduct.ProductDesc = model.ProductDescription;
                        objDTProduct.CV = model.CV;
                        objDTProduct.BV = model.BV;
                        objDTProduct.HSNCode = string.IsNullOrEmpty(model.HSNCode) ? "" : model.HSNCode; 
                        if (model.UserDetails != null)
                        {
                            objDTProduct.UserId = model.UserDetails.UserId;
                        }
                        objDTProduct.RecTimeStamp = DateTime.Now;

                        objDTProduct.GenerateBy = model.UserDetails.PartyCode;
                        objDTProduct.BrandCode = 0;
                        objDTProduct.ProductType = "P";
                        objDTProduct.Prefix = "";
                        objDTProduct.ItemType = "";
                        objDTProduct.BuyingTax = model.ProductTaxDetails.GSTTax;
                        objDTProduct.Weight = model.Weight;
                        objDTProduct.PurchaseRate = model.ProductBarcodeDetails.PurchaseRate;
                        objDTProduct.DP1 = 0;
                        objDTProduct.OtherStateDP = 0;
                        objDTProduct.Exp = 0;
                        objDTProduct.Costing = 0;
                        objDTProduct.FundPoint = 0;
                        if (model.DiscountPer > 0 || model.DiscountInRs > 0)
                        {
                            objDTProduct.IsDiscount = "Y";
                        }
                        else if (model.DiscountPer > 0 && model.DiscountInRs == 0)
                        {
                            objDTProduct.IsDiscount = "Y";
                        }
                        else if (model.DiscountPer == 0 && model.DiscountInRs > 0)
                        {
                            objDTProduct.IsDiscount = "Y";
                        }
                        else
                        {
                            objDTProduct.IsDiscount = "N";
                        }
                        objDTProduct.VDiscount = 0;
                        objDTProduct.GRate = 0;
                        objDTProduct.GMCharge = 0;
                        objDTProduct.GMType = "";
                        objDTProduct.IsCardIssue = model.DisableForFrOrder;//31Jan20
                        objDTProduct.Remarks = "";
                        objDTProduct.TaxType = "I";
                        decimal BarcodeId = (from result in entity.M_BarCodeMaster select result.BId).DefaultIfEmpty(1000000).Max();
                        BarcodeId = BarcodeId + 1;
                        objDTProduct.BId = BarcodeId;
                        objDTProduct.Imported = model.ProductFor;//31Jan20
                        objDTProduct.BNo = "0";
                        objDTProduct.PType = "G";
                        if (model.ProductBarcodeDetails.BarcodeType == "System Generated")
                        { objDTProduct.BarcodeType = "W"; }
                        else
                        {
                            objDTProduct.BarcodeType = "O";
                        }

                        objDTProduct.BCode = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? decimal.Parse(model.ProductBarcodeDetails.Barcode) : 0;
                        objDTProduct.Barcode = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                        objDTProduct.OpStockQty = model.ProductCurrentStockDetails.OpeningStockQty;
                        objDTProduct.LastModified = "";
                        objDTProduct.Val1 = 0;
                        objDTProduct.Val2 = 0;
                        objDTProduct.Company = "U";
                        objDTProduct.PurchaseFrom = "WR";
                        objDTProduct.PurchaseStore = "WR";
                        objDTProduct.IsFlexible = "N";
                        objDTProduct.IsForPC = "N";
                        objDTProduct.SubQty = 0;
                        objDTProduct.CalcKitRate = "N";
                        objDTProduct.FlexiQty = 0;
                        objDTProduct.ImgPath1 = "";
                        objDTProduct.ImgPath2 = "";
                        objDTProduct.ImgPath3 = "";
                        objDTProduct.ImgPath4 = "";
                        objDTProduct.AlterID = model.UserDetails.UserId;
                        // objDTProduct.UnitID = 3;
                        // objDTProduct.UnitName = "Pc.";
                        objDTProduct.MRP = model.ProductBarcodeDetails.MRP;
                        objDTProduct.DP = model.ProductBarcodeDetails.DP;
                        objDTProduct.IsExpired = model.ProductBarcodeDetails.IsExpirable;
                        DateTime MfgDate = DateTime.Now;
                        DateTime ExpDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(model.ProductBarcodeDetails.MfgDateStr))
                        {
                            var SplitDate = model.ProductBarcodeDetails.MfgDateStr.Split('-');
                            string NewDate = SplitDate[1] + "/" + ("0" + SplitDate[0]).Substring(("0" + SplitDate[0]).Length - 2, 2) + "/" + SplitDate[2];
                            MfgDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                            MfgDate = MfgDate.Date;
                        }
                        if (!string.IsNullOrEmpty(model.ProductBarcodeDetails.ExpDateStr))
                        {
                            var SplitDate = model.ProductBarcodeDetails.ExpDateStr.Split('-');
                            string NewDate = SplitDate[1] + "/" + ("0" + SplitDate[0]).Substring(("0" + SplitDate[0]).Length - 2, 2) + "/" + SplitDate[2];
                            ExpDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));                            
                            ExpDate = ExpDate.Date;
                        }
                        objDTProduct.MfgDate = MfgDate.Date;
                        objDTProduct.ExpDate = ExpDate.Date;
                        // objDTProduct.ProductBarcodeId = ProductBarcodeId;
                        // objDTProduct.ProductTaxId = ProductTaxId;
                        bool isBarcodeMasterSave = true, isTaxMasterSave = true;
                        objDTProduct.OrderDesc = "";
                        objDTProduct.GenerateBy = model.UserDetails.UserName;
                        objDTProduct.HSNCode = "";
                        entity.M_ProductMaster.Add(objDTProduct);
                        try
                        {
                            i = entity.SaveChanges();

                            if (i > 0)
                            {
                                if (model.ProductBarcodeDetails != null)
                                {
                                    objDTBarcode.BId = BarcodeId;
                                    objDTBarcode.SupplierCode = "0";
                                    objDTBarcode.BCode = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? decimal.Parse(model.ProductBarcodeDetails.Barcode) : 0;
                                    objDTBarcode.Company = "";
                                    objDTBarcode.Imported = "N";
                                    objDTBarcode.LastModified = "";
                                    objDTBarcode.DP1 = 0;
                                    objDTBarcode.Val1 = 0;
                                    objDTBarcode.Val2 = 0;


                                    objDTBarcode.BarCode = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                                    objDTBarcode.BatchNo = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                                    objDTBarcode.BarCodeType = model.ProductBarcodeDetails.BarcodeType;
                                    objDTBarcode.DP = model.ProductBarcodeDetails.DP;
                                    if (model.ProductBarcodeDetails.IsExpirable == "Y")
                                        objDTBarcode.ExpDate = ExpDate.Date;
                                    else
                                    {
                                        objDTBarcode.ExpDate = DateTime.Now;
                                    }
                                    objDTBarcode.MfgDate = MfgDate.Date;
                                    objDTBarcode.MRP = model.ProductBarcodeDetails.MRP;
                                    objDTBarcode.IsExpired = model.ProductBarcodeDetails.IsExpirable;
                                    objDTBarcode.ProdId = model.ProductCode.ToString();
                                    objDTBarcode.PRate = model.ProductBarcodeDetails.PurchaseRate;
                                    objDTBarcode.Remarks = string.IsNullOrEmpty(model.ProductBarcodeDetails.Remarks) ? "" : model.ProductBarcodeDetails.Remarks;
                                    objDTBarcode.ActiveStatus = model.IsActive ? "Y" : "N";
                                    objDTBarcode.GenerateDate = DateTime.Now;
                                    if (model.UserDetails != null)
                                    {
                                        objDTBarcode.GenerateBy = model.UserDetails.UserName;
                                        objDTBarcode.UserId = model.UserDetails.UserId;
                                        //objDTBarcode.AlterID = model.UserDetails.UserId;
                                    }
                                    objDTBarcode.RecTimeStamp = DateTime.Now;

                                    if (model.ProductBarcodeDetails.BarcodeType == "System Generated")
                                    { objDTBarcode.BType = "W"; }
                                    else
                                    {
                                        objDTBarcode.BType = "O";
                                    }

                                    //BatchCode = (from result in entity.BarcodeMasters select result.BatchCode).Max()??1000000;

                                    //objDTBarcode.BatchCode = BatchCode + 1;
                                    objDTBarcode.BatchNo = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                                    BatchCode = (!string.IsNullOrEmpty(objDTBarcode.BatchNo)) ? decimal.Parse(objDTBarcode.BatchNo) : 1000001;
                                    entity.M_BarCodeMaster.Add(objDTBarcode);
                                    try
                                    {
                                        i = entity.SaveChanges();
                                        if (i > 0)
                                        {
                                            //ProductBarcodeId = (from result in entity.M_BarCodeMaster select result.AId).Max();
                                            isBarcodeMasterSave = true;
                                        }
                                        else
                                        {
                                            isBarcodeMasterSave = false;
                                        }
                                    }
                                    catch (DbEntityValidationException ex)
                                    {

                                    }
                                }

                                if (model.ProductTaxDetails != null)
                                {
                                    objDTTax.WithCForm = 0;
                                    objDTTax.CstTax = 0;
                                    objDTTax.ActiveStatus = model.IsActive ? "Y" : "N";
                                    objDTTax.VatTax = model.ProductTaxDetails.GSTTax;
                                    objDTTax.ProdName = model.ProductName;
                                    objDTTax.Imported = "N";
                                    objDTTax.LastModified = DateTime.Now.ToString();
                                    objDTTax.Remarks = "";
                                    objDTTax.Company = "";
                                    //objDTTax.GeneratedDate = DateTime.Now;
                                    objDTTax.RecTimeStamp = DateTime.Now;

                                    objDTTax.AValue = 0;
                                    objDTTax.STax = 0;
                                    if (model.UserDetails != null)
                                    {
                                        // objDTTax.StateCode = model.UserDetails.StateCode;
                                        objDTTax.StateCode = (int)(from r in entity.M_CompanyMaster select r.CompState).FirstOrDefault();
                                        objDTTax.UserId = model.UserDetails.UserId;
                                        objDTTax.GenerateBy = model.UserDetails.UserName;
                                    }
                                    objDTTax.ProdCode = model.ProductCode.ToString();
                                    i = 0;
                                    entity.M_TaxMaster.Add(objDTTax);
                                    try
                                    {
                                        i = entity.SaveChanges();
                                        if (i > 0)
                                        {
                                            //ProductTaxId = (from result in entity.M_TaxMaster select result.AId).Max();
                                            isTaxMasterSave = true;
                                        }
                                        else
                                        {
                                            isTaxMasterSave = false;
                                        }
                                    }
                                    catch (DbEntityValidationException ex)
                                    {

                                    }
                                }


                                // i = 0;
                                objDtStock.Barcode = model.ProductBarcodeDetails.Barcode;
                                objDtStock.BatchNo = BatchCode.ToString();
                                if (model.UserDetails != null)
                                {
                                    objDtStock.UserId = model.UserDetails.UserId;
                                    objDtStock.UserName = model.UserDetails.UserName;
                                }
                                objDtStock.RecTimeStamp = DateTime.Now;
                                if (objFiscalMaster != null)
                                    objDtStock.FSessId = objFiscalMaster.FSessId;
                                else
                                    objDtStock.FSessId = 0;
                                var res = (from result in entity.TrnStockJvs select result).ToList();
                                if (res.Count == 0)
                                    objDtStock.JNo = 1001;
                                else
                                {
                                    decimal MaxJNo = (from r in res select r.JNo).DefaultIfEmpty(0).Max();
                                    objDtStock.JNo = MaxJNo + 1;
                                }
                                objDtStock.JvNo = "OPN/" + objDtStock.JNo;
                                StockJNo = objDtStock.JNo == 0 ? 1001 : objDtStock.JNo;
                                objDtStock.ProdId = model.ProductCode.ToString();
                                objDtStock.ProductName = (from result in entity.M_ProductMaster where result.ProductCode == model.ProductCode select result.ProductName).FirstOrDefault() ?? "";
                                objDtStock.ProdType = "P";
                                objDtStock.Qty = model.ProductCurrentStockDetails.OpeningStockQty;
                                objDtStock.Remarks = "Opening Stock Of Product Registration";
                                objDtStock.RefNo = objDtStock.JvNo;
                                objDtStock.JType = "O";
                                objDtStock.ActiveStatus = model.IsActive ? "Y" : "N";
                                objDtStock.Version = objversion;
                                if (model.UserDetails != null)
                                {
                                    objDtStock.PartyName = model.UserDetails.PartyName;
                                    objDtStock.FCode = model.UserDetails.FCode;
                                    objDtStock.SoldBy = model.UserDetails.FCode;
                                }

                                objDtStock.StockDate = DateTime.Now;
                                try
                                {
                                    entity.TrnStockJvs.Add(objDtStock);
                                    i = 0;
                                    i = entity.SaveChanges();
                                    if (i > 0)
                                    {

                                       
                                        objResponse.ResponseStatus = "OK";
                                        objResponse.ResponseMessage = "Saved Successfully!";

                                    }
                                    else
                                    {
                                        objResponse.ResponseStatus = "FAILED";
                                        objResponse.ResponseMessage = "Something went wrong!";
                                    }

                                }
                                catch (DbEntityValidationException e)
                                {
                                    foreach (var eve in e.EntityValidationErrors)
                                    {
                                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                        foreach (var ve in eve.ValidationErrors)
                                        {
                                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                                ve.PropertyName, ve.ErrorMessage);
                                        }
                                    }
                                }
                            }
                            
                        }
                        catch (DbEntityValidationException ex)
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }
        public ResponseDetail EditProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            M_ProductMaster objDTProduct = new M_ProductMaster();
            M_BarCodeMaster objDTBarcode = new M_BarCodeMaster();
            M_TaxMaster objDTTax = new M_TaxMaster();
            TrnStockJv objDTStock = new TrnStockJv();
            Im_CurrentStock objCurrentStock = new Im_CurrentStock();
            objResponse.ResponseStatus = "OK";
            objResponse.ResponseMessage = "Updated Successfully!";
            int i = 0;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objDTProduct = (from p in entity.M_ProductMaster where p.ProductCode == model.ProductCode select p).FirstOrDefault();
                    if (objDTProduct != null)
                    {
                        if (model != null)
                        {
                            i = 0;
                            objDTProduct.ProductCode = model.ProductCode;
                            objDTProduct.ProductName = model.ProductName;
                            objDTProduct.ProdId = model.ProductCode.ToString();
                            objDTProduct.PV = model.PV;
                            objDTProduct.RP = model.RP;
                            objDTProduct.SpclOffer = model.SpecialOffer;
                            objDTProduct.HotSell = model.HotSell;
                            objDTProduct.ImagePath = string.IsNullOrEmpty(model.ProductImagePath) ? "" : model.ProductImagePath;
                            objDTProduct.IsImage = string.IsNullOrEmpty(model.ProductImagePath) ? "N" : "Y";
                            objDTProduct.ActiveStatus = model.IsActive ? "Y" : "N";
                            objDTProduct.OnWebSite = model.OnWebsite;
                            objDTProduct.Imported = model.ProductFor;
                            objDTProduct.MsgText = string.IsNullOrEmpty(model.Message) ? "" : model.Message;
                            objDTProduct.MsgStatus = model.MessageStatus;
                            objDTProduct.Weight = model.Weight;
                           objDTProduct.IMEINo = model.MinQty.ToString();
                            objDTProduct.ProdCommssn = model.ProductCommission;
                            objDTProduct.Discount = model.DiscountPer;
                            objDTProduct.DiscInRs = model.DiscountInRs;
                            objDTProduct.UserProdId = string.IsNullOrEmpty(model.UserDefinedCode) ? "" : model.UserDefinedCode;
                            objDTProduct.SubCatId = model.SubCatgeoryId;
                            objDTProduct.CatId = model.CategoryId;
                            objDTProduct.ProductDesc = model.ProductDescription;
                            objDTProduct.CV = model.CV;
                            objDTProduct.BV = model.BV;
                            objDTProduct.HSNCode = string.IsNullOrEmpty(model.HSNCode) ? "" : model.HSNCode;
                            objDTProduct.GenerateBy = model.UserDetails.PartyCode;
                            objDTProduct.BuyingTax = model.ProductTaxDetails.GSTTax;
                            objDTProduct.PurchaseRate = model.ProductBarcodeDetails.PurchaseRate;

                            if (model.DiscountPer > 0 || model.DiscountInRs > 0)
                            {
                                objDTProduct.IsDiscount = "Y";
                            }
                            else if (model.DiscountPer > 0 && model.DiscountInRs == 0)
                            {
                                objDTProduct.IsDiscount = "Y";
                            }
                            else if (model.DiscountPer == 0 && model.DiscountInRs > 0)
                            {
                                objDTProduct.IsDiscount = "Y";
                            }
                            else
                            {
                                objDTProduct.IsDiscount = "N";
                            }


                            //if (model.ProductBarcodeDetails.BarcodeType == "System Generated")
                            //{ objDTProduct.BarcodeType = "W"; }
                            //else
                            //{
                            //    objDTProduct.BarcodeType = "O";
                            //}

                            //objDTProduct.BCode = (string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? decimal.Parse(model.ProductBarcodeDetails.Barcode) : 0;
                            // objDTProduct.Barcode = (string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                            // objDTProduct.OpStockQty = model.ProductCurrentStockDetails.OpeningStockQty;
                            if (model.UserDetails != null)
                            {
                                objDTProduct.LastModified = model.UserDetails.UserName;
                            }


                            objDTProduct.AlterID = model.UserDetails.UserId;

                            objDTProduct.MRP = model.ProductBarcodeDetails.MRP;
                            objDTProduct.DP = model.ProductBarcodeDetails.DP;
                            objDTProduct.IsExpired = model.ProductBarcodeDetails.IsExpirable;
                            DateTime MfgDate = DateTime.Now;
                            DateTime ExpDate = DateTime.Now;
                            if (!string.IsNullOrEmpty(model.ProductBarcodeDetails.MfgDateStr))
                            {
                                var SplitDate = model.ProductBarcodeDetails.MfgDateStr.Split('-');
                                string NewDate = SplitDate[1] + "/" + ("0" + SplitDate[0]).Substring(("0" + SplitDate[0]).Length - 2, 2) + "/" + SplitDate[2];
                                MfgDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                MfgDate = MfgDate.Date;
                            }
                            if (!string.IsNullOrEmpty(model.ProductBarcodeDetails.ExpDateStr))
                            {
                                var SplitDate = model.ProductBarcodeDetails.ExpDateStr.Split('-');
                                string NewDate = SplitDate[1] + "/" + ("0" + SplitDate[0]).Substring(("0" + SplitDate[0]).Length - 2, 2) + "/" + SplitDate[2];
                                ExpDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                ExpDate = ExpDate.Date;
                            }
                            objDTProduct.MfgDate = MfgDate.Date;
                            objDTProduct.ExpDate = ExpDate.Date;
                            i = entity.SaveChanges();
                            //if (i > 0)
                            //{
                                //barcode details
                                string ProductCodeStr = model.ProductCode.ToString();
                                objDTBarcode = (from b in entity.M_BarCodeMaster where b.ProdId == ProductCodeStr && b.BarCode == model.ProductBarcodeDetails.ExisitingBarcode select b).FirstOrDefault();
                                if (objDTBarcode != null)
                                {
                                    //objDTBarcode.BCode = (string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? decimal.Parse(model.ProductBarcodeDetails.Barcode) : 0;
                                    if (model.UserDetails != null)
                                    {
                                        objDTBarcode.LastModified = model.UserDetails.UserName;
                                    }

                                    //objDTBarcode.BarCode = (string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                                    // objDTBarcode.BatchNo = (string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                                    // objDTBarcode.BarCodeType = model.ProductBarcodeDetails.BarcodeType;
                                    objDTBarcode.DP = model.ProductBarcodeDetails.DP;
                                    if (model.ProductBarcodeDetails.IsExpirable == "Y")
                                        objDTBarcode.ExpDate = ExpDate.Date;
                                    else
                                    {
                                        objDTBarcode.ExpDate = DateTime.Now;
                                    }
                                    objDTBarcode.MfgDate = MfgDate.Date;
                                    objDTBarcode.MRP = model.ProductBarcodeDetails.MRP;
                                    objDTBarcode.IsExpired = model.ProductBarcodeDetails.IsExpirable;
                                    objDTBarcode.ProdId = model.ProductCode.ToString();
                                    objDTBarcode.PRate = model.ProductBarcodeDetails.PurchaseRate;
                                    objDTBarcode.Remarks = string.IsNullOrEmpty(model.ProductBarcodeDetails.Remarks) ? "" : model.ProductBarcodeDetails.Remarks;
                                    objDTBarcode.ActiveStatus = model.IsActive ? "Y" : "N";
                                    if (model.ProductBarcodeDetails.BarcodeType == "System Generated")
                                    { objDTBarcode.BType = "W"; }
                                    else
                                    {
                                        objDTBarcode.BType = "O";
                                    }

                                    //BatchCode = (from result in entity.BarcodeMasters select result.BatchCode).Max()??1000000;

                                    //objDTBarcode.BatchCode = BatchCode + 1;
                                    // objDTBarcode.BatchNo = (string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                                    i = 0;
                                    i = entity.SaveChanges();
                                    //if (i > 0)
                                    //{
                                        //tax details
                                        //string ProductCodeStr = model.ProductCode.ToString();
                                        if (model.ProductTaxDetails != null)
                                        {
                                            objDTTax = (from t in entity.M_TaxMaster where t.ProdCode == ProductCodeStr && t.StateCode == model.UserDetails.StateCode select t).FirstOrDefault();
                                            if (objDTTax != null)
                                            {
                                                objDTTax.ActiveStatus = model.IsActive ? "Y" : "N";
                                                objDTTax.VatTax = model.ProductTaxDetails.GSTTax;
                                                objDTTax.ProdName = model.ProductName;
                                                objDTTax.LastModified = DateTime.Now.ToString();
                                                if (model.UserDetails != null)
                                                {
                                                    objDTTax.StateCode = (int)(from r in entity.M_CompanyMaster select r.CompState).FirstOrDefault();
                                                    objDTTax.UserId = model.UserDetails.UserId;
                                                    objDTTax.GenerateBy = model.UserDetails.UserName;
                                                }
                                                objDTTax.ProdCode = model.ProductCode.ToString();
                                                i = 0;
                                                i = entity.SaveChanges();
                                        //if (i > 0)
                                        //{
                                        //    objResponse.ResponseStatus = "OK";
                                        //    objResponse.ResponseMessage = "Updated Succesfully!";
                                            objDTStock = (from s in entity.TrnStockJvs where s.ProdId == ProductCodeStr && s.JType == "O" select s).FirstOrDefault();
                                                    if (objDTStock != null)
                                                    {
                                                        //objDTStock.Barcode = model.ProductBarcodeDetails.Barcode;
                                                        // objDTStock.BatchNo = objDTStock.Barcode.ToString();
                                                        objDTStock.ActiveStatus = model.IsActive ? "Y" : "N";
                                                        objDTStock.ProductName = (from result in entity.M_ProductMaster where result.ProductCode == model.ProductCode select result.ProductName).FirstOrDefault() ?? "";
                                                        if (model.UserDetails != null)
                                                        {
                                                            objDTStock.PartyName = model.UserDetails.PartyName;
                                                            objDTStock.FCode = model.UserDetails.FCode;
                                                            objDTStock.SoldBy = model.UserDetails.FCode;
                                                        }

                                                        // objDTStock.StockDate = DateTime.Now;
                                                        i = 0;
                                                        i = entity.SaveChanges();
                                                        //if (i > 0)
                                                        //{
                                                            //objCurrentStock = (from c in entity.Im_CurrentStock where c.ProdId == ProductCodeStr && c.BType == "O" select c).FirstOrDefault();
                                                            //if (objCurrentStock != null)
                                                            //{
                                                            //    if (model.UserDetails != null)
                                                            //    {
                                                            //        objCurrentStock.FCode = model.UserDetails.FCode;
                                                            //    }

                                                            //    objCurrentStock.ActiveStatus = model.IsActive ? "Y" : "N";
                                                            //    objCurrentStock.ProdId = model.ProductCode.ToString();

                                                            //    //objCurrentStock.Qty = model.ProductCurrentStockDetails.OpeningStockQty;

                                                            //    objCurrentStock.SupplierCode = model.UserDetails.FCode;


                                                            //    objCurrentStock.EntryBy = objCurrentStock.FCode;

                                                            //    //objCurrentStock.Barcode = model.ProductBarcodeDetails.Barcode;
                                                            //    // objCurrentStock.BatchCode = objCurrentStock.Barcode.ToString();
                                                            //    if (model.UserDetails != null)
                                                            //    {
                                                            //        objCurrentStock.UserId = model.UserDetails.UserId;
                                                            //        objCurrentStock.GroupId = model.UserDetails.GroupId;
                                                            //    }

                                                            //    i = 0;
                                                            //    i = entity.SaveChanges();

                                                            objResponse.ResponseStatus = "OK";
                                                            objResponse.ResponseMessage = "Updated Successfully!";
                                                        //}
                                                        //objResponse.ResponseStatus = "OK";
                                                        //objResponse.ResponseMessage = "Updated Successfully!";
                                                        //else
                                                        //{
                                                        //    objResponse.ResponseStatus = "FAILED";
                                                        //    objResponse.ResponseMessage = "Something went wrong!";
                                                        //}
                                                    }
                                                }
                                            }
                                        //}
                                        //else
                                        //{
                                        //    //objResponse.ResponseStatus = "FAILED";
                                        //    //objResponse.ResponseMessage = "Something went wrong!";
                                        //}
                                        //}
                                    }
                                    //}
                                }
                            }

                        }
                    //}
               // }
            }

            catch (Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = "Something went wrong!";
            }
            return objResponse;
        }
        public ResponseDetail DeleteProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            M_ProductMaster objDTProduct = new M_ProductMaster();
            M_BarCodeMaster objDTBarcode = new M_BarCodeMaster();
            M_TaxMaster objDTTax = new M_TaxMaster();
            TrnStockJv objDtStock = new TrnStockJv();
            Im_CurrentStock objDtCurrentStock = new Im_CurrentStock();
            int i = 0;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objDTProduct = (from p in entity.M_ProductMaster where p.ProductCode == model.ProductCode select p).FirstOrDefault();
                    if (objDTProduct != null)
                    {
                        objDTProduct.ActiveStatus = "N";
                        i = 0;
                        i = entity.SaveChanges();
                        if (i > 0)
                        {
                            string ProductCodeStr = model.ProductCode.ToString();
                            objDTBarcode = (from b in entity.M_BarCodeMaster where b.ProdId == ProductCodeStr && b.BarCode == model.ProductBarcodeDetails.ExisitingBarcode select b).FirstOrDefault();
                            if (objDTBarcode != null)
                            {
                                objDTBarcode.ActiveStatus = "N";
                                i = 0;
                                i = entity.SaveChanges();
                                if (i > 0)
                                {
                                    objDTTax = (from t in entity.M_TaxMaster where t.ProdCode == ProductCodeStr && t.StateCode == model.UserDetails.StateCode select t).FirstOrDefault();
                                    if (objDTTax != null)
                                    {
                                        objDTTax.ActiveStatus = "N";
                                        i = 0;
                                        i = entity.SaveChanges();
                                        if (i > 0)
                                        {
                                            objDtStock = (from s in entity.TrnStockJvs where s.ProdId == ProductCodeStr && s.JType == "O" select s).FirstOrDefault();
                                            if (objDtStock != null)
                                            {
                                                objDtStock.ActiveStatus = "N";
                                                i = 0;
                                                i = entity.SaveChanges();
                                                if (i > 0)
                                                {
                                                    objDtCurrentStock = (from c in entity.Im_CurrentStock where c.ProdId == ProductCodeStr && c.BType == "O" select c).FirstOrDefault();
                                                    if (objDtCurrentStock != null)
                                                    {
                                                        objDtCurrentStock.ActiveStatus = "N";
                                                        i = 0;
                                                        i = entity.SaveChanges();
                                                        if (i > 0)
                                                        {
                                                            objResponse.ResponseStatus = "OK";
                                                            objResponse.ResponseMessage = "Deleted Succesfully!";
                                                        }
                                                        else
                                                        {
                                                            objResponse.ResponseStatus = "OK";
                                                            objResponse.ResponseMessage = "Something went wrong!";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
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
        public List<ProductDetails> GetProductList(decimal LoginStateCode)
        {
            List<ProductDetails> objProductList = new List<ProductDetails>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objProductList = (from product in entity.M_ProductMaster
                                      join barcode in entity.M_BarCodeMaster on product.ProdId equals barcode.ProdId
                                      into barcodes
                                      from r in barcodes.Take(1)
                                          //join stockJv in entity.TrnStockJvs on product.ProdId equals stockJv.ProdId
                                          //join currentStock in entity.Im_CurrentStock on product.ProdId equals currentStock.ProdId
                                      join category in entity.M_CatMaster on product.CatId equals category.CatId
                                      join subcategory in entity.M_SubCatMaster on product.SubCatId equals subcategory.SubCatId
                                      from tax in entity.M_TaxMaster where tax.ProdCode== product.ProdId
                                      ///where tax.StateCode==LoginStateCode
                                      //where currentStock.BillType == "OP"
                                      select new ProductDetails
                                      {

                                          ProductId = (int)product.AId,
                                          BV = product.BV,
                                          CategoryId = (int)product.CatId,
                                          ProductCategoryDetails = new CategoryDetails
                                          {
                                              CategoryId = (int)category.CatId,
                                              CategoryName = category.CatName
                                          },
                                          CV = product.CV,
                                          IsCardIssue = product.IsCardIssue,
                                          PType = product.PType,
                                          DiscountInRs = product.DiscInRs,
                                          DiscountPer = product.Discount,
                                          HotSell = product.HotSell,
                                          IsActive = product.ActiveStatus == "Y" ? true : false,
                                          Message = product.MsgText,
                                          MessageStatus = product.MsgStatus,
                                          Weight=product.Weight,
                                          MinQtyStr = product.IMEINo,
                                          OnWebsite = product.OnWebSite,
                                          ProductCode = (int)product.ProductCode,
                                          ProductCodeStr = product.ProdId,
                                          ProductCommission = product.ProdCommssn,
                                          ProductDescription = product.ProductDesc,
                                          ProductImagePath = product.ImagePath,
                                          ProductName = product.ProductName,
                                          ImgFlag= product.ImgFlag,
                                          PV = product.PV,
                                          RP = product.RP,
                                          SpecialOffer = product.SpclOffer,
                                          SubCatgeoryId = (int)product.SubCatId,
                                          UserDefinedCode = product.UserProdId,
                                          HSNCode=product.HSNCode,
                                          ProductBarcodeDetails = new BarcodeDetails
                                          {
                                              ExisitingBarcode = r.BarCode,
                                              Barcode = r.BarCode,
                                              BarcodeType = r.BarCodeType,
                                              BType = r.BType,
                                              DP = r.DP,
                                              ExpDate = r.ExpDate,
                                              GenerateDate = r.GenerateDate,
                                              GeneratedBy = r.GenerateBy,
                                              IsActive = r.ActiveStatus == "Y" ? true : false,
                                              IsExpirable = r.IsExpired,
                                              MfgDate = r.MfgDate,
                                              MRP = r.MRP,
                                              //ProductId=barcode.ProdId,
                                              PurchaseRate = r.PRate,
                                              Remarks = r.Remarks,
                                              UserId = (int)r.UserId
                                          },
                                          ProductCurrentStockDetails = new CurrentStockModel
                                          {
                                              OpeningStockQty = product.OpStockQty
                                          },
                                          ProductSubCategoryDetails = new SubCategoryDetails
                                          {
                                              SubCategoryId = (int)subcategory.SubCatId,
                                              subCategoryName = subcategory.SubCatName
                                          },
                                          ProductTaxDetails = new TaxDetails
                                          {
                                              GSTTax = tax.VatTax,

                                          },

                                      }

                                    ).Distinct().ToList();

                }
            }
            catch (Exception ex)
            {

            }
            return objProductList;
        }

        public ProductDetails GetProductDetail(decimal ProductId,decimal LoginStateCode)
        {
            ProductDetails objProductList = new ProductDetails();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objProductList = (from product in entity.M_ProductMaster
                                      join barcode in entity.M_BarCodeMaster on product.ProdId equals barcode.ProdId
                                      
                                      //join stockJv in entity.TrnStockJvs on product.ProdId equals stockJv.ProdId
                                      //join currentStock in entity.Im_CurrentStock on product.ProdId equals currentStock.ProdId
                                      join category in entity.M_CatMaster on product.CatId equals category.CatId
                                      join subcategory in entity.M_SubCatMaster on product.SubCatId equals subcategory.SubCatId

                                      where product.ProductCode == ProductId
                                      from tax in entity.M_TaxMaster where tax.ProdCode== product.ProdId 
                                      //from stockJv in entity.TrnStockJvs where stockJv.ProdId==product.ProdId
                                      //from currentStock in entity.Im_CurrentStock where currentStock.ProdId== product.ProdId
                                      select new ProductDetails
                                      {

                                          ProductId = (int)product.AId,
                                          BV = product.BV,
                                          CategoryId = (int)product.CatId,
                                          ProductCategoryDetails = new CategoryDetails
                                          {
                                              CategoryId = (int)category.CatId,
                                              CategoryName = category.CatName
                                          },
                                          CV = product.CV,
                                          DiscountInRs = product.DiscInRs,
                                          DiscountPer = product.Discount,
                                          HotSell = product.HotSell,
                                         
                                          IsActive = product.ActiveStatus == "Y" ? true : false,
                                          Message = product.MsgText,
                                          MessageStatus = product.MsgStatus,
                                          MinQtyStr = product.IMEINo,
                                          OnWebsite = product.OnWebSite,
                                          ProductCode = (int)product.ProductCode,
                                          ProductCodeStr = product.ProdId,
                                          ProductCommission = product.ProdCommssn,
                                          ProductDescription = product.ProductDesc,
                                          ProductImagePath = product.ImagePath,
                                          ProductImagePath1= product.ImgPath1,
                                          ProductImagePath2 = product.ImgPath2,
                                          ProductImagePath3 = product.ImgPath3,
                                          ProductImagePath4 = product.ImgPath4,
                                          ProductImagePath5 = product.ImgPath5,
                                          ProductName = product.ProductName,
                                          IsForPC= product.IsForPC,
                                          IsFlexible= product.IsFlexible,
                                          PV = product.PV,
                                          RP = product.RP,
                                          Weight=product.Weight,
                                          HSNCode=product.HSNCode,
                                          SpecialOffer = product.SpclOffer,
                                          SubCatgeoryId = (int)product.SubCatId,
                                          UserDefinedCode = product.UserProdId,
                                          ProductFor = product.Imported,
                                          ImgFlag= product.ImgFlag,
                                          ProductBarcodeDetails = new BarcodeDetails
                                          {
                                              ExisitingBarcode = barcode.BarCode,
                                              Barcode = barcode.BarCode,
                                              BarcodeType = barcode.BarCodeType,
                                              BType = barcode.BType,
                                              DP = barcode.DP,
                                              ExpDate = barcode.ExpDate,
                                              GenerateDate = barcode.GenerateDate,
                                              GeneratedBy = barcode.GenerateBy,
                                              IsActive = barcode.ActiveStatus == "Y" ? true : false,
                                              IsExpirable = barcode.IsExpired,
                                              MfgDate = barcode.MfgDate,
                                              MRP = barcode.MRP,
                                              //ProductId=barcode.ProdId,
                                              PurchaseRate = barcode.PRate,
                                              Remarks = barcode.Remarks,
                                              UserId = (int)barcode.UserId

                                          },
                                          ProductCurrentStockDetails = new CurrentStockModel
                                          {
                                              OpeningStockQty = product.OpStockQty
                                          },
                                          ProductSubCategoryDetails = new SubCategoryDetails
                                          {
                                              SubCategoryId = (int)subcategory.SubCatId,
                                              subCategoryName = subcategory.SubCatName
                                          },
                                          ProductTaxDetails = new TaxDetails
                                          {
                                              GSTTax = tax.VatTax,

                                          },

                                      }


                                    ).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {

            }
            return objProductList;
        }

        
        //1030
        public List<ColorDetails> GetColorList(string ActiveFlag)
        {
            List<ColorDetails> objCategoryList = new List<ColorDetails>();

            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (!string.IsNullOrEmpty(ActiveFlag))
                    {
                        objCategoryList = (from category in entity.M_ColorMaster
                                           where category.ActiveStatus == ActiveFlag
                                           select new ColorDetails
                                           {
                                               Id = (int)category.Id,
                                               ColorName = category.ColorName,
                                               Description = category.Remarks,
                                               IsActive = category.ActiveStatus == "Y" ? true : false
                                           }
                                           ).ToList();
                    }
                    else
                    {
                        objCategoryList = (from category in entity.M_ColorMaster
                                           select new ColorDetails
                                           {
                                               Id = (int)category.Id,
                                               ColorName = category.ColorName,
                                               Description = category.Remarks,
                                               IsActive = category.ActiveStatus == "Y" ? true : false
                                           }
                                          ).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return objCategoryList;
        }

        public ResponseDetail AddColorDetails(ColorDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseStatus = "FAIL";
            objResponse.ResponseMessage = "Something went wrong!";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    M_ColorMaster objDTColor = new M_ColorMaster();
                    objDTColor = (from category in entity.M_ColorMaster where category.Id == model.Id select category).FirstOrDefault();
                    if (objDTColor == null)
                    {
                        objDTColor = new M_ColorMaster();
                    }
                    if (model.IsAdd != "Delete")
                    {
                        if (model.IsAdd == "Add")
                        {
                            decimal CategoryId = (from r in entity.M_ColorMaster select r.Id).DefaultIfEmpty(0).Max();
                            CategoryId = CategoryId + 1;
                            objDTColor.Id = (int)CategoryId;
                        }
                        objDTColor.ColorName = model.ColorName;
                        objDTColor.ActiveStatus = model.IsActive ? "Y" : "N";
                        objDTColor.RecTimeStamp = DateTime.Now;
                        objDTColor.Remarks = string.IsNullOrEmpty(model.Description) ? "" : model.Description;


                        if (model.IsAdd == "Add")
                        {
                            objDTColor.RecTimeStamp = DateTime.Now;
                            entity.M_ColorMaster.Add(objDTColor);
                        }

                    }
                    else
                    {
                        if (objDTColor != null)
                        {
                            objDTColor.ActiveStatus = "N";
                        }
                    }
                    try
                    {
                        int isSaved = entity.SaveChanges();
                        if (isSaved > 0)
                        {
                            if (model.IsAdd == "Add")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Saved Successfully!";
                            }
                            else if (model.IsAdd == "Edit")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Updated Successfully!";

                            }
                            else
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Deleted Successfully!";

                            }
                        }
                        else
                        {
                            objResponse.ResponseStatus = "Failed";
                            objResponse.ResponseMessage = "Something went wrong!";

                        }
                    }
                    catch (DbEntityValidationException ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }


        public ResponseDetail AddSizeDetails(SizeDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseStatus = "FAIL";
            objResponse.ResponseMessage = "Something went wrong!";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    M_SizeMaster objDTSize = new M_SizeMaster();
                    objDTSize = (from category in entity.M_SizeMaster where category.Id == model.Id select category).FirstOrDefault();
                    if (objDTSize == null)
                    {
                        objDTSize = new M_SizeMaster();
                    }
                    if (model.IsAdd != "Delete")
                    {
                        if (model.IsAdd == "Add")
                        {
                            decimal CategoryId = (from r in entity.M_SizeMaster select r.Id).DefaultIfEmpty(0).Max();
                            CategoryId = CategoryId + 1;
                            objDTSize.Id = (int)CategoryId;
                        }
                        objDTSize.Size = model.Size;
                        objDTSize.ActiveStatus = model.IsActive ? "Y" : "N";
                        objDTSize.RecTimeStamp = DateTime.Now;
                        objDTSize.Remarks = string.IsNullOrEmpty(model.Description) ? "" : model.Description;


                        if (model.IsAdd == "Add")
                        {
                            objDTSize.RecTimeStamp = DateTime.Now;
                            entity.M_SizeMaster.Add(objDTSize);
                        }

                    }
                    else
                    {
                        if (objDTSize != null)
                        {
                            objDTSize.ActiveStatus = "N";
                        }
                    }
                    try
                    {
                        int isSaved = entity.SaveChanges();
                        if (isSaved > 0)
                        {
                            if (model.IsAdd == "Add")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Saved Successfully!";
                            }
                            else if (model.IsAdd == "Edit")
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Updated Successfully!";

                            }
                            else
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Deleted Successfully!";

                            }
                        }
                        else
                        {
                            objResponse.ResponseStatus = "Failed";
                            objResponse.ResponseMessage = "Something went wrong!";

                        }
                    }
                    catch (DbEntityValidationException ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }

        //1030
        public List<SizeDetails> GetSizeList(string ActiveFlag)
        {
            List<SizeDetails> objSizeList = new List<SizeDetails>();

            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (!string.IsNullOrEmpty(ActiveFlag))
                    {
                        objSizeList = (from category in entity.M_SizeMaster
                                       where category.ActiveStatus == ActiveFlag
                                       select new SizeDetails
                                       {
                                           Id = (int)category.Id,
                                           Size = category.Size,
                                           Description = category.Remarks,
                                           IsActive = category.ActiveStatus == "Y" ? true : false
                                       }
                                           ).ToList();
                    }
                    else
                    {
                        objSizeList = (from category in entity.M_SizeMaster
                                       select new SizeDetails
                                       {
                                           Id = (int)category.Id,
                                           Size = category.Size,
                                           Description = category.Remarks,
                                           IsActive = category.ActiveStatus == "Y" ? true : false
                                       }
                                          ).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return objSizeList;
        }

        //1030
        public ResponseDetail SaveProductMaster1030(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            M_ProductMaster objDTProduct = new M_ProductMaster();
            M_BarCodeMaster objDTBarcode = new M_BarCodeMaster();
            M_TaxMaster objDTTax = new M_TaxMaster();
            TrnStockJv objDtStock = new TrnStockJv();
            Im_CurrentStock objDtCurrentStock = new Im_CurrentStock();
            string objversion = "";
            M_FiscalMaster objFiscalMaster = new M_FiscalMaster();
            int i = 0;
            // decimal ProductBarcodeId = 0;
            //decimal ProductTaxId = 0 ;
            decimal BatchCode = 1000000;
            decimal StockJNo = 1001;
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (model != null)
                    {
                        objFiscalMaster = (from result in entity.M_FiscalMaster where result.ActiveStatus == "Y" select result).FirstOrDefault();
                        objDTProduct = (from result in entity.M_ProductMaster where result.ProductCode == model.ProductCode select result).FirstOrDefault();
                        objversion = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();
                        if (objDTProduct == null)
                        {
                            objDTProduct = new M_ProductMaster();
                            model.ProductCode = MaxProductCode();
                        }

                        i = 0;
                        
                        objDTProduct.ProductCode = model.ProductCode;
                        objDTProduct.ProductName = model.ProductName;
                        objDTProduct.ProdId = model.ProductCode.ToString();
                        objDTProduct.PV = model.PV;
                        objDTProduct.RP = model.RP;
                        objDTProduct.SpclOffer = model.SpecialOffer;
                        objDTProduct.HotSell = model.HotSell;
                        objDTProduct.ImagePath = string.IsNullOrEmpty(model.ProductImagePath) ? "" : model.ProductImagePath;
                        objDTProduct.ImgPath1 = string.IsNullOrEmpty(model.ProductImagePath1) ? "" : model.ProductImagePath1;
                        objDTProduct.ImgPath2 = string.IsNullOrEmpty(model.ProductImagePath2) ? "" : model.ProductImagePath2;
                        objDTProduct.ImgPath3 = string.IsNullOrEmpty(model.ProductImagePath3) ? "" : model.ProductImagePath3;
                        objDTProduct.ImgPath4 = string.IsNullOrEmpty(model.ProductImagePath4) ? "" : model.ProductImagePath4;
                        objDTProduct.ImgPath5 = string.IsNullOrEmpty(model.ProductImagePath5) ? "" : model.ProductImagePath5;
                        objDTProduct.IsImage = string.IsNullOrEmpty(model.ProductImagePath) ? "N" : "Y";
                        objDTProduct.ActiveStatus = model.IsActive ? "Y" : "N";
                        objDTProduct.OnWebSite = model.OnWebsite;
                        objDTProduct.MsgText = string.IsNullOrEmpty(model.Message) ? "" : model.Message;
                        objDTProduct.MsgStatus = model.MessageStatus;
                        objDTProduct.Imported = model.ProductFor;
                        //objDTProduct.IsCardIssue = model.DisableForFrOrder;
                        // objDTProduct.MinQty = model.MinQty;
                        objDTProduct.IMEINo = model.MinQty.ToString();
                        objDTProduct.ProdCommssn = model.ProductCommission;
                        objDTProduct.Discount = model.DiscountPer;
                        objDTProduct.DiscInRs = model.DiscountInRs;
                        objDTProduct.UserProdId = string.IsNullOrEmpty(model.UserDefinedCode) ? "" : model.UserDefinedCode;
                        objDTProduct.SubCatId = model.SubCatgeoryId;
                        objDTProduct.CatId = model.CategoryId;
                        objDTProduct.ProductDesc = model.ProductDescription;
                        objDTProduct.CV = model.CV;
                        objDTProduct.BV = model.BV;
                        objDTProduct.HSNCode = string.IsNullOrEmpty(model.HSNCode) ? "" : model.HSNCode;
                        objDTProduct.ProductSize = model.Sizes;
                        objDTProduct.ProductColor = model.Colors;
                        if (model.UserDetails != null)
                        {
                            objDTProduct.UserId = model.UserDetails.UserId;
                        }
                        objDTProduct.RecTimeStamp = DateTime.Now;

                        objDTProduct.GenerateBy = model.UserDetails.PartyCode;
                        objDTProduct.BrandCode = 0;
                        objDTProduct.ProductType = "P";
                        objDTProduct.Prefix = "";
                        objDTProduct.ItemType = "";
                        objDTProduct.BuyingTax = 0;
                        objDTProduct.Weight = model.Weight;
                        objDTProduct.PurchaseRate = model.ProductBarcodeDetails.PurchaseRate;
                        objDTProduct.DP1 = 0;
                        objDTProduct.OtherStateDP = 0;
                        objDTProduct.Exp = 0;
                        objDTProduct.Costing = 0;
                        objDTProduct.FundPoint = 0;
                        if (model.DiscountPer > 0 || model.DiscountInRs > 0)
                        {
                            objDTProduct.IsDiscount = "Y";
                        }
                        else if (model.DiscountPer > 0 && model.DiscountInRs == 0)
                        {
                            objDTProduct.IsDiscount = "Y";
                        }
                        else if (model.DiscountPer == 0 && model.DiscountInRs > 0)
                        {
                            objDTProduct.IsDiscount = "Y";
                        }
                        else
                        {
                            objDTProduct.IsDiscount = "N";
                        }
                        objDTProduct.VDiscount = model.VDiscount;
                        objDTProduct.GRate = 0;
                        objDTProduct.GMCharge = 0;
                        objDTProduct.GMType = "";
                        objDTProduct.IsCardIssue = "N";
                        objDTProduct.Remarks = "";
                        objDTProduct.TaxType = "I";
                        decimal BarcodeId = (from result in entity.M_BarCodeMaster select result.BId).DefaultIfEmpty(1000000).Max();
                        BarcodeId = BarcodeId + 1;
                        objDTProduct.BId = BarcodeId;
                        objDTProduct.Imported = "N";
                        objDTProduct.BNo = "0";
                        objDTProduct.PType = "G";
                        if (model.ProductBarcodeDetails.BarcodeType == "System Generated")
                        { objDTProduct.BarcodeType = "W"; }
                        else
                        {
                            objDTProduct.BarcodeType = "O";
                        }

                        objDTProduct.BCode = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? decimal.Parse(model.ProductBarcodeDetails.Barcode) : 0;
                        objDTProduct.Barcode = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                        objDTProduct.OpStockQty = model.ProductCurrentStockDetails.OpeningStockQty;
                        objDTProduct.LastModified = "";
                        objDTProduct.Val1 = 0;
                        objDTProduct.Val2 = 0;
                        objDTProduct.Company = "U";
                        objDTProduct.PurchaseFrom = "WR";
                        objDTProduct.PurchaseStore = "WR";
                        objDTProduct.IsFlexible = model.IsFlexible;
                        objDTProduct.IsForPC = model.IsForPC;
                        objDTProduct.SubQty = 0;
                        objDTProduct.CalcKitRate = "N";
                        objDTProduct.FlexiQty = 0;
                        //objDTProduct.ImgPath1 = "";
                        //objDTProduct.ImgPath2 = "";
                        //objDTProduct.ImgPath3 = "";
                        //objDTProduct.ImgPath4 = "";
                        objDTProduct.AlterID = model.UserDetails.UserId;
                        // objDTProduct.UnitID = 3;
                        // objDTProduct.UnitName = "Pc.";
                        objDTProduct.MRP = model.ProductBarcodeDetails.MRP;
                        objDTProduct.DP = model.ProductBarcodeDetails.DP;
                        objDTProduct.IsExpired = model.ProductBarcodeDetails.IsExpirable;
                        DateTime MfgDate = DateTime.Now;
                        DateTime ExpDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(model.ProductBarcodeDetails.MfgDateStr))
                        {
                            var SplitDate = model.ProductBarcodeDetails.MfgDateStr.Split('-');
                            string NewDate = (SplitDate[1].Length == 1 ? "0" + SplitDate[1] : SplitDate[1]) + "/" + (SplitDate[0].Length == 1 ? "0" + SplitDate[0] : SplitDate[0]) + "/" + SplitDate[2];
                            MfgDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                            MfgDate = MfgDate.Date;
                        }
                        if (!string.IsNullOrEmpty(model.ProductBarcodeDetails.ExpDateStr))
                        {
                            var SplitDate = model.ProductBarcodeDetails.ExpDateStr.Split('-');
                            string NewDate = (SplitDate[1].Length == 1 ? "0" + SplitDate[1] : SplitDate[1]) + "/" + (SplitDate[0].Length == 1 ? "0" + SplitDate[0] : SplitDate[0]) + "/" + SplitDate[2];
                            ExpDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                            ExpDate = ExpDate.Date;
                        }

                        objDTProduct.MfgDate = MfgDate.Date;
                        objDTProduct.ExpDate = ExpDate.Date;
                        // objDTProduct.ProductBarcodeId = ProductBarcodeId;
                        // objDTProduct.ProductTaxId = ProductTaxId;
                        bool isBarcodeMasterSave = true, isTaxMasterSave = true;
                        objDTProduct.OrderDesc = "";
                        objDTProduct.GenerateBy = model.UserDetails.UserName;
                        objDTProduct.HSNCode = "";
                        objDTProduct.ProductColor = "";
                        objDTProduct.ProductSize = "";
                        objDTProduct.ImgFlag = model.ImgFlag;
                        entity.M_ProductMaster.Add(objDTProduct);
                        try
                        {
                            i = entity.SaveChanges();

                            if (i > 0)
                            {
                                if (model.ProductBarcodeDetails != null)
                                {
                                    objDTBarcode.BId = BarcodeId;
                                    objDTBarcode.SupplierCode = "0";
                                    objDTBarcode.BCode = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? decimal.Parse(model.ProductBarcodeDetails.Barcode) : 0;
                                    objDTBarcode.Company = "";
                                    objDTBarcode.Imported = "N";
                                    objDTBarcode.LastModified = "";
                                    objDTBarcode.DP1 = 0;
                                    objDTBarcode.Val1 = 0;
                                    objDTBarcode.Val2 = 0;


                                    objDTBarcode.BarCode = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                                    objDTBarcode.BatchNo = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                                    objDTBarcode.BarCodeType = model.ProductBarcodeDetails.BarcodeType;
                                    objDTBarcode.DP = model.ProductBarcodeDetails.DP;
                                    if (model.ProductBarcodeDetails.IsExpirable == "Y")
                                        objDTBarcode.ExpDate = ExpDate.Date;
                                    else
                                    {
                                        objDTBarcode.ExpDate = DateTime.Now;
                                    }
                                    objDTBarcode.MfgDate = MfgDate.Date;
                                    objDTBarcode.MRP = model.ProductBarcodeDetails.MRP;
                                    objDTBarcode.IsExpired = model.ProductBarcodeDetails.IsExpirable;
                                    objDTBarcode.ProdId = model.ProductCode.ToString();
                                    objDTBarcode.PRate = model.ProductBarcodeDetails.PurchaseRate;
                                    objDTBarcode.Remarks = string.IsNullOrEmpty(model.ProductBarcodeDetails.Remarks) ? "" : model.ProductBarcodeDetails.Remarks;
                                    objDTBarcode.ActiveStatus = model.IsActive ? "Y" : "N";
                                    objDTBarcode.GenerateDate = DateTime.Now;
                                    if (model.UserDetails != null)
                                    {
                                        objDTBarcode.GenerateBy = model.UserDetails.UserName;
                                        objDTBarcode.UserId = model.UserDetails.UserId;
                                        //objDTBarcode.AlterID = model.UserDetails.UserId;
                                    }
                                    objDTBarcode.RecTimeStamp = DateTime.Now;

                                    if (model.ProductBarcodeDetails.BarcodeType == "System Generated")
                                    { objDTBarcode.BType = "W"; }
                                    else
                                    {
                                        objDTBarcode.BType = "O";
                                    }

                                    //BatchCode = (from result in entity.BarcodeMasters select result.BatchCode).Max()??1000000;

                                    //objDTBarcode.BatchCode = BatchCode + 1;
                                    objDTBarcode.BatchNo = (!string.IsNullOrEmpty(model.ProductBarcodeDetails.Barcode)) ? model.ProductBarcodeDetails.Barcode : "0";
                                    BatchCode = (!string.IsNullOrEmpty(objDTBarcode.BatchNo)) ? decimal.Parse(objDTBarcode.BatchNo) : 1000001;
                                    entity.M_BarCodeMaster.Add(objDTBarcode);
                                    try
                                    {
                                        i = entity.SaveChanges();
                                        if (i > 0)
                                        {
                                            //ProductBarcodeId = (from result in entity.M_BarCodeMaster select result.AId).Max();
                                            isBarcodeMasterSave = true;
                                        }
                                        else
                                        {
                                            isBarcodeMasterSave = false;
                                        }
                                    }
                                    catch (DbEntityValidationException ex)
                                    {

                                    }
                                }

                                if (model.ProductTaxDetails != null)
                                {
                                    objDTTax.WithCForm = 0;
                                    objDTTax.CstTax = 0;
                                    objDTTax.ActiveStatus = model.IsActive ? "Y" : "N";
                                    objDTTax.VatTax = model.ProductTaxDetails.GSTTax;
                                    objDTTax.ProdName = model.ProductName;
                                    objDTTax.Imported = "N";
                                    objDTTax.LastModified = DateTime.Now.ToString();
                                    objDTTax.Remarks = "";
                                    objDTTax.Company = "";
                                    //objDTTax.GeneratedDate = DateTime.Now;
                                    objDTTax.RecTimeStamp = DateTime.Now;

                                    objDTTax.AValue = 0;
                                    objDTTax.STax = 0;
                                    if (model.UserDetails != null)
                                    {
                                        // objDTTax.StateCode = model.UserDetails.StateCode;
                                        objDTTax.StateCode = (int)(from r in entity.M_CompanyMaster select r.CompState).FirstOrDefault();
                                        objDTTax.UserId = model.UserDetails.UserId;
                                        objDTTax.GenerateBy = model.UserDetails.UserName;
                                    }
                                    objDTTax.ProdCode = model.ProductCode.ToString();
                                    i = 0;
                                    entity.M_TaxMaster.Add(objDTTax);
                                    try
                                    {
                                        i = entity.SaveChanges();
                                        if (i > 0)
                                        {
                                            //ProductTaxId = (from result in entity.M_TaxMaster select result.AId).Max();
                                            isTaxMasterSave = true;
                                        }
                                        else
                                        {
                                            isTaxMasterSave = false;
                                        }
                                    }
                                    catch (DbEntityValidationException ex)
                                    {

                                    }
                                }


                                // i = 0;
                                objDtStock.Barcode = model.ProductBarcodeDetails.Barcode;
                                objDtStock.BatchNo = BatchCode.ToString();
                                if (model.UserDetails != null)
                                {
                                    objDtStock.UserId = model.UserDetails.UserId;
                                    objDtStock.UserName = model.UserDetails.UserName;
                                }
                                objDtStock.RecTimeStamp = DateTime.Now;
                                if (objFiscalMaster != null)
                                    objDtStock.FSessId = objFiscalMaster.FSessId;
                                else
                                    objDtStock.FSessId = 0;
                                var res = (from result in entity.TrnStockJvs select result).ToList();
                                if (res.Count == 0)
                                    objDtStock.JNo = 1001;
                                else
                                {
                                    decimal MaxJNo = (from r in res select r.JNo).DefaultIfEmpty(0).Max();
                                    objDtStock.JNo = MaxJNo + 1;
                                }
                                objDtStock.JvNo = "OPN/" + objDtStock.JNo;
                                StockJNo = objDtStock.JNo == 0 ? 1001 : objDtStock.JNo;
                                objDtStock.ProdId = model.ProductCode.ToString();
                                objDtStock.ProductName = (from result in entity.M_ProductMaster where result.ProductCode == model.ProductCode select result.ProductName).FirstOrDefault() ?? "";
                                objDtStock.ProdType = "P";
                                objDtStock.Qty = model.ProductCurrentStockDetails.OpeningStockQty;
                                objDtStock.Remarks = "Opening Stock Of Product Registration";
                                objDtStock.RefNo = objDtStock.JvNo;
                                objDtStock.JType = "O";
                                objDtStock.ActiveStatus = model.IsActive ? "Y" : "N";
                                objDtStock.Version = objversion;
                                if (model.UserDetails != null)
                                {
                                    objDtStock.PartyName = model.UserDetails.PartyName;
                                    objDtStock.FCode = model.UserDetails.FCode;
                                    objDtStock.SoldBy = model.UserDetails.FCode;
                                }

                                objDtStock.StockDate = DateTime.Now;
                                try
                                {
                                    entity.TrnStockJvs.Add(objDtStock);
                                    i = 0;
                                    i = entity.SaveChanges();
                                    if (i > 0)
                                    {

                                        //if (model.UserDetails != null)
                                        //{
                                        //    objDtCurrentStock.FCode = model.UserDetails.FCode;
                                        //}
                                        //if (objFiscalMaster != null)
                                        //{
                                        //    objDtCurrentStock.FSessId = objFiscalMaster.FSessId;
                                        //}
                                        //objDtCurrentStock.ActiveStatus = model.IsActive ? "Y" : "N";
                                        //objDtCurrentStock.ProdId = model.ProductCode.ToString();
                                        //objDtCurrentStock.ProdType = "P";
                                        //objDtCurrentStock.Qty = model.ProductCurrentStockDetails.OpeningStockQty;
                                        //objDtCurrentStock.RefNo = "OPN/" + StockJNo;
                                        //objDtCurrentStock.SupplierCode = model.UserDetails.FCode;
                                        //objDtCurrentStock.Version = objversion;
                                        //objDtCurrentStock.Remarks = "Opening Stock For Product Registration";
                                        //objDtCurrentStock.EntryBy = objDtCurrentStock.FCode;
                                        //objDtCurrentStock.RecTimeStamp = DateTime.Now;
                                        //objDtCurrentStock.BillType = "OP";
                                        //objDtCurrentStock.BType = "OP";
                                        //objDtCurrentStock.SType = "I";
                                        //objDtCurrentStock.StockFor = "Gen.Thr.TRG.OpBl";
                                        //objDtCurrentStock.IsDisp = "N";
                                        //objDtCurrentStock.InvoiceNo = "";
                                        //objDtCurrentStock.Barcode = model.ProductBarcodeDetails.Barcode;
                                        //objDtCurrentStock.BatchCode = BatchCode.ToString();
                                        //if (model.UserDetails != null)
                                        //{
                                        //    objDtCurrentStock.UserId = model.UserDetails.UserId;
                                        //    objDtCurrentStock.GroupId = model.UserDetails.GroupId;
                                        //}
                                        //objDtCurrentStock.StockDate = DateTime.Now;


                                        //entity.Im_CurrentStock.Add(objDtCurrentStock);
                                        //i = 0;
                                        //i = entity.SaveChanges();

                                        objResponse.ResponseStatus = "OK";
                                        objResponse.ResponseMessage = "Saved Successfully!";

                                    }
                                    else
                                    {
                                        objResponse.ResponseStatus = "FAILED";
                                        objResponse.ResponseMessage = "Something went wrong!";
                                    }

                                }
                                catch (DbEntityValidationException e)
                                {
                                    foreach (var eve in e.EntityValidationErrors)
                                    {
                                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                        foreach (var ve in eve.ValidationErrors)
                                        {
                                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                                ve.PropertyName, ve.ErrorMessage);
                                        }
                                    }
                                }
                            }

                        }
                        catch (DbEntityValidationException ex)
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = ex.Message + " Inner Exception :- " + ex.InnerException.Message;
            }
            return objResponse;
        }


        //1030
        public ResponseDetail EditProductMaster1030(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            M_ProductMaster objDTProduct = new M_ProductMaster();
            M_BarCodeMaster objDTBarcode = new M_BarCodeMaster();
            M_TaxMaster objDTTax = new M_TaxMaster();
            TrnStockJv objDTStock = new TrnStockJv();
            Im_CurrentStock objCurrentStock = new Im_CurrentStock();
            objResponse.ResponseMessage = "Something Went Wrong!";
            objResponse.ResponseStatus = "FAILED";
            int i = 0;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objDTProduct = (from p in entity.M_ProductMaster where p.ProductCode == model.ProductCode select p).FirstOrDefault();
                    if (objDTProduct != null)
                    {
                        if (model != null)
                        {
                            i = 0;
                            objDTProduct.ProductCode = model.ProductCode;
                            objDTProduct.ProductName = model.ProductName;
                            objDTProduct.ProdId = model.ProductCode.ToString();
                            objDTProduct.PV = model.PV;
                            objDTProduct.RP = model.RP;
                            objDTProduct.SpclOffer = model.SpecialOffer;
                            objDTProduct.HotSell = model.HotSell;
                            objDTProduct.ImagePath = string.IsNullOrEmpty(model.ProductImagePath) ? objDTProduct.ImagePath : model.ProductImagePath;
                            objDTProduct.ImgPath1 = string.IsNullOrEmpty(model.ProductImagePath1) ? objDTProduct.ImgPath1 : model.ProductImagePath1;
                            objDTProduct.ImgPath2 = string.IsNullOrEmpty(model.ProductImagePath2) ? objDTProduct.ImgPath2 : model.ProductImagePath2;
                            objDTProduct.ImgPath3 = string.IsNullOrEmpty(model.ProductImagePath3) ? objDTProduct.ImgPath3 : model.ProductImagePath3;
                            objDTProduct.ImgPath4 = string.IsNullOrEmpty(model.ProductImagePath4) ? objDTProduct.ImgPath4 : model.ProductImagePath4;
                            objDTProduct.ImgPath5 = string.IsNullOrEmpty(model.ProductImagePath5) ? objDTProduct.ImgPath5 : model.ProductImagePath5;
                            objDTProduct.IsImage = string.IsNullOrEmpty(model.ProductImagePath) ? "N" : "Y";
                            objDTProduct.ActiveStatus = model.IsActive ? "Y" : "N";
                            objDTProduct.OnWebSite = model.OnWebsite;
                            objDTProduct.IsFlexible = model.IsFlexible;
                            objDTProduct.Imported = model.ProductFor;
                            //objDTProduct.IsCardIssue = model.DisableForFrOrder ;
                            objDTProduct.IsForPC = model.IsForPC;
                            objDTProduct.MsgText = string.IsNullOrEmpty(model.Message) ? "" : model.Message;
                            objDTProduct.MsgStatus = model.MessageStatus;
                            objDTProduct.Weight = model.Weight;
                            objDTProduct.IMEINo = model.MinQty.ToString();
                            objDTProduct.ProdCommssn = model.ProductCommission;
                            objDTProduct.Discount = model.DiscountPer;
                            objDTProduct.VDiscount = model.VDiscount;
                            objDTProduct.DiscInRs = model.DiscountInRs;
                            objDTProduct.UserProdId = string.IsNullOrEmpty(model.UserDefinedCode) ? "" : model.UserDefinedCode;
                            objDTProduct.SubCatId = model.SubCatgeoryId;
                            objDTProduct.CatId = model.CategoryId;
                            objDTProduct.ProductDesc = model.ProductDescription;
                            objDTProduct.CV = model.CV;
                            objDTProduct.BV = model.BV;
                            objDTProduct.HSNCode = string.IsNullOrEmpty(model.HSNCode) ? "" : model.HSNCode;
                            objDTProduct.ProductColor = model.Colors;
                            objDTProduct.ProductSize = model.Sizes;
                            objDTProduct.GenerateBy = model.UserDetails.PartyCode;

                            objDTProduct.PurchaseRate = model.ProductBarcodeDetails.PurchaseRate;

                            if (model.DiscountPer > 0 || model.DiscountInRs > 0)
                            {
                                objDTProduct.IsDiscount = "Y";
                            }
                            else if (model.DiscountPer > 0 && model.DiscountInRs == 0)
                            {
                                objDTProduct.IsDiscount = "Y";
                            }
                            else if (model.DiscountPer == 0 && model.DiscountInRs > 0)
                            {
                                objDTProduct.IsDiscount = "Y";
                            }
                            else
                            {
                                objDTProduct.IsDiscount = "N";
                            }


                         
                            if (model.UserDetails != null)
                            {
                                objDTProduct.LastModified = model.UserDetails.UserName;
                            }


                            objDTProduct.AlterID = model.UserDetails.UserId;

                            objDTProduct.MRP = model.ProductBarcodeDetails.MRP;
                            objDTProduct.DP = model.ProductBarcodeDetails.DP;
                            objDTProduct.IsExpired = model.ProductBarcodeDetails.IsExpirable;
                            DateTime MfgDate = DateTime.Now;
                            DateTime ExpDate = DateTime.Now;
                            if (!string.IsNullOrEmpty(model.ProductBarcodeDetails.MfgDateStr))
                            {
                                var SplitDate = model.ProductBarcodeDetails.MfgDateStr.Split('-');
                                string NewDate = (SplitDate[1].Length == 1 ? "0" + SplitDate[1] : SplitDate[1]) + "/" + (SplitDate[0].Length == 1 ? "0" + SplitDate[0] : SplitDate[0]) + "/" + SplitDate[2];
                                MfgDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                MfgDate = MfgDate.Date;
                            }
                            if (!string.IsNullOrEmpty(model.ProductBarcodeDetails.ExpDateStr))
                            {
                                var SplitDate = model.ProductBarcodeDetails.ExpDateStr.Split('-');
                                string NewDate = (SplitDate[1].Length == 1 ? "0" + SplitDate[1] : SplitDate[1]) + "/" + (SplitDate[0].Length == 1 ? "0" + SplitDate[0] : SplitDate[0]) + "/" + SplitDate[2];
                                ExpDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                ExpDate = ExpDate.Date;
                            }
                            objDTProduct.MfgDate = MfgDate.Date;
                            objDTProduct.ExpDate = ExpDate.Date;
                            i = entity.SaveChanges();
                          
                            string ProductCodeStr = model.ProductCode.ToString();
                            objDTBarcode = (from b in entity.M_BarCodeMaster where b.ProdId == ProductCodeStr && b.BarCode == model.ProductBarcodeDetails.ExisitingBarcode select b).FirstOrDefault();
                            if (objDTBarcode != null)
                            {
                              
                                if (model.UserDetails != null)
                                {
                                    objDTBarcode.LastModified = model.UserDetails.UserName;
                                }

                                objDTBarcode.DP = model.ProductBarcodeDetails.DP;
                                if (model.ProductBarcodeDetails.IsExpirable == "Y")
                                    objDTBarcode.ExpDate = ExpDate.Date;
                                else
                                {
                                    objDTBarcode.ExpDate = DateTime.Now;
                                }
                                objDTBarcode.MfgDate = MfgDate.Date;
                                objDTBarcode.MRP = model.ProductBarcodeDetails.MRP;
                                objDTBarcode.IsExpired = model.ProductBarcodeDetails.IsExpirable;
                                objDTBarcode.ProdId = model.ProductCode.ToString();
                                objDTBarcode.PRate = model.ProductBarcodeDetails.PurchaseRate;
                                objDTBarcode.Remarks = string.IsNullOrEmpty(model.ProductBarcodeDetails.Remarks) ? "" : model.ProductBarcodeDetails.Remarks;
                                objDTBarcode.ActiveStatus = model.IsActive ? "Y" : "N";
                                if (model.ProductBarcodeDetails.BarcodeType == "System Generated")
                                { objDTBarcode.BType = "W"; }
                                else
                                {
                                    objDTBarcode.BType = "O";
                                }

                              
                                i = 0;
                                i = entity.SaveChanges();
                                
                                if (model.ProductTaxDetails != null)
                                {
                                    objDTTax = (from t in entity.M_TaxMaster where t.ProdCode == ProductCodeStr && t.StateCode == model.UserDetails.StateCode select t).FirstOrDefault();
                                    if (objDTTax != null)
                                    {
                                        objDTTax.ActiveStatus = model.IsActive ? "Y" : "N";
                                        objDTTax.VatTax = model.ProductTaxDetails.GSTTax;
                                        objDTTax.ProdName = model.ProductName;
                                        objDTTax.LastModified = DateTime.Now.ToString();
                                        if (model.UserDetails != null)
                                        {
                                            objDTTax.StateCode = model.UserDetails.StateCode;
                                            objDTTax.UserId = model.UserDetails.UserId;
                                            objDTTax.GenerateBy = model.UserDetails.UserName;
                                        }
                                        objDTTax.ProdCode = model.ProductCode.ToString();
                                        i = 0;
                                        i = entity.SaveChanges();
                                      
                                        objDTStock = (from s in entity.TrnStockJvs where s.ProdId == ProductCodeStr && s.JType == "O" select s).FirstOrDefault();
                                        if (objDTStock != null)
                                        {
                                            //objDTStock.Barcode = model.ProductBarcodeDetails.Barcode;
                                            // objDTStock.BatchNo = objDTStock.Barcode.ToString();
                                            objDTStock.ActiveStatus = model.IsActive ? "Y" : "N";
                                            objDTStock.ProductName = (from result in entity.M_ProductMaster where result.ProductCode == model.ProductCode select result.ProductName).FirstOrDefault() ?? "";
                                            if (model.UserDetails != null)
                                            {
                                                objDTStock.PartyName = model.UserDetails.PartyName;
                                                objDTStock.FCode = model.UserDetails.FCode;
                                                objDTStock.SoldBy = model.UserDetails.FCode;
                                            }

                                            // objDTStock.StockDate = DateTime.Now;
                                            i = 0;
                                            i = entity.SaveChanges();
                                           

                                           

                                            objResponse.ResponseStatus = "OK";
                                            objResponse.ResponseMessage = "Updated Successfully!";
                                           
                                        }
                                    }

                                    objResponse.ResponseStatus = "OK";
                                    objResponse.ResponseMessage = "Updated Successfully!";
                                }
                                
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

        //1030
        public ResponseDetail DeleteProductMaster1030(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            M_ProductMaster objDTProduct = new M_ProductMaster();
            M_BarCodeMaster objDTBarcode = new M_BarCodeMaster();
            M_TaxMaster objDTTax = new M_TaxMaster();
            TrnStockJv objDtStock = new TrnStockJv();
            Im_CurrentStock objDtCurrentStock = new Im_CurrentStock();
            int i = 0;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objDTProduct = (from p in entity.M_ProductMaster where p.ProductCode == model.ProductCode select p).FirstOrDefault();
                    if (objDTProduct != null)
                    {
                        objDTProduct.ActiveStatus = "N";
                        i = 0;
                        i = entity.SaveChanges();
                        if (i > 0)
                        {
                            string ProductCodeStr = model.ProductCode.ToString();
                            objDTBarcode = (from b in entity.M_BarCodeMaster where b.ProdId == ProductCodeStr select b).FirstOrDefault();
                            if (objDTBarcode != null)
                            {
                                objDTBarcode.ActiveStatus = "N";
                                i = 0;
                                i = entity.SaveChanges();
                                if (i > 0)
                                {
                                    objDTTax = (from t in entity.M_TaxMaster where t.ProdCode == ProductCodeStr select t).FirstOrDefault();
                                    if (objDTTax != null)
                                    {
                                        objDTTax.ActiveStatus = "N";
                                        i = 0;
                                        i = entity.SaveChanges();
                                        if (i > 0)
                                        //{
                                        //    objDtStock = (from s in entity.TrnStockJvs where s.ProdId == ProductCodeStr && s.JType == "O" select s).FirstOrDefault();
                                        //    if (objDtStock != null)
                                        //    {
                                        //        objDtStock.ActiveStatus = "N";
                                        //        i = 0;
                                        //        i = entity.SaveChanges();
                                        //        if (i > 0)
                                        //        {
                                        //            objDtCurrentStock = (from c in entity.Im_CurrentStock where c.ProdId == ProductCodeStr && c.BType == "O" select c).FirstOrDefault();
                                        //            if (objDtCurrentStock != null)
                                        //            {
                                        //                objDtCurrentStock.ActiveStatus = "N";
                                        //                i = 0;
                                        //                i = entity.SaveChanges();
                                        //                if (i > 0)
                                        {
                                            objResponse.ResponseStatus = "OK";
                                            objResponse.ResponseMessage = "Deleted Succesfully!";
                                        }
                                        else
                                        {
                                            objResponse.ResponseStatus = "OK";
                                            objResponse.ResponseMessage = "Something went wrong!";
                                        }
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                    }
                                }
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
    }
}
