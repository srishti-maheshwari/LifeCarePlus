using InventoryManagement.API.Models;
using InventoryManagement.Entity.Common;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.HttpContext.Current;

namespace InventoryManagement.API.Controllers
{
    public class LoginAPIController : ApiController
    {
        GenConnection objGetConn = new GenConnection();
        private static string dbName, invDbName, enttConstr, AppConstr, InvConstr, WRPartyCode;
        public LoginAPIController()
        {
            ConnModel obj = objGetConn.GetConstr();
            dbName = obj.Db;
            invDbName = obj.InvDb;
            enttConstr = obj.EnttConnStr;
            AppConstr = obj.AppConnStr;
            InvConstr = obj.InvConnStr;
            WRPartyCode = obj.WRPartyCode;
        }
        //Accordig to new schema Inventory Db
        //public User ValidateUser(LoginModel model)
        //{
        //    User objResponse = new User();
        //    try
        //    {
        //        using(var entity=new InventoryEntities( objGetConn.GetConstr().EnttConnStr))
        //        {
        //            objResponse = (from result in entity.Inv_M_UserMaster
        //                           where result.ActiveStatus == "Y" && result.UserName.ToLower() == model.UserName.ToLower() && result.Passw.ToLower() == model.password.ToLower()
        //                           join ledger in entity.M_LedgerMaster on result.BranchCode equals ledger.PartyCode
        //                           join groupid in entity.M_GroupMaster on ledger.GroupId equals groupid.GroupId
        //                           select new User
        //                           {
        //                                UserId=(int)result.UserId,
        //                                UserName=result.UserName,
        //                                Password=result.Passw,
        //                                 BranchCode=result.BranchCode,
        //                                  PartyCode=ledger.PartyCode,
        //                                   PartyName=ledger.PartyName,
        //                                    GroupId=(int)ledger.GroupId,
        //                                     FCode=ledger.PartyCode,
        //                                      StateCode= (int)ledger.StateCode
        //                           }

        //                         ).FirstOrDefault();
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //    //if (string.IsNullOrEmpty(objResponse.UserName))
        //    //{
        //    //    objResponse = null;
        //    //}
        //    return objResponse;
        //}

        public ConnModel GetCompDetail()
        {
            ConnModel objCon = new ConnModel();
            try
            {
                objCon = objGetConn.GetConstr();
               
            }
            catch (Exception)
            {  
            }
            return objCon;
        }

        public User ValidateUser(LoginModel model)
        {
            User objResponse = new User();
          
            try
            {
                
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objResponse = (from result in entity.Inv_M_UserMaster
                                   where result.ActiveStatus == "Y" && result.UserName == model.UserName && result.Passw == model.password
                                   join ledger in entity.M_LedgerMaster on result.BranchCode equals ledger.PartyCode where ledger.ActiveStatus=="Y"
                                   join groupid in entity.M_GroupMaster on ledger.GroupId equals groupid.GroupId
                                   select new User
                                   {
                                       UserId = (int)result.UserId,
                                       UserName = result.UserName,
                                       Password = result.Passw,
                                       BranchCode = result.BranchCode,
                                       PartyCode = ledger.PartyCode,
                                       PartyName = ledger.PartyName,
                                       GroupId = (int)ledger.GroupId,
                                       FCode = ledger.PartyCode,
                                       StateCode = (int)ledger.StateCode,
                                       IsAdmin = result.IsAdmin,
                                       ParentPartyCode=ledger.ParentPartyCode,
                                       Mobile = result.Mobile,
                                       Email = result.Email
                                   }
                                 ).FirstOrDefault();
                    if((!objResponse.UserName.Equals(model.UserName) || (!objResponse.Password.Equals(model.password)))){
                        objResponse = null;
                    }


                    //dynamic menus
                    objResponse.objMenuList = new List<MenuMasterModel>();
                    if (objResponse!=null && objResponse.IsAdmin == "N")
                    {
                        List<decimal> PermittedMenuId = (from r in entity.Web_M_UserPermissionMaster where r.GroupId == objResponse.UserId select r.MenuId).ToList();
                        objResponse.objMenuList = (from r in entity.Web_M_MenuMaster
                                                   where r.ActiveStatus == "Y" && PermittedMenuId.Contains(r.MenuId)
                                                   select new MenuMasterModel
                                                   {
                                                       MenuId = r.MenuId,
                                                       MenuName = r.MenuName,
                                                       ParentId = r.ParentId,
                                                       ActiveStatus = r.ActiveStatus,
                                                       Hierarchy = r.Hierar,
                                                       OnSelect = r.OnSelect,
                                                       Sequence=r.Sequence,
                                                       ChildSequence=r.ChildSequence
                                                   }).OrderBy(m=>m.Sequence).ToList();
                    }
                    else
                    {

                        objResponse.objMenuList = (from r in entity.Web_M_MenuMaster
                                                   where r.ActiveStatus == "Y"

                                                   select new MenuMasterModel
                                                   {
                                                       MenuId = r.MenuId,
                                                       MenuName = r.MenuName,
                                                       ParentId = r.ParentId,
                                                       ActiveStatus = r.ActiveStatus,
                                                       Hierarchy = r.Hierar,
                                                       OnSelect = r.OnSelect,
                                                       Sequence = r.Sequence,
                                                       ChildSequence = r.ChildSequence
                                                   }).OrderBy(m => m.Sequence).ToList();                        
                    }

                }
            }
            catch (Exception ex)
            {
                objResponse = null;
            }
            try
            {
  objResponse.objCon = objGetConn.GetConstr();
            }
            catch (Exception ex)
            {
                
            }
          
            //if (string.IsNullOrEmpty(objResponse.UserName))
            //{
            //    objResponse = null;
            //}
            if (objResponse != null)
            {
                objResponse.IsSoldByHo = false;
                if (objResponse.GroupId == 0)
                {
                    objResponse.IsSoldByHo = true;
                }
                    if (objResponse.IsSoldByHo)
                    {
                        int indexOfOrderCreationMenu = objResponse.objMenuList.FindIndex(m => m.MenuName == "Order Creation");
                    if (indexOfOrderCreationMenu > -1)
                    {
                        objResponse.objMenuList.RemoveAt(indexOfOrderCreationMenu);
                    }
                    }
               
                else
                {
                    int indexOfOrderCreationMenu = objResponse.objMenuList.FindIndex(m => m.MenuName == "Purchase Reports");
                    if (indexOfOrderCreationMenu > -1)
                    {
                        decimal ParentId = objResponse.objMenuList.Where(m => m.MenuName == "Purchase Reports").Select(m => m.MenuId).FirstOrDefault();
                        objResponse.objMenuList.RemoveAt(indexOfOrderCreationMenu);
                        List<MenuMasterModel> PurchaseReportsMenus = objResponse.objMenuList.Where(m => m.ParentId == ParentId).ToList();
                        foreach (MenuMasterModel obj in PurchaseReportsMenus)
                        {
                            indexOfOrderCreationMenu = objResponse.objMenuList.FindIndex(m => m.MenuId == obj.MenuId);
                            objResponse.objMenuList.RemoveAt(indexOfOrderCreationMenu);
                        }
                    }
                }
                
            }
            
            return objResponse;
        }

        public ResponseDetail ChangePassword(ChangePassword model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                using(var entity=new InventoryEntities( enttConstr))
                {
                    if (model != null)
                    {
                        Inv_M_UserMaster objDtUserMaser = new Inv_M_UserMaster();
                        objDtUserMaser = (from r in entity.Inv_M_UserMaster
                                          where r.Passw == model.CurrentPassword && r.UserName == model.UserName
                                          select r
                                   ).FirstOrDefault();
                        if (objDtUserMaser != null)
                        {
                            objDtUserMaser.Passw = model.NewPassword;
                            int i = entity.SaveChanges();
                            if (i > 0)
                            {
                                objResponse.ResponseStatus = "OK";
                                objResponse.ResponseMessage = "Password successfully changed!";
                            }
                            else
                            {
                                objResponse.ResponseStatus = "Failed";
                                objResponse.ResponseMessage = "Something went wrong!";
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {

            }
            return objResponse;
        }
    }
}
