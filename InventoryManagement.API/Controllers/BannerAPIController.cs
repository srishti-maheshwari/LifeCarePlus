using InventoryManagement.Entity.Common;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;

namespace InventoryManagement.API.Controllers
{
    public class BannerAPIController : ApiController
    {
        GenConnection objGetConn = new GenConnection();
        private static string dbName, invDbName, enttConstr, AppConstr, InvConstr, WRPartyCode;
        public BannerAPIController()
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
            //  CompName = obj.CompName;
        }
        public IEnumerable<E_BannerCat> Get_BannerCatList()
        {
            BLLDBOperations blldb = new BLLDBOperations(InvConstr);
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "GetBannerCatList");
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);
            IEnumerable<E_BannerCat> lst = DbOperation.ConvertDataTable<E_BannerCat>(dt);
            return lst;
        }


        public DataTable SaveBanner(string Action, string BannerCatId)
        {
            BLLDBOperations blldb = new BLLDBOperations(InvConstr);
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "SaveBanner");
            hst.Add("BannerCatId", BannerCatId);
            // hst.Add("BannerId", BannerId);
           // hst.Add("Bannername", Bannername);
            //hst.Add("ImagePath", fname);
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);
            return dt;
            //return dt.Rows[0][0].ToString();
        }


        public string SaveBannerDetail(string Action, string BannerId, string myfile)
        {
            BLLDBOperations blldb = new BLLDBOperations(InvConstr);
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "SaveBannerDetail");

            hst.Add("BannerId", BannerId);

            hst.Add("ImagePath", myfile);
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);

            return dt.Rows[0][0].ToString();
        }


         public string DeleteBannerLstRow(string Action, string BannerId, string BannerDetailId)
        {
            BLLDBOperations blldb = new BLLDBOperations(InvConstr);
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "DeleteBanner");

            hst.Add("BannerId", BannerId);

            hst.Add("BannerDetailId", BannerDetailId);
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);

            return dt.Rows[0][0].ToString();
        }


        public IEnumerable<E_Banner> SrchBanner()
        {
            BLLDBOperations blldb = new BLLDBOperations(InvConstr);
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "SrchBanner");
            //hst.Add("BannerName", BannerName);
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);
            IEnumerable<E_Banner> lst = DbOperation.ConvertDataTable<E_Banner>(dt);
            return lst;
        }


        public IEnumerable<E_Banner> GetBannerSize(string BannerCatId)
        {
            BLLDBOperations blldb = new BLLDBOperations(InvConstr);
            Hashtable hst = new Hashtable();
            DataTable dt;
            hst.Add("Action", "GetBannerSize");
            hst.Add("BannerCatId", BannerCatId);
            dt = blldb.GetDataTable("SP_BannerMaster", CommandType.StoredProcedure, hst);
            IEnumerable<E_Banner> lst = DbOperation.ConvertDataTable<E_Banner>(dt);
            return lst;
        }
    }
}
