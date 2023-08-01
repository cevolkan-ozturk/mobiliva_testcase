using Microsoft.EntityFrameworkCore;
using Mobiliva.Ecommerce.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiliva.Ecommerce.Data.Context
{
    public class EcommerceEfDbContext : DbContext
    {
        public EcommerceEfDbContext( DbContextOptions options): base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set;}

        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }

}
