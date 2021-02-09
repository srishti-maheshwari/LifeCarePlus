using InventoryManagement.Entity;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Contract
{
    interface ICompanyRepository
    {
       string SaveCompany(string Action, string CompanyId, string CompanyName, string CompanyDomain, string userId);
       IEnumerable<E_Company> SrchCompany();
    }
}
