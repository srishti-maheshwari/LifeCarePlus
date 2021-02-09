using InventoryManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Interface
{
    interface I_Company
    {
        string SaveCompany(string Action, string CompanyId, string CompanyName, string CompanyDomain, string userId);
       IEnumerable<E_Company> SrchCompany();
    }
}
