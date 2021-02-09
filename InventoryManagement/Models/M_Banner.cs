using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class M_Banner
    {
        public E_Banner Banner { get; set; }
        public IEnumerable<E_BannerCat> GetBannerCatList { get; set; }
        public IEnumerable<E_Banner> GRDBanner { get; set; }
    }

}