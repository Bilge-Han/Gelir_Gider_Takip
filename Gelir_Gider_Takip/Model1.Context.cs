﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gelir_Gider_Takip
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Gelir_Gider_TakipEntities : DbContext
    {
        public Gelir_Gider_TakipEntities()
            : base("name=Gelir_Gider_TakipEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GELIR_GIDER_KAYITLARI> GELIR_GIDER_KAYITLARI { get; set; }
        public virtual DbSet<GIDER_TIPLERI> GIDER_TIPLERI { get; set; }
    }
}