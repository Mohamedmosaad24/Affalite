using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AffaliteDAL.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin", null, "Admin", "ADMIN" },
                    { "role-affiliate", null, "Affiliate", "AFFILIATE" },
                    { "role-customer", null, "Customer", "CUSTOMER" },
                    { "role-merchant", null, "Merchant", "MERCHANT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user1", 0, "e5225354-41b5-42d5-86ca-0bc8a3e7e7d3", "merchant1@affalite.com", true, "Ahmed Hassan", false, null, "MERCHANT1@AFFALITE.COM", "MERCHANT1@AFFALITE.COM", "AQAAAAIAAYagAAAAEBbBSfBGA5jM89nEYiCvg8+9/V+RIYQ8y3NYog6AOnIsHPmhqlYwF0W+uH7uLfmYew==", "01001234567", false, "b18f7a67-6283-4248-a53d-3ad677d6d9b0", false, "merchant1" },
                    { "user2", 0, "67215f38-a34e-451c-8789-5b8be665061c", "affiliate1@affalite.com", true, "Youssef Ali", false, null, "AFFILIATE1@AFFALITE.COM", "AFFILIATE1@AFFALITE.COM", "AQAAAAIAAYagAAAAEA9/QAFMX0LECw2HNq5bjEelW+HW9BW3htFPCc/5Gp3pjQIqnAYAYlykq/c1VkndIw==", "01001112233", false, "99125844-003e-454e-94ea-cdf0a15f4774", false, "affiliate1" },
                    { "user3", 0, "a49762b8-b848-4667-b397-7461b61585d2", "customer1@affalite.com", true, "Hana Adel", false, null, "CUSTOMER1@AFFALITE.COM", "CUSTOMER1@AFFALITE.COM", "AQAAAAIAAYagAAAAEFqDCtUpGBVLS6ArB879mFRA8qj2zqn55BGgEyIn32HIjiChJc/Mg3tRT7vKcZg6AQ==", "01002223344", false, "40478c9c-ca84-4ff9-966b-2e1a735202c5", false, "customer1" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 15, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(2668), "Electronics", "electronics" },
                    { 2, new DateTime(2026, 4, 15, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(2673), "Fashion", "fashion" }
                });

            migrationBuilder.InsertData(
                table: "Affiliates",
                columns: new[] { "Id", "AppUserId", "Balance", "CreatedAt" },
                values: new object[] { 1, "user2", 1500m, new DateTime(2026, 4, 15, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(3104) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "role-merchant", "user1" },
                    { "role-affiliate", "user2" },
                    { "role-customer", "user3" }
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "AppUserId", "Balance", "CreatedAt" },
                values: new object[] { 1, "user1", 5000m, new DateTime(2026, 4, 15, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(2893) });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "AffilaiteCommission", "AffiliateId", "CreatedAt", "Shiping", "SubTotal", "Total" },
                values: new object[] { 1, 0m, 1, new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(4132), 10m, 0m, 0m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AffiliateCommissionPct", "AffiliateId", "CreatedAt", "CustomerAddress", "CustomerName", "CustomerPhone", "Status", "TotalPrice" },
                values: new object[] { 1, 5m, 1, new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(4760), "123 Street", "David", "01000000004", 1, 2018m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Details", "MerchantId", "Name", "PlatformCommissionPct", "Price", "SaleCount", "Status", "Stock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(3296), "Latest Apple iPhone", "Details here", 1, "iPhone 14", 5m, 999m, 10, 2, 50 },
                    { 2, 2, new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(3453), "Fantasy novel", "Details here", 1, "Harry Potter Book", 2m, 20m, 50, 2, 100 }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "CreatedAt", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(4341), 1, 2 },
                    { 2, 1, new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(4381), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "AffiliateAmount", "CreatedAt", "MerchantAmount", "OrderId", "PlatformAmount", "Status" },
                values: new object[] { 1, 578.99m, new DateTime(2026, 3, 17, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(5421), 17756.00m, 1, 964.99m, 1 });

            migrationBuilder.InsertData(
                table: "MerchantOrder",
                columns: new[] { "MerchantId", "OrderId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(5205), 1, 999m, 1, 2 },
                    { 2, new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(5226), 1, 20m, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "FileName", "ImageUrl", "ProductId" },
                values: new object[,]
                {
                    { 1, "iphone14.jpg", "p1.jpg", 1 },
                    { 2, "iphone14.jpg", "p4.jpg", 1 },
                    { 3, "harrypotter.jpg", "p2.png", 2 },
                    { 4, "harrypotter.jpg", "p3.jpg", 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductReviews",
                columns: new[] { "Id", "AffiliateId", "Comment", "CreatedAt", "ProductId", "Rating" },
                values: new object[,]
                {
                    { 1, 1, "Great phone!", new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(3853), 1, 5 },
                    { 2, 1, "Loved the book", new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(3882), 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "MerchantCommissions",
                columns: new[] { "Id", "CommissionId", "MerchantId", "value" },
                values: new object[] { 1, 1, 1, 20m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-merchant", "user1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-affiliate", "user2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-customer", "user3" });

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MerchantCommissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MerchantOrder",
                keyColumns: new[] { "MerchantId", "OrderId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-affiliate");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-customer");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-merchant");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user3");

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Affiliates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2");
        }
    }
}
