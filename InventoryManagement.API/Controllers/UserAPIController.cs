﻿using InventoryManagement.API.Models;
using InventoryManagement.Entity.Common;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagement.API.Controllers
{
    public class UserAPIController : ApiController
    {
        GenConnection objGetConn = new GenConnection();
        private static string dbName, invDbName, enttConstr, AppConstr, InvConstr, CompName, WRPartyCode;
        public UserAPIController()
        {
            //ConnModel obj = objGetConn.GetConstr();
            //dbName = obj.Db;
            //invDbName = obj.InvDb;
            //enttConstr = obj.EnttConnStr;
            //AppConstr = obj.AppConnStr;
            //InvConstr = obj.InvConnStr;
            //WRPartyCode = obj.WRPartyCode;
            //CompName = obj.CompName;
            ConnModel obj;
            try
            {
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
            catch (Exception)
            {
            }
        }
        public ResponseDetail SetUserRights(List<UserPermissionMasterModel> objPermissionList)
        {
            ResponseDetail objResponse = new ResponseDetail();
            
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    
                    var FullPermissionList = (from r in entity.Web_M_UserPermissionMaster select r).ToList();
                    if (FullPermissionList.Count > 0)
                    {
                        foreach (var obj in objPermissionList)
                        {
                            Web_M_UserPermissionMaster objDTUserPermission = new Web_M_UserPermissionMaster();
                            var IsExistsRecord = (from r in FullPermissionList where r.MenuId == obj.MenuId && r.GroupId == obj.UserId select r).FirstOrDefault();
                            if (obj.IsPermitted || obj.IsEdit)
                            {
                                if (IsExistsRecord != null)
                                {
                                    objDTUserPermission = IsExistsRecord;
                                    objDTUserPermission.IsEdit = obj.IsEdit ? "Y" : "N";
                                }
                                else
                                {
                                    objDTUserPermission.GroupId = obj.UserId;
                                    objDTUserPermission.MenuId = obj.MenuId;
                                    objDTUserPermission.UserId = obj.CurrentLoginUser.UserId;
                                    objDTUserPermission.IsEdit = obj.IsEdit?"Y":"N";
                                    objDTUserPermission.RecTimeStamp = DateTime.Now;
                                    entity.Web_M_UserPermissionMaster.Add(objDTUserPermission);
                                }
                            }
                            else
                            {
                                if (IsExistsRecord != null)
                                {
                                    objDTUserPermission = IsExistsRecord;
                                    entity.Web_M_UserPermissionMaster.Remove(objDTUserPermission);
                                }
                            }
                        }
                    }
                    else
                    {
                            foreach (var obj in objPermissionList)
                            {
                            Web_M_UserPermissionMaster objDTUserPermission = new Web_M_UserPermissionMaster();
                            if (obj.IsPermitted || obj.IsEdit)
                            {
                                objDTUserPermission.GroupId = obj.UserId;
                                objDTUserPermission.MenuId = obj.MenuId;
                                objDTUserPermission.UserId = obj.CurrentLoginUser.UserId;
                                objDTUserPermission.IsEdit = obj.IsEdit ? "Y" : "N";
                                objDTUserPermission.RecTimeStamp = DateTime.Now;
                                entity.Web_M_UserPermissionMaster.Add(objDTUserPermission);
                            }
                            }
                    }
                    int i = 0;
                    i = entity.SaveChanges();
                    if (i >= 0)
                    {
                        objResponse.ResponseMessage = "Saved Successfully";
                        objResponse.ResponseStatus = "OK";
                    }                    
                }   
            }
            catch(Exception ex)
            {
                objResponse.ResponseStatus = "FAILED";
                objResponse.ResponseMessage = "Something went wrong!";
            }
            return objResponse;
        }

        public List<UserPermissionMasterModel> ListUserPermittedMenus(decimal UserId)
        {
            List<UserPermissionMasterModel> objUserPermittedMenus = new List<UserPermissionMasterModel>();
            try
            {
                using(var entity=new InventoryEntities(enttConstr))
                {

                    string partyname = (from l in entity.M_LedgerMaster join u in entity.Inv_M_UserMaster on l.PartyCode equals u.BranchCode where u.UserId == UserId select l.PartyName).FirstOrDefault();

                    objUserPermittedMenus = (from r in entity.Web_M_UserPermissionMaster
                                             where r.GroupId == UserId
                                             select new UserPermissionMasterModel
                                             {
                                                  MenuId=r.MenuId,
                                                  UserId=r.GroupId,
                                                  IsPermitted=true,
                                                  IsEdit = r.IsEdit=="Y"?true:false
                                             }).ToList();

                    foreach (var obj in objUserPermittedMenus)
                    {
                        obj.PartyName = partyname;
                    }
                    var FullMenuList = (from r in entity.Web_M_MenuMaster
                                        from g in entity.M_GrpPermissionMaster
                                        from l in entity.M_LedgerMaster
                                        from u in entity.Inv_M_UserMaster
                                        where r.ActiveStatus == "Y" && u.BranchCode == l.PartyCode && l.GroupId == g.GroupID
                                        && g.MenuID == r.MenuId && u.UserId == UserId
                                        select new MenuMasterModel
                                        {
                                            MenuId = r.MenuId,
                                            MenuName = r.MenuName,
                                            ParentId=r.ParentId,
                                            Sequence=r.Sequence,
                                            Hierarchy=r.Hierar,
                                            ChildSequence=r.ChildSequence,
                                            
                                        }).ToList();
                    if (objUserPermittedMenus.Count==0)
                    {
                        objUserPermittedMenus = new List<UserPermissionMasterModel>();
                        foreach (var obj in FullMenuList)
                        {
                            objUserPermittedMenus.Add(new UserPermissionMasterModel
                            {
                                UserId = UserId,
                                MenuId = obj.MenuId,
                                MenuName = obj.MenuName,
                                IsPermitted = false,
                                IsEdit =false,
                                ParentId = obj.ParentId,
                                Sequence=obj.Sequence,
                                ChildSequence = obj.ChildSequence,
                                ParentName = FullMenuList.Where(m => m.MenuId == obj.ParentId).Select(m => m.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(m => m.MenuId == obj.ParentId).Select(m => m.MenuName).FirstOrDefault(),
                            });
                        }

                        
                        //objUserPermittedMenus.Add(new UserPermissionMasterModel
                        //{
                        //    MenuList = FullMenuList
                        //});
                    }
                    else
                    {
                        int j = 0;
                        foreach (var obj in FullMenuList)
                        {
                            var IsExists = objUserPermittedMenus.Where(m => m.MenuId == obj.MenuId).Select(m => m).FirstOrDefault();
                            if (IsExists == null){
                                objUserPermittedMenus.Add(new UserPermissionMasterModel
                                {
                                    UserId = UserId,
                                    MenuId = obj.MenuId,
                                    MenuName = obj.MenuName,
                                    IsPermitted = false,
                                    IsEdit = false,
                                    ParentId = obj.ParentId,
                                    Sequence=obj.Sequence,
                                    ChildSequence = obj.ChildSequence,
                                    ParentName = FullMenuList.Where(m => m.MenuId == obj.ParentId).Select(m => m.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(m => m.MenuId == obj.ParentId).Select(m => m.MenuName).FirstOrDefault(),
                                });
                            }
                            else
                            {

                                objUserPermittedMenus.Where(m => m.MenuId == obj.MenuId).Select(m =>
                                  {
                                     m.MenuId = m.MenuId;
                                      m.UserId = m.UserId;
                                      m.MenuName = obj.MenuName;
                                      m.IsPermitted = m.IsPermitted;
                                      m.IsEdit = m.IsEdit;
                                      m.ParentId = obj.ParentId;
                                      m.ParentName = FullMenuList.Where(k => k.MenuId == obj.ParentId).Select(k => k.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(k => k.MenuId == obj.ParentId).Select(k => k.MenuName).FirstOrDefault();
                                      m.Sequence = obj.Sequence;
                                      m.ChildSequence = obj.ChildSequence;
                                      return m;
                                  }).ToList();
                            }
                            j++;
                        }
                        //if (objUserPermittedMenus.Count != FullMenuList.Count)
                        //{
                        //    var ListId = objUserPermittedMenus.Select(p => p.MenuId).ToList();
                        //    var ResultList = FullMenuList.Select(m =>m.MenuId).ToList();
                        //    var RemainingList = ResultList.Except(ListId).ToList();
                        //    foreach (var Id in RemainingList)
                        //    {
                        //        objUserPermittedMenus.Add(new UserPermissionMasterModel
                        //        {
                        //            UserId = UserId,
                        //            MenuId = Id,
                        //            MenuName = FullMenuList.Where(m => m.MenuId == Id).Select(m => m.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(m => m.MenuId == Id).Select(m => m.MenuName).FirstOrDefault(),
                        //            IsPermitted = false,
                        //            ParentId = FullMenuList.Where(m => m.MenuId == Id).Select(m => m.ParentId).FirstOrDefault() == null ? 0 : FullMenuList.Where(m => m.MenuId == Id).Select(m => m.ParentId).FirstOrDefault(),
                        //            ParentName = FullMenuList.Where(m => m.MenuId == FullMenuList.Where(p => p.MenuId == Id).Select(p => p.ParentId).FirstOrDefault()).Select(m => m.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(m => m.MenuId == FullMenuList.Where(p => p.MenuId == Id).Select(p => p.ParentId).FirstOrDefault()).Select(m => m.MenuName).FirstOrDefault(),
                        //        });
                        //    }
                        //}
                           
                    }
                }
            }
            catch(Exception ex)
            {

            }
            //  objUserPermittedMenus = objUserPermittedMenus.OrderBy(m=>m.ChildSequence).OrderBy(m=>m.Sequence).ToList();
            objUserPermittedMenus = objUserPermittedMenus.Where(m => m.MenuName != null).OrderBy(m => m.ChildSequence).OrderBy(m => m.Sequence).ToList();


            return objUserPermittedMenus;
        }

        public List<User> GetUserList(bool GetAdmin)
        {
            List<User> objUserList = new List<User>();
            try
            {
                using(var entity=new InventoryEntities(enttConstr))
                {
                    if (GetAdmin == true)
                    {
                        objUserList = (from r in entity.Inv_M_UserMaster
                                       where r.ActiveStatus == "Y" 
                                       select new User
                                       {
                                           UserId = (int)r.UserId,
                                           UserName = r.UserName

                                       }
                                     ).OrderByDescending(r => r.UserName).ToList();
                    }
                    else
                    {
                        objUserList = (from r in entity.Inv_M_UserMaster
                                       where r.ActiveStatus == "Y" && r.IsAdmin == "N"
                                       select new User
                                       {
                                           UserId = (int)r.UserId,
                                           UserName = r.UserName

                                       }
                                     ).OrderByDescending(r => r.UserName).ToList();
                    }
                    
                }
            }
            catch(Exception ex){

            }
            return objUserList;
        }
        public List<User> GetAllUserList()
        {
            List<User> objUserList = new List<User>();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objUserList = (from r in entity.Inv_M_UserMaster
                                   where r.GroupId != 5 && r.GroupId != 6 && r.GroupId != 21
                                   join p in entity.M_LedgerMaster on r.BranchCode equals p.PartyCode
                                   select new User
                                   {
                                       UserId = (int)r.UserId,
                                       UserName = r.UserName,
                                       Password=r.Passw,
                                       FCode=r.FCode,
                                       PartyName=p.PartyName,
                                       Remarks=r.Remarks,
                                       Mobile = r.Mobile,
                                       Email=r.Email,
                                       ActiveStatus=r.ActiveStatus
                                   }
                                 ).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objUserList;
        }
        public User GetUser(int UserId)
        {
            User objUser = new User();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    objUser = (from r in entity.Inv_M_UserMaster
                                   where r.UserId==UserId
                                   
                                   select new User
                                   {
                                       UserId = (int)r.UserId,
                                       UserName = r.UserName,
                                       Password = r.Passw,
                                       FCode = r.FCode,
                                       GroupId=(int)r.GroupId,
                                       Remarks = r.Remarks,
                                       Mobile = r.Mobile,
                                       Email = r.Email,
                                       ActiveStatus = r.ActiveStatus
                                   }
                                 ).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return objUser;
        }
        public decimal GetPartyGroupId(string PartyCode)
        {
            decimal GroupId = 0;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    GroupId = (from r in entity.M_LedgerMaster
                               where r.PartyCode == PartyCode
                               select r.GroupId
                             ).FirstOrDefault();

                }
            }
            catch(Exception ex)
            {

            }
            return GroupId;
        }

        public List<PartyModel> GetPartyListForUsers()
        {
            List<PartyModel> objListParty = new List<PartyModel>();
            try
            {
                using(var entity=new InventoryEntities(enttConstr))
                {
                    objListParty = (from r in entity.M_LedgerMaster
                                    where r.ActiveStatus == "Y" && r.GroupId != 5 && r.GroupId != 6 && r.GroupId != 21
                                    select new PartyModel
                                    {
                                        PartyCode = r.PartyCode,
                                        PartyName=r.PartyName
                                    }).ToList();
                }
            }
            catch(Exception ex)
            {

            }
            return objListParty;
        }
        public ResponseDetail IsDuplicateUserName(string IsActionType, string UserCode, string UserName)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    if (IsActionType == "Add")
                    {
                        var result = (from r in entity.Inv_M_UserMaster
                                      where r.UserName == UserName
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
                    else if (IsActionType == "Edit")
                    {
                        int UId = 0;
                        if (!string.IsNullOrEmpty(UserCode))
                        {
                            UId = int.Parse(UserCode);
                        }
                        var result = (from r in entity.Inv_M_UserMaster
                                      where r.UserName == UserName && r.UserId != UId
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
        public ResponseDetail AddEditUserDetails(User objModel,User LoggedUser)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            Inv_M_UserMaster DTUser = new Inv_M_UserMaster();
            Inv_TempUserMaster TempDTUser = new Inv_TempUserMaster();
            string Version = "";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    Version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();
                    decimal maxUserId = 0;
                    maxUserId = (from r in entity.Inv_M_UserMaster                                 
                                 select r.UserId
                               ).DefaultIfEmpty(0).Max();
                    maxUserId = maxUserId + 1;
                     DTUser = (from r in entity.Inv_M_UserMaster where r.UserId == objModel.UserId  select r).FirstOrDefault();
                    if (DTUser == null)
                    {
                        DTUser = new Inv_M_UserMaster();
                    }
                    else
                    {
                        ////insert into temp table
                        //TempDTUser.UserId = DTUser.UserId;
                        //TempDTUser.UserName = DTUser.UserName;
                        //TempDTUser.Passw = DTUser.Passw;
                        //TempDTUser.Remarks = DTUser.Remarks;
                        //TempDTUser.Status = DTUser.Status;
                        //TempDTUser.FCode = DTUser.FCode;
                        //TempDTUser.BranchCode = DTUser.BranchCode;
                        //TempDTUser.ActiveStatus = DTUser.ActiveStatus;
                        //TempDTUser.CreateBy = DTUser.CreateBy;
                        //TempDTUser.RecTimeStamp = DTUser.RecTimeStamp;
                        //TempDTUser.CreateDate = DTUser.CreateDate;
                        //TempDTUser.GroupId = DTUser.GroupId;
                        //TempDTUser.IsAdmin = DTUser.IsAdmin;
                        //TempDTUser.LastIP = DTUser.LastIP;
                        //TempDTUser.LastLoginTime = DTUser.LastLoginTime;
                        //TempDTUser.LastLogOutTime = DTUser.LastLogOutTime;
                        //TempDTUser.LastModified = DTUser.LastModified;
                        //TempDTUser.LoginStatus = DTUser.LoginStatus;
                        //TempDTUser.LUserId = DTUser.LUserId;
                        //TempDTUser.MRecTimeStamp = DateTime.Now.Date;
                        //TempDTUser.MUserId = LoggedUser.UserId;
                        //TempDTUser.TId = 0;
                        //TempDTUser.UId = DTUser.UId;
                        //TempDTUser.Version = DTUser.Version;
                        //entity.Inv_TempUserMaster.Add(TempDTUser);
                        string AppConnectionString = InvConstr;
                        SqlConnection SC = new SqlConnection(AppConnectionString);

                        string query = "INSERT Inv_TempUserMaster Select *,'"+ LoggedUser.UserId +"',Getdate() FROM Inv_M_UserMaster WHERE UserID='"+ DTUser.UserId + "'";
                        SC.Open();
                        SqlCommand cmd = new SqlCommand(query,SC);
                        cmd.ExecuteNonQuery();
                        SC.Close();
                    }
                        //updating values
                        if (objModel.IsActionName == "Delete")
                        {
                            DTUser.ActiveStatus = "N";
                        }
                        else
                        {
                            DTUser.BranchCode = objModel.FCode;
                            DTUser.FCode = objModel.FCode;
                           
                            
                            DTUser.LastModified = DateTime.Now.Date.ToString();

                            DTUser.GroupId = objModel.GroupId;
                        DTUser.IsAdmin = DTUser.IsAdmin;
                        DTUser.LastIP = "0";
                            DTUser.LastLoginTime = DateTime.Now.Date;
                            DTUser.LastLogOutTime = "";
                            DTUser.LoginStatus = "";
                            DTUser.LUserId = LoggedUser.UserId;
                            DTUser.Passw = objModel.Password;
                            DTUser.RecTimeStamp = DateTime.Now.Date;
                            DTUser.Remarks = objModel.Remarks != null ? objModel.Remarks : ""; 
                            DTUser.Status = objModel.ActiveStatus;
                        DTUser.Mobile = objModel.Mobile;
                        DTUser.Email = objModel.Email;
                        DTUser.ActiveStatus = objModel.ActiveStatus;
                        //DTUser.UId = 0;
                        DTUser.UserName = objModel.UserName;
                            
                            if (objModel.IsActionName == "Add")
                            {
                            DTUser.IsAdmin = "N";
                            DTUser.ActiveStatus = "Y";
                                DTUser.CreateBy = LoggedUser.UserId.ToString();
                                DTUser.CreateDate = DateTime.Now.Date;
                                DTUser.UserId = maxUserId;
                                entity.Inv_M_UserMaster.Add(DTUser);
                            }
                            DTUser.Version = Version;
                        }
                        int i = entity.SaveChanges();
                        if(objModel.IsActionName=="Edit" || i > 0)
                        {
                            if(objModel.IsActionName == "Edit")
                            {
                                objResponse.ResponseMessage = "Updated Successfully!";
                            }
                            else if(objModel.IsActionName == "Add")
                            {
                                objResponse.ResponseMessage = "Saved Successfully!";
                            }
                            else
                            {
                                objResponse.ResponseMessage = "Deleted Successfully!";
                            }
                            
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

        public bool CanUserAccessMenu(int UserID, string MenuFile)
        {
            bool UserCanAcess = false;
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    var result = (from r in entity.Web_M_MenuMaster
                                  join s in entity.Web_M_UserPermissionMaster on r.MenuId equals s.MenuId
                                  where s.GroupId == UserID
                                  select r).ToList();
                    foreach (var obj in result)
                    {
                        string[] onselect = obj.OnSelect.Split('/');
                        if (onselect.Length > 1)
                            if (onselect[1].ToLower() == MenuFile.ToLower())
                            {
                                UserCanAcess = true;
                                break;
                            }
                    }
                }

            }
            catch (Exception)
            {
            }
            return UserCanAcess;
        }

        public string UserCanAccess(int UserID, string MenuFile)
        {
            string UserCanAcess = "";
            try
            {
                using (var entity = new InventoryEntities(enttConstr))
                {
                    var result = (from r in entity.Web_M_MenuMaster
                                  join s in entity.Web_M_UserPermissionMaster on r.MenuId equals s.MenuId
                                  where s.GroupId == UserID
                                  select new {
                                      OnSelect = r.OnSelect,
                                      IsEdit = s.IsEdit
                                  }).ToList();

                    foreach (var obj in result)
                    {
                        string[] onselect = obj.OnSelect.Split('/');
                        if (onselect.Length > 1)
                            if (onselect[1].ToLower() == MenuFile.ToLower())
                            {
                                UserCanAcess = "View";
                                if (obj.IsEdit == "Y")
                                {
                                    UserCanAcess = "Edit";
                                }                                
                                break;
                            }
                    }
                }

            }
            catch (Exception)
            {
            }
            return UserCanAcess;
        }
    }
}
