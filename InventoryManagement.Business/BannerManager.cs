using InventoryManagement.Business.Contract;
using InventoryManagement.DataAccess;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace InventoryManagement.Business
{
    public class BannerManager: IBannerManager
    {
        BannerRepository objBannerRepositoryRepo = new BannerRepository();
        public IEnumerable<E_BannerCat> Get_BannerCatList()
        {
            return (objBannerRepositoryRepo.Get_BannerCatList());
        }
        public DataTable SaveBanner(string Action, string BannerCatId)
        {
            return (objBannerRepositoryRepo.SaveBanner(Action, BannerCatId));
        }
        public string SaveBannerDetail(string Action, string BannerId, string myfile)
        {
            return (objBannerRepositoryRepo.SaveBannerDetail(Action, BannerId, myfile));
        }

        public string DeleteBannerLstRow(string Action, string BannerId, string BannerDetailId)
        {
            return (objBannerRepositoryRepo.DeleteBannerLstRow(Action, BannerId, BannerDetailId));
        }

        public IEnumerable<E_Banner> SrchBanner()
        {
            return (objBannerRepositoryRepo.SrchBanner());
        }

        public IEnumerable<E_Banner> GetBannerSize(string BannerCatId)
        {
            return (objBannerRepositoryRepo.GetBannerSize(BannerCatId));
        }
    }
}