using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PendingListItems.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PendingListItems.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DataContext") { }

        public DbSet<ListItemModel> ListItem { get; set; }
        public DbSet<PriorityModel> Priority { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}