using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarWarsShop.DAL.Entities;

namespace StarWarsShop.DAL
{
    public class StarWarsShopDBContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }

        public StarWarsShopDBContext(DbContextOptions<StarWarsShopDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //для теста напряму пока отсюда цепляться буду, но в будущем все будет через appsettings.json
            optionsBuilder.UseSqlite("Data Source=StarWarsShopDB.db;");
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}