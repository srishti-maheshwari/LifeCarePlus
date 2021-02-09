using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;
using System.Web.Configuration;
using InventoryManagement.Business;
using InventoryManagement.Entity.Common;
using System.Data.Entity.Core.EntityClient;

namespace InventoryManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            try
            {

           
            AreaRegistration.RegisterAllAreas();
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
            LogManager objLogManager = new LogManager();
            try
            {
            string Constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;
            string compid = System.Web.Configuration.WebConfigurationManager.AppSettings["CompID"];
            SqlConnection SC = new SqlConnection(Constr);
            string query = "Select * FROM M_ConnectionMaster WHERE CompanyID='" + compid + "' AND IsActive='1'";
            SqlCommand cmd = new SqlCommand();
            SC.Open();
            cmd.CommandText = query;
            cmd.Connection = SC;
            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);

            SC.Close();
                //User Objresponse = new User();
                //Objresponse.UserId = 1;
                //Objresponse.UserName = "Amit";
                //Objresponse.PartyCode = "";
                //Objresponse.PartyName = "ABCD";
            if (Dt.Rows.Count > 0)
            {
                    for (var i = 0; i < Dt.Rows.Count - 1; i++)
                    {
                        if (Convert.ToInt32(Dt.Rows[i]["ConnTypeID"].ToString()) == 2)
                        { Session["InvConnStr"] = Dt.Rows[i]["ConnectionString"].ToString();
                            Session["InvDb"] = Dt.Rows[i]["DatabaseName"].ToString();
                            Session["EnttConnStr"] = BuildConnectionString(Dt.Rows[i]["ConnectionString"].ToString());
                        }

                        if (Convert.ToInt32(Dt.Rows[i]["ConnTypeID"].ToString()) == 1)
                        { Session["AppConnStr"] = Dt.Rows[i]["ConnectionString"].ToString();
                            Session["Db"] = Dt.Rows[i]["DatabaseName"].ToString();
                        }
                    }
                //System.Configuration.Configuration Config1 = WebConfigurationManager.OpenWebConfiguration("~");
                //Config1.ConnectionStrings.ConnectionStrings["InventoryServices"].ConnectionString = Dt.Rows[0]["ConnectionString"].ToString();
                //Config1.ConnectionStrings.ConnectionStrings["InventoryEntities"].ConnectionString = "metadata=res://*/Models.InventoryManagementEntity.csdl|res://*/Models.InventoryManagementEntity.ssdl|res://*/Models.InventoryManagementEntity.msl;provider=System.Data.SqlClient;provider connection string=\"" + Dt.Rows[0]["ConnectionString"].ToString() + ";multipleactiveresultsets=True;application name=EntityFramework\"";
                //    //Config1.AppSettings.Settings["CVCaption"].Value = "CCV";
                //    //Config1.AppSettings.Settings["BVCaption"].Value = "Neha";
                //    //Config1.AppSettings.Settings["BVCaption"].Value = "Neha";
                //    objLogManager.SaveLog(Objresponse, "SUCCESS-1", Config1.ConnectionStrings.ConnectionStrings["InventoryEntities"].ConnectionString);
                //    Config1.Save(ConfigurationSaveMode.Modified);
                //// ConfigurationManager.RefreshSection("connectionStrings");
                ////ConnectionStringsSection conSetting = (ConnectionStringsSection)Config1.GetSection("connectionStrings");
                ////ConnectionStringSettings StringSettings = new ConnectionStringSettings("InventoryServices",Dt.Rows[0]["ConnectionString"].ToString());
                //SC.Open();
                //query = "UPDATE M_ConnectionMaster SET FldsUpdated='Y' WHERE DatabaseName='" + dbname + "' ";
                // cmd = new SqlCommand(query,SC);
                //cmd.ExecuteNonQuery();
                //    objLogManager.SaveLog(Objresponse, "SUCCESS-2","");
                //    SC.Close();
            }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //     protected void Application_Error()
        //{
        //   var ex = Server.GetLastError();
        //   //log the error!
        // //  _Logger.Error(ex);
        //         System.Diagnostics.Debug.WriteLine(ex);
        //         Response.Redirect("/Home/Error");
        //     }

        private String BuildConnectionString(String constr)
        {
            //// Build the connection string from the provided datasource and database
            //String connString = @"data source=" + DataSource + ";initial catalog=" +
            //Database + ";integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;";

            //// Build the MetaData... feel free to copy/paste it from the connection string in the config file.
            //EntityConnectionStringBuilder esb = new EntityConnectionStringBuilder();
            //esb.Metadata = "res://*/AW_Model.csdl|res://*/AW_Model.ssdl|res://*/AW_Model.msl";
            //esb.Provider = "System.Data.SqlClient";
            //esb.ProviderConnectionString = connString;

            //// Generate the full string and return it
            //return esb.ToString();
            return "metadata=res://*/Models.InventoryManagementEntity.csdl|res://*/Models.InventoryManagementEntity.ssdl|res://*/Models.InventoryManagementEntity.msl;provider=System.Data.SqlClient;provider connection string=\""+ constr + ";multipleactiveresultsets=True;application name=EntityFramework\"";
        }
    }
}
