﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PisDataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DevelopmentPISEntities : DbContext
    {
        public DevelopmentPISEntities()
            : base("name=DevelopmentPISEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Event> EventSet { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> SessionSet { get; set; }
        public DbSet<UserPosition> UserPositionSet { get; set; }
        public DbSet<WhereSolicitation> WhereSolicitationSet { get; set; }
        public DbSet<Share> ShareSet { get; set; }
        public DbSet<Permission> PermissionSet { get; set; }
        public DbSet<MensajeLogSet> MensajeLogSetSet { get; set; }
    }
}
