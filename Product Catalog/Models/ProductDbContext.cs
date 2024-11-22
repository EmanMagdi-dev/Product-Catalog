using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Product_Catalog.DTOs;

namespace Product_Catalog.Models
{
    public class ProductDbContext : IdentityDbContext
    {

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Product_Catalog.DTOs.ProductDTO> ProductDTO { get; set; } = default!;

    }
}
