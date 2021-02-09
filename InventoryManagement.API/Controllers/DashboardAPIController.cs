﻿using InventoryManagement.API.Models;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagement.API.Controllers
{
    public class DashboardAPIController : ApiController
    {
        GenConnection objGetConn = new GenConnection();
        private static string dbName, invDbName, enttConstr, AppConstr, InvConstr, WRPartyCode;
        public DashboardAPIController()
        {
            //ConnModel obj = objGetConn.GetConstr();
            //dbName = obj.Db;
            //invDbName = obj.InvDb;
            //enttConstr = obj.EnttConnStr;
            //AppConstr = obj.AppConnStr;
            //InvConstr = obj.InvConnStr;
            //WRPartyCode = obj.WRPartyCode;
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
           // CompName = obj.CompName;
        }
        public decimal GetFWalletBalance(string LoginPartyCode)
        {
            decimal FWalletBalance = 0;
            try
            {
                string InvConnectionString = InvConstr;
                string db = dbName;
                string dbInv = invDbName;
                SqlConnection SC = new SqlConnection(InvConnectionString);
                SqlCommand cmd = new SqlCommand();

                string query = "Select SUM(CrAmt) - Sum(DrAmt) as Balance  FROM(Select SUM(Amount)CrAmt, 0 as DrAmt FROM " + dbInv + "..TrnVoucher WHERE Vtype = 'R' AND  CrTo = '" + LoginPartyCode + "' UNION ALL Select 0, SUM(Amount) DrAmt FROM " + dbInv + "..TrnVoucher WHERE Vtype = 'R' AND  DrTo = '" + LoginPartyCode + "')as a";
                cmd.CommandText = query;
                //cmd.Parameters.AddWithValue("@IdNo", IdNo);
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        FWalletBalance = decimal.Parse(reader["Balance"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return FWalletBalance;
        }

        public Dashboard GetDashboard(string LoginPartyCode)
        {
            Dashboard ObjDashboard = new Dashboard();
            try
            {
                string WRcode = WRPartyCode;
                using (var entity = new InventoryEntities( enttConstr))
                {
                    if (WRcode.ToUpper()== LoginPartyCode.ToUpper())
                    ObjDashboard = (from r in entity.WRDashboardSummary()
                                    select new Dashboard
                                    {
                                        TodayPurchase = r.TodayPurchase ?? 0,
                                        TodaysDrSale = r.TodaysDrSale ?? 0 ,
                                        TodaysFrSale = r.TodaysFrSale ?? 0,
                                        TodaysSaletoDr = r.TodaysSaletoDr ?? 0,
                                        TodaysSaletoFr = r.TodaysSaletoFr ?? 0,
                                        MonthPurchase = r.MonthPurchase ?? 0,
                                        MonthDrSale = r.MonthDrSale ?? 0,
                                        MonthFrSale = r.MonthFrSale ?? 0,
                                        MonthSaletoDr = r.MonthSaletoDr ?? 0,
                                        MonthSaletoFr = r.MonthSaletoFr ?? 0
                                    }).FirstOrDefault();
                    else
                        ObjDashboard = (from r in entity.FrDashboardSummary(LoginPartyCode)
                                        select new Dashboard
                                        {
                                            TodayPurchase = r.TodayPurchase ?? 0,
                                            TodaysDrSale = r.TodaysDrSale ?? 0,
                                            TodaysFrSale = r.TodaysFrSale ?? 0,
                                            TodaysSaletoDr = r.TodaysSaletoDr ?? 0,
                                            TodaysSaletoFr = r.TodaysSaletoFr ?? 0,
                                            MonthPurchase = r.MonthPurchase ?? 0,
                                            MonthDrSale = r.MonthDrSale ?? 0,
                                            MonthFrSale = r.MonthFrSale ?? 0,
                                            MonthSaletoDr = r.MonthSaletoDr ?? 0,
                                            MonthSaletoFr = r.MonthSaletoFr ?? 0
                                        }).FirstOrDefault();

                    ObjDashboard.WalletBal = GetFWalletBalance(LoginPartyCode);

                }
            }
            catch (Exception ex)
            {

            }
            return ObjDashboard;
        }
    }
}