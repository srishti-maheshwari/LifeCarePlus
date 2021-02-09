using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class E_Banner
    {
        public int BannerId { get; set; }
        public int BannerDetailId { get; set; }
        public int CompanyId { get; set; }
        public int BannerCatId { get; set; }
        public string BannerName { get; set; }
        public string BannerCatName { get; set; }
        public string CompanyName { get; set; }
        public string Size { get; set; }

        [Required(ErrorMessage = "Please select file.")]
        //public HttpPostedFileBase[] files { get; set; }
        public string ImagePath { get; set; }
        public Boolean Isactive { get; set; }
    }


     public class E_BannerCat
    {
        public int BannerCatId { get; set; }
        public string BannerCatName { get; set; }

    }



}