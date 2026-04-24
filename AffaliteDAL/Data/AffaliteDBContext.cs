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
        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<ProductReviews> ProductReviews { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<WithdrawRequest> WithdrawRequests { get; set; }
        
        


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

            modelBuilder.Entity<MerchantOrder>()
                .HasKey(mo => new { mo.MerchantId, mo.OrderId });

            modelBuilder.Entity<MerchantOrder>()
                .HasOne(mo => mo.Merchant)
                .WithMany(m => m.MerchantOrder)
                .HasForeignKey(mo => mo.MerchantId);

            modelBuilder.Entity<MerchantOrder>()
                .HasOne(mo => mo.Order)
                .WithMany(o => o.MerchantOrder)
                .HasForeignKey(mo => mo.OrderId);

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
            // بعد الـ decimal configurations الموجودة ضيف:
            modelBuilder.Entity<Coupon>()
                .Property(c => c.DiscountAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Coupon>()
                .Property(c => c.DiscountPercentage)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<MerchantCommissions>()
                .Property(mc => mc.value)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
             .HasOne(oi => oi.Product)
             .WithMany(p => p.OrderItems)
             .HasForeignKey(oi => oi.ProductId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            ///seeeding
            
            // ---------------- ROLES ----------------
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "role-admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "role-merchant", Name = "Merchant", NormalizedName = "MERCHANT" },
                new IdentityRole { Id = "role-affiliate", Name = "Affiliate", NormalizedName = "AFFILIATE" },
                new IdentityRole { Id = "role-customer", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            // ---------------- USERS ----------------
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser { Id = "user1", UserName = "merchant1", Email = "merchant1@affalite.com", NormalizedUserName = "MERCHANT1@AFFALITE.COM", NormalizedEmail = "MERCHANT1@AFFALITE.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Password@123"), FullName = "Ahmed Hassan", PhoneNumber = "01001234567" },
                new AppUser { Id = "user2", UserName = "affiliate1", Email = "affiliate1@affalite.com", NormalizedUserName = "AFFILIATE1@AFFALITE.COM", NormalizedEmail = "AFFILIATE1@AFFALITE.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Password@123"), FullName = "Youssef Ali", PhoneNumber = "01001112233" },
                new AppUser { Id = "user3", UserName = "customer1", Email = "customer1@affalite.com", NormalizedUserName = "CUSTOMER1@AFFALITE.COM", NormalizedEmail = "CUSTOMER1@AFFALITE.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "Password@123"), FullName = "Hana Adel", PhoneNumber = "01002223344" }
            );

            // ---------------- USER ROLES ----------------
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "role-merchant", UserId = "user1" },
                new IdentityUserRole<string> { RoleId = "role-affiliate", UserId = "user2" },
                new IdentityUserRole<string> { RoleId = "role-customer", UserId = "user3" }
            );

            // ---------------- CATEGORIES ----------------
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Slug = "electronics", CreatedAt = DateTime.UtcNow },
                new Category { Id = 2, Name = "Fashion", Slug = "fashion", CreatedAt = DateTime.UtcNow }
            );

            // ---------------- MERCHANTS ----------------
            modelBuilder.Entity<Merchant>().HasData(
                new Merchant { Id = 1, AppUserId = "user1", Balance = 5000, CreatedAt = DateTime.UtcNow }
            );

            // ---------------- AFFILIATES ----------------
            modelBuilder.Entity<Affiliate>().HasData(
                new Affiliate { Id = 1, AppUserId = "user2", Balance = 1500, CreatedAt = DateTime.UtcNow }
            );
            // 7️⃣ Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 14",
                    CategoryId = 1,
                    Description = "Latest Apple iPhone",
                    Details = "Details here",
                    Price = 999,
                    Stock = 50,
                    SaleCount = 10,
                    MerchantId = 1,
                    PlatformCommissionPct = 5,
                    Status = ProductStatus.Active,
                    CreatedAt = DateTime.Now
                },
                new Product
                {
                    Id = 2,
                    Name = "Harry Potter Book",
                    CategoryId = 2,
                    Description = "Fantasy novel",
                    Details = "Details here",
                    Price = 20,
                    Stock = 100,
                    SaleCount = 50,
                    MerchantId = 1,
                    PlatformCommissionPct = 2,
                    Status = ProductStatus.Active,
                    CreatedAt = DateTime.Now
                }
            );

            // 8️⃣ ProductImages
            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage { Id = 1, ProductId = 1, ImageUrl = "p1.jpg", FileName = "iphone14.jpg" },
                new ProductImage { Id = 2, ProductId = 1, ImageUrl = "p4.jpg", FileName = "iphone14.jpg" },
                new ProductImage { Id = 3, ProductId = 2, ImageUrl = "p2.png", FileName = "harrypotter.jpg" },
                new ProductImage { Id = 4, ProductId = 2, ImageUrl = "p3.jpg", FileName = "harrypotter.jpg" }
            );

            // 9️⃣ ProductReviews
            modelBuilder.Entity<ProductReviews>().HasData(
                new ProductReviews { Id = 1, ProductId = 1, AffiliateId = 1, Comment = "Great phone!", Rating = 5, CreatedAt = DateTime.Now },
                new ProductReviews { Id = 2, ProductId = 2, AffiliateId = 1, Comment = "Loved the book", Rating = 4, CreatedAt = DateTime.Now }
            );

            // 1️⃣0️⃣ Carts
            modelBuilder.Entity<Cart>().HasData(
                new Cart { Id = 1, CreatedAt = DateTime.Now, AffiliateId = 1 }
            );

            // 1️⃣1️⃣ CartItems
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { Id = 1, CartId = 1, ProductId = 1, Quantity = 2, CreatedAt = DateTime.Now },
                new CartItem { Id = 2, CartId = 1, ProductId = 2, Quantity = 1, CreatedAt = DateTime.Now }
            );

            // 1️⃣2️⃣ Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    AffiliateId = 1,
                    CustomerName = "David",
                    CustomerPhone = "01000000004",
                    CustomerAddress = "123 Street",
                    TotalPrice = 2018,
                    AffiliateCommissionPct = 5,
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTime.Now
                }
            );
            modelBuilder.Entity<MerchantOrder>().HasData(
                new MerchantOrder
                {
                    MerchantId = 1,
                    OrderId = 1
                });

            // 1️⃣3️⃣ OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, Price = 999, CreatedAt = DateTime.Now },
                new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, Price = 20, CreatedAt = DateTime.Now }
            );


            // ---------------- COMMISSIONS ----------------
            modelBuilder.Entity<Commission>().HasData(
                new Commission
                {
                    Id = 1,
                    OrderId = 1,
                    AffiliateAmount = 578.99m,
                    PlatformAmount = 964.99m,
                    MerchantAmount = 17756.00m,
                    Status = CommissionStatus.Paid,
                    CreatedAt = DateTime.UtcNow.AddDays(-29)
                }
            );
            modelBuilder.Entity<MerchantCommissions>().HasData(
    new MerchantCommissions
    {
        Id = 1,
        MerchantId = 1,
        CommissionId = 1,
        value = 20
    });
        }
}

}