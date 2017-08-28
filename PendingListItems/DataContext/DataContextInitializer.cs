using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PendingListItems.DataContext;
using PendingListItems.Models;
using System.Data.SqlClient;
using System.Data.Entity;

namespace PendingListItems.DataContext
{
    public class DataContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            SqlConnection.ClearAllPools();
            context.Database.CreateIfNotExists();
            System.Data.Entity.Database.SetInitializer(new NullDatabaseInitializer<DataContext>());

            var priorities = new List<PriorityModel>
            {
                new PriorityModel { Description = "Immediate" },
                new PriorityModel { Description = "High" },
                new PriorityModel { Description = "Medium" },
                new PriorityModel { Description = "Low" }
            };

            priorities.ForEach(s => context.Priority.Add(s));
            context.SaveChanges();

            var periods = new List<PeriodModel>
            {
                new PeriodModel{ PeriodName = "Weekly" },
                new PeriodModel{ PeriodName = "Fortnight" },
                new PeriodModel{ PeriodName = "Montly" }
            };

            periods.ForEach(s => context.Period.Add(s));
            context.SaveChanges();
        }
    }
}