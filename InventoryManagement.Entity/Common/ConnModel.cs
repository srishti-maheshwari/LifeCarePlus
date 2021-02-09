using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class ConnModel

    {
        public string CompID { get; set; }
        public string InvConnStr { get; set; }
        public string InvDb { get; set; }
        public string EnttConnStr { get; set; }
        public string AppConnStr { get; set; }
        public string Db { get; set; }
        public string WRPartyCode { get; set; }
        public string CompName { get; set; }
        public string BVCaption { get; set; }
        public string CVCaption { get; set; }
        public string PVCaption { get; set; }
        public string RPCaption { get; set; }
        public string FirstActivationBill { get; set; }
        public string FirstBillOnMRP { get; set; }
        public string FirstBillonBV { get; set; }
        public string FirstBillonAmt { get; set; }
        public string FirstBillonPV { get; set; }
        public decimal FirstBillMinAmt { get; set; }
        public decimal FirstBillMinBV { get; set; }
        public decimal FirstBillMinPV { get; set; }
        public string FirstIDUpgrade { get; set; }
        //public string CanUpgrade { get; set; }

        public string ShowInvType { get; set; }
        public string ShowOffers { get; set; }
        public string WalletType { get; set; }
        public string LogoPath { get; set; }

        public string ShowRepurchBV { get; set; }
        public string ShowActiveBV { get; set; }
        public string ShowRepurchPV { get; set; }
        public string ShowActivePV { get; set; }

        /*public string FirstActivationBill { get; set; }*/

    }
}