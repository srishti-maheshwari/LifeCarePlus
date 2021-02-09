using InventoryManagement.App_Start;
using InventoryManagement.Business;
using InventoryManagement.Common;
using InventoryManagement.Entity.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: CategoryMaster
        ProductManager objProductManager = new ProductManager();
        LogManager objLogManager = new LogManager();
   
        public ActionResult CategoryMaster()
        {
            CategoryDetails objCategoryModel = new CategoryDetails();
            objCategoryModel.IsAdd = "Add";
            objCategoryModel.IsActive = true;

            var AccessTo = new UserController().UserCanAccess((Session["LoginUser"] as User).UserId, "CategoryMaster");
            if (!string.IsNullOrEmpty(AccessTo))
            {
                ViewBag.UserCanAccess = AccessTo;
                return View(objCategoryModel);
            }
            else
                return RedirectToAction("Dashboard", "Home");           
           
        }

        // POST: SaveCategoryMaster
        [HttpPost]
        public ActionResult SaveCategoryMaster(CategoryDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (model != null)
            {
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;
                }
                objResponse = objProductManager.AddCategoryDetails(model);
                //Added log
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); 
                objLogManager.SaveLog(Session["LoginUser"] as User, model.IsAdd + " Category -" + model.CategoryName, myIP + currentDate);

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        // GET: SubCategoryMaster
        [SessionExpire]
        public ActionResult SubCategoryMaster()
        {
            List<SelectListItem> objCategoryList = new List<SelectListItem>();
            List<SelectListItem> objChildCategoryList = new List<SelectListItem>();
            var result = objProductManager.GetCategoryList("Y");
            SubCategoryDetails model = new SubCategoryDetails();
            bool f = true;
            foreach (var item in result)
            {
                SelectListItem tempobj = new SelectListItem();
                //SelectListItem tempobj1 = new SelectListItem();
                tempobj.Text = item.CategoryName;
                tempobj.Value = item.CategoryId.ToString();
                if (f == true)
                {
                    f = false;
                    model.CategoryId = int.Parse(item.CategoryId.ToString());
                    //model.SubCatId = int.Parse(item.ToString());
                }

                objCategoryList.Add(tempobj);
            }

            ViewBag.ListCategory = objCategoryList;
            // ViewBag.ListChildCategory = objCategoryList;
            model.IsAdd = "Add";
            model.IsActive = true;                  

            var AccessTo = new UserController().UserCanAccess((Session["LoginUser"] as User).UserId, "CategoryMaster");
            if (!string.IsNullOrEmpty(AccessTo))
            {
                ViewBag.UserCanAccess = AccessTo;
                return View(model);
            }
            else
                return RedirectToAction("Dashboard", "Home");
        }

        // POST: SaveSubCategoryMaster
        [HttpPost]
        public ActionResult SaveSubCategoryMaster(SubCategoryDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();

            if (model != null)
            {
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;
                }
                objResponse = objProductManager.AddSubCategoryDetails(model);

                //Added log
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                objLogManager.SaveLog(Session["LoginUser"] as User, model.IsAdd + " Sub Category -" + model.subCategoryName, myIP + currentDate);

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFullSubCategoryList()
        {
            List<SubCategoryDetails> objList = objProductManager.GetSubcategoryDetails(0, "");
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFullCategoryList()
        {
            List<CategoryDetails> objList = objProductManager.GetCategoryList("");
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        // GET: ProductMaster
        [SessionExpire]
        public ActionResult ProductMaster(string ActionName, string ProductCode)
        {
            List<CategoryDetails> objCategoryDetails = objProductManager.GetCategoryList("Y");

            List<SelectListItem> ListCategoryDetails = new List<SelectListItem>();
            List<SelectListItem> ListSubCategoryDetails = new List<SelectListItem>();
            ProductDetails model = new ProductDetails();
            bool f = true;
            foreach (var obj in objCategoryDetails)
            {
                SelectListItem objTemp = new SelectListItem();
                objTemp.Text = obj.CategoryName;
                objTemp.Value = obj.CategoryId.ToString();
                if (f)
                {
                    objTemp.Selected = true;
                    model.CategoryId = obj.CategoryId;
                    f = false;
                }
                ListCategoryDetails.Add(objTemp);
            }
            f = true;
            List<SubCategoryDetails> objSubCategoryDetails = objProductManager.GetSubcategoryDetails(model.CategoryId, "Y");
            foreach (var obj in objSubCategoryDetails)
            {
                SelectListItem objTemp = new SelectListItem();
                objTemp.Text = obj.subCategoryName;
                objTemp.Value = obj.SubCatId.ToString();
                if (f)
                {
                    objTemp.Selected = true;
                    model.SubCatgeoryId = (int)obj.SubCatId;
                    f = false;
                }
                ListSubCategoryDetails.Add(objTemp);
            }
            ViewBag.ListCategory = ListCategoryDetails;
            ViewBag.ListSubCategory = ListSubCategoryDetails;
            List<SelectListItem> objBarcodeType = new List<SelectListItem>();
            objBarcodeType.Add(new SelectListItem { Text = "System Generated", Value = "System Generated" });
            objBarcodeType.Add(new SelectListItem { Text = "Other", Value = "Other" });
            ViewBag.ListBarcodetype = objBarcodeType;
            model.ProductBarcodeDetails = new BarcodeDetails();
            model.ProductBarcodeDetails.BarcodeType = "System Generated";
            model.ProductFor = "J";
            model.ProductBarcodeDetails.Barcode = objProductManager.MaxBarCode().ToString();
            model.ProductBarcodeDetails.ExisitingBarcode = model.ProductBarcodeDetails.Barcode;
            model.ProductCode = objProductManager.MaxProductCode();
            model.ProductImagePath = "/images/DefaultProduct.jpg";

            List<SelectListItem> objProductFor = new List<SelectListItem>();
            objProductFor.Add(new SelectListItem { Text = "Both", Value = "A" });//ALL
            objProductFor.Add(new SelectListItem { Text = "Joining", Value = "J" });
            objProductFor.Add(new SelectListItem { Text = "Repurchase", Value = "R" });

            ViewBag.ListProductFor = objProductFor;

            if (!string.IsNullOrEmpty(ActionName))
            {
                if (ActionName == "Edit")
                {
                    
                    if (!string.IsNullOrEmpty(ProductCode))
                    {
                        //if (!string.IsNullOrEmpty(Passedmodel.ProductImagePath))
                        //{
                        //    var test = Url.Action("GetImage", "Common", new { imageName = "-1" });
                        //    var productPic = test.Replace("-1", Passedmodel.ProductImagePath);
                        //    Passedmodel.ProductImagePath = productPic;
                        //}
                        //else
                        //{
                        //    Passedmodel.ProductImagePath = "/images/DefaultProduct.jpg";
                        //}
                        //model = Passedmodel;
                        decimal productId = decimal.Parse(ProductCode);
                        decimal LoginStateCode = (Session["LoginUser"] as User).StateCode;
                        model = objProductManager.GetProductDetail(productId,LoginStateCode);
                        objSubCategoryDetails = objProductManager.GetSubcategoryDetails(model.CategoryId, "Y");
                        ListSubCategoryDetails = new List<SelectListItem>();
                        foreach (var obj in objSubCategoryDetails)
                        {
                            SelectListItem objTemp = new SelectListItem();
                            objTemp.Text = obj.subCategoryName;
                            objTemp.Value = obj.SubCatId.ToString();
                            if (f)
                            {
                                objTemp.Selected = true;
                                model.SubCatgeoryId = (int)obj.SubCatId;
                                f = false;
                            }
                            ListSubCategoryDetails.Add(objTemp);
                        }
                        ViewBag.ListSubCategory = ListSubCategoryDetails;
                        if (model != null)
                        {
                            model.IsAdd = "Edit";
                            model.MinQty = string.IsNullOrEmpty(model.MinQtyStr) ? 0 : decimal.Parse(model.MinQtyStr);
                        }
                        if (!string.IsNullOrEmpty(model.ProductImagePath))
                        {                           
                            model.ProductImagePath = model.ProductImagePath;
                        }
                        else
                        {
                            model.ProductImagePath = "/images/DefaultProduct.jpg";
                        }
                    }
                }
                else if (ActionName == "Delete")
                {
                    model.IsAdd = "Delete";
                }
                else
                {
                    model.IsAdd = "Add";
                }
            }

            var AccessTo = new UserController().UserCanAccess((Session["LoginUser"] as User).UserId, "ProductList");
            if (!string.IsNullOrEmpty(AccessTo))
            {
                ViewBag.UserCanAccess = AccessTo;
                return View(model);
            }
            else
                return RedirectToAction("Dashboard", "Home");
           
        }
        // POST: SaveProductMaster
        [HttpPost]
        public ActionResult SaveProductMaster(ProductDetails model, HttpPostedFileBase upload)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                if (upload != null)
                {
                    var path = System.Web.HttpContext.Current.Server.MapPath("~/ProductImages");
                    
                    if (upload != null && upload.FileName != null)
                    {
                        if (!Directory.Exists(path))
                        {
                            //DirectoryInfo dInfo = new DirectoryInfo(path);
                            //DirectorySecurity dSecurity = dInfo.GetAccessControl();
                            //dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                            //dInfo.SetAccessControl(dSecurity);

                            Directory.CreateDirectory(path);


                        }

                        //string myfile = Guid.NewGuid() + "-" + BannerImage.FileName;
                        string myfile = Guid.NewGuid() + "-" + Path.GetFileName(upload.FileName);


                        var productImage = Path.Combine(path, myfile);
                        upload.SaveAs(productImage);
                        model.ProductImagePath = "/ProductImages/" + myfile;
                    }
                }
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;


                }
                model.IsAdd = "Add";
                objResponse = objProductManager.SaveProductMaster(model);

                //Added log
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                objLogManager.SaveLog(Session["LoginUser"] as User, model.IsAdd + " Product -" + model.ProductName, myIP + currentDate);

            }
            catch (Exception ex)
            {

            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        // GET: ProductMaster1030
        public ActionResult ProductMaster1030(string ActionName,string ProductCode)
        {

            List<CategoryDetails> objCategoryDetails = objProductManager.GetCategoryList("Y");

            List<SelectListItem> ListCategoryDetails = new List<SelectListItem>();
            List<SelectListItem> ListSubCategoryDetails = new List<SelectListItem>();
            ProductDetails model = new ProductDetails();
            bool f = true;
            foreach (var obj in objCategoryDetails)
            {
                SelectListItem objTemp = new SelectListItem();
                objTemp.Text = obj.CategoryName;
                objTemp.Value = obj.CategoryId.ToString();
                if (f)
                {
                    objTemp.Selected = true;
                    model.CategoryId = obj.CategoryId;
                    f = false;
                }
                ListCategoryDetails.Add(objTemp);
                f = true;
                
            }

            


            List<ColorDetails> objColorDetails = objProductManager.GetColorList("Y");
            List<SizeDetails> objSizeDetails = objProductManager.GetSizeList("Y");
            ViewBag.ListColor = objColorDetails;
            ViewBag.ListSize = objSizeDetails;

            ViewBag.ListCategory = ListCategoryDetails;
            ViewBag.ListSubCategory = ListSubCategoryDetails;
            List<SelectListItem> objBarcodeType = new List<SelectListItem>();
            objBarcodeType.Add(new SelectListItem { Text = "System Generated", Value = "System Generated" });
            objBarcodeType.Add(new SelectListItem { Text = "Other", Value = "Other" });
            ViewBag.ListBarcodetype = objBarcodeType;
            model.ProductBarcodeDetails = new BarcodeDetails();
            model.ProductBarcodeDetails.BarcodeType = "System Generated";
            model.ProductFor = "A";
            model.ProductBarcodeDetails.Barcode = objProductManager.MaxBarCode().ToString();
            model.ProductBarcodeDetails.ExisitingBarcode = model.ProductBarcodeDetails.Barcode;
            model.ProductCode = objProductManager.MaxProductCode();
            model.ProductImagePath = "~/images/DefaultProduct.jpg";
            model.ProductImagePath1 = "~/images/DefaultProduct.jpg";
            model.ProductImagePath2 = "DefaultProduct.jpg";
            model.ProductImagePath3 = "DefaultProduct.jpg";
            model.ProductImagePath4 = "DefaultProduct.jpg";
            model.ProductImagePath5 = "DefaultProduct.jpg";

            List<SelectListItem> objProductFor = new List<SelectListItem>();
            objProductFor.Add(new SelectListItem { Text = "Both", Value = "A" });//ALL
            objProductFor.Add(new SelectListItem { Text = "Joining", Value = "J" });
            objProductFor.Add(new SelectListItem { Text = "Repurchase", Value = "R" });

            ViewBag.ListProductFor = objProductFor;
            if (!string.IsNullOrEmpty(ActionName))
            {
                if (ActionName == "Edit")
                {

                    if (!string.IsNullOrEmpty(ProductCode))
                    {
                       
                        decimal productId = decimal.Parse(ProductCode);
                        decimal LoginStateCode = (Session["LoginUser"] as User).StateCode;
                        model = objProductManager.GetProductDetail(productId, LoginStateCode);
                        if (model != null)
                        {
                            model.IsAdd = "Edit";
                            model.MinQty = string.IsNullOrEmpty(model.MinQtyStr) ? 0 : decimal.Parse(model.MinQtyStr);
                        }
                       
                    }
                }
                else if (ActionName == "Delete")
                {
                    model.IsAdd = "Delete";
                }
                else
                {
                    model.IsAdd = "Add";
                }

                List<SubCategoryDetails> objSubCategoryDetails = objProductManager.GetSubcategoryDetails(model.CategoryId, "Y");
                //foreach (var obj in objSubCategoryDetails)
                //{
                //    SelectListItem objTemp = new SelectListItem();
                //    objTemp.Text = obj.subCategoryName;
                //    objTemp.Value = obj.SubCatId.ToString();

                //    model.SubCatgeoryId = (int)obj.SubCatId;
                //    f = false;

                //    ListSubCategoryDetails.Add(objTemp);
                //}

                foreach (var obj in objSubCategoryDetails)
                {
                    SelectListItem objTemp = new SelectListItem();
                    if (model.CategoryId == obj.SubCategoryId)
                    {
                        objTemp.Selected = true;
                    }
                    objTemp.Text = obj.subCategoryName;
                    objTemp.Value = obj.SubCatId.ToString();
                    ListSubCategoryDetails.Add(objTemp);
                }


                ViewBag.ListCategory = ListCategoryDetails;
                ViewBag.ListSubCategory = ListSubCategoryDetails;


            }

            var AccessTo = new UserController().UserCanAccess((Session["LoginUser"] as User).UserId, "ProductList");
            if (!string.IsNullOrEmpty(AccessTo))
            {
                ViewBag.UserCanAccess = AccessTo;
                return View(model);
            }
            else
                return RedirectToAction("Dashboard", "Home");
        }




        // POST: SaveProductMaster1030
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveProductMaster1030(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                if (model.upload != null)
                {
                    model.ImgFlag = "1";
                    var path = System.Web.HttpContext.Current.Server.MapPath("~/ProductImages");
                    for (int i = 0; i < model.upload.Length; i++)
                    {
                        if (model.upload[i] != null && model.upload[i].FileName != null && !model.upload[i].FileName.Contains("DefaultProduct.jpg"))
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string myfile = Guid.NewGuid() + "-" + Path.GetFileName(model.upload[i].FileName);
                            var productImage = Path.Combine(path, myfile);
                            model.upload[i].SaveAs(productImage);
                            //model.ProductImagePath = "/ProductImages/" + myfile;

                           

                            var req = HttpContext.Request;
                            string baseUrl = myfile;


                            if (i == 0)
                                model.ProductImagePath = baseUrl;
                            else if (i == 1)
                                model.ProductImagePath1 = baseUrl;
                            else if (i == 2)
                                model.ProductImagePath2 = baseUrl;
                            else if (i == 3)
                                model.ProductImagePath3 = baseUrl;
                            else if (i == 4)
                                model.ProductImagePath4 = baseUrl;
                            else if (i == 5)
                                model.ProductImagePath5 = baseUrl;
                        }

                    }
                }
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;


                }
                model.IsAdd = "Add";
              //  model.ImgFlag = '1';
                objResponse = objProductManager.SaveProductMaster1030(model);
            }
            catch (Exception ex)
            {

            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProductMaster1030(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                if (model.upload != null)
                {
                    var path = System.Web.HttpContext.Current.Server.MapPath("~/ProductImages");
                    int count = model.Numberofimages;

                    for (int i = 0; i < model.upload.Length; i++)
                    {

                        if (model.upload[i] != null && model.upload[i].FileName != null && !model.upload[i].FileName.Contains("DefaultProduct.jpg"))
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string myfile = Guid.NewGuid() + "-" + Path.GetFileName(model.upload[i].FileName);
                            var productImage = Path.Combine(path, myfile);
                            model.upload[i].SaveAs(productImage);

                            var req = HttpContext.Request;
                            string baseUrl = myfile;

                            if (i == 0)
                                model.ProductImagePath = baseUrl;
                            else if (i == 1)
                                model.ProductImagePath1 = baseUrl;
                            else if (i == 2)
                                model.ProductImagePath2 = baseUrl;
                            else if (i == 3)
                                model.ProductImagePath3 = baseUrl;
                            else if (i == 4)
                                model.ProductImagePath4 = baseUrl;
                            else if (i == 5)
                                model.ProductImagePath5 = baseUrl;
                        }
                    }
                }
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;


                }
                model.IsAdd = "Edit";
                objResponse = objProductManager.EditProductMaster1030(model);

                //Added log
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                objLogManager.SaveLog(Session["LoginUser"] as User, model.IsAdd + " Product -" + model.ProductName, myIP + currentDate);
            }
            catch (Exception ex)
            {

            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult DeleteProductMaster1030(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            model.IsAdd = "Delete";
            objResponse = objProductManager.DeleteProductMaster1030(model);

            //Added log
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            objLogManager.SaveLog(Session["LoginUser"] as User, model.IsAdd + " Product -" + model.ProductName, myIP + currentDate);

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }


        [SessionExpire]
        public ActionResult ProductList()

        {
            var AccessTo = new UserController().UserCanAccess((Session["LoginUser"] as User).UserId, "ProductList");
            if (!string.IsNullOrEmpty(AccessTo))
            {
                ViewBag.UserCanAccess = AccessTo;
                return View();
            }
            else
                return RedirectToAction("Dashboard", "Home");
        }

        //GET:ListProduct
        [HttpPost]
        public ActionResult GetProductList()
        {
            List<ProductDetails> objProductList = new List<ProductDetails>();
            decimal LoginStateCode = (Session["LoginUser"] as User).StateCode;
            objProductList = objProductManager.GetProductList(LoginStateCode);
            var jsonProduct = Json(objProductList, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
        }

        [HttpPost]
        public ActionResult EditProductMaster(ProductDetails model, HttpPostedFileBase upload)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                if (upload != null)
                {
                    var serverPath = System.Web.HttpContext.Current.Server.MapPath("~/ProductImages");

                    if (!Directory.Exists(serverPath))
                    {
                        Directory.CreateDirectory(serverPath);
                    }
                   
                    if (upload != null && upload.FileName != null)
                    {
                        
                        string myfile = Guid.NewGuid() + "-" + Path.GetFileName(upload.FileName);
                        var productImage = Path.Combine(serverPath, myfile);
                        upload.SaveAs(productImage);
                        model.ProductImagePath = "/ProductImages/" + myfile;                       
                    }
                }
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;


                }
                model.IsAdd = "Edit";
                objResponse = objProductManager.EditProductMaster(model);

                //Added log
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                objLogManager.SaveLog(Session["LoginUser"] as User, model.IsAdd + " Product -" + model.ProductName, myIP + currentDate);
            }
            catch (Exception ex)
            {

            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            model.IsAdd = "Delete";
            objResponse = objProductManager.DeleteProductMaster(model);

            //Added log
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            objLogManager.SaveLog(Session["LoginUser"] as User, model.IsAdd + " Product -" + model.ProductName, myIP + currentDate);

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        // GET: CheckDuplicateMaster
        [HttpPost]
        public ActionResult CheckDuplicateMaster(CheckDuplicateModel model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductManager.IsMasterExists(model);
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetActiveListOfSubCat(CheckDuplicateModel model)
        {
            List<SubCategoryDetails> objSubCategoryDetails = objProductManager.GetSubcategoryDetails(0, "Y");
            return Json(objSubCategoryDetails, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult SizeMaster()
        {
            SizeDetails objCategoryModel = new SizeDetails();
            objCategoryModel.IsAdd = "Add";
            objCategoryModel.IsActive = true;

            var AccessTo = new UserController().UserCanAccess((Session["LoginUser"] as User).UserId, "SizeMaster");
            if (!string.IsNullOrEmpty(AccessTo))
            {
                ViewBag.UserCanAccess = AccessTo;
                return View(objCategoryModel);
            }
            else
                return RedirectToAction("Dashboard", "Home");

        }


        // POST: SaveColorMaster
        [HttpPost]
        public ActionResult SaveSizeMaster(SizeDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (model != null)
            {

                objResponse = objProductManager.AddSizeDetails(model);
                //Added log
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                objLogManager.SaveLog(Session["LoginUser"] as User, model.IsAdd + " Size -" + model.Size, myIP + currentDate);

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFullSizeList()
        {
            List<SizeDetails> objList = objProductManager.GetSizeList("");
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult ColorMaster()
        {
            ColorDetails objCategoryModel = new ColorDetails();
            objCategoryModel.IsAdd = "Add";
            objCategoryModel.IsActive = true;

            var AccessTo = new UserController().UserCanAccess((Session["LoginUser"] as User).UserId, "ColorMaster");
            if (!string.IsNullOrEmpty(AccessTo))
            {
                ViewBag.UserCanAccess = AccessTo;
                return View(objCategoryModel);
            }
            else
                return RedirectToAction("Dashboard", "Home");

        }

        // POST: SaveColorMaster
        [HttpPost]
        public ActionResult SaveColorMaster(ColorDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (model != null)
            {

                objResponse = objProductManager.AddColorDetails(model);
                //Added log
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                objLogManager.SaveLog(Session["LoginUser"] as User, model.IsAdd + " Color -" + model.ColorName, myIP + currentDate);

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetFullColorList()
        {
            List<ColorDetails> objList = objProductManager.GetColorList("");
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

    }
}