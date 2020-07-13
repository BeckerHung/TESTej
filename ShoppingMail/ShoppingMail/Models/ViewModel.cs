using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingMail.Models;

namespace ShoppingMail.Models
{
  
    public class ViewModel
    {
        public ShoppingMail.Models.tMember TMember { get; set; }
        public ShoppingMail.Models.tOrder TOrder { get; set; }
        public ShoppingMail.Models.tOrderDetail TOrderDetail { get; set; }
        public ShoppingMail.Models.tProduct TProducts { get; set; }
        public ShoppingMail.Models.tProductCategory TProductCategory { get; set; }
        public ShoppingMail.Models.tProductProperty TProductProperty { get; set; }
        public ShoppingMail.Models.tProductStock TProductStock { get; set; }


    }
}