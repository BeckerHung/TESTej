//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoppingMail.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductModel
    {
        public int Id { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
        public string Name { get; set; }
        public string SupplierName { get; set; }
        public string Description { get; set; }
        public Nullable<int> Price { get; set; }
        public string Img { get; set; }
        public int Stock { get; set; }
    }
}
