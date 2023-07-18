using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Contexts
{
    public class UpStorageShopDbContext : DbContext, IUpStorageShopDbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<OrderEvent> OrderEvent { get; set; }

        public UpStorageShopDbContext(DbContextOptions<UpStorageShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }
    }
}
