using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class E_Company
    {

        public int CompanyId  {get;set;}
         public string CompanyName { get; set; }
        public string CompanyDomain { get; set; }
         public Boolean IsActive { get; set; }
         public int CreatedBy { get; set; }

    }

    public class E_CompList
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }


}