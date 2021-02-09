using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class M_Company
    {
         public E_Company Company { get; set; }
        public IEnumerable<E_Company> GRDCompany { get; set; }
    }
}