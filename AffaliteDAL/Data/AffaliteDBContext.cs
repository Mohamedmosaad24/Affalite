using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteDAL.Data.Configurations;
using AffaliteDAL.Entities;
using AffaliteDAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AffaliteDAL.Data
{
    public class AffaliteDBContext : IdentityDbContext<AppUser>
    {
        public AffaliteDBContext(DbContextOptions<AffaliteDBContext> options) : base(options)
        {
        }
        public DbSet<Affiliate> Affiliates { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
       .HasIndex(c => c.Slug)
       .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.PlatformCommissionPct)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Merchant>()
                .Property(m => m.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Affiliate>()
                .Property(a => a.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.AffiliateCommissionPct)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(i => i.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Commission>()
                .Property(c => c.AffiliateAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Commission>()
                .Property(c => c.PlatformAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Commission>()
                .Property(c => c.MerchantAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Commission)
                .WithOne(c => c.Order)
                .HasForeignKey<Commission>(c => c.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Affiliate)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AffiliateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Merchant)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.MerchantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Merchant)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.MerchantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<AppUser>()
                .OwnsMany(u => u.RefreshTokens, b =>
                {
                    b.WithOwner().HasForeignKey("AppUserId");
                    b.Property<int>("Id");
                    b.HasKey("Id");
                    b.Property(p => p.Token).IsRequired();
                });

            modelBuilder.Entity<Merchant>()
                .HasOne(m => m.AppUser)
                .WithOne(u => u.MerchantProfile)
                .HasForeignKey<Merchant>(m => m.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Affiliate>()
                .HasOne(a => a.AppUser)
                .WithOne(u => u.AffiliateProfile)
                .HasForeignKey<Affiliate>(a => a.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);



            //seeding data 

            //var hasher = new PasswordHasher<AppUser>();

            //var merchantUser = new AppUser
            //{
            //    Id = "user-merchant-1",
            //    UserName = "merchant1",
            //    NormalizedUserName = "MERCHANT1",
            //    Email = "merchant@test.com",
            //    NormalizedEmail = "MERCHANT@TEST.COM",
            //    EmailConfirmed = true,
            //    FullName = "Merchant One",
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};
            //merchantUser.PasswordHash = hasher.HashPassword(merchantUser, "123456");

            //var affiliateUser = new AppUser
            //{
            //    Id = "user-affiliate-1",
            //    UserName = "affiliate1",
            //    NormalizedUserName = "AFFILIATE1",
            //    Email = "affiliate@test.com",
            //    NormalizedEmail = "AFFILIATE@TEST.COM",
            //    EmailConfirmed = true,
            //    FullName = "Affiliate One",
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};
            //affiliateUser.PasswordHash = hasher.HashPassword(affiliateUser, "123456");

            //modelBuilder.Entity<AppUser>().HasData(merchantUser, affiliateUser);

            //// Merchant & Affiliate
            //modelBuilder.Entity<Merchant>().HasData(
            //    new Merchant
            //    {
            //        Id = 1,
            //        AppUserId = "user-merchant-1",
            //        Balance = 0,
            //        CreatedAt = new DateTime(2024, 1, 1)
            //    }
            //);

            //modelBuilder.Entity<Affiliate>().HasData(
            //    new Affiliate
            //    {
            //        Id = 1,
            //        AppUserId = "user-affiliate-1",
            //        Balance = 0,
            //        CreatedAt = new DateTime(2024, 1, 1)
            //    }
            //);

            //// Categories
            //modelBuilder.Entity<Category>().HasData(
            //    new Category { Id = 1, Name = "Mobiles", Slug = "mobiles", CreatedAt = new DateTime(2024, 1, 1) },
            //    new Category { Id = 2, Name = "Laptops", Slug = "laptops", CreatedAt = new DateTime(2024, 1, 1) }
            //);

            //// Products
            //modelBuilder.Entity<Product>().HasData(
            //    new Product
            //    {
            //        Id = 1,
            //        Name = "iPhone 14",
            //        CategoryId = 1,
            //        MerchantId = 1,
            //        Price = 30000,
            //        Stock = 10,
            //        ImageUrl = "518f36c2-0b27-46bf-a813-ff46a4d2168a.jpg",
            //        PlatformCommissionPct = 10,
            //        Status = ProductStatus.Active,
            //        CreatedAt = new DateTime(2024, 1, 1)
            //    },
            //    new Product
            //    {
            //        Id = 2,
            //        Name = "Samsung S23",
            //        CategoryId = 1,
            //        MerchantId = 1,
            //        Price = 25000,
            //        Stock = 15,
            //        ImageUrl = "518f36c2-0b27-46bf-a813-ff46a4d2168a.jpg",
            //        PlatformCommissionPct = 10,
            //        Status = ProductStatus.Active,
            //        CreatedAt = new DateTime(2024, 1, 1)
            //    },
            //    new Product
            //    {
            //        Id = 3,
            //        Name = "Dell Laptop",
            //        CategoryId = 2,
            //        MerchantId = 1,
            //        Price = 40000,
            //        Stock = 5,
            //        ImageUrl = "518f36c2-0b27-46bf-a813-ff46a4d2168a.jpg",
            //        PlatformCommissionPct = 12,
            //        Status = ProductStatus.Active,
            //        CreatedAt = new DateTime(2024, 1, 1)
            //    }
            //);

            //// Order
            //modelBuilder.Entity<Order>().HasData(
            //    new Order
            //    {
            //        Id = 1,
            //        MerchantId = 1,
            //        AffiliateId = 1,
            //        CustomerName = "Ahmed",
            //        CustomerPhone = "01000000000",
            //        CustomerAddress = "Cairo",
            //        TotalPrice = 30000,
            //        AffiliateCommissionPct = 5,
            //        Status = OrderStatus.Shipped,
            //        CreatedAt = new DateTime(2024, 1, 1)
            //    }
            //);

            //// Order Items
            //modelBuilder.Entity<OrderItem>().HasData(
            //    new OrderItem
            //    {
            //        Id = 1,
            //        OrderId = 1,
            //        ProductId = 1,
            //        Quantity = 1,
            //        Price = 30000,
            //        CreatedAt = new DateTime(2024, 1, 1)
            //    }
            //);

            //// Commission
            //modelBuilder.Entity<Commission>().HasData(
            //    new Commission
            //    {
            //        Id = 1,
            //        OrderId = 1,
            //        AffiliateAmount = 1500,
            //        PlatformAmount = 3000,
            //        MerchantAmount = 25500,
            //        Status = CommissionStatus.Failed,
            //        CreatedAt = new DateTime(2024, 1, 1)
            //    }
            //);

            //// Roles
            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

    }

}