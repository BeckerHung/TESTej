using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingMail.Models
{
    public class Shoppingcarmodel
    {
        public int? fOrderId { get; set; }
        public string fUserId { get; set; }
        public Nullable<int> fPId { get; set; }
        public string fPName { get; set; }
        public Nullable<int> fPrice { get; set; }
        public Nullable<int> fMaxQty { get; set; }
        public Nullable<int> fOrderQty { get; set; }
        public string fImg { get; set; }    
        public Nullable<int> fChangeQTY { get; set; }
        public string fIsApproved { get; set; }
        public Nullable<int> fAId_1 { get; set; }
        public string fAName { get; set; }
        public Nullable<int> fAId_2 { get; set; }
        public Nullable<int> fAId_3 { get; set; }
        public int fSupplyLimit { get; set; }


    }
}