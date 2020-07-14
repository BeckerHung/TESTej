using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingMail.Models
{
    public class CategoryModel
    {
        public string name { get; set; }
        public int Id { get; set; }
        public int Parent_Id { get; set; }
        public List<CategoryModel> subdir { get; set; }
    }
}