﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingMail.Models
{
    public class ItemModel
    {
        protected int _id, _categoryid, _price ;
        protected string _name, _suppliername, _description, _img ;

        public int Id 
        {
            get {return _id; }
            set
            {
                if (_id >= 0)
                {
                    _id = value;
                }
            }
        }       
        public int CategoryId
        {
            get { return _categoryid; }
            set
            {
                if (_categoryid >= 0) 
                {
                    _categoryid = value;
                }
            }
        }      
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string SupplierName
        {
            get { return _suppliername; }
            set { _suppliername = value; }
        }
        public string Description 
        {
            get { return _description; }
            set { _description = value; }
        }
        public int Price
        {
            get { return _price; }
            set 
            {
                if (_price >= 0) 
                {
                    _price = value;
                } 
            }
        }
        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }

    }
}