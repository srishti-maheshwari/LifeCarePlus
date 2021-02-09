using InventoryManagement.API.Controllers;
using InventoryManagement.DataAccess.Contract;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace InventoryManagement.DataAccess
{
    public class BannerRepository : IBannerRepository
    {
        BannerAPIController objBannerApiController = new BannerAPIController();
        public IEnumerable<E_BannerCat> Get_BannerCatList()
        {
            return (objBannerApiController.Get_BannerCatList());
        }
        public DataTable SaveBanner(string Action, string BannerCatId)
        {
            return (objBannerApiController.SaveBanner(Action, BannerCatId));
        }
        public string SaveBannerDetail(string Action, string BannerId, string myfile)
        {
            return (objBannerApiController.SaveBannerDetail(Action, BannerId, myfile));
        }
        public IEnumerable<E_Banner> SrchBanner()
        {
            return (objBannerApiController.SrchBanner());
        }

        public IEnumerable<E_Banner> GetBannerSize(string BannerCatId)
        {
            return (objBannerApiController.GetBannerSize(BannerCatId));
        }

        public string DeleteBannerLstRow(string Action, string BannerId, string BannerDetailId)
        {
            return (objBannerApiController.DeleteBannerLstRow(Action, BannerId, BannerDetailId));
        }
    }
}