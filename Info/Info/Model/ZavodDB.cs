using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Info
{
    public class ZavodDB : DbContext
    {
        private string _databasePath;

        public DbSet<Instruments> Instruments { get; set; }

        public DbSet<Location> Locations { get; set; }

        public ZavodDB(string databasePath)
        {
            _databasePath = databasePath;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
