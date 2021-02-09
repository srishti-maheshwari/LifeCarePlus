using InventoryManagement.Business;
using InventoryManagement.Common;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace InventoryManagement.Controllers
{
    public class LoginController : Controller
    {
        LoginManager objLoginManager = new LoginManager();
        LogManager objLogManager = new LogManager();
        TransactionManager objTransacManager = new TransactionManager();
        // GET: Login
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            FormsAuthentication.SignOut();
            InventorySession.LoginUser = null;
            Session["LoginUser"] = null;
            Session["MenuList"] = null;
            Session["LogoPath"]= objLoginManager.GetCompDetail().LogoPath;
            try
            {
                return View(model);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult ValidateUser(LoginModel model)
        {
            string URL = Request.Url.Host;
            ResponseDetail objResponseModel = new ResponseDetail();
            List<DisptachOrderModel> objDispatchOrderList = new List<DisptachOrderModel>();
            objResponseModel.ResponseStatus = "FAILED";
            var dispatchOrderCount = 0;
            var FranchiseOrderCount = 0;
            objResponseModel.ResponseMessage = "Something went wrong!";
            if (model != null)
            {
                if ((!string.IsNullOrEmpty(model.UserName)) && (!string.IsNullOrEmpty(model.password)))
                {

                    User Objresponse = objLoginManager.ValidateUser(model);
                    if (Objresponse != null)
                    {
                        objResponseModel.ResponseStatus = "OK";
                        objResponseModel.ResponseMessage = "Success!";
                        objResponseModel.ResponseMobile = Objresponse.Mobile;
                        objResponseModel.ResponseEmail= Objresponse.Email;
                        objResponseModel.ResponseUserID = Objresponse.UserId;
                        Session["LoginUser"] = Objresponse;
                        Session["MenuList"] = Objresponse.objMenuList;
                        string PartyCode = "0";
                        string CurrentPartyCode = (Session["LoginUser"] as User).PartyCode;
                        if (CurrentPartyCode != Objresponse.objCon.WRPartyCode.ToString())
                        {
                            PartyCode = CurrentPartyCode;
                        }
                           
                        string FromDate = "All"; string ToDate = "All";  string IdNo = "0"; string OrderNo = "0";

                        //objDispatchOrderList = objTransacManager.GetDispatchOrderList(FromDate, ToDate, PartyCode,  IdNo);
                        objDispatchOrderList = objTransacManager.GetDispatchOrderList(FromDate, ToDate, PartyCode, "Pending", IdNo, OrderNo, "A", false);
                        dispatchOrderCount = objDispatchOrderList != null ? objDispatchOrderList.Count() : 0;
                        Session["dispatchOrderCount"] = dispatchOrderCount;
                        List<PartyOrderModel> FranchiseOrder = objTransacManager.GetOrderList("All", CurrentPartyCode, "P");
                        FranchiseOrderCount = FranchiseOrder != null ? FranchiseOrder.Count() : 0;
                        Session["FranchiseOrder"] = FranchiseOrderCount;
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        Session["WRPartyCode"] = Objresponse.objCon.WRPartyCode;
                        Session["BVCaption"] = Objresponse.objCon.BVCaption;
                        Session["CVCaption"] = Objresponse.objCon.CVCaption;
                        Session["PVCaption"] = Objresponse.objCon.PVCaption;
                        Session["RPCaption"] = Objresponse.objCon.RPCaption;
                        Session["LogoPath"] = Objresponse.objCon.LogoPath;
                        //Added log
                        string hostName = Dns.GetHostName();                        
                        string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                        string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;                        
                        objLogManager.SaveLog(Objresponse,"Logged In", myIP + currentDate);
                    }
                    else
                    {
                        Session["MenuList"] = null;
                        Session["LoginUser"] = null;
                        objResponseModel.ResponseStatus = "FAILED";
                        objResponseModel.ResponseMessage = "Incorrect Username or Password!";
                    }
                    return Json(objResponseModel, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(objResponseModel,  JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePassword()
        {
            ChangePassword objModel = new ChangePassword();
            
            return PartialView(objModel);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objLoginManager.ChangePassword(objModel);

            //Added log
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;
            objLogManager.SaveLog(Session["LoginUser"] as User, "Changed Password", myIP + currentDate);

            return Json(objResponse,JsonRequestBehavior.AllowGet);
        }

        public ActionResult SessionExpire()
        {
            //Added log
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;
            objLogManager.SaveLog(Session["LoginUser"] as User, "logged Out", myIP + currentDate);
            FormsAuthentication.SignOut();
            return View();
        }
        
    }
}