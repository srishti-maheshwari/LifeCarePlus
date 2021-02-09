using InventoryManagement.DataLayer;
using InventoryManagement.Entity;
using InventoryManagement.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace InventoryManagement.Repoistry
{
    public class R_Company:I_Company
    {

        public string SaveCompany(string Action, string CompanyId, string CompanyName, string CompanyDomain, string userId)
        {
            BLLDBOperations blldb = new BLLDBOperations();
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", Action);
            hst.Add("CompanyId", CompanyId);
            hst.Add("CompanyName", CompanyName);
            hst.Add("CompanyDomain", CompanyDomain);
            hst.Add("userId", userId);
           
            dt = blldb.GetDataTable("SP_CompanyMaster", CommandType.StoredProcedure, hst);
            return dt.Rows[0][0].ToString();

        }

        public IEnumerable<E_Company> SrchCompany()
        {
            BLLDBOperations blldb = new BLLDBOperations();
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "SrchCompany");
            //hst.Add("CompanyName", CompanyName);
            dt = blldb.GetDataTable("SP_CompanyMaster", CommandType.StoredProcedure, hst);
            IEnumerable<E_Company> lst = DbOperation.ConvertDataTable<E_Company>(dt);
            return lst;
        }
    }
}