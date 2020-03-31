using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramwork.Contexts
{
    public class CheckUrlContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlite("Data Source=Db_CheckUrl.db");            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<UrlScheduler> UrlSchedulers { get; set; }
        public DbSet<UrlCheckList> UrlChecks { get; set; }
    }
}
