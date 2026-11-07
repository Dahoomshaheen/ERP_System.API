using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ERP_System.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<SalesHeader> salesHeaders { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
