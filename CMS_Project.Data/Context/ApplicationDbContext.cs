using CMS_Project.Data.SeedData;
using CMS_Project.Entity.Entities.Concrete;
using CMS_Project.Map.Mapping.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_Project.Data.Context
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new PageMap());
            builder.ApplyConfiguration(new ProductMap());         
            builder.ApplyConfiguration(new SeedPages());

            base.OnModelCreating(builder);

            //builder.Entity<Product>().Property(x => x.UnitPrice).HasColumnType("decimal");          
            //builder.Entity<IBaseEntity>().Property(x => x.CreateDate).HasColumnType("datetime2");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
