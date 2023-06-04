using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUpStorageShopDbContext
    {
        DbSet<Order> Order { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<OrderEvent> OrderEvent { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}
