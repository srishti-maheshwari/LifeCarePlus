using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity
{
    public class E_Banner
    {
        public int Bannerid { get; set; }
        public int CompanyId { get; set; }
        public string BannerName { get; set; }
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please select file.")]
        //public HttpPostedFileBase[] files { get; set; }
        public string ImagePath { get; set; }
        public Boolean Isactive { get; set; }
    }
}