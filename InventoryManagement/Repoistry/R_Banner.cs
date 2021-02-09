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
    public class R_Banner:I_Banner
    {

        public IEnumerable<E_CompList> Get_CompanyList()
        {
            BLLDBOperations blldb = new BLLDBOperations();
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "GetCompList");
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);
            IEnumerable<E_CompList> lst = DbOperation.ConvertDataTable<E_CompList>(dt);
            return lst;
        }


      public DataTable SaveBanner(string Action, string CompanyId, string Bannername)
        {
            BLLDBOperations blldb = new BLLDBOperations();
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "SaveBanner");
            hst.Add("CompanyId", "0");
           // hst.Add("BannerId", BannerId);
            hst.Add("Bannername", Bannername);
            //hst.Add("ImagePath", fname);
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);
            return dt;
            //return dt.Rows[0][0].ToString();
        }


        public string SaveBannerDetail(string Action, string BannerId, string myfile)
        {
            BLLDBOperations blldb = new BLLDBOperations();
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "SaveBannerDetail");
        
             hst.Add("BannerId", BannerId);
          
            hst.Add("ImagePath", myfile);
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);
            
            return dt.Rows[0][0].ToString();
        }


        public IEnumerable<E_Banner> SrchBanner()
        {
            BLLDBOperations blldb = new BLLDBOperations();
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "SrchBanner");
            //hst.Add("BannerName", BannerName);
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);
            IEnumerable<E_Banner> lst = DbOperation.ConvertDataTable<E_Banner>(dt);
            return lst;
        }

    }
}