using InventoryManagement.Business;
using InventoryManagement.Entity.Common;
using InventoryManagement.Models;
//using InventoryManagement.Interface;
//using InventoryManagement.Models;
//using InventoryManagement.Repoistry;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    public class BannerController : Controller
    {
        BannerManager objBannerManager = new BannerManager();

        //public BannerController()
        //{
        //    this._ibanner = new R_Banner();
        //}
        // GET: Banner
        public ActionResult Banner(M_Banner obj)
        {
            obj.GetBannerCatList = objBannerManager.Get_BannerCatList();
            obj.GRDBanner = objBannerManager.SrchBanner();
            return View(obj);
        }


        public ActionResult getBannerSize(M_Banner obj, string BannerCatId)
        {
            IEnumerable<E_Banner> objl = objBannerManager.GetBannerSize(BannerCatId);
            return Json(new { objl });
        }

        [HttpPost]
        public ActionResult SaveBanner(string Action)
        {
            string BannerCatId = Request.Form["BannerCatId"];
            //string Bannername = Request.Form["Bannername"];
            string BannerId = Request.Form["BannerId"];

            string fname = "";
            string myfile = "";
            string save = "";
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;

                //var path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads");

                DataTable dt = objBannerManager.SaveBanner(Action, BannerCatId);

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        myfile = Guid.NewGuid() + "-" + Path.GetFileName(file.FileName);
                        //filename = file.FileName;
                    }
                    var path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fname = Path.Combine(path + myfile);
                    //fname = Path.Combine(Server.MapPath("~/Uploads/") + filename);
                    file.SaveAs(fname);
                    save = objBannerManager.SaveBannerDetail(Action, dt.Rows[0]["BannerId"].ToString(), myfile);
                }

            }

            return Json(new { save });
        }


        public ActionResult DeleteBannerLstRow(string Action,string BannerId,string BannerDetailId,string BannerName,string ImagePath)
        {
            var filePath = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/" + ImagePath);
            //string filePath = Server.MapPath("~/Uploads" + ImagePath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            //if (System.IO.File.Exists(filePath))
            //{
            //    System.IO.File.Delete(filePath);
            //}
            string del = objBannerManager.DeleteBannerLstRow(Action, BannerId, BannerDetailId);
            return Json(new { del });
        }
    }
}