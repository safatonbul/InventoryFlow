using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryFlow.Entities.Entities;
using Microsoft.Identity.Client;

namespace InventoryFlow.DataAccess.Context
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        { 
        }
        public DbSet<Product> Products { get; set; }
        
    }
}
