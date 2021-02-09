using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;
using System.Web.Configuration;

namespace InventoryManagement.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {

           
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           // GetConstr();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void GetConstr()
        {
            string Constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;
            string dbname = System.Web.Configuration.WebConfigurationManager.AppSettings["INVDatabase"];
            SqlConnection SC = new SqlConnection(Constr);
            string query = "Select * FROM M_ConnectionMaster WHERE DatabaseName='" + dbname + "'";
            SqlCommand cmd = new SqlCommand();
            SC.Open();
            cmd.CommandText = query;
            cmd.Connection = SC;
            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);

            SC.Close();

            if (Dt.Rows.Count > 0)
            {
                System.Configuration.Configuration Config1 = WebConfigurationManager.OpenWebConfiguration("~");
                Config1.ConnectionStrings.ConnectionStrings["InventoryServices"].ConnectionString = Dt.Rows[0]["ConnectionString"].ToString();
                Config1.ConnectionStrings.ConnectionStrings["InventoryEntities"].ConnectionString = "metadata=res://*/Models.InventoryManagementEntity.csdl|res://*/Models.InventoryManagementEntity.ssdl|res://*/Models.InventoryManagementEntity.msl;provider=System.Data.SqlClient;provider connection string=\"" + Dt.Rows[0]["ConnectionString"].ToString() + ";multipleactiveresultsets=True;application name=EntityFramework\"";
                Config1.AppSettings.Settings["CVCaption"].Value = "CCV";
                Config1.AppSettings.Settings["BVCaption"].Value = "Neha";
                Config1.AppSettings.Settings["BVCaption"].Value = "Neha";
                Config1.Save(ConfigurationSaveMode.Modified);
                // ConfigurationManager.RefreshSection("connectionStrings");
                //ConnectionStringsSection conSetting = (ConnectionStringsSection)Config1.GetSection("connectionStrings");
                //ConnectionStringSettings StringSettings = new ConnectionStringSettings("InventoryServices",Dt.Rows[0]["ConnectionString"].ToString());
            }
        }
    }
}
