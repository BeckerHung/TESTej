﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbShoppingMailEntities : DbContext
    {
        public dbShoppingMailEntities()
            : base("name=dbShoppingMailEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tMember> tMember { get; set; }
        public virtual DbSet<tOrder> tOrder { get; set; }
        public virtual DbSet<tOrderDetail> tOrderDetail { get; set; }
        public virtual DbSet<tProductCategory> tProductCategory { get; set; }
        public virtual DbSet<tProductProperty> tProductProperty { get; set; }
        public virtual DbSet<ThumbsUp> ThumbsUp { get; set; }
        public virtual DbSet<tExchangeRate> tExchangeRate { get; set; }
        public virtual DbSet<tAttributes> tAttributes { get; set; }
        public virtual DbSet<tProductStock> tProductStock { get; set; }
        public virtual DbSet<tProduct> tProduct { get; set; }
        public virtual DbSet<ItemModel> ItemModel { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
    }
}
