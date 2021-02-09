using InventoryManagement.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Interface
{
    interface I_Banner
    {

        IEnumerable<E_CompList> Get_CompanyList();
        DataTable SaveBanner(string Action,string CompanyId, string Bannername);
        string SaveBannerDetail(string Action, string BannerId,   string myfile);
        IEnumerable<E_Banner> SrchBanner();
    }
}
