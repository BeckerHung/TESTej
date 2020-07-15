using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingMail.Models
{
    public class ProductModel_2 : ItemModel_2, ICalculateQty
    {
        protected int _stock;
        public int Stock
        {
            get { return _stock; }
            set
            {
                if (_stock >= 0)
                {
                    _stock = value;
                }
            }
        }

        public int CalculateQty()
        {
            throw new NotImplementedException();
        }
    }
}