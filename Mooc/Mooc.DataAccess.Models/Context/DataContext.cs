using Mooc.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.Context
{
    public class DataContext: DbContext
    {
        public DataContext() : base(GetConnectionString())
        {
        }

        private static string GetConnectionString()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ConnectionString"];
            return settings.ConnectionString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SubjectCategory> SubjectCategorys { get; set; }



    }
}
