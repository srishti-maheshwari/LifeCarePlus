using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Business.Contract
{
    interface IBannerManager
    {
        IEnumerable<E_BannerCat> Get_BannerCatList() ;
        DataTable SaveBanner(string Action, string BannerCatId);
        string SaveBannerDetail(string Action, string BannerId, string myfile);
        IEnumerable<E_Banner> SrchBanner();
    }
}
