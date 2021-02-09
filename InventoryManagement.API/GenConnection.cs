using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using InventoryManagement.Entity.Common;
namespace InventoryManagement.API
{
    public class GenConnection
    {
        public ConnModel GetConstr()
        {
            ConnModel objcon = new ConnModel();
            try
            {
                //System.Web.HttpContext.Current.Session["DBName"] = "VisionRoots";
                //System.Web.HttpContext.Current.Session["AppDBConStr"] = "Data Source=103.74.54.49;Initial Catalog=VisionRoots;Integrated Security=False;User Id=usrvisn;Password=Su$n!428P#t;";
                //System.Web.HttpContext.Current.Session["InvDBConStr"] = "Data Source=103.74.54.49;Initial Catalog=VRInv;Integrated Security=False;User Id=usrvisn;Password=Su$n!428P#t;";
                //System.Web.HttpContext.Current.Session["enttConStr"] = BuildConnectionString(System.Web.HttpContext.Current.Session["InvDBConStr"].ToString());

                string Constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;
                string ImgPath = System.Web.Configuration.WebConfigurationManager.AppSettings["ImgPath"];
                // string compid = System.Web.Configuration.WebConfigurationManager.AppSettings["CompID"];
                string compid = "0";
                string URL = System.Web.HttpContext.Current.Request.Url.Host.ToUpper().Replace("HTTP://", "").Replace("HTTPS://", "").Replace("WWW.", "").Replace("/", "").Replace("BASICFRANCHISE.", "").Replace("FRANCHISE.", "");// System.Web.HttpContext.Current.Request.UserHostName;
                string query;

                System.Web.HttpContext.Current.Session["URL"] = URL;
                if (URL == "LOCALHOST")
                {
                    compid = System.Web.Configuration.WebConfigurationManager.AppSettings["TmpCompID"];
                    query = "Select a.*,'"+ ImgPath + "'+ b.Logo as Logo FROM M_ConnectionMaster a, M_CompanyMasterNew b WHERE a.CompanyID=b.ID AND a.CompanyID='" + compid + "' AND a.IsActive='1'";
                }
                else
                    query = "Select a.*,'"+ ImgPath + "'+ b.Logo as Logo FROM M_ConnectionMaster a, M_CompanyMasterNew b WHERE a.CompanyID=b.ID AND b.URL='" + URL + "' AND a.IsActive='1'";

                SqlConnection SC = new SqlConnection(Constr);

                //string query = "Select a.*,'"+ ImgPath + "'+ b.Logo as Logo FROM M_ConnectionMaster a, M_CompanyMasterNew b WHERE a.CompanyID=b.ID AND (b.URL='" + URL + "' OR ('"+ URL +"'='LOCALHOST' AND b.ID='"+ compid +  "'))  AND a.IsActive='1'";
                SqlCommand cmd = new SqlCommand();

                DataTable Dt = new DataTable();
                try
                {
                    SC.Open();
                    cmd.CommandText = query;
                    cmd.Connection = SC;
                    SqlDataAdapter Da = new SqlDataAdapter(cmd);
                    Da.Fill(Dt);
                    SC.Close();
                }
                catch (Exception ex)
                {
                    if (SC.State == ConnectionState.Open) SC.Close();

                }

                //query = "UPDATE M_CompanyMasterNew SET CrntURL='" + System.Web.HttpContext.Current.Request.Url.Host + "; " + System.Web.HttpContext.Current.Request.UserHostName + "' WHERE ID=3";
                //SC.Open();
                //cmd = new SqlCommand(query,SC);
                //cmd.ExecuteNonQuery();
                //SC.Close();

                if (Dt.Rows.Count > 0)// || (URL=="LOCALHOST"))
                {
                   
                    compid = Dt.Rows[0]["CompanyID"].ToString();

                    objcon.CompID = compid;
                    objcon.LogoPath= Dt.Rows[0]["Logo"].ToString();
                    for (var i = 0; i < Dt.Rows.Count ; i++)
                    {
                        if (Convert.ToInt32(Dt.Rows[i]["ConnTypeID"].ToString()) == 2)
                        {
                            objcon.InvConnStr = Dt.Rows[i]["ConnectionString"].ToString();
                            objcon.InvDb = Dt.Rows[i]["DatabaseName"].ToString();
                            objcon.EnttConnStr = BuildConnectionString(Dt.Rows[i]["ConnectionString"].ToString());
                        }

                        if (Convert.ToInt32(Dt.Rows[i]["ConnTypeID"].ToString()) == 1)
                        {
                            objcon.AppConnStr = Dt.Rows[i]["ConnectionString"].ToString();
                            objcon.Db = Dt.Rows[i]["DatabaseName"].ToString();
                        }
                    }


                     query = "Select * FROM DbCaptions WHERE CompID='" + compid + "' AND IsActive='1'";
                    SC.Open();
                    cmd = new SqlCommand(query,SC);
                    SqlDataReader Dr= cmd.ExecuteReader();

                    if (Dr.Read())
                    {
                        objcon.BVCaption = Dr["BVCaption"].ToString();
                        objcon.CVCaption = Dr["CVCaption"].ToString();
                        objcon.PVCaption = Dr["PVCaption"].ToString();
                        objcon.RPCaption = Dr["RPCaption"].ToString();
                        objcon.WRPartyCode = Dr["WRPartyCode"].ToString();
                        objcon.CompName = Dr["CompName"].ToString();

                    }
                    Dr.Close();
                    SC.Close();
                }
            }
            catch (Exception ex)
            {
            }
            if (objcon != null)
            {
                System.Web.HttpContext.Current.Session["ConModel"] = objcon;

            }

            return objcon;
        }

        private String BuildConnectionString(String constr)
        {
            return "metadata=res://*/Models.InventoryManagementEntity.csdl|res://*/Models.InventoryManagementEntity.ssdl|res://*/Models.InventoryManagementEntity.msl;provider=System.Data.SqlClient;provider connection string=\"" + constr + ";multipleactiveresultsets=True;application name=EntityFramework\"";
        }

        public ConnModel GetDistBillInfo()
        {
            string Constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;
            SqlConnection SC = new SqlConnection(Constr);
            ConnModel objcon = new ConnModel();
            try
            {
                objcon= GetConstr();
            string query = "Select * FROM DbCaptions WHERE CompID='" + objcon.CompID + "' AND IsActive='1'";
            SC.Open();
            SqlCommand cmd = new SqlCommand(query, SC);
            SqlDataReader Dr = cmd.ExecuteReader();

            if (Dr.Read())
            {
                objcon.FirstActivationBill = Dr["FirstActivationBill"].ToString();
                objcon.FirstBillOnMRP = Dr["FirstBillOnMRP"].ToString();
                objcon.FirstBillMinAmt = Convert.ToDecimal( Dr["FirstBillMinAmt"].ToString());
                objcon.FirstBillMinBV = Convert.ToDecimal( Dr["FirstBillMinBV"].ToString());
                    objcon.FirstBillMinPV = Convert.ToDecimal(Dr["FirstBillMinPV"].ToString());

                    objcon.FirstBillonBV = Dr["FirstBillonBV"].ToString().ToUpper();
                    objcon.FirstBillonAmt= Dr["FirstBillonAmt"].ToString().ToUpper();
                    objcon.FirstBillonPV = Dr["FirstBillonPV"].ToString().ToUpper();
                    objcon.FirstIDUpgrade = Dr["FirstIDUpgrade"].ToString().ToUpper();

                    objcon.ShowInvType = Dr["ShowInvType"].ToString().ToUpper();
                    objcon.ShowOffers = Dr["ShowOffers"].ToString().ToUpper();

                    objcon.ShowActiveBV = Dr["ShowActiveBV"].ToString().ToUpper();
                    objcon.ShowRepurchBV = Dr["ShowRepurchBV"].ToString().ToUpper();
                    objcon.ShowActivePV = Dr["ShowActivePV"].ToString().ToUpper();
                    objcon.ShowRepurchPV = Dr["ShowRepurchPV"].ToString().ToUpper();

                    objcon.WalletType = Dr["WalletType"].ToString().ToUpper();
            }
            Dr.Close();
            SC.Close();
            }
            catch (Exception)
            {
                if (SC.State == ConnectionState.Open) SC.Close();
            }
            return objcon;
        }
    }
}